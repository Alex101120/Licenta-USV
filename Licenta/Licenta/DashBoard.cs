using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Defaults;
using LiveCharts.WinForms;
using ClosedXML.Excel;
using System.IO;


namespace Licenta
{
    public partial class DashBoard : Form
    {
        Server server;
        private bool isDraggingChart = false;
        private Point chartStartPoint = Point.Empty;
        private SettingsForm _settingsForm;
        string defaultPath = @"D:\Licenta\Licenta-USV\Licenta\Logs";

        public DashBoard()
        {
            InitializeComponent();
            server = new Server();
            server.UsersUpdated += OnUsersUpdated;
            server.MesajPrimit += UpdateSenzorData;
            server.ConnectToServer();
            UpdateDateTime();
            this.FormClosing += new FormClosingEventHandler(Dashboard_FormClosing);
            _settingsForm = new SettingsForm(this);
            _settingsForm.PathSchimbat += PathSchimbat;
            LoadDefaultPath();





            comboBox1.Items.Add("SingleTestView");
            comboBox1.Items.Add("MultiTextView");
            comboBox1.Items.Add("ListView");
            comboBox1.Items.Add("ChartLine");
            comboBox1.Items.Add("AngularGaugeChart");

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
                        BackColor = Color.Transparent,
                        Size = new Size(100, 40),
                        AutoCheck = true,
                        AutoSize = false
                    };

                    radioButton.CheckedChanged += RadioButton_CheckedChanged;

                    Panel userPanel = new Panel
                    {
                        Name = $"{user.Username}_Panel",
                        Size = MainDashboard.Size,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Visible = false
                    };

                    // Label pentru starea utilizatorului (online/offline)
                    Label statusLabel = new Label
                    {
                        Name = $"{user.Username}_StatusLabel",
                        Text = user.IsActive ? "●Online" : "●Offline",
                        TextAlign = ContentAlignment.TopRight, // Afișează textul în partea de sus dreapta
                        Dock = DockStyle.None, // Setează DockStyle.None pentru a putea controla poziționarea manual
                        Location = new Point(userPanel.Width - 100, 5), // Poziționează label-ul în partea de sus dreapta
                        Size = new Size(90, 20), // Dimensiunea label-ului
                        Font = new Font(Font,FontStyle.Bold),
                        ForeColor = user.IsActive ? Color.Green : Color.Red,
                        BackColor = Color.Transparent
                    };

                    // Populate the panel with controls as needed
                    Label userLabel = new Label
                    {
                        Text = $"Panou de Comanda pentru {user.Username}",
                        TextAlign = ContentAlignment.TopCenter,
                        Size = new Size(90, 20), // Dimensiunea label-ului
                        Font = new Font(Font, FontStyle.Bold),
                        AutoSize = true
                    };
                    userPanel.Controls.Add(userLabel);

                    groupBox.Controls.Add(radioButton);
                    groupBox.Controls.Add(pictureBox);

                    // Adăugare label pentru starea utilizatorului în panoul utilizatorului
                    userPanel.Controls.Add(statusLabel);

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

