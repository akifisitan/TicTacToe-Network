using System.Net.Sockets;

namespace tictactoe_network_server
{
    public class User
    {
        public string Username { get; set; }
        public Socket Socket { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public int Draws { get; set; }

        public bool HasTurn { get; set; }
        public bool InGame { get; set; }
        public string Shape { get; set; }

        public User(string username, Socket socket)
        {
            Username = username;
            Socket = socket;
            Wins = 0;
            Losses = 0;
            Draws = 0;
            InGame = false;
            HasTurn = false;
            Shape = "-";
        }
    }
}