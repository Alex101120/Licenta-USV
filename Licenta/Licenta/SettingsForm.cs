using Microsoft.Win32;
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
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Licenta
{

    public partial class SettingsForm : Form
    {

        private DashBoard _dashBoard;
        public event EventHandler PathSchimbat;
        public string BasePathLog { get; private set; }
        public string BasePathExcel { get; private set; }
        public SettingsForm()
        {
            InitializeComponent();
            Path.Text = @"D:\Licenta\Licenta-USV\Licenta\Logs";
        }

        private void Path_Click(object sender, EventArgs e)
        {

        }
        public SettingsForm(DashBoard dashBoard)
        {
            InitializeComponent();
            _dashBoard = dashBoard;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.Description = "Select a folder for logs";
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog1.ShowNewFolderButton = true;

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderBrowserDialog1.SelectedPath;
                    BasePathExcel = folderPath;
                    Path.Text = folderPath;
                    PathSchimbat?.Invoke(this, EventArgs.Empty);
                }
            }
        }

       

       
        public string GetBasePathExcel()
        {
            return BasePathExcel;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _dashBoard.Show();
            PathSchimbat?.Invoke(this, EventArgs.Empty);
        }
    }
}
