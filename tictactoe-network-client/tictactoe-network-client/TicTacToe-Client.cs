using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace tictactoe_network_client {
    public partial class MainWindow : Form {

        // Client socket 
        private Socket clientSocket;
        
        // List to store the board UI elements / Example: board[1] = gameBoard1
        private List<Label> board = new List<Label> { null };

        // Client's username
        private string username = "";

        // Control flow variables
        private bool terminating = false;
        private bool connected = false; 
        private bool inGame = false;
        private bool isActivePlayer = false;

        //  --- UI Logic ---

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
            // Check if the username is suitable
            username = txtBoxUsername.Text.Trim();
            if (username.Length < 4 || username.Length > 64) { 
                logs.AppendText("Please make sure your username is between 4 and 64 characters.\n");
                return;
            }
            // Disable the button while attempting to connect
            btnConnect.Enabled = false;
            // Disconnect logic
            if (btnConnect.Text == "Disconnect") {
                clientSocket.Close();
                boxUsername.Visible = false;
                gameBoard.Visible = false;
                boxScores.Visible = false;
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
                clientSocket.Send(Encoding.Default.GetBytes(username));
                // Receive response from the server
                Byte[] buffer = new Byte[64];
                clientSocket.Receive(buffer);
                string incomingMessage = Encoding.Default.GetString(buffer);
                string serverResponse = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                // Close the socket if the username is not available or the server is full
                if (serverResponse.Equals("USERNAME_NOT_AVAILABLE")) {
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
                    labelUsername.Text = username;
                    labelRole.Text = "Waiting for game";
                    boxUsername.Visible = true;
                    gameBoard.Visible = true;
                    boxScores.Visible = true;
                    // Change the connect button to the disconnect button
                    btnConnect.Text = "Disconnect";
                    // Fill up the game board (Should be only done once optimally)
                    if (board.Count < 9) {
                        for (int i = 1; i <= 9; i++) {
                            Label boardX = Controls.Find($"board{i}", true).FirstOrDefault() as Label;
                            if (boardX != null) {
                                board.Add(boardX);
                            }
                        }
                    }
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
        
        // Send game choices to the server 
        private void btnPlay_Click(object sender, EventArgs e) {
            string message = txtBoxChoice.Text;
            int playerChoice;
            if (Int32.TryParse(message, out playerChoice) && 1 <= playerChoice && playerChoice <= 9) {
                // logs.AppendText($"Your play: {message}\n");
                txtBoxChoice.Text = "";
                clientSocket.Send(Encoding.Default.GetBytes(message));
                btnPlay.Enabled = false;
                txtBoxChoice.Enabled = false;
            }
            else {
                logs.AppendText("Please enter a number. (1-9)\n");
            }
        }
        
        //  --- UI Logic End ---
        
        //  --- Main Functions ---
        
        // Receive messages from the server
        private void ReceiveMessages() {
            while (connected) {
                try {
                    // Receive messages from the server
                    Byte[] buffer = new Byte[128];
                    clientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string serverMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    string[] messages = serverMessage.Split('\n');
                    for (int i = 0; i < messages.Length - 1; i++) {
                        string message = messages[i];
                        // logs.AppendText($"Incoming message: {message}\n");
                        // Start receiving game related messages
                        if (!inGame && (message.StartsWith("GAME_START") || message.StartsWith("GAME_RESUME"))) {
                            string[] splitMessage = message.Split('_');
                            string role = splitMessage[2];
                            labelRole.Text = role;
                            ClearBoard();
                            if (role != "SPECTATOR")
                            {
                                if (role == "PLAYER1") {
                                    btnPlay.Enabled = true;
                                    txtBoxChoice.Enabled = true;
                                }
                                isActivePlayer = true;
                            }
                            inGame = true;
                            // logs.AppendText(message.StartsWith("GAME_START") ? "The game has started!\n" : "Continuing the game...\n");
                            continue;
                        }
                        // Get scoreboard information
                        if (message.StartsWith("SCOREBOARD")) {
                            string[] splitMessage = message.Split('/');
                            txtBoxScores.AppendText(
                                $"-> {splitMessage[1]} ({splitMessage[2]}/{splitMessage[3]}/{splitMessage[4]})\n");
                            continue;
                        }
                        // Clear the scoreboard
                        if (message.Equals("CLEAR_SCOREBOARD")) {
                            txtBoxScores.Clear();
                            continue;
                        }
                        if (inGame) {
                            if (message.Equals("YOUR_TURN")) {
                                txtBoxChoice.Enabled = true;
                                btnPlay.Enabled = true;
                                continue;
                            }

                            if (message.Equals("GAME_PAUSE")) {
                                txtBoxChoice.Enabled = false;
                                btnPlay.Enabled = false;
                                inGame = false;
                                labelRole.Text = "Waiting for game";
                                logs.AppendText("The game has been paused.\n");
                                continue;
                            }

                            if (message.Equals("GAME_RESET")) {
                                ClearBoard();
                                txtBoxChoice.Enabled = false;
                                btnPlay.Enabled = false;
                                inGame = false;
                                labelRole.Text = "In Lobby";
                                logs.AppendText("The game has been reset.\n");
                                continue;
                            }
                            if (message.StartsWith("BOARD_B")) {
                                // "BOARD_BX2OX5XO8X"
                                string[] splitMessage = message.Split('_');
                                ReceiveBoardState(splitMessage[1]);
                                continue;
                            }
                            if (message.Contains("GAME_END")) {
                                // GAME_END_DRAW
                                string[] splitMessage = message.Split(new char[] { '_' }, 3);
                                string result = "DRAW" == splitMessage[2]
                                    ? "Game ended in a draw!"
                                    : $"{splitMessage[2]} wins!";
                                logs.AppendText($"{result}\n");
                                labelRole.Text = "In Lobby";
                                inGame = false;
                                txtBoxChoice.Enabled = false;
                                btnPlay.Enabled = false;
                                continue;
                            }
                        }
                        // Generic messages from the server
                        logs.AppendText($"Server: {message}\n");
                    }
                }
                // Handle exceptions
                catch {
                    if (!terminating) {
                        logs.AppendText("Disconnected from the server.\n");
                        // gameBoard.Invoke(new Action(() => gameBoard.Visible = false));
                        ClearBoard();
                        inGame = false;
                        boxUsername.Visible = false;
                        gameBoard.Visible = false;
                        boxScores.Visible = false;
                        btnConnect.Text = "Connect";
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
        
        //  --- Main Functions End ---
        
        //  --- Helper Functions ---
        
        private void ReceiveBoardState(string receivedBoard) {
            for (int i = 1; i <= 9; i++) {
                board[i].Text = receivedBoard[i].ToString();
            }
        }

        private void ClearBoard() {
            for (int i = 1; i <= 9; i++) {
                board[i].Text = i.ToString();
            }
        }
        
        //  --- Helper Functions End ---
    }
}
