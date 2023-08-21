using MySql.Data.MySqlClient;
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

namespace SlotPOS
{
    public partial class EditPassword : Form
    {
        public String user_id { get; set; }

        public EditPassword()
        {
            InitializeComponent();
            TextBoxPassword.AutoSize = false;
            TextBoxPassword.Height = 40;
        }

        private void ButtonEditPassword_Click(object sender, EventArgs e)
        {
            if (TextBoxPassword.Text.Length == 6)
            {
                if (!checkOthersPassword(TextBoxPassword.Text.ToString()))
                {
                    Database database = new Database();
                    MySqlConnection connection = new MySqlConnection(database.connString);
                    connection.Open();
                    string query = "UPDATE users SET User_Password=@newPassword where User_ID=@user_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@newPassword", PasswordHelper.ComputeSha256Hash(TextBoxPassword.Text.ToString()));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Password updated successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Someone has already claimed that password, enter different one.");
                }
            }
            else
            {
                MessageBox.Show("Please enter password of length 6");
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
    }
}
