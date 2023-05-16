using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace tictactoe_network_server {
    public class Game {
        public List<Label> Board { get; }
        public PlayerPair Players { get; }
        private Queue<string> WaitList { get; }
        private HashSet<string> LeftGame { get; }
        public bool IsActive { get; private set; }
        public bool IsAwaitingPlayer { get; private set; }

        public Game() {
            Board = new List<Label>(10) { null };
            Players = new PlayerPair();
            WaitList = new Queue<string>();
            LeftGame = new HashSet<string>();
            IsActive = false;
            IsAwaitingPlayer = false;
        }

        public HashSet<string> PlayerUsernames() {
            return new HashSet<string> {
                Players.Player1.Username,
                Players.Player2.Username
            };
        }

        public void RemovePlayer(string username) {
            if (username == Players.Player1.Username) {
                Players.Player1 = null;
            }
            else
            {
                Players.Player2 = null;
            }
        }
        
        public void StartGame(Player player1, Player player2) {
            Players.Player1 = player1;
            Players.Player2 = player2;
            Players.Player1.Shape = "X";
            Players.Player2.Shape = "O";
            Players.Player1.HasTurn = true;
            Players.Player2.HasTurn = false;
            IsActive = true;
        }

        public string ResumeGame(Player newPlayer) {
            string resumeStatus = "";
            if (Players.Player1 == null) {
                Players.Player1 = newPlayer;
                Players.Player1.Shape = "X";
                Players.Player2.Shape = "O";
                IsActive = true;
                IsAwaitingPlayer = false;
                resumeStatus =  Players.Player1.Username;
            }
            else if (Players.Player2 == null) {
                Players.Player2 = newPlayer;
                Players.Player1.Shape = "X";
                Players.Player2.Shape = "O";
                IsActive = true;
                IsAwaitingPlayer = false;
                resumeStatus = Players.Player2.Username;
            }
            return resumeStatus;
        }

        public void ResetGame() {
            IsActive = false;
            IsAwaitingPlayer = false;
            Players.Clear();
            WaitList.Clear();
            LeftGame.Clear();
            ResetGameBoard();
        }
        
        public void EndGame() {
            IsActive = false;
            IsAwaitingPlayer = false;
            Players.Clear();
            WaitList.Clear();
            LeftGame.Clear();
        }

        // Function which maps the board state as a string and returns it
        // Example: "BX2OX5XO8X"
        public string BoardToString() {
            StringBuilder sb = new StringBuilder("B");
            for (int i = 1; i <= 9; i++) {
                sb.Append(Board[i].Text);
            }
            return sb.ToString();
        }

        public string PickNewPlayerFromWaitList() {
            IsActive = false;
            IsAwaitingPlayer = true;
            if (WaitList.Count == 0) return "";
            string newPlayerUsername = WaitList.Dequeue();
            while (LeftGame.Contains(newPlayerUsername)) {
                if (WaitList.Count > 0)
                    newPlayerUsername = WaitList.Dequeue();
                else
                    return "";
            }
            return newPlayerUsername;
        }
        
        public void AddToWaitList(string username) {
            WaitList.Enqueue(username);
        }

        public void AddToLeftGameList(string username) {
            LeftGame.Add(username);
        }
        
        // Function to modify board UI, returns false if the board is full 
        public bool MakeMove(int boardPosition, string shape) {
            if (Board[boardPosition].Text == "O" || Board[boardPosition].Text == "X") {
                return false;
            }
            Board[boardPosition].Text = shape;
            return true;
        }
        
        // Function to check if there is a winner, returns true if there is a winner
        public bool CheckIfWinner() {
            return (Board[1].Text == Board[2].Text && Board[2].Text == Board[3].Text && Board[1].Text != "1") ||
                   (Board[4].Text == Board[5].Text && Board[5].Text == Board[6].Text && Board[4].Text != "4") ||
                   (Board[7].Text == Board[8].Text && Board[8].Text == Board[9].Text && Board[7].Text != "7") ||
                   // Check Columns
                   (Board[1].Text == Board[4].Text && Board[4].Text == Board[7].Text && Board[1].Text != "1") ||
                   (Board[2].Text == Board[5].Text && Board[5].Text == Board[8].Text && Board[2].Text != "2") ||
                   (Board[3].Text == Board[6].Text && Board[6].Text == Board[9].Text && Board[3].Text != "3") ||
                   // Check Diagonals
                   (Board[1].Text == Board[5].Text && Board[5].Text == Board[9].Text && Board[1].Text != "1") ||
                   (Board[3].Text == Board[5].Text && Board[5].Text == Board[7].Text && Board[3].Text != "3");
        }
        
        // Function to check if the board is full, returns true if the board is full
        public bool BoardIsFull() {
            for (int i = 1; i <= 9; i++) {
                if (Board[i].Text != i.ToString()) return false;
            } 
            return true;
        }
        
        // Function to reset the game board to a clean state
        private void ResetGameBoard() {
            for (int i = 1; i <= 9; i++) {
                Board[i].Text = i.ToString();
            }
        }
    }
}