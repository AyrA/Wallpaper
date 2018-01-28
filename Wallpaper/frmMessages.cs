﻿using System;
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
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    public partial class frmMessages : Form
    {
        public frmMessages()
        {
            InitializeComponent();
        }

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