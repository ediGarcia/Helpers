
namespace LibraryTest
{
    partial class TestForm
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
            this.BtnDownloadTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnDownloadTest
            // 
            this.BtnDownloadTest.Location = new System.Drawing.Point(305, 55);
            this.BtnDownloadTest.Name = "BtnDownloadTest";
            this.BtnDownloadTest.Size = new System.Drawing.Size(122, 35);
            this.BtnDownloadTest.TabIndex = 0;
            this.BtnDownloadTest.Text = "DownloadTest";
            this.BtnDownloadTest.UseVisualStyleBackColor = true;
            this.BtnDownloadTest.Click += new System.EventHandler(this.BtnDownloadTest_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnDownloadTest);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnDownloadTest;
    }
}