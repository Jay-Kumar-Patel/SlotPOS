using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using SlotPOS.Utils;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Hangfire.Storage.JobStorageFeatures;

namespace SlotPOS
{
    public partial class LoginScreen : Form
    {
        /* Initializing Variables. */
        private String password = "";
        private String hidden = "";
        private readonly int borderSize = 2;
        private Size formSize;
        public LoginScreen()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(41, 39, 40);
        }

        #region MovingScreen
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

        #endregion

        #region Common Utils
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        private void PanelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void LabelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Button Work
        private void Button_0_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "0";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "1";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "2";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "3";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "4";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "5";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "6";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "7";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "8";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            if (check(password))
            {
                password += "9";
                hidden += "*";
                LabelPassword.Text = hidden;
            }
        }

        private Boolean check(String password)
        {
            if (password.Length < 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_DEL_Click(object sender, EventArgs e)
        {
            if (password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                hidden = hidden.Substring(0, hidden.Length - 1);
                LabelPassword.Text = hidden;
            }
        }

        #endregion

        #region Checking
        private void Button_Cnfrm_Click(object sender, EventArgs e)
        {
            if (password.Length == 6)
            {
                Database dataBase = new Database();
                try
                {
                    //create the SqlConnection object
                    using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                    {
                        string query = $"SELECT * FROM Users WHERE User_Password = \"{PasswordHelper.ComputeSha256Hash(password)}\" and Is_Active=1;";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        conn.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string userId = dr.GetString("User_ID");
                                Properties.Settings.Default.UserID = userId;
                                Properties.Settings.Default.Save();

                                string userName = dr.GetString("User_Name");
                                Properties.Settings.Default.UserName = userName;
                                Properties.Settings.Default.Save();
                            }
                            
                            dr.Close();
                            conn.Close();

                            CheckAndAddShift();
                            FindCounting();
                        }
                        else
                        {
                            MessageBox.Show("Login Won't Work, Sorry", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dr.Close();
                            conn.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FindCounting()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = "SELECT Counting FROM store_details";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string result = reader["Counting"].ToString();
                            
                            Properties.Settings.Default.Counting = Convert.ToDateTime(result);
                        }
                        else
                        {
                            Properties.Settings.Default.Counting = Convert.ToDateTime("8:00:00");
                        }
                    }
                }
            }
        }

        private void CheckAndAddShift()
        {
            Database dataBase = new Database();

            using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
            {
                string query = $"SELECT * FROM shift_table WHERE Login_ID = {Properties.Settings.Default.UserID} and Status=1;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    String startTime = "";
                    while (dr.Read())
                    {
                        startTime = dr.GetString("Start_Time");
                    }

                    Form popUp = new Form();
                    try
                    {
                        using (AskShift pop = new AskShift())
                        {
                            pop.success = false;
                            popUp.StartPosition = FormStartPosition.Manual;
                            popUp.FormBorderStyle = FormBorderStyle.None;
                            popUp.Opacity = .70d;
                            popUp.BackColor = Color.Black;
                            popUp.WindowState = FormWindowState.Maximized;
                            popUp.TopMost = true;
                            popUp.Location = this.Location;
                            popUp.ShowInTaskbar = false;
                            popUp.Show();

                            pop.Owner = popUp;
                            pop.FormBorderStyle = FormBorderStyle.Fixed3D;
                            pop.ShowDialog();

                            popUp.Dispose();

                            if(pop.success)
                            {
                                //Hide this form.
                                this.Hide();

                                //Display DashBoardScreen.
                                DashBoardScreen dashBoardScreen = new();
                                dashBoardScreen.Show();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { popUp.Dispose(); }

                }
                else
                {
                    try
                    {
                        //create the SqlConnection object
                        using (MySqlConnection connect = new MySqlConnection(dataBase.connString))
                        {
                            String queryInsert = "INSERT INTO shift_table (Login_ID, User_Name) VALUES(@user_id, @user_name)";
                            MySqlCommand command = new MySqlCommand(queryInsert, connect);

                            command.Parameters.AddWithValue("@user_id", Properties.Settings.Default.UserID);
                            command.Parameters.AddWithValue("@user_name", Properties.Settings.Default.UserName);

                            connect.Open();
                            command.ExecuteNonQuery();
                            connect.Close();
                            Properties.Settings.Default.NewUser = true;

                            //Hide this form.
                            this.Hide();

                            //Display DashBoardScreen.
                            DashBoardScreen dashBoardScreen = new();
                            dashBoardScreen.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
     
            
        }

        #endregion

        #region Keyboard Working
        private void LoginScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                string buttonName = "Button_" + (e.KeyCode - Keys.NumPad0).ToString();
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
                {
                    button.PerformClick();
                }
            }
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                string buttonName = "Button_" + (e.KeyCode - Keys.D0).ToString();
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
                {
                    button.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string buttonName = "Button_Cnfrm";
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
                {
                    button.PerformClick();
                }

            }
            else if (e.KeyCode == Keys.Back)
            {
                Button_DEL.PerformClick();
            }
        }

        #endregion

    }
}
