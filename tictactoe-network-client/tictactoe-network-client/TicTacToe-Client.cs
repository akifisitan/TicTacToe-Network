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
        bool inGame = false;
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
                    if (serverResponse.Equals("USERNAME_IS_USED")) {
                        logs.AppendText("There is already a player with this username!\n");
                        btnConnect.Enabled = true;
                    } else {
                        txtBoxMessage.Enabled = true;
                        btnConnect.Text = "Disconnect";
                        btnConnect.Enabled = true;
                        btnSend.Enabled = true;
                        connected = true;
                        logs.AppendText($"Connected to the server as {username}.\n");

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                }
                catch {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else {
                logs.AppendText("Check the port.\n");
            }
            btnConnect.Enabled = true;
        }
        
        // Maybe add a separate listener for the game? For later

        private void Receive()
        {
            while (connected) {
                try {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string message = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (!inGame && message.Equals("GAME_START")) {
                        inGame = true;
                        gameBoard.Invoke(new Action(() => gameBoard.Visible = true));
                        logs.AppendText("The game has started!\n");
                        continue;
                    }

                    if (message.StartsWith("BOARD_ADD")) // BOARD_ADD_X_1
                    {
                        string[] splitMessage = message.Split('_');
                        SetBoardText(int.Parse(splitMessage[2]), splitMessage[3]);
                    }
                    if (message.StartsWith("GAME_END")) // GAME_END_DRAW
                    {
                        string[] splitMessage = message.Split(new char[] { '_' }, 3);
                        string result = "DRAW" == splitMessage[2] ? "Game ended in a draw" 
                            : $"{splitMessage[2]} won!";
                        logs.AppendText($"{result}\n");
                        inGame = false;
                        continue;
                    }
                    // logs.AppendText($"Server: {message}\n");
                }
                catch {
                    if (!terminating)
                    {
                        logs.AppendText("Disconnected from the server\n");
                        gameBoard.Invoke(new Action(() => gameBoard.Visible = false));
                        btnConnect.Enabled = true;
                        txtBoxMessage.Enabled = false;
                        btnSend.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }
        private bool SetBoardText(int boardNumber, string shape)
        {
            Label board = Controls.Find($"board{boardNumber}", true).FirstOrDefault() as Label;
            if (board == null) {
                // Board with specified number not found
                return false;
            }
            // Board is already occupied
            if (board.Text == "O" || board.Text == "X") {
                return false;
            }                
            // Board is empty
            board.Text = shape;
            return true;
            
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
            if (message != "" && message.Length <= 64)
            {
                logs.AppendText($"{username}: {message}\n");
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
            }

        }
        
    }
}
