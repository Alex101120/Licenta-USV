using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Matrix.Xmpp.Roster;
using static Licenta.DashBoard;

namespace Licenta
{
    internal class Server
    {
        public XmppClient client;
        
        public string Destinatar;
        public string Mesaj;
        public List<User> activeUsers = new List<User>();
        public event EventHandler UsersUpdated;


        public Server()
        {
            client = new XmppClient
            {
                XmppDomain = "localhost", // Domeniul serverului XMPP
                Username = "admin", // Numele de utilizator pentru aplicația ta
                Password = "admin", // Parola utilizatorului
                Resource = "CSharpApp" // O resursă opțională pentru identificarea sesiunii
                
        };

            // Abonează-te la evenimentul OnMessage pentru a gestiona mesajele primite
            client.OnMessage += (sender, e) =>
            {
                Debug.WriteLine($"Message from {e.Message.From}: {e.Message.Body}");
                string Destinatar = e.Message.From;
                string Mesaj = e.Message.Body;
                if (Mesaj == "Online")
                {
                   activeUsers.Add(new User { Username = Destinatar, IsActive = true });
                    UsersUpdated?.Invoke(this, EventArgs.Empty);

                }

                
            };

            // Abonează-te la evenimentul OnError pentru a gestiona erorile
            client.OnError += (sender, e) =>
            {
                Debug.WriteLine($"Error: {e.Exception}");
            };

            // Abonează-te la evenimentul OnAuthError pentru a gestiona erorile de autentificare
            client.OnAuthError += (sender, e) =>
            {
                Debug.WriteLine("Authentication Error: " + e.Failure);
            };

            // Handle roster updates
          

            // Handle presence updates
            
        }

        public List<User> GetActiveUsers()
        {
            Debug.WriteLine("2");
            return activeUsers;
        }
        // Metodă pentru a conecta clientul la serverul XMPP
        public void ConnectToServer()
        {
            // Conectează-te la server
            client.Open();
            
        }

        // Metodă pentru a închide conexiunea la serverul XMPP
        public void DisconnectFromServer()
        {
            // Închide conexiunea la server
            client.Close();
        }

        public void SendMessage(string to, string messageBody)
        {
            if (client != null)
            {
                client.Send(new Message
                {
                    To = to,
                    Body = messageBody,
                    Type = MessageType.Chat
                });
            }
            else
            {
                Console.WriteLine("Client is not connected to the XMPP server.");
            }
        }

        public bool IsConnected()
        {
            return client.StreamActive;
        }
       
    }
}

   



