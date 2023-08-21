using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class DashBoardScreen : Form
    {
        private int borderSize = 2;
        private Size formSize;
        public static DashBoardScreen dashBoardScreen;
        public DashBoardScreen()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(41, 39, 40);
            dashBoardScreen = this;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;

            #region Form Resize

            const int HTCLIENT = 1;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            // Resizing feature implementation.
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTTOP;
                            else
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize))
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTBOTTOM;
                            else
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)
                {
                    formSize = this.ClientSize;
                }
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void DashBoardScreen_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            TicketScreen ticketScreen = new TicketScreen();
            openChildForm(ticketScreen);
            activeForm = ticketScreen;
        }

        #region Fragment
        private Form? activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Adding Form to the PanelDesktop.
            PanelMain.Controls.Add(childForm);
            PanelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {
            openChildForm(new TicketScreen());
            ButtonHome.CustomBorderColor = Color.FromArgb(178, 8, 55);
            ButtonReports.CustomBorderColor = Color.Transparent;
            ButtonSettings.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderColor = Color.Transparent;
            ButtonUnknown.CustomBorderColor = Color.Transparent;
        }

        private void ButtonHistory_Click(object sender, EventArgs e)
        {
            openChildForm(new HistoryScreen());
            ButtonHome.CustomBorderColor = Color.Transparent;
            ButtonReports.CustomBorderColor = Color.Transparent;
            ButtonSettings.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderColor = Color.FromArgb(178, 8, 55);
            ButtonUnknown.CustomBorderColor = Color.Transparent;
        }

        private void ButtonReports_Click(object sender, EventArgs e)
        {
            openChildForm(new ReportScreen());
            ButtonReports.CustomBorderColor = Color.FromArgb(178, 8, 55);
            ButtonHome.CustomBorderColor = Color.Transparent;
            ButtonSettings.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderColor = Color.Transparent;
            ButtonUnknown.CustomBorderColor = Color.Transparent;
        }

        private void ButtonUnknown_Click(object sender, EventArgs e)
        {
            openChildForm(new SlotSetup());
            ButtonSettings.CustomBorderColor = Color.Transparent;
            ButtonReports.CustomBorderColor = Color.Transparent;
            ButtonHome.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderColor = Color.Transparent;
            ButtonUnknown.CustomBorderColor = Color.FromArgb(178, 8, 55);
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            openChildForm(new SettingScreen());
            ButtonSettings.CustomBorderColor = Color.FromArgb(178, 8, 55);
            ButtonReports.CustomBorderColor = Color.Transparent;
            ButtonHome.CustomBorderColor = Color.Transparent;
            ButtonHistory.CustomBorderColor = Color.Transparent;
            ButtonUnknown.CustomBorderColor = Color.Transparent;
        }

        #endregion

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


    }
}
