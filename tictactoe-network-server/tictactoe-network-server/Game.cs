﻿using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace tictactoe_network_server {
    public class Game {
        
        // Stores labels of the game board for easier UI manipulation
        public List<Label> Board { get; }
        
        // Stores the active players of the game
        public PlayerPair Players { get; }
        
        // Queue to store the waiting players
        private Queue<string> WaitList { get; }
        
        // True if the game is ongoing
        public bool IsActive { get; set; }
        
        // True if the game is paused and requires an eligible player to join
        public bool IsAwaitingPlayer { get; set; }
        
        // Stores player which had the turn before a game pause (0: None, 1: Player1, 2: Player2)
        public int TurnBeforePause { get; set; }
        
        public Game() {
            Board = new List<Label>(10) { null };
            Players = new PlayerPair();
            WaitList = new Queue<string>();
            IsActive = false;
            IsAwaitingPlayer = false;
            TurnBeforePause = 0;
        }

        public bool IsPlayer(string username) {
            if (Players.Player1 != null && username == Players.Player1.Username)
                return true;
            if (Players.Player2 != null && username == Players.Player2.Username)
                return true;
            return false;
        }

        public int DeterminePlayerNumber(string username) {
            if (!IsPlayer(username)) return 0;
            return username == Players.Player1.Username ? 1 : 2;
        }

        public void RemovePlayer(string username) {
            if (Players.Player1 != null && username == Players.Player1.Username) {
                Players.Player1 = null;
            }
            else if (Players.Player2 != null && username == Players.Player2.Username) {
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

        // Find a way to resume the
        public int ResumeGame(Player newPlayer) {
            int resumeStatus;
            // If the game is missing both players
            if (Players.Player1 == null && Players.Player2 == null) {
                newPlayer.Shape = "X";
                Players.Player1 = newPlayer;
                resumeStatus = 0;
            }
            // If the game is only missing player 1
            else if (Players.Player1 == null) {
                newPlayer.Shape = "X";
                Players.Player1 = newPlayer;
                Players.Player2.Shape = "O";
                Players.Player1.HasTurn = TurnBeforePause == 1;
                Players.Player2.HasTurn = !Players.Player1.HasTurn;
                resumeStatus =  1;
            }
            // If the game is only missing player 2
            else if (Players.Player2 == null) {
                Players.Player2 = newPlayer;
                Players.Player1.Shape = "X";
                Players.Player2.Shape = "O";
                Players.Player1.HasTurn = TurnBeforePause == 1;
                Players.Player2.HasTurn = !Players.Player1.HasTurn;
                resumeStatus = 2;
            }
            // Error, should not happen
            else {
                resumeStatus = -1;
            }
            return resumeStatus;
        }

        public void ResetGame() {
            IsActive = false;
            IsAwaitingPlayer = false;
            Players.Clear();
            WaitList.Clear();
            ResetGameBoard();
        }
        
        public void EndGame() {
            IsActive = false;
            IsAwaitingPlayer = false;
            Players.Clear();
            WaitList.Clear();
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
            return WaitList.Count == 0 ? "" : WaitList.Dequeue();
        }
        
        public void AddToWaitList(string username) {
            WaitList.Enqueue(username);
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
                if (Board[i].Text == i.ToString()) return false;
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