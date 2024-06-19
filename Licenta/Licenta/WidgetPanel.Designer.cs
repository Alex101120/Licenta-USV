namespace Licenta
{
    partial class WidgetPanel
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
            this.VariableChoser = new System.Windows.Forms.ComboBox();
            this.WidgetType = new System.Windows.Forms.ComboBox();
            this.Add = new System.Windows.Forms.Button();
            this.VariableChoserName = new System.Windows.Forms.Label();
            this.WidgetTypeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VariableChoser
            // 
            this.VariableChoser.FormattingEnabled = true;
            this.VariableChoser.Location = new System.Drawing.Point(12, 46);
            this.VariableChoser.Name = "VariableChoser";
            this.VariableChoser.Size = new System.Drawing.Size(157, 21);
            this.VariableChoser.TabIndex = 0;
            this.VariableChoser.SelectedIndexChanged += new System.EventHandler(this.VariableChoser_SelectedIndexChanged);
            // 
            // WidgetType
            // 
            this.WidgetType.FormattingEnabled = true;
            this.WidgetType.Location = new System.Drawing.Point(12, 107);
            this.WidgetType.Name = "WidgetType";
            this.WidgetType.Size = new System.Drawing.Size(157, 21);
            this.WidgetType.TabIndex = 1;
            this.WidgetType.SelectedIndexChanged += new System.EventHandler(this.WidgetType_SelectedIndexChanged);
            // 
            // Add
            // 
            this.Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.Location = new System.Drawing.Point(12, 147);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(94, 42);
            this.Add.TabIndex = 2;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // VariableChoserName
            // 
            this.VariableChoserName.AutoSize = true;
            this.VariableChoserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VariableChoserName.Location = new System.Drawing.Point(7, 18);
            this.VariableChoserName.Name = "VariableChoserName";
            this.VariableChoserName.Size = new System.Drawing.Size(174, 25);
            this.VariableChoserName.TabIndex = 3;
            this.VariableChoserName.Text = "VariableChoser";
            // 
            // WidgetTypeLabel
            // 
            this.WidgetTypeLabel.AutoSize = true;
            this.WidgetTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WidgetTypeLabel.Location = new System.Drawing.Point(12, 79);
            this.WidgetTypeLabel.Name = "WidgetTypeLabel";
            this.WidgetTypeLabel.Size = new System.Drawing.Size(137, 25);
            this.WidgetTypeLabel.TabIndex = 4;
            this.WidgetTypeLabel.Text = "WidgetType";
            // 
            // WidgetPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 214);
            this.Controls.Add(this.WidgetTypeLabel);
            this.Controls.Add(this.VariableChoserName);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.WidgetType);
            this.Controls.Add(this.VariableChoser);
            this.Name = "WidgetPanel";
            this.Text = "WidgetPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox VariableChoser;
        private System.Windows.Forms.ComboBox WidgetType;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label VariableChoserName;
        private System.Windows.Forms.Label WidgetTypeLabel;
    }
}