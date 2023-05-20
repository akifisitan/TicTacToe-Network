namespace tictactoe_network_server {
    public class PlayerPair {
        public Player Player1 { get; set; } // Player1's shape is always X
        public Player Player2 { get; set; }

        public PlayerPair() {
            Player1 = null;
            Player2 = null;
        }

        public void Clear() {
            Player1 = null;
            Player2 = null;
        }
        
    }

}