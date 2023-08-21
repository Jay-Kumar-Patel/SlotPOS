using MySql.Data.MySqlClient;
using SlotPOS.Utils;

namespace SlotPOS
{
    public partial class SettingScreen : Form
    {
        Database database;
        MySqlConnection connection;

        public int promoAmount = 10000;
        public int matchplayAmount = 10000;


        public SettingScreen()
        {
            InitializeComponent();
        }

        private void ButtonStoreInfo_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (StoreInformation pop = new StoreInformation())
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
        }

        private void ButtonUserManagement_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (UserManagement pop = new UserManagement())
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
        }

        private void ButtonReporting_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (Reporting pop = new Reporting())
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
        }

        private void ButtonPromoSetup_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (PromoSetup pop = new PromoSetup())
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
        }

        private void ButtonShiftManagement_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (ShiftManagement pop = new ShiftManagement())
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
        }

        private void ButtonTruncate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to truncate the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                database = new Database();
                connection = new MySqlConnection(database.connString);
                emptyDailyReporting();
                deleteAllMachineTables();
                emptyTransactions();
                emptyTicketDetails();
                emptyMachineDetails();
                emptyMachineGroup();
                emptyShiftTable();
                emptyUsers();
                emptyOwnerDetails();
                resetPromoSetup();
                emptyStoreDetails();

                MessageBox.Show("Database truncated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void emptyDailyReporting()
        {
            String query = "DELETE FROM daily_reporting";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void deleteAllMachineTables()
        {
            List<String> machineNames = new List<string>();

            connection.Open();

            String query = "SELECT table_name FROM information_schema.tables WHERE table_schema='slotpayout'";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = reader["table_name"].ToString();

                    if (tableName.StartsWith("machine_") && !tableName.Equals("machine_details") && !tableName.Equals("machine_group"))
                    {
                        machineNames.Add(tableName);
                    }
                }
                connection.Close();
            }

            deleteMachines(machineNames);
        }

        private void deleteMachines(List<String> machineNames)
        {
            for (int i = 0; i < machineNames.Count; i++)
            {
                String queryforDeleteTable = "DROP TABLE " + machineNames[i];
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(queryforDeleteTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void emptyTransactions()
        {
            String query = "DELETE FROM transactions";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyTicketDetails()
        {
            String query = "DELETE FROM ticket_details";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyMachineDetails()
        {
            String query = "DELETE FROM machine_details";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyMachineGroup()
        {
            String query = "DELETE FROM machine_group";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyShiftTable()
        {
            String query = "DELETE FROM shift_table";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyUsers()
        {
            String query = "DELETE FROM users WHERE User_Type!=\"Technician\"";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


        private void emptyOwnerDetails()
        {
            String query = "DELETE FROM owner_details";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void resetPromoSetup()
        {
            String query1 = "UPDATE promo_setup SET Is_Promo=1, Is_MatchPlay=1, Promo_Amount=@promoAmount, Matchplay_Amount=@matchplayAmount";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query1, connection))
            {
                command.Parameters.AddWithValue("@promoAmount", promoAmount);
                command.Parameters.AddWithValue("@matchplayAmount", matchplayAmount);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void emptyStoreDetails()
        {
            String query = "DELETE FROM store_details";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


    }
}
