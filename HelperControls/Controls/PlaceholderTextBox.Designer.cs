namespace HelperControls.Controls
{
    sealed partial class PlaceholderTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TxtTextBox
            // 
            this.TxtTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtTextBox.Location = new System.Drawing.Point(0, 3);
            this.TxtTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.TxtTextBox.Name = "TxtTextBox";
            this.TxtTextBox.Size = new System.Drawing.Size(148, 13);
            this.TxtTextBox.TabIndex = 0;
            this.TxtTextBox.Enter += new System.EventHandler(this.TxtTextBox_Enter);
            this.TxtTextBox.Leave += new System.EventHandler(this.TxtTextBox_Leave);
            // 
            // PlaceholderTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.TxtTextBox);
            this.Name = "PlaceholderTextBox";
            this.Size = new System.Drawing.Size(148, 20);
            this.EnabledChanged += new System.EventHandler(this.PlaceholderTextBox_EnabledChanged);
            this.FontChanged += new System.EventHandler(this.PlaceholderTextBox_FontChanged);
            this.ForeColorChanged += new System.EventHandler(this.PlaceholderTextBox_ForeColorChanged);
            this.Enter += new System.EventHandler(this.PlaceholderTextBox_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtTextBox;
    }
}
