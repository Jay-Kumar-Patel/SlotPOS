using MySql.Data.MySqlClient;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SlotPOS
{
    public partial class AddOwner : Form
    {
        public AddOwner()
        {
            InitializeComponent();
            TextBoxEmail.AutoSize = false;
            TextBoxEmail.Height = 50;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            String email = TextBoxEmail.Text.ToString().Trim();


            if (email.Length != 0)
            {
                try
                {
                    var emailAddress = new MailAddress(email);
                    if(!checkOtheremail(TextBoxEmail.Text.ToString()))
                    {
                        Database dataBase = new Database();
                        try
                        {
                            using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                            {
                                String queryInsert = "INSERT INTO owner_details (Owner_Mail, isSlotReport, isShiftReport)" +
                                    " VALUES (@mail, 1, 1)";
                                MySqlCommand command = new MySqlCommand(queryInsert, conn);

                                command.Parameters.AddWithValue("@mail", email);

                                conn.Open();
                                command.ExecuteNonQuery();
                                conn.Close();

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
                        MessageBox.Show("Email already exists");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Please enter e-mail in proper format.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Email...");
            }
        }

        private bool checkOtheremail(string email)
        {
            bool passwordExists;
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM owner_details WHERE Owner_Mail = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    passwordExists = count > 0;
                }
            }

            return passwordExists;
        }

        private void TextBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] allowedCharacters = {
                'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J',
                'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T',
                'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z', '1', '2', '3', '4', '5', '6', '7', '8',
                '9', '0', '@', '.', '_', '+', '%'
            };
            
            char uppercaseChar = char.ToUpper(e.KeyChar);

            // Check if the pressed key is in the list of allowed characters
            if (!allowedCharacters.Contains(uppercaseChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
