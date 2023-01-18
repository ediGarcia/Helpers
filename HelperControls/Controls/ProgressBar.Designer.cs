
namespace HelperControls.Controls
{
    partial class ProgressBar
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
            this.PnlProgress = new System.Windows.Forms.Panel();
            this.LblMessageOver = new System.Windows.Forms.Label();
            this.LblMessageUnder = new System.Windows.Forms.Label();
            this.PnlBorderTop = new System.Windows.Forms.Panel();
            this.PnlBorderLeft = new System.Windows.Forms.Panel();
            this.PnlBorderBottom = new System.Windows.Forms.Panel();
            this.PnlBorderRight = new System.Windows.Forms.Panel();
            this.PnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlProgress
            // 
            this.PnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PnlProgress.BackColor = System.Drawing.Color.LimeGreen;
            this.PnlProgress.Controls.Add(this.LblMessageOver);
            this.PnlProgress.Location = new System.Drawing.Point(0, 0);
            this.PnlProgress.Margin = new System.Windows.Forms.Padding(0);
            this.PnlProgress.Name = "PnlProgress";
            this.PnlProgress.Size = new System.Drawing.Size(0, 35);
            this.PnlProgress.TabIndex = 0;
            // 
            // LblMessageOver
            // 
            this.LblMessageOver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LblMessageOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMessageOver.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblMessageOver.Location = new System.Drawing.Point(0, 0);
            this.LblMessageOver.Margin = new System.Windows.Forms.Padding(0);
            this.LblMessageOver.Name = "LblMessageOver";
            this.LblMessageOver.Size = new System.Drawing.Size(443, 35);
            this.LblMessageOver.TabIndex = 0;
            this.LblMessageOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMessageUnder
            // 
            this.LblMessageUnder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMessageUnder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMessageUnder.Location = new System.Drawing.Point(0, 0);
            this.LblMessageUnder.Margin = new System.Windows.Forms.Padding(0);
            this.LblMessageUnder.Name = "LblMessageUnder";
            this.LblMessageUnder.Size = new System.Drawing.Size(443, 35);
            this.LblMessageUnder.TabIndex = 1;
            this.LblMessageUnder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlBorderTop
            // 
            this.PnlBorderTop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlBorderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBorderTop.Location = new System.Drawing.Point(0, 0);
            this.PnlBorderTop.Name = "PnlBorderTop";
            this.PnlBorderTop.Size = new System.Drawing.Size(443, 1);
            this.PnlBorderTop.TabIndex = 2;
            // 
            // PnlBorderLeft
            // 
            this.PnlBorderLeft.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlBorderLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlBorderLeft.Location = new System.Drawing.Point(442, 1);
            this.PnlBorderLeft.Name = "PnlBorderLeft";
            this.PnlBorderLeft.Size = new System.Drawing.Size(1, 34);
            this.PnlBorderLeft.TabIndex = 3;
            // 
            // PnlBorderBottom
            // 
            this.PnlBorderBottom.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlBorderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBorderBottom.Location = new System.Drawing.Point(0, 34);
            this.PnlBorderBottom.Name = "PnlBorderBottom";
            this.PnlBorderBottom.Size = new System.Drawing.Size(442, 1);
            this.PnlBorderBottom.TabIndex = 4;
            // 
            // PnlBorderRight
            // 
            this.PnlBorderRight.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlBorderRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlBorderRight.Location = new System.Drawing.Point(0, 1);
            this.PnlBorderRight.Name = "PnlBorderRight";
            this.PnlBorderRight.Size = new System.Drawing.Size(1, 33);
            this.PnlBorderRight.TabIndex = 5;
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PnlBorderRight);
            this.Controls.Add(this.PnlBorderBottom);
            this.Controls.Add(this.PnlBorderLeft);
            this.Controls.Add(this.PnlBorderTop);
            this.Controls.Add(this.PnlProgress);
            this.Controls.Add(this.LblMessageUnder);
            this.Name = "ProgressBar";
            this.Size = new System.Drawing.Size(443, 35);
            this.SizeChanged += new System.EventHandler(this.ProgressBar_SizeChanged);
            this.PnlProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlProgress;
        private System.Windows.Forms.Label LblMessageOver;
        private System.Windows.Forms.Label LblMessageUnder;
        private System.Windows.Forms.Panel PnlBorderTop;
        private System.Windows.Forms.Panel PnlBorderLeft;
        private System.Windows.Forms.Panel PnlBorderBottom;
        private System.Windows.Forms.Panel PnlBorderRight;
    }
}
