using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Net;
using Matrix.Xmpp.Muc;
using System;
using System.Diagnostics;

namespace Licenta
{
    internal class Connect
    {
        private XmppClient client;

        public Connect()
        {
            client = new XmppClient
            {
                XmppDomain = "localhost",    // Domeniul serverului XMPP
                Username = "admin",              // Numele de utilizator pentru aplicația ta
                Password = "admin",         // Parola utilizatorului
                Resource = "CSharpApp"         // O resursă opțională pentru identificarea sesiunii
            };

            // Abonează-te la evenimentul OnMessage pentru a gestiona mesajele primite
            client.OnMessage += (sender, e) =>
            {
                Debug.WriteLine($"Message from {e.Message.From}: {e.Message.Body}");
            };

            // Abonează-te la evenimentul OnError pentru a gestiona erorile
            client.OnError += (sender, e) =>
            {
                Console.WriteLine($"Error: {e.Exception}");
            };

            // Abonează-te la evenimentul OnAuthError pentru a gestiona erorile de autentificare
            client.OnAuthError += (sender, e) =>
            {
                Console.WriteLine("Authentication Error: " + e.Failure);
            };
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
        public void CreateChatRoom(string roomName, string mucService, string xmppDomain)
        {
            
            string command = $"ejabberdctl create_room {roomName} {mucService} {xmppDomain}";

            // Creează un proces pentru a executa comanda
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe"; // Specificăm că utilizăm cmd.exe pentru a executa comanda
            process.StartInfo.Arguments = $"/c {command}"; // Specificăm comanda care trebuie executată
            process.StartInfo.UseShellExecute = false; // Folosim false pentru a putea redirecționa ieșirea
            process.StartInfo.RedirectStandardOutput = true; // Redirecționăm ieșirea procesului
            process.Start(); // Pornim procesul

            // Așteptăm ca procesul să se încheie
            process.WaitForExit();

            // Citim și afișăm ieșirea procesului
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }
    }
}
