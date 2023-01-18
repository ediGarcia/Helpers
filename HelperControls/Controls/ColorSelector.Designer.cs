namespace HelperControls.Controls
{
    partial class ColorSelector
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
            this.LblCor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblCor
            // 
            this.LblCor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblCor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblCor.Location = new System.Drawing.Point(0, 0);
            this.LblCor.Name = "LblCor";
            this.LblCor.Size = new System.Drawing.Size(145, 29);
            this.LblCor.TabIndex = 1;
            this.LblCor.Text = "Transparente";
            this.LblCor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblCor.Click += new System.EventHandler(this.LblCor_Click);
            // 
            // UctColorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LblCor);
            this.Name = "UctColorSelector";
            this.Size = new System.Drawing.Size(145, 29);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label LblCor;
    }
}
