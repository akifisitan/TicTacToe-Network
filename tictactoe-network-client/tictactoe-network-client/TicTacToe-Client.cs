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

namespace tictactoe_network_client {
    public partial class MainWindow : Form {

        // Client socket 
        Socket clientSocket;

        // Client's username
        string username = "";

        // Control flow variables
        bool terminating = false;
        bool connected = false; 
        bool inGame = false; 

        public MainWindow() {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
            InitializeComponent();
        }
        
        private void MainWindow_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        // Connect & Disconnect button logic
        private void button_connect_Click(object sender, EventArgs e) {
            if (txtBoxUsername.Text == "") return;
            // Disable the button while attempting to connect
            btnConnect.Enabled = false;
            // Disconnect logic
            if (btnConnect.Text == "Disconnect") {
                clientSocket.Close();
                connected = false;
                btnConnect.Text = "Connect";
                btnConnect.Enabled = true;
                return;
            }
            // Check if the port number is an integer
            string ip = txtBoxIp.Text;
            int portNum;
            if (!Int32.TryParse(txtBoxPort.Text, out portNum)) {
                logs.AppendText("Check the port.\n");
                btnConnect.Enabled = true;
                return;
            }
            // Create a socket for the new user
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try {
                // Attempt to connect to the server
                clientSocket.Connect(ip, portNum);
                // Connected to the server, send username to the server to check if it is available
                username = txtBoxUsername.Text;
                clientSocket.Send(Encoding.Default.GetBytes(username));
                // Receive response from the server
                Byte[] buffer = new Byte[64];
                clientSocket.Receive(buffer);
                string incomingMessage = Encoding.Default.GetString(buffer);
                string serverResponse = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                // Close the socket if the username is not available or the server is full
                if (serverResponse.Equals("USERNAME_IS_USED")) {
                    logs.AppendText("There is already a player with this username!\n");
                    clientSocket.Close();
                }
                else if (serverResponse.Equals("SERVER_IS_FULL")) {
                    logs.AppendText("The server is at maximum capacity!\n");
                    clientSocket.Close();
                }
                // Positive response, update the UI
                else {
                    connected = true;
                    logs.AppendText($"Connected to the server as {username}.\n");
                    txtBoxUsername.Enabled = false;
                    txtBoxIp.Enabled = false;
                    txtBoxPort.Enabled = false;
                    // Change the connect button to the disconnect button
                    btnConnect.Text = "Disconnect";
                    // Start a thread to receive messages from the server
                    Thread receiveThread = new Thread(ReceiveMessages);
                    receiveThread.Start();
                }
            }
            // Handle exceptions
            catch {
                logs.AppendText("Could not connect to the server!\n");
            }
            btnConnect.Enabled = true;
        }
        
        // Receive messages from the server
        private void ReceiveMessages() {
            while (connected) {
                try {
                    // Receive messages from the server
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string message = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    // Start receiving game related messages
                    if (!inGame && message.Equals("GAME_START")) {
                        inGame = true;
                        gameBoard.Invoke(new Action(() => gameBoard.Visible = true));
                        txtBoxChoice.Enabled = true;
                        btnPlay.Enabled = true;
                        ClearBoard();
                        logs.AppendText("The game has started!\n");
                        continue;
                    }
                    if (inGame) {
                        if (message.Equals("GAME_RESET")) {
                            ClearBoard();
                            txtBoxChoice.Enabled = false;
                            btnPlay.Enabled = false;
                            inGame = false;
                            logs.AppendText("The game has been reset.\n");
                            continue;
                        }
                        if (message.StartsWith("BOARD_ADD")) { // BOARD_ADD_X_1
                            string[] splitMessage = message.Split('_');
                            SetBoardText(int.Parse(splitMessage[2]), splitMessage[3]);
                            continue;
                        }
                        if (message.StartsWith("GAME_END")) { // GAME_END_DRAW
                            string[] splitMessage = message.Split(new char[] { '_' }, 3);
                            string result = "DRAW" == splitMessage[2]
                                ? "Game ended in a draw!"
                                : $"{splitMessage[2]} wins!";
                            logs.AppendText($"{result}\n");
                            inGame = false;
                            txtBoxChoice.Enabled = false;
                            btnPlay.Enabled = false;
                            continue;
                        }
                    }
                    // Generic messages from the server
                    logs.AppendText($"Server: {message}\n");
                }
                // Handle exceptions
                catch {
                    if (!terminating) {
                        logs.AppendText("Disconnected from the server.\n");
                        gameBoard.Invoke(new Action(() => gameBoard.Visible = false));
                        btnConnect.Enabled = true;
                        txtBoxUsername.Enabled = true;
                        txtBoxIp.Enabled = true;
                        txtBoxPort.Enabled = true;
                        txtBoxChoice.Enabled = false;
                        btnPlay.Enabled = false;
                    }
                    clientSocket.Close();
                    connected = false;
                }
            }
        }
        
        // Functions for manipulating the game board
        private void SetBoardText(int boardNumber, string shape) {
            Label board = Controls.Find($"board{boardNumber}", true).FirstOrDefault() as Label;
            if (board != null) {
                board.Text = shape;
            }
        }

        private void ClearBoard() {
            for (int i = 1; i <= 9; i++) {
                Label board = Controls.Find($"board{i}", true).FirstOrDefault() as Label;
                if (board != null) {
                    board.Text = i.ToString();
                }
            }
        }
        
        // Send game choices to the server 
        private void btnPlay_Click(object sender, EventArgs e) {
            string message = txtBoxChoice.Text;
            if (message != "" && message.Length <= 64) {
                // logs.AppendText($"Your play: {message}\n");
                txtBoxChoice.Text = "";
                clientSocket.Send(Encoding.Default.GetBytes(message));
            }
        }
        
    }
}
