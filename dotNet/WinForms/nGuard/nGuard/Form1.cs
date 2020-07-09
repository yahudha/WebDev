using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nGuard
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            try
            {
                Sleep.IsStarted = false;
                InitializeComponent();
                this.MaximizeBox = false;
                button1.Image = Properties.Resources.Start;
                button2.Image = Properties.Resources.mini;
                button3.Image = Properties.Resources.close;

                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! App will be automatically closed.");
            }
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    notifyIcon.Visible = true;


                    if (Sleep.IsStarted)
                    {
                        notifyIcon.Icon = nGuard.Properties.Resources.start_thick;
                        notifyIcon.Text = "Prevent Sleep Started";
                        notifyIcon.ShowBalloonTip(1000, "Minimized to Tray!", "Prevent Sleep is Started", new ToolTipIcon());
                    }
                    else
                    {
                        notifyIcon.Icon = nGuard.Properties.Resources.stop_thick;
                        notifyIcon.Text = "Prevent Sleep Stopped";
                        notifyIcon.ShowBalloonTip(1000, "Minimized to Tray!", "Prevent Sleep is Stopped", new ToolTipIcon());
                    }
                    //notifyIcon.ShowBalloonTip(1000);
                }
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to restart the Application.");
                Application.Restart();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to restart the Application.");
                Application.Restart();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sleep.IsStarted)
                {
                    Sleep.AllowSleep();
                    button1.Image = Properties.Resources.Start;
                }
                else
                {
                    button1.Image = Properties.Resources.Stop;
                    Sleep.PreventSleep();
                }
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to restart the Application.");
                Application.Restart();
            }
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to restart the Application.");
                Application.Restart();
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to restart the Application.");
                Application.Restart();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                this.Close();
                Environment.Exit(0);
            }
            catch
            {
                MessageBox.Show("We have ran into techincal glitch! We will try to close the Application.");
                Environment.Exit(0);

            }
        }
    }


    internal static class Sleep
    {

        public static void PreventSleep()
        {
            IsStarted = true;
            SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
        }

        public static void AllowSleep()
        {
            IsStarted = false;
            SetThreadExecutionState(ExecutionState.EsContinuous);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [Flags]
        private enum ExecutionState : uint
        {
            EsAwaymodeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }
        public static bool IsStarted { get; set; }


    }


}
