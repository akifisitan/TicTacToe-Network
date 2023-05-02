using System.Net.Sockets;

namespace tictactoe_network_server
{
    public class User
    {
        public string Username { get; set; }
        public Socket Socket { get; set; }
        public int Points { get; set; }
        public bool HasTurn { get; set; }
        public bool InGame { get; set; }
        public string Shape { get; set; }

        public User(string username, Socket socket)
        {
            Username = username;
            Socket = socket;
            Points = 0;
            InGame = false;
            HasTurn = false;
            Shape = "-";
        }
    }
}