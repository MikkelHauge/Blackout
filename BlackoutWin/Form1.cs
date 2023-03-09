using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Drawing2D;
using System;
using static System.TimeZoneInfo;


namespace BlackoutWin
{
    public partial class Blackout : Form
    {
        public static class Win32API
        {
            public const int SPI_SETDESKWALLPAPER = 20;
            public const int SPIF_UPDATEINIFILE = 0x01;
            public const int SPIF_SENDWININICHANGE = 0x02;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr GetForegroundWindow();


            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;

                public int Width { get { return Right - Left; } }
                public int Height { get { return Bottom - Top; } }
            }


        }
        private bool isHighLighted = false;
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPI_SETSLIDESHOW = 0x1018;
        private const int SS_USERSELECTED = 0x0004;
        private const int SPIF_SENDWININICHANGE = 0x02;
        private bool activated = false;
        private bool hasBeenFullscreen = false;

        string folderPath;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);


        public Blackout()
        {
            InitializeComponent();
            notifyIcon1.Text = "Blackout";
        }

        private void button1_Click(object sender, EventArgs e)
        { //"activate" knappen. den eneste knap!

            if (!activated)
            {
                activated = true;
                button1.Text = "Deactivate";
                detectFullscreenTimer.Enabled = true;
                detectFullscreenTimer.Start();
                notifyIcon1.Text = "Fullscreen detection started...";

            }
            else
            {
                activated = false;
                button1.Text = "Activate";
                detectFullscreenTimer.Enabled = false;
                detectFullscreenTimer.Stop();
                notifyIcon1.Text = "Fullscreen detection deactivated...";
            }
        }

        private void detectFullscreenTimer_Tick(object sender, EventArgs e)
        {
            bool fullscreenDetected = false;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                Rectangle bounds = Screen.AllScreens[i].Bounds;
                IntPtr handle = Win32API.GetForegroundWindow();
                if (Win32API.GetWindowRect(handle, out Win32API.Rect rect) && rect.Width == bounds.Width && rect.Height == bounds.Height)
                {
                    labelinfo.Text = "Fullscreen detection:\nFullscreen!\nSetting Wallpapers to black!";
                    fullscreenDetected = true;
                    SetWallpaperToBlack();
                    hasBeenFullscreen = true;
                }
            }
            if (!fullscreenDetected )
            {
                if(!hasBeenFullscreen){
                    labelinfo.Text = "Fullscreen detection:\nNot detected!";

                } else
                {
                    labelinfo.Text = "Fullscreen detection:\nNone!\nSetting Wallpapers to slideshow!";
                    hasBeenFullscreen = false;
                    SetWallpaperBackToSlideshow();
                }
            }
        }

        private void SetWallpaperToBlack()
        {
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "black.jpg";
            IntPtr imagePathPtr = Marshal.StringToHGlobalUni(imagePath);
            try
            {
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePathPtr, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            finally
            {
                Marshal.FreeHGlobal(imagePathPtr);
            }
        }

        private void SetWallpaperBackToSlideshow()
        {
            string[] wallpapers = Directory.GetFiles(folderPath);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\PerMonitorSettings", true);
            if (wallpapers.Length > 0)
            {
                // Set a random wallpaper for each monitor
  
                    int randomIndex = new Random().Next(0, wallpapers.Length);
                    string randomWallpaper = wallpapers[randomIndex];

                    // Set the wallpaper for the current screen
                    SetWallpaper(randomWallpaper);
            }
            else
            {
                // Handle the case where there are no wallpaper files
                labelinfo.Text = "Ingen wallpapers fundet i mappen:\n" + @"F:\Pictures\Wallpapers";
            }
            

        }

        private void SetWallpaper(string wallpaperPath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            // Set wallpaper style to "Fit"
            key.SetValue(@"WallpaperStyle", "10");

            // Set wallpaper tiling to "Tile"
            key.SetValue(@"TileWallpaper", "0");
            // Set the new wallpaper
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized; // Add this line
            this.ShowInTaskbar = false;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Blackout";
            notifyIcon1.BalloonTipText = "Blackout has been minimized to the system tray.\nYou can only close it from here.";

            shutDownProcedure(sender, e);
        }

        private void shutDownProcedure(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (FormWindowState.Minimized == this.WindowState)
                {
                    notifyIcon1.Visible = true;
                    if (!IsDisposed)
                    {
                        notifyIcon1.ShowBalloonTip(500);
                    }
                }
                else if (FormWindowState.Normal == this.WindowState)
                {
                    notifyIcon1.Visible = false;
                    this.ShowInTaskbar = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); // Restore the form
            this.WindowState = FormWindowState.Normal; // Restore the form's window state
            notifyIcon1.Visible = false; // Hide the system tray icon
        }

        private void restoreWindowMenuItem_click(object sender, EventArgs e)
        {
            this.Show(); // Restore the form
            this.WindowState = FormWindowState.Normal; // Restore the form's window state
            notifyIcon1.Visible = false; // Hide the system tray icon
        }

        private void closeApplicationSystemTray_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            activated = false; // deaktivér
            button1_Click(sender, e); // burde sætte wallpaper osv, til slideshow igen, fordi "activated" er false
            SetWallpaperBackToSlideshow(); // sætter slideshow i gang igen, hvis brugeren ikke selv gjorde det.
            Application.Exit(); // lukker programmet.
        }


        // highlighting af text, til activate-knappen
        private void highlighter(object sender, EventArgs e)
        {
            if (!isHighLighted)
            {
                button1.ForeColor = Color.White;
                isHighLighted = true;
            }
            else
            {
                button1.ForeColor = Color.DarkGray;
                isHighLighted = false;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            try
            {
                string settingsFile = Path.Combine(Application.StartupPath, "settings.txt");
                if (File.Exists(settingsFile))
                {
                    folderPath = File.ReadAllText(settingsFile);
                    label2.Text = "Loaded:\n" + folderPath;
                    

                }
                else
                {
                    // Prompt the user to choose a folder
                    using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            folderPath = dialog.SelectedPath;

                            try
                            {
                                //Pass the filepath and filename to the StreamWriter Constructor
                                StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, "settings.txt"));
                                //Write a line of text
                                sw.WriteLine(folderPath);
                                //Write a second line of text
                                sw.WriteLine("The folder for selecting a random wallpaper. Feel free to change this directly, within this file.\nJust remember to save!\nsaved using my amazing code!");
                                //Close the file
                                sw.Close();
                            }
                            catch (Exception eex)
                            {
                                Console.WriteLine("Exception: " + eex.Message);
                            }
                            finally
                            {
                                Console.WriteLine("Executing finally block.");
                            }
                            label2.Text = "Will be looking for wallpapers in:\n" + folderPath;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}