using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licenta
{
    public partial class Form1 : Form
    {
        Connect connect = new Connect();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            connect.ConnectToServer();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect.SendMessage("test1@localhost", "aaa");
            ///ejabberdctl create_room "room1" "conference.localhost" "localhost"
            // ejabberdctl send_message chat "admin@localhost" "test1@localhost" Restart "aa"
        }
    }
}
