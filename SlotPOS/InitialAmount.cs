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
    public partial class InitialAmount : Form
    {
        public InitialAmount()
        {
            InitializeComponent();
            TextBoxAmount.AutoSize = false;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxAmount.Text))
            {
                EnterStartingDrawerTransaction();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Amount.");
            }
        }

        private void EnterStartingDrawerTransaction()
        {
            try
            {
                Database dataBase = new Database();
                using (MySqlConnection connection = new MySqlConnection(dataBase.connString))
                {
                    connection.Open();
                    string query = "UPDATE shift_table SET Starting_Drawer = @Expense WHERE Login_ID = @UserId AND Status = @Status";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Expense", (ulong)(Decimal.Parse(TextBoxAmount.Text) * 100));
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                        command.Parameters.AddWithValue("@Status", 1);

                        Properties.Settings.Default.Balance = decimal.Parse(TextBoxAmount.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);

                        int rowsAffected = command.ExecuteNonQuery();

                        this.Close();
                    }
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

        private void InitialAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string buttonName = "ButtonConfirm";
                Control[] foundButtons = this.Controls.Find(buttonName, true);
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
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
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
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
                if (foundButtons.Length > 0 && foundButtons[0] is Button button)
                {
                    button.PerformClick();
                }
            }
        }
    }
}
