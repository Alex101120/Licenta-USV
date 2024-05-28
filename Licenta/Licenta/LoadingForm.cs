using System;
using System.Diagnostics;
using System.Windows.Forms;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;

namespace Licenta
{
    public partial class LoadingForm : Form
    {
        Connect connect = new Connect();
        Timer connectTimer;
        Timer statusTimer;

        public LoadingForm()
        {
            InitializeComponent();
            InitializeConnectTimer();
            InitializeStatusTimer();
        }

        private void InitializeConnectTimer()
        {
            connectTimer = new Timer();
            connectTimer.Interval = 2000; // 2 secunde
            connectTimer.Tick += new EventHandler(ConnectTimerEventProcessor);
            connectTimer.Start();
        }

        private void InitializeStatusTimer()
        {
            statusTimer = new Timer();
            statusTimer.Interval = 1000; // 1 secundă
            statusTimer.Tick += new EventHandler(StatusTimerEventProcessor);
            statusTimer.Start();
        }

        private void ConnectTimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            connectTimer.Stop();
            connectServer();
        }

        private void StatusTimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            UpdateConnectionStatus();
        }

        private void connectServer()
        {
            connect.ConnectToServer();
        }

        private void LoadForm()
        {
            // Dezactivează formularul curent
            this.Enabled = false;

            statusTimer.Stop();
            Timer loadTimer = new Timer();
            loadTimer.Interval = 2000; // 2 secunde
            loadTimer.Tick += (sender, e) =>
            {
                // Opriți timerul
                loadTimer.Stop();

                // Creează o instanță a clasei Dashboard
                DashBoard dashboard = new DashBoard();

                // Afișează formularul Dashboard
                dashboard.Show();

                // Ascundeți formularul curent
                this.Hide();
               
            };

            // Porniți Timer-ul
            loadTimer.Start();
        }
        private void UpdateConnectionStatus()
        {
            bool status = connect.IsConnected();
            if (status)
            {
                progressBar1.Value = 100;
                Connecting.Text = "Conectat";
                LoadForm();
               
            }
            else
            {
                progressBar1.Value = 10;
                Connecting.Text = "Conectare..";
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            // Implementarea pentru Form1_Load
        }

        private void Connecting_TextChanged(object sender, EventArgs e)
        {
            // Implementarea pentru Connecting_TextChanged
        }
    }
}
