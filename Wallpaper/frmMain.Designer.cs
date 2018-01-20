namespace Wallpaper
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnChangeWP = new System.Windows.Forms.Button();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pbCurrent = new System.Windows.Forms.PictureBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.FBD = new System.Windows.Forms.FolderBrowserDialog();
            this.NFI = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChangeWP
            // 
            this.btnChangeWP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeWP.Location = new System.Drawing.Point(237, 38);
            this.btnChangeWP.Name = "btnChangeWP";
            this.btnChangeWP.Size = new System.Drawing.Size(135, 23);
            this.btnChangeWP.TabIndex = 4;
            this.btnChangeWP.Text = "Change Wallpaper now";
            this.btnChangeWP.UseVisualStyleBackColor = true;
            this.btnChangeWP.Click += new System.EventHandler(this.btnChangeWP_Click);
            // 
            // nudTimeout
            // 
            this.nudTimeout.Location = new System.Drawing.Point(74, 41);
            this.nudTimeout.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.nudTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(70, 20);
            this.nudTimeout.TabIndex = 3;
            this.nudTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTimeout.ValueChanged += new System.EventHandler(this.nudTimeout_ValueChanged);
            // 
            // tbDirectory
            // 
            this.tbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirectory.Location = new System.Drawing.Point(12, 12);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.ReadOnly = true;
            this.tbDirectory.Size = new System.Drawing.Size(316, 20);
            this.tbDirectory.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(334, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(38, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pbCurrent
            // 
            this.pbCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCurrent.Location = new System.Drawing.Point(12, 67);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(360, 183);
            this.pbCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCurrent.TabIndex = 4;
            this.pbCurrent.TabStop = false;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(12, 43);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(56, 13);
            this.lblInterval.TabIndex = 2;
            this.lblInterval.Text = "Interval (s)";
            // 
            // FBD
            // 
            this.FBD.Description = "Select image Directory";
            // 
            // NFI
            // 
            this.NFI.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NFI.BalloonTipText = "Double click to open";
            this.NFI.BalloonTipTitle = "Wallpaper Changer";
            this.NFI.Icon = ((System.Drawing.Icon)(resources.GetObject("NFI.Icon")));
            this.NFI.Text = "notifyIcon1";
            this.NFI.Visible = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbDirectory);
            this.Controls.Add(this.nudTimeout);
            this.Controls.Add(this.btnChangeWP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "frmMain";
            this.Text = "Wallpaper Changer";
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangeWP;
        private System.Windows.Forms.NumericUpDown nudTimeout;
        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.PictureBox pbCurrent;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.FolderBrowserDialog FBD;
        private System.Windows.Forms.NotifyIcon NFI;
    }
}

