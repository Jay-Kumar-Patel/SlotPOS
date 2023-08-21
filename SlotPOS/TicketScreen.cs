using MySql.Data.MySqlClient;
using SlotPOS.Utils;
using System.Data;
using System.Net.Mail;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SlotPOS
{
    public partial class TicketScreen : Form
    {
        public static String machineNo = "";
        public static TicketScreen ticketScreen;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer MachineTimer;


        private int promoMaxAmount = 0;
        private int matchPlayMaxAmount = 0;
        public TicketScreen()
        {
            InitializeComponent();

            /* Timer for updating clock dynamically. */
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            /* Timer for updating machines periodically. */
            MachineTimer = new System.Windows.Forms.Timer();
            MachineTimer.Interval = 15000;
            MachineTimer.Tick += new EventHandler(MachineTimer_Tick);
            MachineTimer.Start();

            /* Removing Visibility on start time for logout buttons. */
            TextBoxSearch.AutoSize = false;
            ButtonTakeBreak.Visible = false;
            ButtonEndShift.Visible = false;
            ticketScreen = this;

            setPromoMatchPlaySetup();
        }

        private void setPromoMatchPlaySetup()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            String query = "select * from promo_setup";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow row in data.Tables[0].Rows)
            {
                String is_promo = row[0].ToString();
                promoMaxAmount = (int)row[1];
                String is_matchplay = row[2].ToString();
                matchPlayMaxAmount = (int)row[3];

                if (is_promo.Equals("0"))
                {
                    PanelPromo.Visible = false;
                }

                if (is_matchplay.Equals("0"))
                {
                    PanelMatchPlay.Visible = false;
                }
            }
            connection.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LabelDate.Text = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss");
        }

        private void MachineTimer_Tick(object sender, EventArgs e)
        {
            fetchMachineDetails();
        }

        Button addButton(String id)
        {
            int height = (flowLayoutPanel1.Width - 80) / 4;
            Button currButton = new Button();
            string currMachineNo;
            if (id.Contains("_"))
            {
                string[] parts = id.Split('_');
                currMachineNo = parts[0];

                string timestamp = parts[1];
                int year = int.Parse(timestamp.Substring(0, 4));
                int month = int.Parse(timestamp.Substring(4, 2));
                int day = int.Parse(timestamp.Substring(6, 2));
                int hour = int.Parse(timestamp.Substring(8, 2));
                int minute = int.Parse(timestamp.Substring(10, 2));
                int second = int.Parse(timestamp.Substring(12, 2));

                // Step 3: Format the date and time components
                string formattedTimestamp = $"{day:D2}/{month:D2}/{year} {hour:D2}:{minute:D2}:{second:D2}";


                currButton.Text = $"Machine - {currMachineNo} \n({formattedTimestamp})";
            }
            else
            {
                currMachineNo = id.ToString();
                currButton.Text = "Machine - " + currMachineNo;
            }

            currButton.FlatAppearance.BorderSize = 1;
            currButton.FlatAppearance.BorderColor = Color.Black;
            currButton.FlatStyle = FlatStyle.Flat;
            currButton.Font = new System.Drawing.Font("Bookman Old Style", 11.8F, FontStyle.Bold, GraphicsUnit.Point);
            currButton.Width = height;
            currButton.Height = height;
            currButton.ForeColor = Color.MidnightBlue;
            currButton.BackColor = Color.White;

            currButton.TextAlign = ContentAlignment.MiddleCenter;
            currButton.UseVisualStyleBackColor = false;
            currButton.Margin = new Padding(5);
            return currButton;
        }

        private void TicketScreen_Load(object sender, EventArgs e)
        {

        }

        private void TicketScreen_Shown(object sender, EventArgs e)
        {
            fetchMachineDetails();
            onlineMachines();
            GetHistoryWithHeading();
            CheckNewUser();
        }

        private void CheckNewUser()
        {
            if (Properties.Settings.Default.NewUser)
            {
                Form popUp = new Form();
                try
                {
                    using (InitialAmount pop = new InitialAmount())
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
                    LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
                    Properties.Settings.Default.NewUser = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { popUp.Dispose(); }
            }
            else
            {
                LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
            }
        }

        private void GetHistoryWithHeading()
        {
            FetchHistory();
        }

        private void FetchHistory()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            String query = "select TIME(DateAndTime) AS Time, Transaction_Type as Type, FORMAT(Amount / 100, 2) AS Amount, Machine_No from transactions ORDER BY DateAndTime DESC LIMIT 20";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            DataGridViewRecent.DataSource = data.Tables[0];
            DataGridViewRecent.Columns["Time"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewRecent.Columns["Type"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewRecent.Columns["Amount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewRecent.Columns["Machine_No"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void onlineMachines()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            String query = "select * from machine_details where Is_Deleted=0";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            int onlineMachine = 0;
            int totalMachine = 0;

            foreach (DataRow row in data.Tables[0].Rows)
            {
                totalMachine++;
                if (row[2].ToString() == "1")
                {
                    onlineMachine++;
                }
            }

            status.Text = onlineMachine.ToString();
            total.Text = totalMachine.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ticketNumber = TextBoxSearch.Text.Trim();

            TicketExistsInDatabase(ticketNumber);

            LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
            FetchHistory();
            TextBoxSearch.Clear();
        }

        private static void TicketExistsInDatabase(string ticketNumber)
        {
            bool ticketExists;

            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                string query = $"SELECT COUNT(*), Ticket_Amount, Machine_No FROM ticket_details WHERE Ticket_No = @TicketNo";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TicketNo", ticketNumber);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int rowCount = reader.GetInt32(0);
                            long ticket_amount = reader.GetInt64(1);
                            string machine_number = reader.GetString(2);

                            ticketExists = rowCount > 0;

                            connection.Close();
                            if (ticketExists)
                            {
                                DialogResult result = MessageBox.Show($"Ticket No.: {ticketNumber} \nTicket Amount: ${decimal.Parse(ticket_amount.ToString()) / 100} \nMachine Number: {machine_number} \nDo you want to redeem it?", "Redeem Ticket",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    DateTime now = DateTime.Now;
                                    String queryInsert = "INSERT INTO transactions (User_ID, Transaction_Type, Ticket_No, Machine_No, Amount, DateAndTime)" +
                                        " VALUES (@UserId, @Transaction_Type, @Ticket_No, @Machine_No, @Amount, @DateAndTime)";
                                    MySqlCommand command2 = new MySqlCommand(queryInsert, connection);

                                    command2.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                                    command2.Parameters.AddWithValue("@Transaction_Type", "Cash_Out");
                                    command2.Parameters.AddWithValue("@Ticket_No", ticketNumber);

                                    command2.Parameters.AddWithValue("@Machine_No", machine_number);
                                    command2.Parameters.AddWithValue("@Amount", ticket_amount);
                                    command2.Parameters.AddWithValue("@DateAndTime", now);

                                    connection.Open();
                                    command2.ExecuteNonQuery();
                                    connection.Close();

                                    String queryUpdate = "UPDATE ticket_details SET Ticket_Status = 1 Where Ticket_No = " + ticketNumber;
                                    MySqlCommand commandUpdate = new MySqlCommand(queryUpdate, connection);

                                    connection.Open();
                                    commandUpdate.ExecuteNonQuery();


                                    Properties.Settings.Default.Balance = Properties.Settings.Default.Balance - decimal.Parse(ticket_amount.ToString()) / 100;


                                }
                            }
                            else
                            {
                                MessageBox.Show($"Ticket {ticketNumber} does not exist in the database!");
                            }
                        }
                    }
                }
            }
        }


        void ButtonClicked(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            String entireText = currentButton.Text;
            if (entireText.Contains('('))
            {
                int indexFirstDash = entireText.IndexOf('-');
                int indexSecondDash = entireText.IndexOf('(', indexFirstDash + 1);
                string currMachineNo = entireText.Substring(indexFirstDash + 1, indexSecondDash - indexFirstDash - 1).Trim();

                // Step 2: Extract formatted timestamp
                int indexOpenParenthesis = entireText.IndexOf('(');
                int indexCloseParenthesis = entireText.IndexOf(')');
                string formattedTimestamp = entireText.Substring(indexOpenParenthesis + 1, indexCloseParenthesis - indexOpenParenthesis - 1).Trim();
                string convertedTimestamp = DateTime.ParseExact(formattedTimestamp, "dd/MM/yyyy HH:mm:ss", null).ToString("yyyyMMddHHmmss");

                machineNo = currMachineNo + "_" + convertedTimestamp;

            }
            else
            {
                machineNo = entireText.Substring(10);
            }

            if (currentButton != null)
            {
                Form popUp = new Form();
                try
                {
                    Properties.Settings.Default.Balance = decimal.Parse(LabelBalance.Text[1..], NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
                    using (PendingTickets pop = new PendingTickets())
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
                    LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
                    GetHistoryWithHeading();
                    fetchMachineDetails();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { popUp.Dispose(); }
            }
        }

        void fetchMachineDetails()
        {
            flowLayoutPanel1.Controls.Clear();
            Database dataBase = new Database();

            MySqlConnection connection = new MySqlConnection(dataBase.connString);
            string searchBoxText = TextBoxSearch.Text;
            if (TextBoxSearch.Text.Length > 3)
            {
                searchBoxText = "";
            }

            String query = $"select * from ticket_details WHERE Ticket_Status = 0 AND isPromoTicket = 0 AND Machine_No LIKE '%{searchBoxText}%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            HashSet<String> machinesPendingTickets = new HashSet<String>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                machinesPendingTickets.Add(row[2].ToString());
            }

            foreach (String i in machinesPendingTickets)
            {
                Button machine = addButton(i);
                flowLayoutPanel1.Controls.Add(machine);
                machine.Click += new System.EventHandler(this.ButtonClicked);
            }
        }

        #region Log Out Handling
        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            ShowButtons();
        }

        private void ShowButtons()
        {
            if (ButtonEndShift.Visible == false || ButtonTakeBreak.Visible == false)
            {
                ButtonEndShift.Visible = true;
                ButtonTakeBreak.Visible = true;
            }
            else
            {
                ButtonEndShift.Visible = false;
                ButtonTakeBreak.Visible = false;
            }
        }
        #endregion


        private void ButtonEndShift_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (CloseShift pop = new CloseShift())
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

                    if (pop.success)
                    {
                        GoToLoginScreen();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }

        }

        private void ButtonTakeBreak_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Balance = decimal.Parse(LabelBalance.Text.Substring(1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
            GoToLoginScreen();
        }


        private void GoToLoginScreen()
        {
            DashBoardScreen.dashBoardScreen.Close();

            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
        }

        private void ButtonMatchPlay_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                Properties.Settings.Default.Balance = decimal.Parse(LabelBalance.Text.Substring(1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
                using (MatchPlay pop = new MatchPlay())
                {
                    pop.matchPlay_max_amount = matchPlayMaxAmount;

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
                GetHistoryWithHeading();
                LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }
        }

        private void ButtonPromoCode_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (Promo pop = new Promo())
                {
                    pop.promo_max_amount = promoMaxAmount;

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
                GetHistoryWithHeading();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }
        }

        private void ButtonDrop_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                Properties.Settings.Default.Balance = decimal.Parse(LabelBalance.Text.Substring(1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
                using (Drop pop = new Drop())
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
                GetHistoryWithHeading();
                LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }
        }

        private void ButtonFill_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                Properties.Settings.Default.Balance = decimal.Parse(LabelBalance.Text.Substring(1), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
                using (Fill pop = new Fill())
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
                GetHistoryWithHeading();
                LabelBalance.Text = "$" + Properties.Settings.Default.Balance.ToString("N2", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { popUp.Dispose(); }
        }

        private void DataGridViewRecent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
            {
                e.Value = "-";
            }

            if (e.ColumnIndex == 3 && e.RowIndex >= 0 && e.Value.ToString().Contains("_"))
            {
                DataGridViewRow row = DataGridViewRecent.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[e.ColumnIndex];

                string[] parts = cell.Value.ToString().Split('_');

                string deletedTimestamp = parts[1];
                DateTime timestamp;
                if (DateTime.TryParseExact(deletedTimestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp))
                {
                    string formattedValue = $"{parts[0]} (Deleted)";
                    cell.Value = formattedValue;
                }
                cell.Style.ForeColor = Color.Red;
                cell.Style.SelectionForeColor = Color.Red;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxSearch.Text.Length > 0 && TextBoxSearch.Text.Length < 4 && MachineTimer.Enabled)
            {
                MachineTimer.Stop();
                fetchMachineDetails();
            }
            else if (TextBoxSearch.Text.Length > 0 && TextBoxSearch.Text.Length < 4)
            {
                fetchMachineDetails();
            }
            else
            {
                fetchMachineDetails();
                MachineTimer.Start();
            }
        }
    }
}

