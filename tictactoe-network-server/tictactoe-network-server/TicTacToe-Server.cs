﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        
        //  --- UI Logic ---
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
        
        // Start / Reset game button logic
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
            StartNewGame();
        }
        
        //  --- UI Logic End ---
        
        //  --- Main Functions ---
        
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
                            SendMessage(username, "GAME_START_SPECTATOR\n");
                            SendMessage(username, $"BOARD_{game.BoardToString()}\n");
                            SendMessage(username, "You are spectating the ongoing game.\n");
                            foreach (Player player in scores.Values) {
                                SendMessage(username, $"SCOREBOARD/{player.Username}/{player.Wins}/" +
                                                  $"{player.Losses}/{player.Draws}\n");
                            }
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
                        SendMessage(player.Username, "Please wait for your turn!\n");
                        continue;
                    }
                    int playerChoice;
                    // The sent message is not a valid choice, client side already checks for this
                    if (!(Int32.TryParse(incomingMessage, out playerChoice) && 1 <= playerChoice &&
                          playerChoice <= 9)) {
                        SendMessage(player.Username, "Please enter a number. (1-9)\n");
                        continue;
                    }
                    // Board place is full
                    if (!game.MakeMove(playerChoice, player.Shape)) {
                        SendMessage(player.Username, "That place is full!\n");
                        SendMessage(player.Username, "YOUR_TURN\n");
                        continue;
                    }
                    // Correct play
                    logs.AppendText($"{player.Username}'s ({player.Shape}) play: {incomingMessage}\n");
                    NotifyClients($"BOARD_{game.BoardToString()}\n");
                    NotifyClients($"{player.Username}'s ({player.Shape}) play: {playerChoice}\n");
                    // Get the next player from the gamePlayers list
                    Player nextPlayer = player.Username != game.Players.Player1.Username 
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
                        Thread.Sleep(5000);
                        StartNewGame();
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
                        Thread.Sleep(5000);
                        StartNewGame();
                        continue;
                    }
                    // Game continues, set the current turn to the other player
                    player.HasTurn = false;
                    nextPlayer.HasTurn = true;
                    NotifyClients($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
                    NotifyPlayer(nextPlayer.Username, "YOUR_TURN\n");
                    logs.AppendText($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
                    txtTurn.Text = $"{nextPlayer.Shape}'s Turn";
                }
                // Happens when a client disconnects
                catch {
                    connected = false;
                    try {
                        string leavingPlayer = user.Username;
                        user.Socket.Close();
                        user = null;
                        logs.AppendText($"{leavingPlayer} left the room.\n");
                        connectedClients.Remove(leavingPlayer);
                        NotifyClients($"{leavingPlayer} left the room.\n");
                        RefreshPlayerList();
                        // If an active player leaves the game
                        if (game.IsActive && game.IsPlayer(leavingPlayer)) {
                            PauseGame();
                            logs.AppendText($"Looking for a replacement for {leavingPlayer}.\n");
                            game.RemovePlayer(leavingPlayer);
                            ResumeGame();
                        }
                        // If the game is waiting for a player to continue and an active player leaves the game
                        else if (game.IsAwaitingPlayer && game.IsPlayer(leavingPlayer)) {
                            game.RemovePlayer(leavingPlayer);
                        }
                    } catch (Exception e) {
                        logs.AppendText($"Error occurred after a player left: {e}\n");
                    }
                }
            }
        }
        
        //  --- Main Functions End ---
        
        //  --- Helper Functions ---
        
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

        // Send a message to a connected user
        private void SendMessage(string username, string message) {
            try {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                ((User)connectedClients[username]).Socket.Send(buffer);
                // user.Socket.Send(buffer);
            } catch (Exception e) {
                logs.AppendText($"Error while sending a message to user: {username}:\n{e}\n");
            }
        }
        
        // Send a message to every connected user 
        private void NotifyClients(string message) {
            foreach (string username in connectedClients.Keys) {
                SendMessage(username, message);
            }
        }
        
        // Send a message to the ongoing game's spectators
        private void NotifySpectators(string message) {
            foreach (string username in connectedClients.Keys) {
                if (game.IsPlayer(username)) continue;
                SendMessage(username, message);
            }
        }

        // Send a message to one of the ongoing game's players
        private void NotifyPlayer(string playerUsername, string message) {
            try {
                SendMessage(playerUsername, message);
            } catch (Exception e) {
                logs.AppendText($"Error messaging player: {e}\n");
            }
        }
        
        // Send the scoreboard state to every user as "SCOREBOARD/username/wins/losses/draws" for each player
        private void BroadcastScoreboard() {
            foreach (string username in connectedClients.Keys) {
                SendMessage(username, "CLEAR_SCOREBOARD\n");
                foreach (Player player in scores.Values) {
                    SendMessage(username, 
                        $"SCOREBOARD/{player.Username}/{player.Wins}/{player.Losses}/{player.Draws}\n");
                }
            }
        }
        
        // Start a new game
        private void StartNewGame() {
            game.ResetGame();
            List<string> selectedPlayers = new List<string>();
            foreach (string username in connectedClients.Keys) {
                if (selectedPlayers.Count < 2)
                    selectedPlayers.Add(username);
                else
                    game.AddToWaitList(username);
            }
            // Get the players from the scoreboard data if they exist or create new players 
            string p1Username = selectedPlayers[0];
            Player player1 = scores.ContainsKey(p1Username) ? 
                scores[p1Username] : new Player(p1Username);
            string p2Username = selectedPlayers[1];
            Player player2 = scores.ContainsKey(p2Username) ?
                scores[p2Username] : new Player(p2Username);
            // Start the game
            game.StartGame(player1, player2);
            logs.AppendText("The game has started.\n");
            logs.AppendText($"{player1.Username}'s ({player1.Shape}) turn.\n");
            // Notify everyone that the game started
            NotifySpectators("GAME_START_SPECTATOR\n");
            NotifyPlayer(player1.Username, "GAME_START_PLAYER1\n");
            NotifyPlayer(player2.Username, "GAME_START_PLAYER2\n");
            NotifyPlayer(player1.Username, "YOUR_TURN\n");
            NotifyClients("The game has started.\n");
            NotifyClients($"{player1.Username}'s ({player1.Shape}) turn.\n");
            gameBoard.Visible = true;
            btnStartGame.Text = "Reset Game";
            txtTurn.Text = "X's turn";
        }
        
        // Pause the ongoing game
        private void PauseGame() {
            game.IsActive = false;
            game.IsAwaitingPlayer = true;
            // Store the current state of the game, that is who the turn belongs to
            game.TurnBeforePause = game.Players.Player1.HasTurn ? 1 : 2;
            NotifyClients("GAME_PAUSE\n");
        }
        
        // Attempts to resume the paused game
        private bool ResumeGame() {
            // Pick a new player from the queue, discarding players that have left the game
            string newPlayerUsername = game.PickNewPlayerFromWaitList();
            while (!connectedClients.Contains(newPlayerUsername)) {
                if (newPlayerUsername == "") {
                    logs.AppendText("The game will resume as soon as a new player joins.\n");
                    NotifyClients("The game will resume as soon as a new player joins.\n");
                    return false;
                }
                newPlayerUsername = game.PickNewPlayerFromWaitList();
            }
            Player newPlayer = new Player(newPlayerUsername);
            if (scores.ContainsKey(newPlayerUsername)) {
                newPlayer.Wins = scores[newPlayerUsername].Wins;
                newPlayer.Draws = scores[newPlayerUsername].Draws;
                newPlayer.Losses = scores[newPlayerUsername].Losses;
            }
            int resumeStatus = game.ResumeGame(newPlayer);
            int newPlayerNumber = game.DeterminePlayerNumber(newPlayer.Username);
            if (resumeStatus == 0) {
                logs.AppendText($"Set {newPlayerUsername} as Player{newPlayerNumber}.\n");
                NotifyClients($"Set {newPlayerUsername} as Player{newPlayerNumber}.\n");
                logs.AppendText("The game is still missing a player.\n");
                NotifyClients("The game is still missing a player.\n");
                return false;
            }
            if (resumeStatus == -1) {
                logs.AppendText("An error occurred in the game, please reset it.\n");
                return false;
            }
            game.IsAwaitingPlayer = false;
            game.IsActive = true;
            NotifySpectators("GAME_RESUME_SPECTATOR\n");
            NotifyPlayer(game.Players.Player1.Username, "GAME_RESUME_PLAYER1\n");
            NotifyPlayer(game.Players.Player2.Username, "GAME_RESUME_PLAYER2\n");
            Player nextPlayer = game.TurnBeforePause == 1 ? game.Players.Player1 : game.Players.Player2;
            NotifyPlayer(nextPlayer.Username, "YOUR_TURN\n");
            NotifyClients($"BOARD_{game.BoardToString()}\n");
            logs.AppendText($"Resuming the game with {newPlayerUsername} as Player{newPlayerNumber}.\n");
            NotifyClients($"Resuming the game with {newPlayerUsername} as Player{newPlayerNumber}.\n");
            logs.AppendText($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
            NotifyClients($"{nextPlayer.Username}'s ({nextPlayer.Shape}) turn.\n");
            return true;
        }

        // Reset the ongoing game to a fresh state
        private void ResetGame() {
            NotifyClients("GAME_RESET\n");
            game.ResetGame();
            gameBoard.Visible = false;
            btnStartGame.Text = "Start Game";
            logs.AppendText("Game has been reset.\n");
        }
        
        //  --- Helper Functions End ---
    }
}