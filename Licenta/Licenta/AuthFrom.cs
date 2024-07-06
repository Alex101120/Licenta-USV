using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Licenta.Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Licenta
{
    public partial class AuthFrom : Form
    {

        public string username;
        public string password;
        public event EventHandler OnLogin;


        public AuthFrom()
        {
            InitializeComponent();

            username = "admin";
            password = "admin";
            CasutaNumeUtilizator.Text = username; 
            CasutaParola.Text = password;

            OnLogin += UpdateCred;
            LoginButton.TabStop = false;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.FlatAppearance.BorderSize = 0;
            LoginButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);



        }

        private void UpdateCred(object sender, EventArgs e)
        {
           
            username = CasutaNumeUtilizator.Text;
            password = CasutaParola.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = CasutaNumeUtilizator.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = CasutaParola.Text;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (CasutaNumeUtilizator.Text == "admin" && CasutaParola.Text == "admin")
            {
                OnLogin?.Invoke(this, EventArgs.Empty);
                LoadingForm loadform = new LoadingForm();
                loadform.Show();
                this.Hide();
            }
            else
            {
                var result = MessageBox.Show("Eroare de autentificare, numele de utilizator sau parola sunt gresite! ", "Ieşire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }


            }
        }
      

    public string GetUsername()
        {   
            Debug.WriteLine("Nume Utilizator " + username);
            return username;
        }
        public string GetPassword()
        {
            Debug.WriteLine("Parola " + password);
            return password;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
