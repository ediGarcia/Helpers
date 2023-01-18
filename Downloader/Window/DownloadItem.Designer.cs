
namespace Downloader.Window
{
    partial class DownloadItem
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
            this.LblFileName = new System.Windows.Forms.Label();
            this.LblElapsedTime = new System.Windows.Forms.Label();
            this.LblDownloadInfo = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TotTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.PrbProgress = new HelperControls.Controls.ProgressBar();
            this.SuspendLayout();
            // 
            // LblFileName
            // 
            this.LblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblFileName.AutoEllipsis = true;
            this.LblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFileName.Location = new System.Drawing.Point(8, 5);
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(479, 23);
            this.LblFileName.TabIndex = 0;
            this.LblFileName.Text = "File_Name.fln";
            // 
            // LblElapsedTime
            // 
            this.LblElapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblElapsedTime.AutoSize = true;
            this.LblElapsedTime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LblElapsedTime.Location = new System.Drawing.Point(487, 5);
            this.LblElapsedTime.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LblElapsedTime.Name = "LblElapsedTime";
            this.LblElapsedTime.Size = new System.Drawing.Size(64, 17);
            this.LblElapsedTime.TabIndex = 1;
            this.LblElapsedTime.Text = "00:00:00";
            this.LblElapsedTime.Visible = false;
            // 
            // LblDownloadInfo
            // 
            this.LblDownloadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDownloadInfo.AutoEllipsis = true;
            this.LblDownloadInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LblDownloadInfo.Location = new System.Drawing.Point(9, 52);
            this.LblDownloadInfo.Name = "LblDownloadInfo";
            this.LblDownloadInfo.Size = new System.Drawing.Size(548, 17);
            this.LblDownloadInfo.TabIndex = 3;
            this.LblDownloadInfo.Text = "Donwload not started.";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Image = global::Downloader.Properties.Resources.StartIcon;
            this.BtnCancel.Location = new System.Drawing.Point(519, 27);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(28, 23);
            this.BtnCancel.TabIndex = 4;
            this.TotTooltip.SetToolTip(this.BtnCancel, "Cancel Download");
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PrbProgress
            // 
            this.PrbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrbProgress.Location = new System.Drawing.Point(12, 28);
            this.PrbProgress.Name = "PrbProgress";
            this.PrbProgress.OverProgressForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.PrbProgress.Size = new System.Drawing.Size(501, 21);
            this.PrbProgress.TabIndex = 5;
            this.PrbProgress.UnderProgressForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // DownloadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.PrbProgress);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblDownloadInfo);
            this.Controls.Add(this.LblElapsedTime);
            this.Controls.Add(this.LblFileName);
            this.Name = "DownloadItem";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(565, 79);
            this.Load += new System.EventHandler(this.DownloadItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblFileName;
        private System.Windows.Forms.Label LblElapsedTime;
        private System.Windows.Forms.Label LblDownloadInfo;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ToolTip TotTooltip;
        private HelperControls.Controls.ProgressBar PrbProgress;
    }
}
