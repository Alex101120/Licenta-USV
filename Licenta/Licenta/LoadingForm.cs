using System;
using System.Diagnostics;
using System.Windows.Forms;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;

namespace Licenta
{
    public partial class LoadingForm : Form
    {
        Server server = new Server();
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
            server.ConnectToServer();
        }

        private void LoadForm()
        {
     
            this.Enabled = false;

            statusTimer.Stop();
            Timer loadTimer = new Timer();
            loadTimer.Interval = 2000; // 2 secunde
            loadTimer.Tick += (sender, e) =>
            {
              
                loadTimer.Stop();

              
                DashBoard dashboard = new DashBoard();

             
                dashboard.Show();

               
                this.Hide();
               
            };

          
            loadTimer.Start();
        }
        private void UpdateConnectionStatus()
        {
            bool status = server.IsConnected();
            if (status)
            {
                progressBar1.Value = 100;
                ConectareLabel.Text = "Conectare reusita";
                LoadForm();
               
            }
            else
            {
                progressBar1.Value = 10;
                ConectareLabel.Text = "Conectare..";
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Connecting_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void ConectareLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
