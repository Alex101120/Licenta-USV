namespace Licenta
{
    partial class LoadingForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.presenceManager1 = new Matrix.Xmpp.Client.PresenceManager();
            this.ConectareLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(79, 45);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(543, 39);
            this.progressBar1.TabIndex = 0;
            // 
            // presenceManager1
            // 
            this.presenceManager1.XmppClient = null;
            // 
            // ConectareLabel
            // 
            this.ConectareLabel.AutoSize = true;
            this.ConectareLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConectareLabel.Location = new System.Drawing.Point(251, 9);
            this.ConectareLabel.Name = "ConectareLabel";
            this.ConectareLabel.Size = new System.Drawing.Size(176, 33);
            this.ConectareLabel.TabIndex = 1;
            this.ConectareLabel.Text = "Conectare..";
            this.ConectareLabel.Click += new System.EventHandler(this.ConectareLabel_Click);
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 105);
            this.Controls.Add(this.ConectareLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "LoadingForm";
            this.Text = "Conectare..";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private Matrix.Xmpp.Client.PresenceManager presenceManager1;
        private System.Windows.Forms.Label ConectareLabel;
    }
}

