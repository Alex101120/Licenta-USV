﻿namespace Licenta
{
    partial class DashBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Matrix.IO.Cache.MemoryStorage<Matrix.Xmpp.Disco.Info> memoryStorage_11 = new Matrix.IO.Cache.MemoryStorage<Matrix.Xmpp.Disco.Info>();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Adauga = new System.Windows.Forms.Button();
            this.discoManager1 = new Matrix.Xmpp.Client.DiscoManager();
            this.MainDashboard = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataTime = new System.Windows.Forms.Label();
            this.DataAndTIme = new System.Windows.Forms.Timer(this.components);
            this.Time = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenExcel = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.DeschideExcelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(217, 525);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Adauga
            // 
            this.Adauga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Adauga.Location = new System.Drawing.Point(864, 483);
            this.Adauga.Name = "Adauga";
            this.Adauga.Size = new System.Drawing.Size(112, 33);
            this.Adauga.TabIndex = 0;
            this.Adauga.Text = "Adauga";
            this.Adauga.UseVisualStyleBackColor = true;
            this.Adauga.Click += new System.EventHandler(this.button1_Click);
            // 
            // discoManager1
            // 
            this.discoManager1.AutoDiscover = false;
            this.discoManager1.AutoReplyToDiscoInfo = false;
            this.discoManager1.AutoSendCaps = false;
            this.discoManager1.CapsStorage = memoryStorage_11;
            this.discoManager1.Node = null;
            this.discoManager1.XmppClient = null;
            // 
            // MainDashboard
            // 
            this.MainDashboard.Location = new System.Drawing.Point(223, 0);
            this.MainDashboard.Name = "MainDashboard";
            this.MainDashboard.Size = new System.Drawing.Size(578, 525);
            this.MainDashboard.TabIndex = 1;
            this.MainDashboard.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(813, 456);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(183, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(813, 409);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(183, 21);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(844, 386);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "NumeSenzor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(863, 433);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "TipData";
            // 
            // DataTime
            // 
            this.DataTime.AutoSize = true;
            this.DataTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataTime.Location = new System.Drawing.Point(803, 9);
            this.DataTime.Name = "DataTime";
            this.DataTime.Size = new System.Drawing.Size(48, 20);
            this.DataTime.TabIndex = 5;
            this.DataTime.Text = "Data";
            this.DataTime.Click += new System.EventHandler(this.label3_Click);
            // 
            // DataAndTIme
            // 
            this.DataAndTIme.Interval = 1000;
            this.DataAndTIme.Tick += new System.EventHandler(this.DataAndTIme_Tick);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.Location = new System.Drawing.Point(803, 29);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(47, 20);
            this.Time.TabIndex = 6;
            this.Time.Text = "Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(941, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Setari";
            // 
            // OpenExcel
            // 
            this.OpenExcel.BackgroundImage = global::Licenta.Properties.Resources.download__2_;
            this.OpenExcel.Location = new System.Drawing.Point(834, 329);
            this.OpenExcel.Name = "OpenExcel";
            this.OpenExcel.Size = new System.Drawing.Size(52, 50);
            this.OpenExcel.TabIndex = 10;
            this.OpenExcel.UseVisualStyleBackColor = true;
            this.OpenExcel.Click += new System.EventHandler(this.OpenExcel_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.BackgroundImage = global::Licenta.Properties.Resources.download__1___4_;
            this.SettingsButton.Location = new System.Drawing.Point(945, 329);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(51, 50);
            this.SettingsButton.TabIndex = 8;
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // DeschideExcelLabel
            // 
            this.DeschideExcelLabel.AutoSize = true;
            this.DeschideExcelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeschideExcelLabel.Location = new System.Drawing.Point(809, 302);
            this.DeschideExcelLabel.Name = "DeschideExcelLabel";
            this.DeschideExcelLabel.Size = new System.Drawing.Size(98, 24);
            this.DeschideExcelLabel.TabIndex = 11;
            this.DeschideExcelLabel.Text = "Deschide";
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 528);
            this.Controls.Add(this.DeschideExcelLabel);
            this.Controls.Add(this.OpenExcel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.DataTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.MainDashboard);
            this.Controls.Add(this.Adauga);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DashBoard";
            this.Text = "DashBoard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Adauga;
        private Matrix.Xmpp.Client.DiscoManager discoManager1;
        private System.Windows.Forms.Panel MainDashboard;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DataTime;
        private System.Windows.Forms.Timer DataAndTIme;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OpenExcel;
        private System.Windows.Forms.Label DeschideExcelLabel;
    }
}