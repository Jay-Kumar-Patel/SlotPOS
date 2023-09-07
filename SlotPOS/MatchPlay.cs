using MySql.Data.MySqlClient;
using SlotPOS.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class MatchPlay : Form
    {
        public int matchPlay_max_amount { get; set; }
        public MatchPlay()
        {
            InitializeComponent();
            //TextBoxMachine.AutoSize = false;
            //TextBoxAmount.AutoSize = false;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {

            //Location Fetch
            //Ticket Amount String create 20 dollar
            //Call Printer Class
            //Return value parse in ulong

            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            String query = "select * from store_details";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            String storeName = null;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                storeName = row[0].ToString();
            }
            connection.Close();

            String ticketnumber = Printer.Receipt("40.00", storeName);

            ulong result;

            if (ulong.TryParse(ticketnumber, out result))
            {
                EnterMatchPlayTransaction();
                EnterCashInTransaction();
                Properties.Settings.Default.Balance = Properties.Settings.Default.Balance + decimal.Parse(result.ToString().Trim());

                AddShiftValue();
            }
            else
            {
                MessageBox.Show("Please try again later!");
            }

            this.Close();


            /*if (!string.IsNullOrEmpty(TextBoxMachine.Text))
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
                    if (amount <= Convert.ToDecimal(matchPlay_max_amount) / 100)
                    {
                        connection.Close();
                        EnterMatchPlayTransaction();
                        EnterCashInTransaction();
                        Properties.Settings.Default.Balance = Properties.Settings.Default.Balance + decimal.Parse(TextBoxAmount.Text);

                        AddShiftValue();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Appropriate Amount. " + "Maximum Possible Value: $" + Convert.ToDecimal(matchPlay_max_amount) / 100);
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
            }*/
        }

        private void EnterMatchPlayTransaction()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            connection.Open();

            String queryInsert = "INSERT INTO transactions (User_ID, Transaction_Type, Amount, DateAndTime)" +
                        " VALUES (@UserId, @Transaction_Type, @Amount, @DateAndTime)";
            MySqlCommand command = new MySqlCommand(queryInsert, connection);

            string userId = Properties.Settings.Default.UserID;
            DateTime now = DateTime.Now;

            command.Parameters.AddWithValue("@UserId", ulong.Parse(userId));
            command.Parameters.AddWithValue("@Transaction_Type", "Match_Play");
            //command.Parameters.AddWithValue("@Machine_No", "");
            command.Parameters.AddWithValue("@Amount", (ulong)(20 * 100));
            command.Parameters.AddWithValue("@DateAndTime", now);

            command.ExecuteNonQuery();
            connection.Close();
        }

        private void EnterCashInTransaction()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            connection.Open();

            String queryInsert = "INSERT INTO transactions (User_ID, Transaction_Type, Amount, DateAndTime)" +
                        " VALUES (@UserId, @Transaction_Type, @Amount, @DateAndTime)";
            MySqlCommand command = new MySqlCommand(queryInsert, connection);

            string userId = Properties.Settings.Default.UserID;
            DateTime now = DateTime.Now;

            command.Parameters.AddWithValue("@UserId", ulong.Parse(userId));
            command.Parameters.AddWithValue("@Transaction_Type", "Cash_In");
            //command.Parameters.AddWithValue("@Machine_No", "");
            command.Parameters.AddWithValue("@Amount", (20 * 100));
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

                string query = $"SELECT Total_In, Match_Play FROM shift_table WHERE Login_ID = {Properties.Settings.Default.UserID} and Status=1;";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ulong existingMatchplay = reader.GetUInt64("Match_Play");
                    ulong existingTotalIn = reader.GetUInt64("Total_In");

                    decimal amount = Convert.ToDecimal(20 * 100);

                    // Calculate the updated values
                    decimal updatedMatchplay = existingMatchplay + (ulong)amount;
                    decimal updatedTotalIn = existingTotalIn + (ulong)amount;

                    // Update the match_play and total_in columns in the database
                    connection.Close();
                    string updateQuery = "UPDATE shift_table SET Match_Play = @updatedMatchplay, Total_In = @updatedTotalIn WHERE Login_ID = @userId AND Status = 1";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@updatedMatchplay", updatedMatchplay);
                    updateCommand.Parameters.AddWithValue("@updatedTotalIn", updatedTotalIn);
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

        private void MatchPlay_KeyDown(object sender, KeyEventArgs e)
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
    }
}
