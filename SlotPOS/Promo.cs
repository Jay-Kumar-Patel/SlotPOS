﻿using MySql.Data.MySqlClient;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class Promo : Form
    {
        public int promo_max_amount { get; set; }

        public Promo()
        {
            InitializeComponent();
            TextBoxMachine.AutoSize = false;
            TextBoxAmount.AutoSize = false;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxMachine.Text))
            {
                Database dataBase = new Database();
                MySqlConnection connection = new MySqlConnection(dataBase.connString);
                connection.Open();

                String query = "SELECT * FROM machine_details WHERE Machine_No = @Machine_No";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Machine_No", ulong.Parse(TextBoxMachine.Text));

                MySqlDataReader dr = command.ExecuteReader();

                if (dr.HasRows && !string.IsNullOrEmpty(TextBoxAmount.Text) && decimal.Parse(TextBoxAmount.Text.ToString()) != 0)
                {
                    decimal amount = Convert.ToDecimal(TextBoxAmount.Text);
                    if (amount <= Convert.ToDecimal(promo_max_amount)/100)
                    {
                        connection.Close();
                        EnterPromoTransaction();
                        AddShiftValue();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Appropriate Amount. " + "Maximum Possible Value: $" + Convert.ToDecimal(promo_max_amount) / 100);
                    }
                }
                else if (!string.IsNullOrEmpty(TextBoxAmount.Text))
                {
                    MessageBox.Show("Amount cannot be zero.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dr.HasRows)
                {
                    MessageBox.Show("Please enter Proper Amount.");
                }
                else
                {
                    MessageBox.Show("Such Machine Doesn't Exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter Machine Number.");
            }
        }

        private void EnterPromoTransaction()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            connection.Open();

            String queryInsert = "INSERT INTO transactions (User_ID, Transaction_Type, Machine_No, Amount, DateAndTime)" +
                        " VALUES (@UserId, @Transaction_Type, @Machine_No, @Amount, @DateAndTime)";
            MySqlCommand command = new MySqlCommand(queryInsert, connection);

            string userId = Properties.Settings.Default.UserID;
            DateTime now = DateTime.Now;

            command.Parameters.AddWithValue("@UserId", ulong.Parse(userId));
            command.Parameters.AddWithValue("@Transaction_Type", "Promo");
            command.Parameters.AddWithValue("@Machine_No", ulong.Parse(TextBoxMachine.Text));
            command.Parameters.AddWithValue("@Amount", (ulong)(decimal.Parse(TextBoxAmount.Text) * 100));
            command.Parameters.AddWithValue("@DateAndTime", now);

            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddShiftValue()
        {
            Database dataBase = new Database();
            using (MySqlConnection connection = new MySqlConnection(dataBase.connString))
            {
                connection.Open();

                string query = $"SELECT Promo FROM shift_table WHERE Login_ID = {Properties.Settings.Default.UserID} and Status=1;";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ulong existingMatchplay = reader.GetUInt64("Promo");

                    decimal amount = Convert.ToDecimal(TextBoxAmount.Text) * 100;

                    // Calculate the updated values
                    decimal updatedMatchplay = existingMatchplay + (ulong)amount;

                    // Update the match_play and total_in columns in the database
                    connection.Close();
                    string updateQuery = "UPDATE shift_table SET Promo = @updatedPromo WHERE Login_ID = @userId AND Status = 1";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@updatedPromo", updatedMatchplay);
                    updateCommand.Parameters.AddWithValue("@userId", Properties.Settings.Default.UserID);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void TextBoxMachine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void Promo_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBoxMachine_KeyDown(object sender, KeyEventArgs e)
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
