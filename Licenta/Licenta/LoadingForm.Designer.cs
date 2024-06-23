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
            this.Connecting = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(119, 301);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(543, 39);
            this.progressBar1.TabIndex = 0;
            // 
            // presenceManager1
            // 
            this.presenceManager1.XmppClient = null;
            // 
            // Connecting
            // 
            this.Connecting.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connecting.Location = new System.Drawing.Point(302, 346);
            this.Connecting.Multiline = true;
            this.Connecting.Name = "Connecting";
            this.Connecting.ReadOnly = true;
            this.Connecting.Size = new System.Drawing.Size(174, 46);
            this.Connecting.TabIndex = 1;
            this.Connecting.TextChanged += new System.EventHandler(this.Connecting_TextChanged);
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Connecting);
            this.Controls.Add(this.progressBar1);
            this.Name = "LoadingForm";
            this.Text = "Loading...";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private Matrix.Xmpp.Client.PresenceManager presenceManager1;
        private System.Windows.Forms.TextBox Connecting;
    }
}

