using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Licenta
{
    public partial class AuthFrom : Form
    {
        public string username ="admin";
        public string password ="admin";
        public AuthFrom()
        {
            InitializeComponent();
            textBox1.Text = username;
            textBox2.Text = password;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

            username = textBox1.Text;
            password = textBox2.Text;





            // Creează o instanță a clasei Dashboard
            LoadingForm loadform = new LoadingForm();

            // Afișează formularul Dashboard
            loadform.Show();


            this.Hide();




        }

        public string GetUsername()
        {
            return username;
        }
        public string GetPassword()
        {
            return password;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
