using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Sets Desktop Wallpaper
/// </summary>
public static class DesktopWallpaper
{
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDWININICHANGE = 0x02;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    /// <summary>
    /// Wallpaper Style
    /// </summary>
    public enum Style
    {
        /// <summary>
        /// Fill desktop with image.
        /// Scales until entire screen is full, cuts off excessive content to keep aspect ratio
        /// </summary>
        Fill,
        /// <summary>
        /// Fit into area keeping the aspect ratio
        /// scales until image touches border, add borders where needed to keep aspect ratio
        /// </summary>
        Fit,
        /// <summary>
        /// Span multiple monitors
        /// scales, W7 and newer only
        /// </summary>
        Span,
        /// <summary>
        /// Tile across desktop
        /// repeats on X and Y axis if too small until screen is full
        /// </summary>
        Tile,
        /// <summary>
        /// Center image on screen
        /// doesn't scales up, adds border if too small, like <see cref="Fit"/> if too large
        /// </summary>
        Center,
        /// <summary>
        /// Fill desktop with image
        /// stretches image to fit dimensions, ignores aspect ratio
        /// </summary>
        Stretch
    }

    /// <summary>
    /// Sets Desktop Wallpaper
    /// </summary>
    /// <param name="Source">Image Data</param>
    /// <param name="ImageStyle">Wallpaper style</param>
    /// <returns>true if sucessfully set</returns>
    public static bool Set(byte[] Source, Style ImageStyle)
    {
        using (var MS = new MemoryStream(Source, false))
        {
            return Set(MS, ImageStyle);
        }
    }

    /// <summary>
    /// Sets Desktop Wallpaper
    /// </summary>
    /// <param name="Source">Image Data</param>
    /// <param name="ImageStyle">Wallpaper style</param>
    /// <returns>true if sucessfully set</returns>
    public static bool Set(string Source, Style ImageStyle)
    {
        using (var FS = File.OpenRead(Source))
        {
            return Set(FS, ImageStyle);
        }
    }

    /// <summary>
    /// Sets Desktop Wallpaper
    /// </summary>
    /// <param name="Source">Image Data</param>
    /// <param name="ImageStyle">Wallpaper style</param>
    /// <remarks>Stream is neither closed nor rewound</remarks>
    /// <returns>true if sucessfully set</returns>
    public static bool Set(Stream Source, Style ImageStyle)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
        try
        {
            using (Image I = Image.FromStream(Source))
            {
                I.Save(tempPath, ImageFormat.Bmp);
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Source is not a valid Image", ex);
        }

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        switch(ImageStyle)
        {
            case Style.Fill:
                key.SetValue(@"WallpaperStyle", 10.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            case Style.Fit:
                key.SetValue(@"WallpaperStyle", 6.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            case Style.Span:
                key.SetValue(@"WallpaperStyle", 22.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            case Style.Stretch:
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            case Style.Tile:
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
                break;
            case Style.Center:
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            default:
                throw new ArgumentException("Invalid Wallpaper Style");
        }

        return SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
    }
}