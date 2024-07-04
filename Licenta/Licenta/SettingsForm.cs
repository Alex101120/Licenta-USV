using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
          
           
        }

        private void Path_Click(object sender, EventArgs e)
        {

        }
        public SettingsForm(DashBoard dashBoard)
        {
            InitializeComponent();
            LoadDefaultPath();
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
                    PathLabel.Text = folderPath;
                    PathSchimbat?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        private void LoadDefaultPath()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string configFilePath = Path.Combine(projectDirectory, "Conf.txt");

            // Verificăm dacă fișierul de configurare există
            if (File.Exists(configFilePath))
            {
                // Citim path-ul din fișierul de configurare
                Debug.WriteLine("Este");
                BasePathExcel = File.ReadAllText(configFilePath);
                PathLabel.Text = BasePathExcel;
            }
            else
            {
                // Dacă fișierul de configurare nu există, setăm un path implicit
                Debug.WriteLine("Nu este");
                BasePathExcel = @"D:\Licenta\Licenta-USV\Licenta\Logs";
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
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
