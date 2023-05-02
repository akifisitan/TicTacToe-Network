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
    public partial class MainWindow : Form
    {

        bool terminating = false;
        bool connected = false; 
        bool inGame = false;
        Socket clientSocket;
        private string username = "";

        public MainWindow()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
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
                        txtBoxChoice.Enabled = true;
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

                    if (inGame)
                    {
                        if (message.StartsWith("BOARD_ADD")) // BOARD_ADD_X_1
                        {
                            string[] splitMessage = message.Split('_');
                            SetBoardText(int.Parse(splitMessage[2]), splitMessage[3]);
                            continue;
                        }

                        if (message.StartsWith("GAME_END")) // GAME_END_DRAW
                        {
                            string[] splitMessage = message.Split(new char[] { '_' }, 3);
                            string result = "DRAW" == splitMessage[2]
                                ? "Game ended in a draw"
                                : $"{splitMessage[2]} won!";
                            logs.AppendText($"{result}\n");
                            inGame = false;
                            continue;
                        }
                    }

                    logs.AppendText($"Server: {message}\n");
                }
                catch {
                    if (!terminating)
                    {
                        logs.AppendText("Disconnected from the server\n");
                        gameBoard.Invoke(new Action(() => gameBoard.Visible = false));
                        btnConnect.Enabled = true;
                        txtBoxChoice.Enabled = false;
                        btnSend.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }
        private void SetBoardText(int boardNumber, string shape) {
            Label board = Controls.Find($"board{boardNumber}", true).FirstOrDefault() as Label;
            if (board != null) {
                board.Text = shape;
            }
        }

        private void ClearBoard() {
            for (int i = 1; i <= 9; i++) {
                Label board = Controls.Find($"board{i}", true).FirstOrDefault() as Label;
                board.Text = "-";
            }
        }

        private void Disconnect()
        {
            clientSocket.Close();
            connected = false;
            btnConnect.Text = "Connect";
        }

        private void MainWindow_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = txtBoxChoice.Text;
            if (message != "" && message.Length <= 64)
            {
                logs.AppendText($"{username}: {message}\n");
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
            }

        }
        
    }
}
