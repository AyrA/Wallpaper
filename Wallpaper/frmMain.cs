using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Wallpaper
{
    /// <summary>
    /// Main Form
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// Supported Images
        /// </summary>
        public const string IMAGE_EXT = ".bmp|.png|.jpg|.jpeg|.gif";

        /// <summary>
        /// Serializable settings
        /// </summary>
        /// <remarks>Only public types can be serialized for some bizarre reason</remarks>
        public struct Settings
        {
            public string DirectoryName;
            public string[] FileNames;
            public int Timeout;
        }

        #region Vars

        private List<string> Images;
        private string LastDir;
        private Random R;
        private Timer T;
        private string SettingsFile;
        private frmMessages Logger;

        #endregion

        /// <summary>
        /// Initializes form and processes settings
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            R = new Random();
            T = new Timer();
            Logger = new frmMessages();
            //Default
            T.Interval = 60000;

            SettingsFile = Path.Combine(Application.StartupPath, "settings.xml");
            var Data = ReadSettings();
            if (Data.FileNames != null)
            {
                Images = new List<string>(Data.FileNames);
            }
            if (Data.Timeout >= nudTimeout.Minimum && Data.Timeout <= nudTimeout.Maximum)
            {
                T.Interval = Data.Timeout * 1000;
                nudTimeout.Value = Data.Timeout;
            }
            if (!string.IsNullOrEmpty(Data.DirectoryName))
            {
                FBD.SelectedPath = tbDirectory.Text = LastDir = Data.DirectoryName;
            }
            T.Tick += delegate
            {
                T.Stop();
                SetRandomImage();
                T.Start();
            };
            SetRandomImage();
            T.Start();
            ShowDebug("Application started");
        }

        #region Logic

        /// <summary>
        /// Saves current settings
        /// </summary>
        private void SaveSettings()
        {
            ShowDebug("Saving default settings");
            SaveSettings(new Settings()
            {
                DirectoryName = LastDir,
                FileNames = Images?.ToArray(),
                Timeout = T.Interval / 1000
            });
        }

        /// <summary>
        /// Saves specific settings
        /// </summary>
        /// <param name="S">Settings</param>
        private void SaveSettings(Settings S)
        {
            ShowDebug("Writing settings to file");
            try
            {
                File.WriteAllText(SettingsFile, S.Serialize());
            }
            catch (Exception ex)
            {
                ShowError("Unable to save settings.\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Restores last settings
        /// </summary>
        /// <returns>Settings</returns>
        private Settings ReadSettings()
        {
            ShowDebug("Reading settings");
            if (File.Exists(SettingsFile))
            {
                try
                {
                    return File.ReadAllText(SettingsFile).Deserialize<Settings>();
                }
                catch (Exception ex)
                {
                    ShowError("Problem reading settings file.\r\n" + ex.Message);
                }
            }
            ShowDebug(SettingsFile + " doesn't exists. Returning defaults");
            return default(Settings);
        }

        /// <summary>
        /// Removes a random item from the image list and returns it
        /// </summary>
        /// <returns>Random item, null if list is empty or uninitialized</returns>
        private string PopRandom()
        {
            ShowDebug("Getting random entry from image list");
            if (Images == null || Images.Count == 0)
            {
                ShowInfo("Image list is empty.");
                return null;
            }
            lock (Images)
            {
                var I = R.Next(Images.Count);
                var S = Images[I];
                Images.RemoveAt(I);
                return Path.Combine(LastDir, S);
            }
        }

        /// <summary>
        /// Checks if the given file name is an image file
        /// </summary>
        /// <param name="FileName">File name with ot without path</param>
        /// <returns>true, if image</returns>
        /// <remarks>Does test by file extension only</remarks>
        private bool IsImageFile(string FileName)
        {
            return IMAGE_EXT.Split('|').Any(m => FileName.ToLower().EndsWith(m));
        }

        /// <summary>
        /// Gets all image file names from a folder
        /// </summary>
        /// <param name="Folder">Directory to read images from</param>
        private void GetImages(string Folder)
        {
            ShowDebug("Reloading Images");
            try
            {
                Images = new List<string>(Directory.GetFiles(Folder).Where(m => IsImageFile(m)).Select(m => m.Substring(Folder.Length).Trim('/', '\\')));
                LastDir = Folder;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to enumerate files in {Folder}", ex);
            }
        }

        /// <summary>
        /// Sets a random image from the list as wallpaper
        /// </summary>
        private void SetRandomImage()
        {
            if (!string.IsNullOrEmpty(LastDir))
            {
                if (Images == null || Images.Count == 0)
                {
                    ShowDebug("Populating empty image list");
                    try
                    {
                        GetImages(LastDir);
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                    }
                    if (Images == null || Images.Count == 0)
                    {
                        ShowWarning("Directory doesn't contanins any images. Timer stopped");
                        T.Stop();
                    }
                }
                string ImageFile = PopRandom();
                if (!string.IsNullOrEmpty(ImageFile))
                {
                    if (File.Exists(ImageFile))
                    {
                        ShowDebug("Setting image");
                        try
                        {
                            DesktopWallpaper.Set(ImageFile, DesktopWallpaper.Style.Stretch);
                            Image I = Image.FromFile(ImageFile);
                            if (pbCurrent.Image != null)
                            {
                                pbCurrent.Image.Dispose();
                            }
                            pbCurrent.Image = I;
                            ShowDebug("Image set");
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex);
                        }
                    }
                    else
                    {
                        ShowInfo($"{ImageFile} from cache no longer exists");
                    }
                }
                else
                {
                    ShowWarning("Couldn't get image from directory. Timer stopped");
                    T.Stop();
                }
                SaveSettings();
            }
            else
            {
                ShowDebug("Directory not yet selected, skipping wallpaper function");
            }
        }

        /// <summary>
        /// Shows debug message
        /// </summary>
        /// <param name="Message">Message</param>
        private void ShowDebug(string Message)
        {
            Program.Debug(Message);
#if DEBUG
            ShowInfo(string.Format("DEBUG: {0}", Message));
#endif
        }

        /// <summary>
        /// Shows informational message
        /// </summary>
        /// <param name="Message">Message</param>
        private void ShowInfo(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                Logger.AddMessage(Message, MessageType.Info);
            }
        }

        /// <summary>
        /// Shows warning message
        /// </summary>
        /// <param name="Message">Message</param>
        private void ShowWarning(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                Logger.AddMessage(Message, MessageType.Warning);
                Logger.Show();
            }
        }

        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="Message">Error message</param>
        private void ShowError(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                //MessageBox.Show(Program.Debug(Message), "Problem setting Wallpaper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AddMessage(Message, MessageType.Error);
                Logger.Show();
            }
        }

        /// <summary>
        /// Shows an error message generated from an exception
        /// </summary>
        /// <param name="ex">Exception</param>
        private void ShowError(Exception ex)
        {
            ShowError($"Unhandled Exception: {ex.Message}\nLocation: {ex.StackTrace}");
        }

        #endregion

        #region Events

        private void btnChangeWP_Click(object sender, EventArgs e)
        {
            T.Stop();
            SetRandomImage();
            T.Start();
        }

        private void nudTimeout_ValueChanged(object sender, EventArgs e)
        {
            T.Interval = (int)nudTimeout.Value * 1000;
            SaveSettings();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                GetImages(tbDirectory.Text = FBD.SelectedPath);
                SaveSettings();
            }
        }

        private void NFI_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            NFI.Visible = false;
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                NFI.Visible = true;
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void nextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRandomImage();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Logger.Show();
            }
        }

        #endregion
    }
}
