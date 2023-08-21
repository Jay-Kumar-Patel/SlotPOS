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
    public partial class AddNewUser : Form
    {
        public AddNewUser()
        {
            InitializeComponent();
            TextBoxUsername.AutoSize = false;
            TextBoxPassword.AutoSize = false;
            TextBoxCnfrmPswd.AutoSize = false;

            TextBoxUsername.Height = 40;
            TextBoxPassword.Height = 40;
            TextBoxCnfrmPswd.Height = 40;

            ComboBoxType.SelectedIndex = 0;

        }

        private void ButtonAddUser_Click(object sender, EventArgs e)
        {
            String user_name = TextBoxUsername.Text.ToString();
            String type = ComboBoxType.SelectedItem.ToString();
            String password = TextBoxPassword.Text.ToString();
            String confirm_password = TextBoxCnfrmPswd.Text.ToString();

            if (user_name.Trim().Length != 0)
            {
                if (password.Trim().Length != 0 && confirm_password.Trim().Length != 0)
                {
                    if (password.Length != 6)
                    {
                        MessageBox.Show("Enter password of length 6");
                    }
                    else if (password.Trim().Equals(confirm_password.Trim()))
                    {
                        if (!checkOthersPassword(password))
                        {
                            Database dataBase = new Database();
                            try
                            {
                                using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                                {
                                    String queryInsert = "INSERT INTO users (User_Name, User_Password, User_Type, Is_Active)" +
                                        " VALUES (@user_name, @user_password, @user_type, @user_status)";
                                    MySqlCommand command = new MySqlCommand(queryInsert, conn);

                                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                                    command.Parameters.AddWithValue("@user_name", string.Join(" ", user_name.Split().Select(word => textInfo.ToTitleCase(word))));
                                    command.Parameters.AddWithValue("@user_password", PasswordHelper.ComputeSha256Hash(password));
                                    command.Parameters.AddWithValue("@user_type", type);
                                    command.Parameters.AddWithValue("@user_status", 1);

                                    conn.Open();
                                    command.ExecuteNonQuery();
                                    conn.Close();

                                    MessageBox.Show("User created successfully");

                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Someone has already claimed that password, enter different one.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password and Confirm Password Not Match...");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Password...");
                }
            }
            else
            {
                MessageBox.Show("Enter Username...");
            }

        }

        private bool checkOthersPassword(String textBoxPassword)
        {
            bool passwordExists;
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE User_Password = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password", PasswordHelper.ComputeSha256Hash(textBoxPassword));

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    passwordExists = count > 0;
                }
            }

            return passwordExists;

        }

        private void TextBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }

        private void ComboBoxType_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }

        private void TextBoxCnfrmPswd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
