using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe_network_server
{
    public partial class Form1 : Form
    {
        
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Dictionary which maps username to user for ease of access
        Dictionary<string, User> connectedClients = new Dictionary<string, User>();

        bool terminating = false;
        bool listening = false;
        
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if (Int32.TryParse(txtBoxPort.Text, out var serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                btnListen.Enabled = false;
                txtBoxPort.Enabled = false;
                btnSendMessage.Enabled = true;
                txtBoxMessage.Enabled = true;
                
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText($"Started listening on port: {serverPort}\n");

            }
            else
            {
                logs.AppendText("Please check the port number \n");
            }
        }
        
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    logs.AppendText("A user is attempting to join the room...\n");
                    // Receive the username as a message from the user
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    // Check if the username is already connected 
                    if (connectedClients.ContainsKey(username))
                    {
                        newClient.Send(Encoding.Default.GetBytes(
                            "There is already a player with this username!")
                        );
                        newClient.Close();
                        logs.AppendText("There is already a player with this username!");
                    }
                    else
                    {
                        // Send a welcome message to the user
                        newClient.Send(Encoding.Default.GetBytes("Welcome!"));
                        // Create a user and add it to the users dictionary
                        User user = new User(username, newClient, 0);
                        connectedClients.Add(user.Username, user);
                        logs.AppendText($"{username} joined the room.\n");
                        // Start a separate thread to receive the messages to be sent from the new client  
                        Thread receiveThread = new Thread(() => Receive(user)); 
                        receiveThread.Start();
                    }
                }
                catch (Exception e)
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText($"The socket stopped working: {e.Message}\n");
                    }

                }
            }
        }
        
        // Receive messages from users
        private void Receive(User user) 
        {
            bool connected = true;

            while(connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    user.Socket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    logs.AppendText($"{user.Username}: {incomingMessage}\n");
                }
                catch (Exception e)
                {
                    if (!terminating)
                    {
                        logs.AppendText($"{user.Username} has disconnected.\n");
                    }
                    user.Socket.Close();
                    connectedClients.Remove(user.Username);
                    connected = false;
                }
            }
        }
        
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string message = txtBoxMessage.Text;
            if (message != "" && message.Length <= 64)
            {
                logs.AppendText($"Server: {message}\n");
                Byte[] buffer = Encoding.Default.GetBytes(message);
                foreach (User user in connectedClients.Values)
                {
                    try
                    {
                        user.Socket.Send(buffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        txtBoxMessage.Enabled = false;
                        btnSendMessage.Enabled = false;
                        txtBoxPort.Enabled = true;
                        btnListen.Enabled = true;
                        serverSocket.Close();
                    }
                }
            }
        }
    }
}