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

namespace Licenta
{
    public partial class DashBoard : Form
    {
        Server server;
        private bool isDraggingChart = false;
        private Point chartStartPoint = Point.Empty;

        public DashBoard()
        {
            InitializeComponent();
            server = new Server();
            server.UsersUpdated += OnUsersUpdated;
            server.MesajPrimit += UpdateSenzorData;
            server.ConnectToServer();
            UpdateDateTime();



            comboBox1.Items.Add("Text UltimaDataPrimita");
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
            UpdateSenzorData();
            UpdateLabels();
            UpdateAllCharts();
        }

        void Refresh()
        {
            flowLayoutPanel1.Controls.Clear();
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
                if (selectedText1 == "Text UltimaDataPrimita")
                {
                   
                    TextLabel(selectedText2);
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

        private void TextLabel(string sensorName)
        {
            Panel currentPanel = MainDashboard.Controls.OfType<Panel>().FirstOrDefault(panel => panel.Visible);

            if (currentPanel != null)
            {
                Dictionary<string, List<string>> sensorData = server.GetSenzorData();
                if (sensorData.ContainsKey(sensorName))
                {
                    Label sensorLabel = new Label
                    {
                        Text = $" {sensorName}: {sensorData[sensorName].Last()}",
                        AutoSize = true,
                        BackColor = Color.Transparent,
                        Location = new Point(10, 10), // Default location; can be adjusted as needed
                        Size = new Size(90, 20), // Dimensiunea label-ului
                        Font = new Font(Font, FontStyle.Bold),
                    };

                    // Add mouse event handlers for moving the label
                    sensorLabel.MouseDown += SensorLabel_MouseDown;
                    sensorLabel.MouseMove += SensorLabel_MouseMove;
                    sensorLabel.MouseUp += SensorLabel_MouseUp;

                    currentPanel.Controls.Add(sensorLabel);
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
                        Location = new Point(333, 333),
                        
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

                    var existingChart = currentPanel.Controls.OfType<LiveCharts.WinForms.CartesianChart>().FirstOrDefault();
                    if (existingChart != null)
                    {
                        currentPanel.Controls.Remove(existingChart);
                    }

                    currentPanel.Controls.Add(chart);
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

                    var existingGauge = currentPanel.Controls.OfType<LiveCharts.WinForms.AngularGauge>().FirstOrDefault();
                    if (existingGauge != null)
                    {
                        currentPanel.Controls.Remove(existingGauge);
                    }

                    currentPanel.Controls.Add(angularGauge);
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
        private void UpdateLabels()
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
                            string sensorName = lbl.Text.Split(':')[0];
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
