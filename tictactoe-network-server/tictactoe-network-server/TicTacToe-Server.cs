using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class MainWindow : Form {
        // Server socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Dictionary which maps username to user for ease of access
        Dictionary<string, User> connectedClients = new Dictionary<string, User>(); 
        List<User> gamePlayers = new List<User>();

        bool terminating = false;
        bool listening = false;
        
        public MainWindow() {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            int serverPort;
            if (Int32.TryParse(txtBoxPort.Text, out serverPort)) {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);
                listening = true; btnListen.Enabled = false;
                txtBoxPort.Enabled = false; 
                btnStartGame.Enabled = true; 
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();
                logs.AppendText($"Started listening on port: {serverPort}\n");
            } else {
                logs.AppendText("Please check the port number \n");
            }
        }
        // Accept incoming connections
        private void Accept() {
            while (listening) {
                try {
                    Socket newClient = serverSocket.Accept();
                    // Receive the username as a message from the user
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    // Check if the provided username is already connected   
                    if (connectedClients.ContainsKey(username)) {
                        newClient.Send(Encoding.Default.GetBytes("USERNAME_IS_USED"));
                        newClient.Close();
                        logs.AppendText("Rejected connection as there is already a player with this username.\n");
                    } else {
                        // Create a user and add it to the users dictionary
                        User user = new User(username, newClient);
                        connectedClients.Add(user.Username, user);
                        sendMessage(user, "Welcome!");
                        logs.AppendText($"{username} joined the room.\n");
                        RefreshPlayerList();
                        // Start a separate thread to receive messages from the connected client  
                        Thread receiveThread = new Thread(() => Receive(user)); 
                        receiveThread.Start();
                    }
                }
                catch (Exception e) {
                    if (terminating) {
                        listening = false;
                    } else {
                        logs.AppendText($"The socket stopped working: {e.Message}\n");
                    }
                }
            }
        }

        // Method for refreshing the player list ui
        private void RefreshPlayerList() {
            txtBoxPlayers.Clear();
            StringBuilder sb = new StringBuilder();
            foreach (string username in connectedClients.Keys) {
                sb.Append($"-> {username}\n");
            }
            txtBoxPlayers.AppendText(sb.ToString());
        }
        
        // Receive messages from users
        private void Receive(User user) {
            bool connected = true;
            while (connected && !terminating) {
                try
                {
                    Byte[] buffer = new Byte[64];
                    user.Socket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (!user.InGame) continue;
                    if (!user.HasTurn) {
                        sendMessage(user, "Please wait for your turn!");
                        continue;
                    }
                    string playerChoice2 = $"{incomingMessage[0]}";
                    int playerChoice;
                    if (!(Int32.TryParse(playerChoice2, out playerChoice) && 0 <= playerChoice &&
                          playerChoice <= 9)) {
                        sendMessage(user, "Please enter a number between 1 and 9.");
                        continue;
                    }
                    if (!SetBoardText(playerChoice, user.Shape)) {
                        sendMessage(user, "That place is full!");
                        continue;
                    }
                    notifyGamePlayers($"BOARD_ADD_{playerChoice}_{user.Shape}");
                    if (checkWinner()) {
                        logs.AppendText($"{user.Username} won!\n");
                        notifyGamePlayers($"GAME_END_{user.Username}");
                        user.InGame = false;
                        continue;
                    }
                    if (!checkBoardRemaining())
                    {
                        logs.AppendText("It is a draw!\n");
                        notifyGamePlayers("GAME_END_DRAW");
                        user.InGame = false;
                        continue;
                    }
                    // Set the other player's turn
                    user.HasTurn = false;
                    User nextPlayer = user.Username != gamePlayers[0].Username ? gamePlayers[0] : gamePlayers[1];
                    nextPlayer.HasTurn = true;
                    notifyGamePlayers($"{nextPlayer.Username}'s turn.");
                    logs.AppendText($"{nextPlayer.Username}'s turn.\n");
                    logs.AppendText($"{user.Username}: {incomingMessage}\n");
                }
                catch (Exception e)
                {
                    if (!terminating)
                    {
                        logs.AppendText($"{user.Username} left the room.\n");
                    }
                    user.Socket.Close();
                    connectedClients.Remove(user.Username);
                    RefreshPlayerList();
                    connected = false;
                }
            }
        }
        
        private void MainWindow_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            listening = false;
            terminating = true;
            Environment.Exit(0);
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

        private bool checkWinner() {
            // Rows
            if (board1.Text == board2.Text && board2.Text == board3.Text && board1.Text != "-")
            {
                return true;
            }
            if (board4.Text == board5.Text && board5.Text == board6.Text && board4.Text != "-")
            {
                return true;
            }
            if (board7.Text == board8.Text && board8.Text == board9.Text && board7.Text != "-")
            {
                return true;
            }
            // Columns
            if (board1.Text == board4.Text && board4.Text == board7.Text && board1.Text != "-")
            {
                return true;
            }
            if (board2.Text == board5.Text && board5.Text == board8.Text && board2.Text != "-")
            {
                return true;
            }
            if (board3.Text == board6.Text && board6.Text == board9.Text && board3.Text != "-")
            {
                return true;
            }
            // Diagonals
            if (board1.Text == board5.Text && board5.Text == board9.Text && board1.Text != "-")
            {
                return true;
            }
            if (board3.Text == board5.Text && board5.Text == board7.Text && board3.Text != "-")
            {
                return true;
            }
            // No winner
            return false;
        }

        private bool checkBoardRemaining() {
            return board1.Text == "-" || board2.Text == "-" || board3.Text == "-" ||
                   board4.Text == "-" || board5.Text == "-" || board6.Text == "-" ||
                   board7.Text == "-" || board8.Text == "-" || board9.Text == "-";
        }

        private void resetGameBoard()
        {
            board1.Text = "-"; board2.Text = "-"; board3.Text = "-";
            board4.Text = "-"; board5.Text = "-"; board6.Text = "-";
            board7.Text = "-"; board8.Text = "-"; board9.Text = "-";
        }

        
        // Receive a single message from a connected user
        private string receiveMessage(User user) {
            if (!connectedClients.ContainsKey(user.Username)) {
                logs.AppendText("This user is not connected to the server!\n");
                return "";
            }
            Byte[] buffer = new Byte[64];
            user.Socket.Receive(buffer);
            string incomingMessage = Encoding.Default.GetString(buffer);
            string newMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
            return newMessage;
        }

        // Send a message to a connected user
        private void sendMessage(User user, string message) {
            try
            {
                if (!connectedClients.ContainsKey(user.Username)) {
                    logs.AppendText("This user is not connected to the server!\n");
                    return;
                }
                Byte[] buffer = Encoding.Default.GetBytes(message);
                user.Socket.Send(buffer);
            }
            catch {
                logs.AppendText("There is a problem! Check the connection...\n");
                terminating = true;
                txtBoxPort.Enabled = true;
                btnListen.Enabled = true;
                serverSocket.Close();
            }
        }
        
        private void btnStartGame_Click(object sender, EventArgs e) {
            if (btnStartGame.Text == "Reset")
            {
                gamePlayers.Clear();
                gameBoard.Visible = false;
                resetGameBoard();
                btnStartGame.Text = "Start Game";
                return;
            }
            if (connectedClients.Count != 2) {
                logs.AppendText("There are not enough players to start the game!\n");
                return;
            }
            // Remember to only add 2 people
            foreach (User user in connectedClients.Values) {
                gamePlayers.Add(user);
            }
            User player1 = gamePlayers[0];
            User player2 = gamePlayers[1];
            player1.InGame = true; player1.Shape = "X";
            player2.InGame = true; player2.Shape = "O";
            player1.HasTurn = true;
            logs.AppendText("Game has started.\n");
            logs.AppendText($"{player1.Username}'s turn.\n");
            notifyGamePlayers("GAME_START");
            notifyGamePlayers($"{player1.Username}'s turn.");
            gameBoard.Visible = true;
            btnStartGame.Text = "Reset";
        }

        private void notifyGamePlayers(string message) {
            foreach (User user in connectedClients.Values) {
                sendMessage(user, message);
            }
        }


        
    }
}