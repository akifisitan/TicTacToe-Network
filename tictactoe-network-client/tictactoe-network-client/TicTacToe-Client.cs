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
        private bool connected = false; 
        private bool inGame = false;

        //  --- UI Logic ---

        public MainWindow() {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
            InitializeComponent();
        }
        
        private void MainWindow_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            connected = false;
            Environment.Exit(0);
        }

        // Connect & Disconnect button logic
        private void button_connect_Click(object sender, EventArgs e) {
            // Check if the username is suitable
            username = txtBoxUsername.Text.Trim();
            if (username.Length < 4 || username.Length > 12) {
                logs.AppendText("Please make sure your username length is between 4 and 12 characters.\n");
                return;
            }
            if (!CheckUsername()) {
                logs.AppendText("Please make sure your username only consists of:\n" +
                                "- Uppercase (A-Z)\n" +
                                "- Lowercase (a-z)\n" +
                                "- Digits (0-9)\n");
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
                logs.AppendText("Please enter a valid port number.\n");
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
                int bytesRead = clientSocket.Receive(buffer);
                string serverResponse = Encoding.Default.GetString(buffer, 0, bytesRead).Trim('\0');
                // Close the socket if the username is not available or the server is full
                if (serverResponse.Equals("SERVER_IS_FULL")) {
                    logs.AppendText("Server: The server is at maximum capacity.\n");
                    clientSocket.Close();
                }
                else if (serverResponse.Equals("USERNAME_NOT_AVAILABLE")) {
                    logs.AppendText("Server: This username is not available.\n");
                    clientSocket.Close();
                }
                // Positive response, update the UI
                else {
                    connected = true;
                    logs.AppendText($"Connected to the server as {username}.\n");
                    logs.AppendText($"Server: Welcome to the game, {username}!\n");
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
                logs.AppendText("Could not connect to the server.\n");
            }
            btnConnect.Enabled = true;
        }
        
        // Send game choices to the server 
        private void btnPlay_Click(object sender, EventArgs e) {
            string message = txtBoxChoice.Text;
            txtBoxChoice.Clear();
            int playerChoice;
            if (Int32.TryParse(message, out playerChoice) && 1 <= playerChoice && playerChoice <= 9) {
                // DEBUG logs.AppendText($"{username}: {message}\n");
                clientSocket.Send(Encoding.Default.GetBytes(message));
                btnPlay.Enabled = false;
                txtBoxChoice.Enabled = false;
            }
            else {
                logs.AppendText("Please make a valid move. (1-9)\n");
            }
        }
        
        //  --- UI Logic End ---
        
        //  --- Main Functions ---
        
        // Receive messages from the server
        private void ReceiveMessages() {
            while (connected) {
                try {
                    // Receive messages from the server
                    Byte[] buffer = new Byte[256];
                    int bytesRead = clientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer, 0, bytesRead).Trim('\0');
                    // logs.AppendText($"---------\nIncoming message: {incomingMessage}\n---------\n");
                    string[] messages = incomingMessage.Split('\n');
                    for (int i = 0; i < messages.Length - 1; i++) {
                        string message = messages[i];
                        // DEBUG logs.AppendText($"Incoming message: {message}\n");
                        // Start receiving game related messages
                        if (!inGame && (message.StartsWith("GAME_START") || message.StartsWith("GAME_RESUME"))) {
                            string[] splitMessage = message.Split('_');
                            string role = splitMessage[2];
                            labelRole.Text = role;
                            ClearBoard();
                            inGame = true;
                            continue;
                        }
                        // Get scoreboard information
                        if (message.StartsWith("SB_ENTRY")) {
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
                                logs.AppendText("Server: The game has been paused.\n");
                                continue;
                            }
                            if (message.Equals("GAME_RESET")) {
                                ClearBoard();
                                txtBoxChoice.Enabled = false;
                                btnPlay.Enabled = false;
                                inGame = false;
                                labelRole.Text = "In Lobby";
                                logs.AppendText("Server: The game has been reset.\n");
                                continue;
                            }
                            if (message.StartsWith("BOARD_B")) {
                                // "BOARD_BX2OX5XO8X"
                                string[] splitMessage = message.Split('_');
                                SetBoardState(splitMessage[1]);
                                continue;
                            }
                            if (message.Contains("GAME_END")) {
                                // GAME_END_DRAW
                                string[] splitMessage = message.Split(new char[] { '_' }, 3);
                                string result = "DRAW" == splitMessage[2]
                                    ? "Server: Game ended in a draw!"
                                    : $"Server: {splitMessage[2]} wins!";
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
                    // logs.AppendText($"Exception occurred: {e}\n");
                    logs.AppendText("Disconnected from the server.\n");
                    ClearBoard();
                    inGame = false;
                    boxUsername.Visible = false;
                    gameBoard.Visible = false;
                    boxScores.Visible = false;
                    txtBoxScores.Clear();
                    btnConnect.Text = "Connect";
                    btnConnect.Enabled = true;
                    txtBoxUsername.Enabled = true;
                    txtBoxIp.Enabled = true;
                    txtBoxPort.Enabled = true;
                    txtBoxChoice.Enabled = false;
                    btnPlay.Enabled = false;
                    inGame = false;
                    clientSocket.Close();
                    connected = false;
                }
            }
        }
        
        //  --- Main Functions End ---
        
        //  --- Helper Functions ---
        
        private void SetBoardState(string receivedBoard) {
            for (int i = 1; i <= 9; i++) {
                board[i].Text = receivedBoard[i].ToString();
            }
        }

        private void ClearBoard() {
            for (int i = 1; i <= 9; i++) {
                board[i].Text = i.ToString();
            }
        }

        private bool CheckUsername() {
            foreach (char c in username) {
                if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9')) {
                    return false;
                }
            }
            return true;
        }
        
        //  --- Helper Functions End ---
    }
}
