using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Matrix.Xmpp.Roster;
using static Licenta.DashBoard;
using System.Linq;
using System.Timers;

namespace Licenta
{
    internal class Server
    {
        public XmppClient client;
        
        public string Destinatar;
        public string Mesaj;
        public List<User> activeUsers = new List<User>();
        public event EventHandler UsersUpdated;

        private Dictionary<string, Timer> userTimers = new Dictionary<string, Timer>();
        private const double timeoutInterval = 300000;




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
                string destinatar = e.Message.From;
                string mesaj = e.Message.Body;
                string raspunsping = "Mesaj Primit";
                if (mesaj == "Online")
                {
                    var user = activeUsers.FirstOrDefault(u => u.Username == destinatar);
                    if (user == null)
                    {
                        user = new User { Username = destinatar, IsActive = true };
                        activeUsers.Add(user);
                        SendMessage(destinatar, raspunsping);
                        UsersUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        user.IsActive = true;
                    }

                    ResetUserTimer(destinatar);
                    WriteTextToFile(@"D:\\Licenta\\Licenta-USV\\Licenta\\Logs\\LogsMesajePrimite.txt", $"Am primit mesajul '{mesaj}' Primit de la {destinatar}");
                    WriteTextToFile(@"D:\\Licenta\\Licenta-USV\\Licenta\\Logs\\LogsMesajeTrimise.txt", $"Am trimis mesajul '{raspunsping}' la {destinatar}");
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

           
            
        }

        public List<User> GetActiveUsers()
        {
           
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
        static void WriteTextToFile(string filePath, string text)
        {
            try
            {
                string currentDate = DateTime.Now.ToShortDateString();
                string currentTime = DateTime.Now.ToLongTimeString();

             
                bool dateExists = File.Exists(filePath) && File.ReadAllText(filePath).Contains(currentDate);

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    if (!dateExists)
                    {
                        writer.WriteLine($"Date: {currentDate}");
                    }

                    writer.WriteLine($"{currentTime}: {text}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public bool IsConnected()
        {
            return client.StreamActive;
        }
        private void ResetUserTimer(string username)
        {
            if (userTimers.ContainsKey(username))
            {
                userTimers[username].Stop();
                userTimers[username].Start();
            }
            else
            {
                Timer timer = new Timer(timeoutInterval);
                timer.Elapsed += (sender, e) => SetUserInactive(username);
                timer.AutoReset = false;
                timer.Start();
                userTimers[username] = timer;
            }
        }

        private void SetUserInactive(string username)
        {
            var user = activeUsers.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.IsActive = false;
                UsersUpdated?.Invoke(this, EventArgs.Empty);
                WriteTextToFile("Log.txt", $"Utilizatorul {username} a fost setat ca inactiv.");
            }
        }

    }
}

   



