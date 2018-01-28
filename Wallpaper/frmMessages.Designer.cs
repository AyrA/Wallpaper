namespace Wallpaper
{
    partial class frmMessages
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
            this.pbMsgImg = new System.Windows.Forms.PictureBox();
            this.tbMessages = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMsgImg)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMsgImg
            // 
            this.pbMsgImg.Location = new System.Drawing.Point(12, 12);
            this.pbMsgImg.Name = "pbMsgImg";
            this.pbMsgImg.Size = new System.Drawing.Size(128, 128);
            this.pbMsgImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMsgImg.TabIndex = 0;
            this.pbMsgImg.TabStop = false;
            // 
            // tbMessages
            // 
            this.tbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessages.BackColor = System.Drawing.SystemColors.Window;
            this.tbMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMessages.DetectUrls = false;
            this.tbMessages.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessages.Location = new System.Drawing.Point(146, 12);
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.ReadOnly = true;
            this.tbMessages.Size = new System.Drawing.Size(497, 305);
            this.tbMessages.TabIndex = 2;
            this.tbMessages.Text = "";
            // 
            // frmMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 331);
            this.Controls.Add(this.tbMessages);
            this.Controls.Add(this.pbMsgImg);
            this.Name = "frmMessages";
            this.Text = "Messages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMessages_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbMsgImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMsgImg;
        private System.Windows.Forms.RichTextBox tbMessages;
    }
}