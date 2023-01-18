
namespace Downloader.Window
{
    partial class DownloadPopup
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
            this.BtnStartAll = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.DlsDownloads = new Downloader.Window.DownloadList();
            this.SuspendLayout();
            // 
            // BtnStartAll
            // 
            this.BtnStartAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStartAll.Location = new System.Drawing.Point(167, 303);
            this.BtnStartAll.Name = "BtnStartAll";
            this.BtnStartAll.Size = new System.Drawing.Size(149, 31);
            this.BtnStartAll.TabIndex = 1;
            this.BtnStartAll.Text = "Start All";
            this.BtnStartAll.UseVisualStyleBackColor = true;
            this.BtnStartAll.Click += new System.EventHandler(this.BtnStartAll_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnClear.Enabled = false;
            this.BtnClear.Location = new System.Drawing.Point(12, 303);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(149, 31);
            this.BtnClear.TabIndex = 2;
            this.BtnClear.Text = "Clear Finished";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Location = new System.Drawing.Point(397, 303);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(149, 31);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DlsDownloads
            // 
            this.DlsDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DlsDownloads.AutoScroll = true;
            this.DlsDownloads.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DlsDownloads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DlsDownloads.Location = new System.Drawing.Point(12, 12);
            this.DlsDownloads.Name = "DlsDownloads";
            this.DlsDownloads.Size = new System.Drawing.Size(534, 285);
            this.DlsDownloads.TabIndex = 0;
            // 
            // DownloadPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 346);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnStartAll);
            this.Controls.Add(this.DlsDownloads);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DownloadPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloads";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadPopup_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private DownloadList DlsDownloads;
        private System.Windows.Forms.Button BtnStartAll;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnClose;
    }
}