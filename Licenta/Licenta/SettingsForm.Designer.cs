namespace Licenta
{
    partial class SettingsForm
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
            this.PathLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ChangePath = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BackButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.PathLabel.Location = new System.Drawing.Point(72, 125);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(315, 22);
            this.PathLabel.TabIndex = 0;
            this.PathLabel.Text = "D:\\Licenta\\Licenta-USV\\Licenta\\Logs";
            this.PathLabel.Click += new System.EventHandler(this.Path_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MidnightBlue;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(416, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Calea de salvare a documentelor Excel:";
            // 
            // ChangePath
            // 
            this.ChangePath.BackColor = System.Drawing.Color.Silver;
            this.ChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePath.Location = new System.Drawing.Point(320, 165);
            this.ChangePath.Name = "ChangePath";
            this.ChangePath.Size = new System.Drawing.Size(80, 52);
            this.ChangePath.TabIndex = 2;
            this.ChangePath.Text = "Schimba Calea";
            this.ChangePath.UseVisualStyleBackColor = false;
            this.ChangePath.Click += new System.EventHandler(this.button1_Click);
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Silver;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(50, 166);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(80, 51);
            this.BackButton.TabIndex = 3;
            this.BackButton.Text = "Inapoi";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Licenta.Properties.Resources.IconFIsier__2_;
            this.pictureBox1.Location = new System.Drawing.Point(36, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Licenta.Properties.Resources.LogoUsv_removebg_preview;
            this.pictureBox2.Location = new System.Drawing.Point(112, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(220, 212);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(460, 231);
            this.Controls.Add(this.ChangePath);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ChangePath;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}