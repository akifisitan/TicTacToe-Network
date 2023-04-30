using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe_network_client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        private string username = "";

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            if (btnConnect.Text == "Disconnect")
            {
                clientSocket.Close();
                connected = false;
                btnConnect.Text = "Connect";
                btnConnect.Enabled = true;
                return;
            }
            
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = txtBoxIp.Text;
            int portNum;
            
            if (Int32.TryParse(txtBoxPort.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(ip, portNum); 
                    username = txtBoxUsername.Text;
                    clientSocket.Send(Encoding.Default.GetBytes(username));
                    
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string serverResponse = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (serverResponse.Equals("There is already a player with this username!"))
                    {
                        logs.AppendText("There is already a player with this username!");
                        btnConnect.Enabled = true;
                    }
                    else
                    {
                        txtBoxMessage.Enabled = true;
                        btnConnect.Text = "Disconnect";
                        btnConnect.Enabled = true;
                        btnSend.Enabled = true;
                        connected = true;
                        logs.AppendText($"Connected to the server as {username}\n");

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }
            btnConnect.Enabled = true;
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    logs.AppendText($"Server: {incomingMessage}\n");
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("Disconnected from the server\n");
                        btnConnect.Enabled = true;
                        txtBoxMessage.Enabled = false;
                        btnSend.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Disconnect()
        {
            clientSocket.Close();
            connected = false;
            btnConnect.Text = "Connect";
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = txtBoxMessage.Text;
            logs.AppendText($"{username}: {message}\n");

            if (message != "" && message.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
            }

        }
        
    }
}
