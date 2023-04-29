using System.Net.Sockets;

namespace tictactoe_network_server
{
    public class User
    {
        public string Username { get; set; }
        public Socket Socket { get; set; }
        public int Points { get; set; }
    
        public User(string username, Socket socket, int points)
        {
            Username = username;
            Socket = socket;
            Points = points;
        }
    }
}