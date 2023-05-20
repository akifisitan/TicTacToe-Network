using System.Net.Sockets;

namespace tictactoe_network_server {
    public class User {
        public string Username { get; }
        public Socket Socket { get; }
        
        public User(string username, Socket socket) {
            Username = username;
            Socket = socket;
        }
    }
}