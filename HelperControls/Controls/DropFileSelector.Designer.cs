namespace HelperControls.Controls
{
    partial class DropFileSelector
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
            this.components = new System.ComponentModel.Container();
            this.LblFile = new System.Windows.Forms.Label();
            this.OfdFile = new System.Windows.Forms.OpenFileDialog();
            this.FbdFile = new System.Windows.Forms.FolderBrowserDialog();
            this.TotFile = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // LblFile
            // 
            this.LblFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblFile.Location = new System.Drawing.Point(0, 0);
            this.LblFile.Name = "LblFile";
            this.LblFile.Size = new System.Drawing.Size(416, 52);
            this.LblFile.TabIndex = 0;
            this.LblFile.Text = "label1";
            this.LblFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DropFileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LblFile);
            this.Name = "DropFileSelector";
            this.Size = new System.Drawing.Size(416, 52);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblFile;
        private System.Windows.Forms.OpenFileDialog OfdFile;
        private System.Windows.Forms.FolderBrowserDialog FbdFile;
        private System.Windows.Forms.ToolTip TotFile;
    }
}
