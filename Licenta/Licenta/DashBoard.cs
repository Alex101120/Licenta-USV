using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
                        flowLayoutPanel1.Invoke(new MethodInvoker(UpdateUser));
                        return;
                    }

                    // Create GUI controls
                    GroupBox groupBox = new GroupBox();
                    groupBox.Name = "localhost";
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Size = new Size(280, 150);
                    pictureBox.Image = Image.FromFile(@"D:\Licenta\Licenta-USV\Licenta\Assets\download.jpg");
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    RadioButton radioButton = new RadioButton
                    {
                        Name = user.Username,
                        Text = user.Username, // Adding the username as the radio button text
                        Size = new Size(100, 40), // Set the custom size (width, height)
                        AutoCheck = true,
                        AutoSize = false // Disable AutoSize to apply custom size
                    };

                    // Add CheckedChanged event handler
                    radioButton.CheckedChanged += RadioButton_CheckedChanged;

                    // Add controls to GroupBox
                    groupBox.Controls.Add(radioButton);
                    groupBox.Controls.Add(pictureBox);

                    flowLayoutPanel1.Controls.Add(groupBox);
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton radioButton) || !radioButton.Checked)
                return;

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is GroupBox groupBox)
                {
                    foreach (Control innerControl in groupBox.Controls)
                    {
                        if (innerControl is RadioButton rb && rb != radioButton)
                        {
                            rb.Checked = false;
                        }
                    }
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

            pictureBox.Size = new Size(280, 150);
            pictureBox.Image = Image.FromFile(@"D:\Licenta\Licenta-USV\Licenta\Assets\download.jpg");
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            RadioButton radioButton = new RadioButton
            {
                Name = "localhost",
                Text = "localhost", // Adding the username as the radio button text
                Size = new Size(100, 40), // Set the custom size (width, height)
                AutoSize = false,
            };

            // Add CheckedChanged event handler
            radioButton.CheckedChanged += RadioButton_CheckedChanged;

            // Add controls to GroupBox
            groupBox.Controls.Add(radioButton);
            groupBox.Controls.Add(pictureBox);

            flowLayoutPanel1.Controls.Add(groupBox);
        }

        private void OnUsersUpdated(object sender, EventArgs e)
        {
            UpdateUser();
        }

        void Refresh()
        {
            flowLayoutPanel1.Controls.Clear();
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
