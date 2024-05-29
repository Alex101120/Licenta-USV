using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licenta
{
    public partial class DashBoard : Form
    {
        Server server;
       

        public DashBoard()
        {
            InitializeComponent();
            

            flowLayoutPanel1.ControlAdded += flowLayoutPanel1_ControlAdded;
            flowLayoutPanel1.ControlAdded += flowLayoutPanel1_ControlRemoved;

            server = new Server();
           




        }
        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }
        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            
        }

        void UpdateUser()
        {
            Debug.WriteLine(server.Mesaj);
            List<User> activeUsers = server.GetActiveUsers();

            // Verificare dacă activeUsers nu este null
            if (activeUsers != null)
            {
                foreach (User user in activeUsers)
                {
                    // Aici puteți crea butonul pentru fiecare utilizator
                    Button button = new Button();
                    button.Name = user.Username; // Setăm conținutul butonului cu numele utilizatorului
                    flowLayoutPanel1.Controls.Add(button); // Adăugați orice alte proprietăți sau evenimente necesare pentru buton
                }
            }
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        public class User
        {
            public string Username { get; set; }
            public bool IsActive { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                    UpdateUser();
                
            
        }
    }
}
