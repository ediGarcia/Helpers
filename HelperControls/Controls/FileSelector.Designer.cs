namespace HelperControls.Controls
{
    partial class FileSelector
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
            this.TxtFile = new System.Windows.Forms.TextBox();
            this.BtnFile = new System.Windows.Forms.Button();
            this.FbdFile = new System.Windows.Forms.FolderBrowserDialog();
            this.OfdFile = new System.Windows.Forms.OpenFileDialog();
            this.SfdFile = new System.Windows.Forms.SaveFileDialog();
            this.LnkFile = new System.Windows.Forms.LinkLabel();
            this.TotFile = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // TxtFile
            // 
            this.TxtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFile.Location = new System.Drawing.Point(0, 0);
            this.TxtFile.Name = "TxtFile";
            this.TxtFile.Size = new System.Drawing.Size(439, 20);
            this.TxtFile.TabIndex = 0;
            this.TxtFile.Text = "No file selected";
            this.TxtFile.TextChanged += new System.EventHandler(this.TxtFile_TextChanged);
            // 
            // BtnFile
            // 
            this.BtnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFile.Location = new System.Drawing.Point(445, 0);
            this.BtnFile.Name = "BtnFile";
            this.BtnFile.Size = new System.Drawing.Size(24, 20);
            this.BtnFile.TabIndex = 1;
            this.BtnFile.Text = "...";
            this.BtnFile.UseVisualStyleBackColor = true;
            this.BtnFile.Click += new System.EventHandler(this.BtnFile_Click);
            // 
            // FbdFile
            // 
            this.FbdFile.ShowNewFolderButton = false;
            // 
            // LnkFile
            // 
            this.LnkFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LnkFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LnkFile.Location = new System.Drawing.Point(0, 0);
            this.LnkFile.Name = "LnkFile";
            this.LnkFile.Size = new System.Drawing.Size(439, 20);
            this.LnkFile.TabIndex = 2;
            this.LnkFile.TabStop = true;
            this.LnkFile.Text = "No file selected";
            this.LnkFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkFile_LinkClicked);
            this.LnkFile.Resize += new System.EventHandler(this.LnkFile_Resize);
            // 
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnFile);
            this.Controls.Add(this.TxtFile);
            this.Controls.Add(this.LnkFile);
            this.Name = "FileSelector";
            this.Size = new System.Drawing.Size(469, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtFile;
        private System.Windows.Forms.Button BtnFile;
        private System.Windows.Forms.OpenFileDialog OfdFile;
        private System.Windows.Forms.SaveFileDialog SfdFile;
        private System.Windows.Forms.LinkLabel LnkFile;
        public System.Windows.Forms.FolderBrowserDialog FbdFile;
        private System.Windows.Forms.ToolTip TotFile;
    }
}
