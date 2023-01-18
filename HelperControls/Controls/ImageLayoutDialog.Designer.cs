namespace HelperControls.Controls
{
	partial class ImageLayoutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageLayoutDialog));
			this.RdbZoom = new System.Windows.Forms.RadioButton();
			this.RdbStretch = new System.Windows.Forms.RadioButton();
			this.RdbCenter = new System.Windows.Forms.RadioButton();
			this.RdbTile = new System.Windows.Forms.RadioButton();
			this.RdbNone = new System.Windows.Forms.RadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// RdbZoom
			// 
			this.RdbZoom.Appearance = System.Windows.Forms.Appearance.Button;
			this.RdbZoom.AutoSize = true;
			this.RdbZoom.Image = ((System.Drawing.Image)(resources.GetObject("RdbZoom.Image")));
			this.RdbZoom.Location = new System.Drawing.Point(179, 3);
			this.RdbZoom.Name = "RdbZoom";
			this.RdbZoom.Size = new System.Drawing.Size(38, 25);
			this.RdbZoom.TabIndex = 28;
			this.toolTip1.SetToolTip(this.RdbZoom, "Zoom");
			this.RdbZoom.UseVisualStyleBackColor = true;
			this.RdbZoom.Click += new System.EventHandler(this.RdbLayout_Click);
			// 
			// RdbStretch
			// 
			this.RdbStretch.Appearance = System.Windows.Forms.Appearance.Button;
			this.RdbStretch.AutoSize = true;
			this.RdbStretch.Image = ((System.Drawing.Image)(resources.GetObject("RdbStretch.Image")));
			this.RdbStretch.Location = new System.Drawing.Point(135, 3);
			this.RdbStretch.Name = "RdbStretch";
			this.RdbStretch.Size = new System.Drawing.Size(38, 25);
			this.RdbStretch.TabIndex = 27;
			this.toolTip1.SetToolTip(this.RdbStretch, "Esticar");
			this.RdbStretch.UseVisualStyleBackColor = true;
			this.RdbStretch.Click += new System.EventHandler(this.RdbLayout_Click);
			// 
			// RdbCenter
			// 
			this.RdbCenter.Appearance = System.Windows.Forms.Appearance.Button;
			this.RdbCenter.AutoSize = true;
			this.RdbCenter.Checked = true;
			this.RdbCenter.Image = ((System.Drawing.Image)(resources.GetObject("RdbCenter.Image")));
			this.RdbCenter.Location = new System.Drawing.Point(91, 3);
			this.RdbCenter.Name = "RdbCenter";
			this.RdbCenter.Size = new System.Drawing.Size(38, 25);
			this.RdbCenter.TabIndex = 26;
			this.RdbCenter.TabStop = true;
			this.toolTip1.SetToolTip(this.RdbCenter, "Centralizar");
			this.RdbCenter.UseVisualStyleBackColor = true;
			this.RdbCenter.Click += new System.EventHandler(this.RdbLayout_Click);
			// 
			// RdbTile
			// 
			this.RdbTile.Appearance = System.Windows.Forms.Appearance.Button;
			this.RdbTile.AutoSize = true;
			this.RdbTile.Image = ((System.Drawing.Image)(resources.GetObject("RdbTile.Image")));
			this.RdbTile.Location = new System.Drawing.Point(47, 3);
			this.RdbTile.Name = "RdbTile";
			this.RdbTile.Size = new System.Drawing.Size(38, 24);
			this.RdbTile.TabIndex = 25;
			this.toolTip1.SetToolTip(this.RdbTile, "Preencher");
			this.RdbTile.UseVisualStyleBackColor = true;
			this.RdbTile.Click += new System.EventHandler(this.RdbLayout_Click);
			// 
			// RdbNone
			// 
			this.RdbNone.Appearance = System.Windows.Forms.Appearance.Button;
			this.RdbNone.AutoSize = true;
			this.RdbNone.Image = ((System.Drawing.Image)(resources.GetObject("RdbNone.Image")));
			this.RdbNone.Location = new System.Drawing.Point(3, 3);
			this.RdbNone.Name = "RdbNone";
			this.RdbNone.Size = new System.Drawing.Size(38, 24);
			this.RdbNone.TabIndex = 24;
			this.toolTip1.SetToolTip(this.RdbNone, "Nenhum");
			this.RdbNone.UseVisualStyleBackColor = true;
			this.RdbNone.Click += new System.EventHandler(this.RdbLayout_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 5000000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ShowAlways = true;
			// 
			// UctImageLayout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.RdbZoom);
			this.Controls.Add(this.RdbStretch);
			this.Controls.Add(this.RdbCenter);
			this.Controls.Add(this.RdbTile);
			this.Controls.Add(this.RdbNone);
			this.Name = "UctImageLayout";
			this.Size = new System.Drawing.Size(221, 31);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton RdbZoom;
		private System.Windows.Forms.RadioButton RdbStretch;
		private System.Windows.Forms.RadioButton RdbCenter;
		private System.Windows.Forms.RadioButton RdbTile;
		private System.Windows.Forms.RadioButton RdbNone;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
