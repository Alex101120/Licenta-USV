﻿using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Matrix.Xmpp.Roster;
using static Licenta.DashBoard;

using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace Licenta
{
    internal class Server
    {
        public XmppClient client;
        public bool error = false;
        public string Destinatar;
        public string Mesaj;
        public List<User> activeUsers = new List<User>();
        public event EventHandler UsersUpdated;
        public event EventHandler MesajPrimit;

        private Dictionary<string, System.Timers.Timer> userTimers = new Dictionary<string, System.Timers.Timer>();
        private const double timeoutInterval = 300000;
        Dictionary<string, List<string>> sensorData = new Dictionary<string, List<string>>();
        public string baseLogPath = @"D:\Licenta\Licenta-USV\Licenta\Logs";
        string sensorName;
        





        public Server()
        {
            AuthFrom authFrom = new AuthFrom();
            

            client = new XmppClient
            {
                XmppDomain = "localhost", // Domeniul serverului XMPP
                Username = authFrom.GetUsername(), // Numele de utilizator
                Password = authFrom.GetPassword(), // Parola utilizatorului
                Resource = "CSharpApp" // O resursă opțională pentru identificarea sesiunii
            };
            

            client.OnMessage += (sender, e) =>
            {
                Debug.WriteLine($"Message from {e.Message.From}: {e.Message.Body}");
                string destinatar = e.Message.From;
                string mesaj = e.Message.Body;
                string raspunsping = "Mesaj Primit";

                //Sistem de prezenţă
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
                    user.IsActive = true;
                    
                }


                else
                {
                    
                    string[] parts = mesaj.Split('/');
                    if (parts.Length == 2)
                    {
                        
                        sensorName = parts[0]; // Exemplu: "SenzorA"
                        string sensorValue = parts[1]; // Exemplu: "6"
                        Debug.WriteLine($"{sensorName}: {sensorValue}");

                        // Verificare dacă există deja cheia (numele senzorului) în dicționar
                        if (!sensorData.ContainsKey(sensorName))
                        {
                            // Dacă nu există, adaugă o nouă intrare în dicționar
                            sensorData[sensorName] = new List<string>();
                        }

                        // Adaugă valoarea la lista asociată cu numele senzorului
                        sensorData[sensorName].Add(sensorValue);
                       
                        WriteSensorDataToFile(destinatar, sensorName, sensorValue);
                        MesajPrimit?.Invoke(this, EventArgs.Empty);
                    }
                    
                }

                        

                    ResetUserTimer(destinatar);
                    try
                    {
                        
                    string receivedLogFilePath = Path.Combine(baseLogPath, $"{destinatar}_LogsMesajePrimite.txt");
                    string sentLogFilePath = Path.Combine(baseLogPath, $"{destinatar}_LogsMesajeTrimise.txt");

                        // Ensure the directory exists
                        string directoryPath = Path.GetDirectoryName(receivedLogFilePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        WriteTextToFile(receivedLogFilePath, $"Am primit mesajul '{mesaj}' Primit de la {destinatar}");
                        WriteTextToFile(sentLogFilePath, $"Am trimis mesajul '{raspunsping}' la {destinatar}");

                       

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occurred: {ex.Message}");
                    }
                
            };

            // Evenimentul OnError pentru a gestiona erorile
            client.OnError += (sender, e) =>
            {
                Debug.WriteLine($"Error: {e.Exception}");
                var result = MessageBox.Show("Eroare la conectarea serverului " + e.Exception, "Ieşire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            };

            // Evenimentul OnAuthError pentru a gestiona erorile de autentificare
            client.OnAuthError += (sender, e) =>
            {
                
                Debug.WriteLine("Eroare la autentificare: " + e.Failure);

                var result = MessageBox.Show("Eroare de autentificare, numele de utilizator sau parola sunt gresite! " + e.Exception, "Ieşire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            };

           

        }

        

        public Dictionary<string, List<string>> GetSenzorData()
        {
            return sensorData;
        }

        public List<User> GetActiveUsers()
        {
           
            return activeUsers;
        }
     
        public void ConnectToServer()
        {
           
            client.Open();
            
        }

        
        public void DisconnectFromServer()
        {
           
            client.Close();
        }

        public void SendMessage(string to, string messageBody)
        {
            if (client != null)
            {
                client.Send(new Matrix.Xmpp.Client.Message
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
                var result = MessageBox.Show("Eroare la scrierea în fişier: " + ex.Message, "Ieşire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
        private void WriteSensorDataToFile(string destinatar, string sensorName, string sensorValue)
        {
            try
            {                            
                string sensorLogFilePath = Path.Combine(baseLogPath, $"{destinatar}_{sensorName}.txt");               
                string directoryPath = Path.GetDirectoryName(sensorLogFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }          
                WriteTextToFile(sensorLogFilePath, $"Valoarea pentru {sensorName}: {sensorValue}");
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show("Eroare la scrierea în fişier: " + ex.Message, "Ieşire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
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
                System.Timers.Timer timer = new System.Timers.Timer(timeoutInterval);
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
                string baseLogPath = @"D:\Licenta\Licenta-USV\Licenta\Logs";
                string BasicLog = Path.Combine(baseLogPath, $"BasicLog.txt");
                user.IsActive = false;
                UsersUpdated?.Invoke(this, EventArgs.Empty);
                WriteTextToFile(BasicLog, $"Utilizatorul {username} a fost setat ca inactiv.");
            }
        }


    }
}

   



