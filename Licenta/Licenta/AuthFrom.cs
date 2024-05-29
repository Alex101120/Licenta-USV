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
        public AuthFrom()
        {
            InitializeComponent();
            textBox1.Text = "admin";
            textBox2.Text = "admin";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                this.Enabled = false;

                
                
                    

                    // Creează o instanță a clasei Dashboard
                    LoadingForm loadform = new LoadingForm();

                    // Afișează formularul Dashboard
                    loadform.Show();

                    // Ascundeți formularul curent
                    this.Hide();

              

                
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
