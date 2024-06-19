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
            Matrix.IO.Cache.MemoryStorage<Matrix.Xmpp.Disco.Info> memoryStorage_11 = new Matrix.IO.Cache.MemoryStorage<Matrix.Xmpp.Disco.Info>();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.WidgetAdd = new System.Windows.Forms.Button();
            this.discoManager1 = new Matrix.Xmpp.Client.DiscoManager();
            this.MainDashboard = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(217, 448);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // WidgetAdd
            // 
            this.WidgetAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WidgetAdd.Location = new System.Drawing.Point(807, 353);
            this.WidgetAdd.Name = "WidgetAdd";
            this.WidgetAdd.Size = new System.Drawing.Size(114, 95);
            this.WidgetAdd.TabIndex = 0;
            this.WidgetAdd.Text = "+";
            this.WidgetAdd.UseVisualStyleBackColor = true;
            this.WidgetAdd.Click += new System.EventHandler(this.button1_Click);
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
            this.MainDashboard.Size = new System.Drawing.Size(578, 448);
            this.MainDashboard.TabIndex = 1;
            this.MainDashboard.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 450);
            this.Controls.Add(this.MainDashboard);
            this.Controls.Add(this.WidgetAdd);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DashBoard";
            this.Text = "DashBoard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button WidgetAdd;
        private Matrix.Xmpp.Client.DiscoManager discoManager1;
        private System.Windows.Forms.Panel MainDashboard;
    }
}