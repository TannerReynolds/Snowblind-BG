using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SnowblindBG
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        public Form1()
        {
            SetStartup();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = false;

            Screen targetScreen = Screen.AllScreens.FirstOrDefault(s => s.Bounds.Width == 1280 && s.Bounds.Height == 1024);
            if (targetScreen != null)
            {
                this.Location = targetScreen.Bounds.Location;
                this.Size = targetScreen.Bounds.Size;
            }
            else
            {
                MessageBox.Show("No screen with resolution 1024x1024 found. Exiting.");
                Application.Exit();
            }
        }

        private void SetStartup()
        {
            try
            {
                // The path to the key where Windows looks for startup applications
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (rkApp != null)
                {
                    // Add the application to startup
                    rkApp.SetValue("YourAppName", Application.ExecutablePath.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        const int WS_EX_NOACTIVATE = 0x08000000;
        const int WS_EX_TOOLWINDOW = 0x00000080;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= (int)(WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return baseParams;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //IntPtr progman = FindWindow("Progman", null);
            //SetParent(this.Handle, progman);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
