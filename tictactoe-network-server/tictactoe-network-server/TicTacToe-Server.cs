using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe_network_server {
    public partial class MainWindow : Form {

        // Server capacity
        private const int ServerCapacity = 4;

        // Server socket
        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Dictionary which maps usernames to users for ease of access
        private Dictionary<string, User> connectedClients = new Dictionary<string, User>(ServerCapacity);
        
        // Dictionary to store game score data
        private Dictionary<string, Player> scores = new Dictionary<string, Player>();
        
        // Variable to store and manipulate game data
        private Game game = new Game();
        
        // Control flow variables
        private bool terminating = false;
        private bool listening = false;
        
        public MainWindow() {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
            InitializeComponent();
        }
        
        private void MainWindow_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
        
        // Start button logic
        private void btnStart_Click(object sender, EventArgs e) {
            int serverPort;
            // Check if the port number is a valid integer and start the server if it is
            if (Int32.TryParse(txtBoxPort.Text, out serverPort)) {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);
                listening = true; btnListen.Enabled = false;
                txtBoxPort.Enabled = false; 
                btnStartGame.Enabled = true;
                // Start a thread to accept incoming connections
                Thread acceptThread = new Thread(AcceptConnections);
                acceptThread.Start();
                logs.AppendText($"Started listening on port: {serverPort}\n");
            } else {
                logs.AppendText("Please check the port number!\n");
            }
        }
        
        // Accept incoming connections
        private void AcceptConnections() {
            while (listening) {
                try {
                    Socket newClient = serverSocket.Accept();
                    // Receive the username as a message from the user
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    // Check if the provided username is already connected or if the server capacity is full
                    if (connectedClients.Count >= ServerCapacity) {
                        newClient.Send(Encoding.Default.GetBytes("SERVER_IS_FULL"));
                        newClient.Close();
                        logs.AppendText("Rejected connection as the room is already at max capacity.\n");
                    }
                    else if (connectedClients.ContainsKey(username)) {
                        newClient.Send(Encoding.Default.GetBytes("USERNAME_IS_USED"));
                        newClient.Close();
                        logs.AppendText("Rejected connection as there is already a player with this username.\n");
                    } else {
                        newClient.Send(Encoding.Default.GetBytes("JOIN_SUCCESS"));
                        // Create a user and add it to the users dictionary
                        User user = new User(username, newClient);
                        connectedClients.Add(user.Username, user);
                        logs.AppendText($"{username} joined the room.\n");
                        RefreshPlayerList();
                        // Add user to the game waitList if there is an ongoing game
                        // Problem with this arises when the game is paused due to the lack of players
                        if (game.IsActive) {
                            game.AddToWaitList(username);
                        }
                        else if (game.IsAwaitingPlayer) {
                            game.AddToWaitList(username);
                            string newPlayerUsername = game.PickNewPlayerFromWaitList();
                            if (newPlayerUsername == "") {
                                logs.AppendText("No suitable player found, waiting for another user to join...\n");
                            }
                            else {
                                Player newPlayer = scores.ContainsKey(newPlayerUsername) ?
                                    scores[newPlayerUsername] : new Player(newPlayerUsername);
                                string resumeGameStatus = game.ResumeGame(newPlayer);
                                logs.AppendText(resumeGameStatus != ""
                                    ? $"Resuming the game with {newPlayerUsername} as the new player.\n"
                                    : "Error resuming the game.\n");
                            }
                        }
                        // Start a separate thread to receive messages from the connected client  
                        Thread receiveThread = new Thread(() => ReceiveMessages(user)); 
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
        
        // Method for updating the scoreboard
        private void UpdateScoreboard() {
            txtBoxScores.Clear();
            StringBuilder sb = new StringBuilder();
            foreach (Player player in scores.Values) {
                sb.Append($"-> {player.Username} ({player.Wins}/{player.Losses}/{player.Draws})\n");
            }
            txtBoxScores.AppendText(sb.ToString());
        }
        
        // Receive game moves from clients
        private void ReceiveMessages(User user) {
            bool connected = true;
            while (connected && !terminating) {
                try {
                    Byte[] buffer = new Byte[64];
                    user.Socket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (!game.PlayerUsernames().Contains(user.Username)) continue;
                    Player player = user.Username == game.Players.Player1.Username 
                        ? game.Players.Player1 : game.Players.Player2;
                    // Game logic below
                    if (!player.HasTurn) {
                        SendMessage(user, "Please wait for your turn!");
                        continue;
                    }
                    int playerChoice;
                    // The sent message is not a valid choice
                    if (!(Int32.TryParse($"{incomingMessage}", out playerChoice) && 0 <= playerChoice &&
                          playerChoice <= 9)) {
                        SendMessage(user, "Please enter a number. (1-9)");
                        continue;
                    }
                    // Board place is full
                    if (!game.MakeMove(playerChoice, player.Shape)) {
                        SendMessage(user, "That place is full!");
                        continue;
                    }
                    // Correct play
                    logs.AppendText($"{player.Username}'s ({player.Shape}) play: {incomingMessage}\n");
                    NotifyClients($"BOARD_{game.BoardToString()}");
                    // NotifyClients($"BOARD_ADD_{playerChoice}_{player.Shape}");
                    NotifyClients($"{user.Username}'s ({player.Shape}) play: {playerChoice}");
                    // Get the next player from the gamePlayers list
                    Player nextPlayer = user.Username != game.Players.Player1.Username 
                        ? game.Players.Player1 : game.Players.Player2;
                    // Current player wins
                    if (game.CheckIfWinner()) {
                        logs.AppendText($"{player.Username} wins!\n");
                        txtTurn.Text = $"{player.Shape} wins!";
                        // Increment wins and losses respectively & update the scoreboard
                        player.Wins++;
                        scores[player.Username] = player;
                        nextPlayer.Losses++;
                        scores[nextPlayer.Username] = nextPlayer;
                        UpdateScoreboard();
                        // Notify the game players
                        NotifyClients($"GAME_END_{user.Username}");
                        game.EndGame();
                        btnStartGame.Text = "Start Game";
                        continue;
                    }
                    // Draw, as the board is full
                    if (game.BoardIsFull()) {
                        logs.AppendText("Game ended in a draw!\n");
                        txtTurn.Text = "Draw!";
                        player.Draws++;
                        scores[player.Username] = player;
                        nextPlayer.Draws++;
                        scores[nextPlayer.Username] = nextPlayer;
                        UpdateScoreboard();
                        NotifyClients("GAME_END_DRAW");
                        game.EndGame();
                        btnStartGame.Text = "Start Game";
                        continue;
                    }
                    // Game continues, set the current turn to the other player
                    player.HasTurn = false;
                    nextPlayer.HasTurn = true;
                    NotifyClients($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.");
                    logs.AppendText($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
                    txtTurn.Text = $"{nextPlayer.Shape}'s Turn";
                }
                // Happens when a client disconnects
                catch (Exception e) {
                    if (!terminating) {
                        logs.AppendText($"{user.Username} left the room.\n");
                    }
                    if (game.IsActive && game.PlayerUsernames().Contains(user.Username)) {
                        logs.AppendText($"{user.Username} has left the game. Looking for possible candidates...\n");
                        game.RemovePlayer(user.Username);
                        game.AddToLeftGameList(user.Username);
                        string newPlayerUsername = game.PickNewPlayerFromWaitList();
                        if (newPlayerUsername == "") {
                            logs.AppendText("No suitable player found, waiting for an eligible user to join...\n");
                        }
                        else {
                            Player newPlayer = scores.ContainsKey(newPlayerUsername) ?
                                scores[newPlayerUsername] : new Player(newPlayerUsername);
                            string resumeGameStatus = game.ResumeGame(newPlayer);
                            logs.AppendText(resumeGameStatus != ""
                                ? $"Resuming the game with {newPlayerUsername} as the new player.\n"
                                : "Error resuming the game.\n");
                        }
                    }
                    user.Socket.Close();
                    connectedClients.Remove(user.Username);
                    RefreshPlayerList();
                    connected = false;
                }
            }
        }
        
        // Send a message to a connected user
        private void SendMessage(User user, string message) {
            try {
                if (!connectedClients.ContainsKey(user.Username)) {
                    logs.AppendText($"{user.Username} is not connected to the server!\n");
                    return;
                }
                Byte[] buffer = Encoding.Default.GetBytes(message);
                user.Socket.Send(buffer);
            } catch {
                logs.AppendText("There is a problem! Check the connection...\n");
                terminating = true;
                txtBoxPort.Enabled = true;
                btnListen.Enabled = true;
                serverSocket.Close();
            }
        }
        
        // Start game button logic
        private void btnStartGame_Click(object sender, EventArgs e) {
            if (btnStartGame.Text == "Reset Game") {
                NotifyClients("GAME_RESET");
                game.ResetGame();
                gameBoard.Visible = false;
                btnStartGame.Text = "Start Game";
                logs.AppendText("Game has been reset.\n");
                return;
            }
            if (connectedClients.Count < 2) {
                logs.AppendText("There are not enough players to start the game!\n");
                return;
            }
            // Fill up the game board (Should be only done once optimally)
            if (game.Board.Count < 9) {
                for (int i = 1; i <= 9; i++) {
                    Label board = Controls.Find($"board{i}", true).FirstOrDefault() as Label;
                    if (board != null) {
                        game.Board.Add(board);
                    }
                }
            }
            game.ResetGame();
            List<string> selectedPlayers = new List<string>();
            List<string> awaitingPlayers = new List<string>();
            // Randomly pick 2 players from the connected players
            foreach (string username in connectedClients.Keys) { awaitingPlayers.Add(username); }
            Random rng = new Random();
            while (selectedPlayers.Count < 2) {
                int randomIndex = rng.Next(awaitingPlayers.Count);
                selectedPlayers.Add(awaitingPlayers[randomIndex]);
                awaitingPlayers.RemoveAt(randomIndex);
            }
            // Add the remaining players names to the wait list
            foreach (string s in awaitingPlayers) game.AddToWaitList(s);
            // Either create a new player or get the player from the scoreboard data
            string potentialPlayer1 = selectedPlayers[0];
            string potentialPlayer2 = selectedPlayers[1];
            Player player1 = scores.ContainsKey(potentialPlayer1) ? 
                scores[potentialPlayer1] : new Player(potentialPlayer1);
            Player player2 = scores.ContainsKey(potentialPlayer2) ? 
                scores[potentialPlayer2] : new Player(potentialPlayer2);
            // Start the game
            game.StartGame(player1, player2);
            logs.AppendText("Game has started!\n");
            logs.AppendText($"{player1.Username}'s ({player1.Shape}) turn.\n");
            // Notify everyone that the game started
            NotifyClients("GAME_START");
            NotifyClients($"{player1.Username}'s ({player1.Shape}) turn.");
            gameBoard.Visible = true;
            btnStartGame.Text = "Reset Game";
            txtTurn.Text = "X's turn";
        }
        
        // Function to send messages to the participants of the current game
        private void NotifyClients(string message) {
            foreach (User user in connectedClients.Values) {
                SendMessage(user, message);
            }
        }
        
        private void NotifySpectators(string message) {
            foreach (User user in connectedClients.Values) {
                if (game.PlayerUsernames().Contains(user.Username)) continue;
                SendMessage(user, message);
            }
        }

        private void NotifyPlayers(string message) {
            SendMessage(connectedClients[game.Players.Player1.Username], message);
            SendMessage(connectedClients[game.Players.Player2.Username], message);
        }
        
    }
}