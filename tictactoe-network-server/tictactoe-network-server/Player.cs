namespace tictactoe_network_server {
    public class Player {
        public string Username { get; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public bool HasTurn { get; set; }
        public string Shape { get; set; }

        public Player(string username) {
            Username = username;
            Wins = 0;
            Losses = 0;
            Draws = 0;
            HasTurn = false;
            Shape = ""; // Will be set by the game, if in-game. Player 1 is always X
        }
    }
}