        void UpdateSenzorData()
        {
            Dictionary<string, List<string>> sensorData = server.GetSenzorData();
            List<User> activeUsers = server.GetActiveUsers();

            // Scriem datele în fișierele Excel și actualizăm combobox-ul
            WriteDataToExcel(sensorData, defaultPath, activeUsers);

            foreach (var sensorName in sensorData.Keys)
            {
                // Verifică dacă numele senzorului există deja în comboBox2
                if (!comboBox2.Items.Contains(sensorName))
                {
                    comboBox2.Items.Add(sensorName);
                }
            }
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        public class User
        {
            public string Username { get; set; }
            public bool IsActive { get; set; }
        }

       

        private void OnUsersUpdated(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void UpdateSenzorData(object sender, EventArgs e)
        {
            UpdateSingleLabel();
            UpdateAllCharts();
            UpdateMultiTextLabels();
            UpdateListViews();
            UpdateSenzorData();
        }

      
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string selectedText1 = comboBox1.SelectedItem.ToString();
                string selectedText2 = comboBox2.SelectedItem.ToString();
                if (selectedText1 == "SingleTestView")
                {
                   
                    SingleTextLabel(selectedText2);
                }
                if(selectedText1 == "MultiTextView")
                {
                    MultiTextLabel(selectedText2);
                }
                if(selectedText1 == "ListView")
                {
                    
                    ListViewCreate(selectedText2);
                }
                if (selectedText1 == "ChartLine")
                {
                    
                    LineChart(selectedText2);
                }
                if(selectedText1 == "AngularGaugeChart")
                {
                    AngularGaugeChart(selectedText2);
                }
                
            }
            else
            {
                var result = MessageBox.Show("Toate optiunile trebuie completate! " , "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DataAndTIme_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            // Set the text of the label to the current date and time
            DataTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            Time.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        private void SingleTextLabel(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    Label sensorLabel = currentPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == $"{sensorName}_SingleTextLabel");
                    if (sensorLabel == null)
                    {
                        sensorLabel = new Label
                        {
                            Name = $"{sensorName}_SingleTextLabel", // Unique name for identification
                            Text = $" {sensorName}: {sensorData[sensorName].Last()}",
                            AutoSize = true,
                            BackColor = Color.Transparent,
                            Location = new Point(10, 10), // Default location; can be adjusted as needed
                            Size = new Size(90, 20),
                            Font = new Font(Font, FontStyle.Bold),
                        };

                        // Add mouse event handlers for moving the label
                        sensorLabel.MouseDown += SensorLabel_MouseDown;
                        sensorLabel.MouseMove += SensorLabel_MouseMove;
                        sensorLabel.MouseUp += SensorLabel_MouseUp;
                        sensorLabel.MouseClick += SensorLabel_MouseClick;

                        currentPanel.Controls.Add(sensorLabel);
                    }
                    else
                    {
                        sensorLabel.Text = $" {sensorName}: {sensorData[sensorName].Last()}";
                    }
                }
            }
            else
            {
                var result = MessageBox.Show("Selecteaza un tabel ", "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }
            }
        }

        private void MultiTextLabel(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    List<string> recentData = sensorData[sensorName].Skip(Math.Max(0, sensorData[sensorName].Count - 5)).ToList();
                    recentData.Reverse();
                    string labelText = $"{sensorName}: {string.Join(", ", recentData)}";

                    Label sensorLabel = currentPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == $"{sensorName}_MultiTextLabel");
                    if (sensorLabel == null)
                    {
                        sensorLabel = new Label
                        {
                            Name = $"{sensorName}_MultiTextLabel", // Unique name for identification
                            Text = labelText,
                            AutoSize = true,
                            BackColor = Color.Transparent,
                            Location = new Point(10, 10),
                            Size = new Size(90, 20),
                            Font = new Font(Font, FontStyle.Bold),
                        };

                        sensorLabel.MouseDown += SensorLabel_MouseDown;
                        sensorLabel.MouseMove += SensorLabel_MouseMove;
                        sensorLabel.MouseUp += SensorLabel_MouseUp;
                        sensorLabel.MouseClick += SensorLabel_MouseClick;

                        currentPanel.Controls.Add(sensorLabel);
                    }
                    else
                    {
                        sensorLabel.Text = labelText;
                    }
                }
            }
            else
            {
                var result = MessageBox.Show("Selecteaza un tabel ", "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }
            }

        }


