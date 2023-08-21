using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SlotPOS
{
    public partial class CloseShift : Form
    {
        public bool fromShiftManagementScreen { get; set; }
        public String shiftId { get; set; }
        public String loginId { get; set; }

        public Boolean success { get; set; }

        public CloseShift()
        {
            InitializeComponent();
            TextBoxAmount.AutoSize = false;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxAmount.Text))
            {
                EnterExpenseTransaction();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Amount.");
            }
        }

        private void EnterExpenseTransaction()
        {
            try
            {
                Database dataBase = new Database();
                using (MySqlConnection connection = new MySqlConnection(dataBase.connString))
                {
                    connection.Open();
                    if (fromShiftManagementScreen)
                    {
                        string query = "UPDATE shift_table SET Expense = @Expense, Status = 0, End_Time = @EndTime WHERE Login_ID = @loginId AND Shift_ID = @shiftId AND Status = @Status";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Expense", (ulong)(Decimal.Parse(TextBoxAmount.Text) * 100));
                            command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                            command.Parameters.AddWithValue("@Status", 1);
                            command.Parameters.AddWithValue("@EndTime", DateTime.Now);
                            command.Parameters.AddWithValue("@loginId", loginId);
                            command.Parameters.AddWithValue("@shiftId", shiftId);

                            command.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        string query = "UPDATE shift_table SET Expense = @Expense, Status = 0, End_Time = @EndTime WHERE Login_ID = @UserId AND Status = @Status";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Expense", (ulong)(Decimal.Parse(TextBoxAmount.Text) * 100));
                            command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                            command.Parameters.AddWithValue("@Status", 1);
                            command.Parameters.AddWithValue("@EndTime", DateTime.Now);

                            int rowsAffected = command.ExecuteNonQuery();

                            success = true;

                            
                        }
                    }
                    connection.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private void TextBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((System.Windows.Forms.TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }

            if (((System.Windows.Forms.TextBox)sender).Text.Contains("."))
            {
                string[] parts = ((System.Windows.Forms.TextBox)sender).Text.Split('.');
                if (parts.Length > 1 && parts[1].Length >= 2 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void CloseShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string buttonName = "ButtonConfirm";
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is System.Windows.Forms.Button button)
                {
                    button.PerformClick();
                }
            }
        }

        private void ButtonConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string buttonName = "ButtonConfirm";
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is System.Windows.Forms.Button button)
                {
                    button.PerformClick();
                }
            }
        }

        private void TextBoxAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string buttonName = "ButtonConfirm";
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is System.Windows.Forms.Button button)
                {
                    button.PerformClick();
                }
            }
        }
    }
}
