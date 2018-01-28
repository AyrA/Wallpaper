using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wallpaper.Properties;

namespace Wallpaper
{
    /// <summary>
    /// Types of Log Messages
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Informational
        /// </summary>
        Info,
        /// <summary>
        /// Warning
        /// </summary>
        Warning,
        /// <summary>
        /// Error
        /// </summary>
        Error
    }
    public partial class frmMessages : Form
    {
        public frmMessages()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="Message">Message</param>
        /// <param name="T">Message Type</param>
        public void AddMessage(string Message, MessageType T)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                if (Enum.IsDefined(typeof(MessageType), T))
                {
                    //Append Text
                    tbMessages.AppendText(Message + "\n");
                    //Set Color
                    tbMessages.Select(tbMessages.Text.Length - Message.Length - 1, Message.Length);
                    tbMessages.SelectionColor = ColorFromType(T);
                    SetImage(T);
                }
                else
                {
                    AddMessage("Attempting to add unsupported message type", MessageType.Error);
                }
            }
            else
            {
                AddMessage("(null)", T);
            }
        }

        /// <summary>
        /// Set image according to the message type
        /// </summary>
        /// <param name="T">Type</param>
        private void SetImage(MessageType T)
        {
            Image I = null;
            switch (T)
            {
                case MessageType.Info:
                    I = Resources.ing_info;
                    break;
                case MessageType.Warning:
                    I = Resources.img_warn;
                    break;
                case MessageType.Error:
                    I = Resources.img_err;
                    break;
            }
            if (I != null)
            {
                if (pbMsgImg.Image != null)
                {
                    pbMsgImg.Image.Dispose();
                }
                pbMsgImg.Image = I;
            }
        }

        /// <summary>
        /// Gets the color of a type
        /// </summary>
        /// <param name="T">Message Type</param>
        /// <returns>Color</returns>
        /// <remarks>Defaults to Black for unsupported types</remarks>
        private Color ColorFromType(MessageType T)
        {
            switch (T)
            {
                case MessageType.Error:
                    return Color.Red;
                case MessageType.Warning:
                    return Color.Orange;
                case MessageType.Info:
                    return Color.DarkBlue;
                default:
                    return Color.Black;
            }
        }

        /// <summary>
        /// Prevent user from closing with [X]
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Close Event Arguments</param>
        private void frmMessages_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
