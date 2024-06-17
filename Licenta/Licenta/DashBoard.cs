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
                if (flowLayoutPanel1.InvokeRequired)
                {
                    flowLayoutPanel1.Invoke(new MethodInvoker(UpdateUser));
                    return;
                }

                flowLayoutPanel1.Controls.Clear();
                MainDashboard.Controls.Clear(); // Clear the MainDashboard panel first

                foreach (User user in activeUsers)
                {
                    if (flowLayoutPanel1.InvokeRequired)
                    {
                        flowLayoutPanel1.Invoke(new MethodInvoker(UpdateUser));
                        return;
                    }

                    GroupBox groupBox = new GroupBox();
                    groupBox.Name = "localhost";
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Size = new Size(280, 150);
                    pictureBox.Image = Image.FromFile(@"D:\Licenta\Licenta-USV\Licenta\Assets\download.jpg");
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    RadioButton radioButton = new RadioButton
                    {
                        Name = user.Username,
                        Text = user.Username,
                        Size = new Size(100, 40),
                        AutoCheck = true,
                        AutoSize = false
                    };

                    radioButton.CheckedChanged += RadioButton_CheckedChanged;

                    Panel userPanel = new Panel
                    {
                        Name = $"{user.Username}_Panel",
                        Size = MainDashboard.Size,
                        BackColor = Color.Blue,
                        Visible = false
                    };

                    // Populate the panel with controls as needed
                    Label userLabel = new Label
                    {
                        Text = $"User Info for {user.Username}",
                        AutoSize = true
                    };
                    userPanel.Controls.Add(userLabel);

                    groupBox.Controls.Add(radioButton);
                    groupBox.Controls.Add(pictureBox);

                    // Add the userPanel to the MainDashboard instead of the groupBox
                    MainDashboard.Controls.Add(userPanel);

                    flowLayoutPanel1.Controls.Add(groupBox);
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton radioButton) || !radioButton.Checked)
                return;

            // Hide all panels and uncheck all radio buttons
            foreach (Control control in MainDashboard.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Visible = false;
                }
            }

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

            // Show the selected panel
            Panel selectedPanel = MainDashboard.Controls[$"{radioButton.Name}_Panel"] as Panel;
            if (selectedPanel != null)
            {
                selectedPanel.Visible = true;
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
                Text = "localhost",
                Size = new Size(100, 40), 
                AutoSize = false,
            };

          
            radioButton.CheckedChanged += RadioButton_CheckedChanged;

          
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

        

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
