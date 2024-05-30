using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
            server = new Server();
            server.UsersUpdated += OnUsersUpdated;
            server.ConnectToServer();
        }


        void UpdateUser()
        {
            Debug.WriteLine(server.Mesaj);
            List<User> activeUsers = server.GetActiveUsers();

            if (activeUsers != null)
            {
                foreach (User user in activeUsers)
                {
                    if (flowLayoutPanel1.InvokeRequired)
                    {
                        // Dacă nu suntem pe firul UI principal, apelăm UpdateUser din nou pe firul UI principal
                        flowLayoutPanel1.Invoke(new MethodInvoker(UpdateUser));
                        return;
                    }

                    // Creăm controalele GUI
                    GroupBox groupBox = new GroupBox();
                    groupBox.Name = "localhost";
                    PictureBox pictureBox = new PictureBox();
                    Panel radioPanel = new Panel();

                    pictureBox.Size = new Size(280, 150);
                    pictureBox.Image = Image.FromFile(@"D:\Licenta\Licenta-USV\Licenta\Assets\download.jpg");
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    RadioButton radioButton = new RadioButton
                    {
                        Name = user.Username,
                        Text = user.Username, // Adding the username as the radio button text
                        Size = new Size(100, 40), // Set the custom size (width, height)
                        AutoSize = false // Disable AutoSize to apply custom size
                    };

                    // Adăugăm RadioButton în Panel
                    radioPanel.Controls.Add(radioButton);

                    // Adăugăm controalele la GroupBox
                    groupBox.Controls.Add(radioButton);
                    groupBox.Controls.Add(pictureBox);

                    // Adăugăm GroupBox la FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(groupBox);
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
            GroupBox groupBox = new GroupBox();
                groupBox.Name = "localhost";
            PictureBox pictureBox = new PictureBox();
            Panel radioPanel = new Panel();

            
            pictureBox.Size = new Size(280, 150);
            pictureBox.Image = Image.FromFile(@"D:\Licenta\Licenta-USV\Licenta\Assets\download.jpg");
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
           
            RadioButton radioButton = new RadioButton
            {

                Name = "localhost",
                Text = "localhost", // Adding the username as the radio button text
                Size = new Size(100, 40), // Set the custom size (width, height)
               
                AutoSize = false,


            };// Disable AutoSize to apply custom size
            
            radioPanel.Controls.Add(radioButton);
            groupBox.Controls.Add(radioButton);
            groupBox.Controls.Add(pictureBox);
            flowLayoutPanel1.Controls.Add(groupBox);
            

        }
        

            private void OnUsersUpdated(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
