namespace HelperControls.Controls
{
    partial class PasswordField
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
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPassword
            // 
            this.TxtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPassword.Location = new System.Drawing.Point(0, 0);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '●';
            this.TxtPassword.Size = new System.Drawing.Size(164, 20);
            this.TxtPassword.TabIndex = 0;
            this.TxtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // BtnShow
            // 
            this.BtnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.BtnShow.Image = global::Helpers.Properties.Resources.ShowPassword;
            this.BtnShow.Location = new System.Drawing.Point(163, 0);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(31, 20);
            this.BtnShow.TabIndex = 1;
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            this.BtnShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnShow_MouseDown);
            this.BtnShow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnShow_MouseUp);
            // 
            // PasswordField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnShow);
            this.Controls.Add(this.TxtPassword);
            this.Name = "PasswordField";
            this.Size = new System.Drawing.Size(194, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button BtnShow;
    }
}
