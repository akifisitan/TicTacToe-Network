using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private OrderedDictionary connectedClients = new OrderedDictionary(ServerCapacity);
        
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
                    else if (connectedClients.Contains(username)) { 
                        newClient.Send(Encoding.Default.GetBytes("USERNAME_NOT_AVAILABLE"));
                        newClient.Close();
                        logs.AppendText("Rejected connection as there is already a player with this username.\n");
                    } else {
                        newClient.Send(Encoding.Default.GetBytes("JOIN_SUCCESS"));
                        logs.AppendText($"{username} joined the room.\n");
                        NotifyClients($"{username} joined the room.\n");
                        // Create a user and add it to the users dictionary
                        User user = new User(username, newClient);
                        connectedClients.Add(user.Username, user);
                        RefreshPlayerList();
                        // Add user to the game waitList if there is an ongoing game
                        if (game.IsActive) {
                            game.AddToWaitList(username);
                            SendMessage(user, "GAME_START_SPECTATOR\n");
                            SendMessage(user, $"BOARD_{game.BoardToString()}\n");
                            SendMessage(user, "Joined the ongoing game.\n");
                        }
                        else if (game.IsAwaitingPlayer) {
                            game.AddToWaitList(username);
                            ResumeGame();
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

        // Send the scoreboard state to every user as "SCOREBOARD/username/wins/losses/draws" for each player
        private void BroadcastScoreboard() {
            foreach (User user in connectedClients.Values) {
                foreach (Player player in scores.Values) {
                    SendMessage(user, $"SCOREBOARD/{player.Username}/{player.Wins}/" +
                                      $"{player.Losses}/{player.Draws}\n");
                }
            }
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
                    // logs.AppendText($"DEBUG: Incoming message from client {user.Username}:\n{incomingMessage}\n");
                    // Makes sure that a player that is not in the game cannot message
                    if (!game.IsPlayer(user.Username)) continue;
                    Player player = user.Username == game.Players.Player1.Username 
                        ? game.Players.Player1 : game.Players.Player2;
                    // Not really necessary since the UI should be updated via message
                    if (!player.HasTurn) {
                        SendMessage(user, "Please wait for your turn!\n");
                        continue;
                    }
                    int playerChoice;
                    // The sent message is not a valid choice, client side already checks for this
                    if (!(Int32.TryParse(incomingMessage, out playerChoice) && 1 <= playerChoice &&
                          playerChoice <= 9)) {
                        SendMessage(user, "Please enter a number. (1-9)\n");
                        continue;
                    }
                    // Board place is full
                    if (!game.MakeMove(playerChoice, player.Shape)) {
                        SendMessage(user, "That place is full!\n");
                        SendMessage(user, "YOUR_TURN\n");
                        continue;
                    }
                    // Correct play
                    logs.AppendText($"{player.Username}'s ({player.Shape}) play: {incomingMessage}\n");
                    NotifyClients($"BOARD_{game.BoardToString()}\n");
                    NotifyClients($"{user.Username}'s ({player.Shape}) play: {playerChoice}\n");
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
                        BroadcastScoreboard();
                        NotifyClients($"GAME_END_{user.Username}\n");
                        game.EndGame();
                        // btnStartGame.Text = "Start Game";
                        StartGame();
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
                        BroadcastScoreboard();
                        NotifyClients("GAME_END_DRAW\n");
                        game.EndGame();
                        // btnStartGame.Text = "Start Game";
                        StartGame();
                        continue;
                    }
                    // Game continues, set the current turn to the other player
                    player.HasTurn = false;
                    nextPlayer.HasTurn = true;
                    NotifyClients($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
                    NotifyPlayer(nextPlayer, "YOUR_TURN\n");
                    logs.AppendText($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
                    txtTurn.Text = $"{nextPlayer.Shape}'s Turn";
                }
                // Happens when a client disconnects
                catch {
                    string leavingPlayer = user.Username;
                    if (!terminating) {
                        logs.AppendText($"{leavingPlayer} left the room.\n");
                    }
                    connected = false;
                    user.Socket.Close();
                    user = null;
                    connectedClients.Remove(leavingPlayer);
                    NotifyClients($"{leavingPlayer} left the room.\n");
                    RefreshPlayerList();
                    // If an active player leaves the game
                    if (game.IsActive && game.IsPlayer(leavingPlayer)) {
                        game.IsActive = false;
                        game.IsAwaitingPlayer = true;
                        // Store the current state of the game, that is who the turn belongs to
                        game.TurnBeforePause = game.Players.Player1.HasTurn ? 1 : 2;
                        logs.AppendText($"Looking for possible replacements for {leavingPlayer}...\n");
                        NotifyClients("GAME_PAUSE\n");
                        game.RemovePlayer(leavingPlayer);
                        game.AddToLeftGameList(leavingPlayer);
                        ResumeGame();
                    }
                    // If the game is waiting for a player to continue and an active player leaves the game
                    else if (game.IsAwaitingPlayer && game.IsPlayer(leavingPlayer)) {
                        game.RemovePlayer(leavingPlayer);
                        game.AddToLeftGameList(leavingPlayer);
                    }
                }
            }
        }

        private void ResumeGame() {
            string newPlayerUsername = game.PickNewPlayerFromWaitList();
            // DEBUG logs.AppendText($"Chose {newPlayerUsername} as user.\n");
            if (newPlayerUsername == "") {
                logs.AppendText("The game will resume when an eligible user joins.\n");
                NotifyClients("The game will resume when an eligible user joins.\n");
            }
            else {
                Player newPlayer = scores.ContainsKey(newPlayerUsername) ?
                    scores[newPlayerUsername] : new Player(newPlayerUsername);
                int resumeStatus = game.ResumeGame(newPlayer);
                if (resumeStatus == 0) {
                    logs.AppendText("Still missing a player.\n");
                    NotifyClients("Still missing a player.\n");
                }
                else {
                    game.IsAwaitingPlayer = false;
                    game.IsActive = true;
                    NotifySpectators("GAME_RESUME_SPECTATOR\n");
                    NotifyPlayer(game.Players.Player1, "GAME_RESUME_PLAYER1\n");
                    NotifyPlayer(game.Players.Player2, "GAME_RESUME_PLAYER2\n");
                    NotifyPlayer(game.TurnBeforePause == 1 ? game.Players.Player1 : game.Players.Player2, 
                        "YOUR_TURN\n");
                    NotifyClients($"BOARD_{game.BoardToString()}\n");
                    logs.AppendText($"Resuming the game with {newPlayerUsername} as the new player.\n");
                    NotifyClients($"Resuming the game with {newPlayerUsername} as the new player.\n");
                }
            }
        }
        
        // Send a message to a connected user
        private void SendMessage(User user, string message) {
            try {
                if (!connectedClients.Contains(user.Username)) {
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
                ResetGame();
                return;
            }
            if (connectedClients.Count < 2) {
                logs.AppendText("There are not enough players to start the game!\n");
                return;
            }
            // Fill up the game board (Will only be done once)
            if (game.Board.Count < 9) {
                for (int i = 1; i <= 9; i++) {
                    Label board = Controls.Find($"board{i}", true).FirstOrDefault() as Label;
                    if (board != null) { game.Board.Add(board); }
                }
            }
            StartGame();
        }

        private void ResetGame() {
            NotifyClients("GAME_RESET\n");
            game.ResetGame();
            gameBoard.Visible = false;
            btnStartGame.Text = "Start Game";
            logs.AppendText("Game has been reset.\n");
        }
        
        private void StartGame() {
            game.ResetGame();
            List<string> selectedPlayers = new List<string>();
            foreach (string username in connectedClients.Keys) {
                if (selectedPlayers.Count < 2)
                    selectedPlayers.Add(username);
                else
                    game.AddToWaitList(username);
            }
            // Either create a new player or get the player from the scoreboard data
            string p1Username = selectedPlayers[0];
            Player player1 = scores.ContainsKey(p1Username) ? 
                scores[p1Username] : new Player(p1Username);
            string p2Username = selectedPlayers[1];
            Player player2 = scores.ContainsKey(p2Username) ?
                scores[p2Username] : new Player(p2Username);
            // Start the game
            game.StartGame(player1, player2);
            logs.AppendText("Game has started!\n");
            logs.AppendText($"{player1.Username}'s ({player1.Shape}) turn.\n");
            // Notify everyone that the game started
            NotifySpectators("GAME_START_SPECTATOR\n");
            NotifyPlayer(player1, "GAME_START_PLAYER1\n");
            NotifyPlayer(player2, "GAME_START_PLAYER2\n");
            NotifyPlayer(player1, "YOUR_TURN\n");
            NotifyClients("The game has started.\n");
            NotifyClients($"{player1.Username}'s ({player1.Shape}) turn.\n");
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
                if (game.IsPlayer(user.Username)) continue;
                SendMessage(user, message);
            }
        }

        private void NotifyPlayer(Player player, string message) {
            SendMessage((User) connectedClients[player.Username], message);
        }
        
    }
}