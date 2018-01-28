using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Wallpaper
{
    public static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern void OutputDebugString([MarshalAs(UnmanagedType.LPStr)]string Message);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug("Application started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            Debug("Application ended");
        }

        /// <summary>
        /// Writes a message to the global debug logger
        /// </summary>
        /// <param name="Message">Message</param>
        /// <returns>Message</returns>
        public static string Debug(string Message)
        {
            OutputDebugString(Message);
            return Message;
        }

        /// <summary>
        /// Writes a message to the global debug logger
        /// </summary>
        /// <param name="Format">Message Format</param>
        /// <param name="Params">Message Parameter</param>
        /// <returns>Message</returns>
        public static string Debug(string Format, params object[] Params)
        {
            return Debug(string.Format(Format, Params));
        }
    }
}
