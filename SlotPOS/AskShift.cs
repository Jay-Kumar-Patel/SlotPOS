using MySql.Data.MySqlClient;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class AskShift : Form
    {
        public Boolean success { get; set; }
        public AskShift()
        {
            InitializeComponent();
        }

        private void ButtonContinue_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.NewUser = false;
            success = true;
            this.Close();
        }

        private void ButtonEndShift_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (CloseShift pop = new CloseShift())
                {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }

            try
            {
                Database dataBase = new Database();
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
                    success = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
    }
}
