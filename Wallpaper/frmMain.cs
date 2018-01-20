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
        public struct Settings
        {
            public string DirectoryName;
            public string[] FileNames;
            public int Timeout;
        }

        private List<string> Images;
        private string LastDir;
        private Random R;
        private Timer T;
        private string SettingsFile;

        /// <summary>
        /// Initializes form and processes settings
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            R = new Random();
            T = new Timer();
            //Default
            T.Interval = 60000;

            SettingsFile = Path.Combine(Application.StartupPath, "settings.xml");
            var Data = ReadSettings();
            if (Data.FileNames != null)
            {
                Images = new List<string>(Data.FileNames);
            }
            if (Data.Timeout >= nudTimeout.Minimum && Data.Timeout<=nudTimeout.Maximum)
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
        }

        /// <summary>
        /// Saves current settings
        /// </summary>
        private void SaveSettings()
        {
            SaveSettings(new Settings() {
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
            return default(Settings);
        }

        /// <summary>
        /// Removes a random item from the image list and returns it
        /// </summary>
        /// <returns>Random item, null if list is empty or uninitialized</returns>
        private string PopRandom()
        {
            if (Images == null || Images.Count == 0)
            {
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
            try
            {
                Images = new List<string>(Directory.GetFiles(Folder).Where(m => IsImageFile(m)).Select(m => m.Substring(Folder.Length).Trim('/', '\\')));
                LastDir = Folder;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Directory", ex);
            }
        }

        /// <summary>
        /// Sets a random image from the list as wallpaper
        /// </summary>
        private void SetRandomImage()
        {
            if (Images == null || Images.Count == 0)
            {
                try
                {
                    GetImages(LastDir);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
            string ImageFile = PopRandom();
            if (!string.IsNullOrEmpty(ImageFile))
            {
                if (File.Exists(ImageFile))
                {
                    DesktopWallpaper.Set(ImageFile, DesktopWallpaper.Style.Stretch);
                    Image I = Image.FromFile(ImageFile);
                    if (pbCurrent.Image != null)
                    {
                        pbCurrent.Image.Dispose();
                    }
                    pbCurrent.Image = I;
                }
            }
            SaveSettings();
        }

        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="Message">Error message</param>
        private void ShowError(string Message)
        {
            MessageBox.Show(Message, "Problem setting Wallpaper", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an error message generated from an exception
        /// </summary>
        /// <param name="ex">Exception</param>
        private void ShowError(Exception ex)
        {
            ShowError(ex.Message);
        }

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
    }
}