        private void ListViewCreate(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    ListView listView = currentPanel.Controls.OfType<ListView>().FirstOrDefault(lv => lv.Name == "CombinedSensorListView");

                    if (listView == null)
                    {
                        // Create a new ListView if it doesn't exist
                        listView = new ListView
                        {
                            Name = "CombinedSensorListView",
                            Size = new Size(400, 200), // Adjusted size for better visibility
                            Location = new Point(10, 10),
                            View = View.Details,
                            FullRowSelect = true,
                            GridLines = true
                        };

                        // Add columns for sensor name and value
                        listView.Columns.Add("Senzor", 150); // Column for sensor name
                        listView.Columns.Add("Data", 200); // Column for data

                        listView.MouseDown += ListView_MouseDown;
                        listView.MouseMove += ListView_MouseMove;
                        listView.MouseUp += ListView_MouseUp;
                        listView.MouseClick += ListView_MouseClick;

                        currentPanel.Controls.Add(listView);
                    }

                    // Add or update rows in the ListView
                    foreach (var data in sensorData[sensorName])
                    {
                        bool itemExists = false;
                        foreach (ListViewItem item in listView.Items)
                        {
                            if (item.Text == sensorName)
                            {
                                item.SubItems.Add(data); // Add data under existing sensor column
                                itemExists = true;
                                break;
                            }
                        }

                        if (!itemExists)
                        {
                            // Add new item with sensor name and data
                            ListViewItem newItem = new ListViewItem(sensorName);
                            newItem.SubItems.Add(data);
                            listView.Items.Add(newItem);
                        }
                    }
                }
            }
            else
            {
                var result = MessageBox.Show("Selecteaza un tabel ", "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }
            }
        }







        private void LineChart(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    LiveCharts.WinForms.CartesianChart chart = new LiveCharts.WinForms.CartesianChart
                    {
                        Size = new Size(200, 200),
                        Name = $"{sensorName}_Chart",
                        Location = new Point(333, 393),
                       
                        
                        
                    };

                    LiveCharts.Wpf.LineSeries lineSeries = new LiveCharts.Wpf.LineSeries
                    {
                        Title = sensorName,
                        Values = new LiveCharts.ChartValues<double>(sensorData[sensorName].Select(double.Parse).ToList()),
                        PointGeometry = null
                    };

                    chart.Series = new LiveCharts.SeriesCollection { lineSeries };

                    chart.AxisX.Add(new LiveCharts.Wpf.Axis
                    {
                        Title = "Time",
                        Labels = sensorData[sensorName].Select((s, index) => index.ToString()).ToArray()
                    });

                    chart.AxisY.Add(new LiveCharts.Wpf.Axis
                    {
                        Title = "Value"
                    });

                    chart.MouseDown += Chart_MouseDown;
                    chart.MouseMove += Chart_MouseMove;
                    chart.MouseUp += Chart_MouseUp;
                    chart.MouseClick += Chart_MouseClick;
                   
                    var existingChart = currentPanel.Controls.OfType<LiveCharts.WinForms.CartesianChart>().FirstOrDefault();
                    if (existingChart != null)
                    {
                        currentPanel.Controls.Remove(existingChart);
                    }

                    currentPanel.Controls.Add(chart);
                }
            }
            else
            {
                var result = MessageBox.Show("Selecteaza un tabel ", "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }
            }
        }

        private void AngularGaugeChart(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    LiveCharts.WinForms.AngularGauge angularGauge = new LiveCharts.WinForms.AngularGauge
                    {
                        Size = new Size(200, 200),
                        Name = $"{sensorName}_AngularGauge",
                        Value = double.Parse(sensorData[sensorName].Last()),
                        FromValue = 0, // Set the starting value of the gauge
                        ToValue = 100, // Set the maximum value of the gauge
                        TicksForeground = System.Windows.Media.Brushes.Gray,
                        Location = new Point(0, 333),
                        BackColor = Color.White,
                       
                    };

                    angularGauge.MouseDown += Chart_MouseDown;
                    angularGauge.MouseMove += Chart_MouseMove;
                    angularGauge.MouseUp += Chart_MouseUp;
                    angularGauge.MouseClick += AngularGauge_MouseClick;

                    var existingGauge = currentPanel.Controls.OfType<LiveCharts.WinForms.AngularGauge>().FirstOrDefault();
                    if (existingGauge != null)
                    {
                        currentPanel.Controls.Remove(existingGauge);
                    }

                    currentPanel.Controls.Add(angularGauge);
                }
            }
            else
            {
                var result = MessageBox.Show("Selecteaza un tabel ", "Iesire", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {

                }
            }
        }

        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingChart = true;
            startPoint = new Point(e.X, e.Y);
            Console.WriteLine("Se Incearca Mutarea");
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                LiveCharts.WinForms.PieChart chart = sender as LiveCharts.WinForms.PieChart;
                chart.Location = new Point(chart.Location.X + e.X - startPoint.X, chart.Location.Y + e.Y - startPoint.Y);
            }
        }

        private void Chart_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingChart = false;
        }

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        private void SensorLabel_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }


        private void SensorLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Label lbl = sender as Label;
                lbl.Location = new Point(lbl.Location.X + e.X - startPoint.X, lbl.Location.Y + e.Y - startPoint.Y);
            }
        }

        private void SensorLabel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        private void UpdateSingleLabel()
        {
            Dictionary<string, List<string>> sensorData = server.GetSenzorData();

            foreach (Control control in MainDashboard.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control innerControl in panel.Controls)
                    {
                        if (innerControl is Label lbl)
                        {
                            string sensorName = lbl.Name.Split('_')[0];
                            if (sensorData.ContainsKey(sensorName))
                            {
                                lbl.Text = $" {sensorName}: {sensorData[sensorName].Last()}";
                            }
                        }
                    }
                }
            }
        }

        private void UpdateAllCharts()
        {
            foreach (Panel panel in MainDashboard.Controls.OfType<Panel>())
            {
                // Verificăm și actualizăm CartesianChart
                CartesianChart cartesianChart = panel.Controls.OfType<CartesianChart>().FirstOrDefault();
                if (cartesianChart != null)
                {
                    string sensorName = cartesianChart.Name.Replace("_Chart", "");
                    Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                    if (sensorData.ContainsKey(sensorName))
                    {
                        var lineSeries = cartesianChart.Series[0] as LiveCharts.Wpf.LineSeries;
                        if (lineSeries != null)
                        {
                            lineSeries.Values.Clear();
                            lineSeries.Values.AddRange(sensorData[sensorName].Select(value => (object)double.Parse(value)).ToList());
                        }
                    }
                }

                // Verificăm și actualizăm AngularGauge
                LiveCharts.WinForms.AngularGauge angularGauge = panel.Controls.OfType<LiveCharts.WinForms.AngularGauge>().FirstOrDefault();
                if (angularGauge != null)
                {
                    string sensorName = angularGauge.Name.Replace("_AngularGauge", "");
                    Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                    if (sensorData.ContainsKey(sensorName))
                    {
                        angularGauge.Value = double.Parse(sensorData[sensorName].Last());
                    }
                }
            }
        }
        private void UpdateMultiTextLabels()
        {
            Dictionary<string, List<string>> sensorData = server.GetSenzorData();

            foreach (Panel panel in MainDashboard.Controls.OfType<Panel>())
            {
                foreach (Control control in panel.Controls.OfType<Label>())
                {
                    string[] parts = control.Name.Split('_');
                    if (parts.Length == 2 && parts[1] == "MultiTextLabel")
                    {
                        string sensorName = parts[0];
                        if (sensorData.ContainsKey(sensorName))
                        {
                            List<string> recentData = sensorData[sensorName].Skip(Math.Max(0, sensorData[sensorName].Count - 5)).ToList();
                            recentData.Reverse();
                            control.Text = $"{sensorName}: {string.Join(", ", recentData)}";
                        }
                    }
                }
            }
        }


        private void UpdateListViews()
        {
            Dictionary<string, List<string>> sensorData = server.GetSenzorData();

            foreach (Panel panel in MainDashboard.Controls.OfType<Panel>())
            {
                foreach (ListView listView in panel.Controls.OfType<ListView>())
                {
                    string sensorName = listView.Columns[0].Text.Replace(" Senzor", ""); // Adjusted to match "Senzor" column

                    if (sensorData.ContainsKey(sensorName))
                    {
                        HashSet<string> existingItems = new HashSet<string>();

                        foreach (ListViewItem item in listView.Items)
                        {
                            existingItems.Add(item.SubItems[1].Text); // Store existing data values
                        }

                        // Add new items from sensorData that are not already in the ListView
                        foreach (var data in sensorData[sensorName])
                        {
                            if (!existingItems.Contains(data))
                            {
                                ListViewItem newItem = new ListViewItem(sensorName);
                                newItem.SubItems.Add(data);
                                listView.Items.Add(newItem);
                            }
                        }
                    }
                }
            }
        }





        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                ListView listView = sender as ListView;
                listView.Location = new Point(listView.Location.X + e.X - startPoint.X, listView.Location.Y + e.Y - startPoint.Y);
            }
        }

        private void ListView_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
            {
                ListView listView = sender as ListView;
                Panel parentPanel = listView.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Remove(listView);
                }
            }
        }

        private void AngularGauge_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
            {
                LiveCharts.WinForms.AngularGauge gauge = sender as LiveCharts.WinForms.AngularGauge;
                Panel parentPanel = gauge.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Remove(gauge);
                }
            }
        }
        private void Chart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
            {
                LiveCharts.WinForms.CartesianChart chart = sender as LiveCharts.WinForms.CartesianChart;
                Panel parentPanel = chart.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Remove(chart);
                }
            }
        }
        private void SensorLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
            {
                Label lbl = sender as Label;
                Panel parentPanel = lbl.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Remove(lbl);
                }
            }
        }
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Asigură-te că vrei să închizi aplicația

            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Anulează închiderea formularului
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            server.DisconnectFromServer();
            Application.Exit(); // Închide întreaga aplicație
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _settingsForm.Show();
        }
        private void LoadDefaultPath()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string configFilePath = Path.Combine(projectDirectory, "Conf.txt");

            // Verificăm dacă fișierul de configurare există
            if (File.Exists(configFilePath))
            {
                // Citim path-ul din fișierul de configurare
                defaultPath = File.ReadAllText(configFilePath);
            }
            else
            {
                // Dacă fișierul de configurare nu există, setăm un path implicit
                defaultPath = @"D:\Licenta\Licenta-USV\Licenta\Logs";
            }
        }

        private void PathSchimbat(object sender, EventArgs e)
        {
            if (_settingsForm.GetBasePathExcel() != null)
            {
                defaultPath = _settingsForm.GetBasePathExcel();
                Debug.WriteLine("AmSchimbatPath");

                // Actualizăm fișierul de configurare
                SalvarePathConfig(defaultPath);
            }
        }


       public void WriteDataToExcel(Dictionary<string, List<string>> sensorData, string defaultPath, List<User> activeUsers)
{
    // Verificăm dacă folderul defaultPath există, altfel îl creăm
    if (!Directory.Exists(defaultPath))
    {
        Directory.CreateDirectory(defaultPath);
    }

    // Parcurgem fiecare utilizator activ pentru a crea sau actualiza fișierele Excel
    foreach (var user in activeUsers)
    {
        string userName = user.Username;
        string fileName = Path.Combine(defaultPath, $"{userName}.xlsx");

        // Inițializăm workbook cu o valoare implicită pentru a evita eroarea CS0165
        XLWorkbook workbook = null;

        try
        {
            if (File.Exists(fileName))
            {
                // Dacă fișierul există, îl încărcăm pentru a adăuga date noi la sfârșitul sheet-ului existent
                workbook = new XLWorkbook(fileName);
            }
            else
            {
                // Dacă fișierul nu există, creăm un workbook nou
                workbook = new XLWorkbook();
            }

            // Iterăm prin fiecare sensor și adăugăm datele în sheet-ul corespunzător
            foreach (var sensor in sensorData)
            {
                // Verificăm dacă există deja un sheet cu numele senzorului
                IXLWorksheet sensorWorksheet;
                if (workbook.TryGetWorksheet(sensor.Key, out sensorWorksheet))
                {
                    // Dacă sheet-ul există, adăugăm datele la sfârșitul acestuia
                    int lastRow = sensorWorksheet.LastRowUsed().RowNumber();
                    int rowIndex = lastRow + 1;

                    foreach (var value in sensor.Value)
                    {
                        sensorWorksheet.Cell(rowIndex, 1).Value = value;
                        sensorWorksheet.Cell(rowIndex, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        rowIndex++;
                    }
                }
                else
                {
                    // Dacă sheet-ul nu există, îl creăm și adăugăm datele
                    sensorWorksheet = workbook.Worksheets.Add(sensor.Key);

                    sensorWorksheet.Cell(1, 1).Value = "Date Senzor";
                    sensorWorksheet.Cell(1, 2).Value = "Data de Transmitere";

                    int rowIndex = 2;
                    foreach (var value in sensor.Value)
                    {
                        sensorWorksheet.Cell(rowIndex, 1).Value = value;
                        sensorWorksheet.Cell(rowIndex, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        rowIndex++;
                    }
                }
            }

            // Salvăm workbook-ul
            workbook.SaveAs(fileName);
        }
        catch (IOException ex)
        {
            // Aici gestionăm cazul în care fișierul Excel este deja deschis
            MessageBox.Show($"Fișierul Excel '{Path.GetFileName(fileName)}' este deja deschis. Vă rugăm să-l închideți și apoi să încercați din nou.", "Fișier deschis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // În acest punct, poți decide să iei alte acțiuni, cum ar fi să întrerupi procesul sau să returnezi o valoare specială
        }
        catch (Exception ex)
        {
            // Aici gestionăm alte tipuri de excepții dacă este cazul
            MessageBox.Show($"Eroare la deschiderea sau salvarea fișierului Excel: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // În acest punct, poți decide să iei alte acțiuni, cum ar fi să întrerupi procesul sau să returnezi o valoare specială
        }
    }
}


        private void SalvarePathConfig(string path)
        {
            try
            {
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                string configFilePath = Path.Combine(projectDirectory, "Conf.txt");

                // Salvăm path-ul în fișierul de configurare
                File.WriteAllText(configFilePath, path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Eroare la salvarea fișierului de configurare: " + ex.Message);
                // Poți adăuga aici gestionarea erorilor cum consideri necesar
            }
        }

        private void OpenExcel_Click(object sender, EventArgs e)
        {
            // Inițializăm dialogul de deschidere a fișierului
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Setăm filtrul pentru tipurile de fișiere acceptate (doar fișiere Excel)
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.InitialDirectory = defaultPath;

            // Afisăm dialogul și verificăm dacă utilizatorul a selectat un fișier
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Verificăm dacă fișierul există
                if (File.Exists(filePath))
                {
                    try
                    {
                        // Deschidem fișierul Excel folosind ClosedXML
                        XLWorkbook workbook = new XLWorkbook(filePath);

                        // Poți face diverse operații cu workbook-ul aici

                        // Salvăm workbook-ul (dacă dorești să faci modificări)
                        workbook.Save();

                        // Eliberăm resursele
                        workbook.Dispose();
                        Process.Start(filePath);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la deschiderea fișierului Excel: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Fișierul '{filePath}' nu există.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
       
    }
}
