using MySql.Data.MySqlClient;
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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SlotPOS
{
    public partial class PendingTickets : Form
    {
        public PendingTickets()
        {
            InitializeComponent();
        }

        private void pendingTickets_Load(object sender, EventArgs e)
        {

            String machineNo = TicketScreen.machineNo;
            String currMachineNo;
            if (machineNo.Contains("_"))
            {
                string[] parts = machineNo.Split('_');
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


                LabelMachineNo.Text = $"Machine No.: {currMachineNo} \n({formattedTimestamp})";
            }
            else
            {
                currMachineNo = machineNo.ToString();
                LabelMachineNo.Text = "Machine No.: " + currMachineNo;
            }

            FetchGameName(machineNo);
            FetchTickets(machineNo);
            CreateHeading();
        }

        private void FetchGameName(String machineNo)
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);
            String query = $"select * from machine_details where Machine_No = \"{machineNo}\"";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            String GameName = "";
            foreach (DataRow row in data.Tables[0].Rows)
            {
                GameName += row[1].ToString();
            }

            LabelGameName.Text = "Game Name: " + GameName.ToUpperInvariant();
        }

        private void FetchTickets(String machineNo)
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);
            String query = $"select * from ticket_details where Machine_No = \"{machineNo}\" and Ticket_Status=0";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            bool ticketPending = false; 

            foreach (DataRow row in data.Tables[0].Rows)
            {
                if (!row[5].ToString().Equals("1"))
                {
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.Dock = DockStyle.Top;
                    tableLayoutPanel.Height = 60;
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                    Button Redeem = addButton(row[0].ToString());
                    Label TicketNo = addLabel(row[0].ToString());
                    decimal Ticketmount = (decimal)ulong.Parse(Convert.ToString(row[1])) / 100;
                    Label TicketAmount = addLabel(Ticketmount.ToString());
                    tableLayoutPanel.Controls.Add(TicketNo, 0, 0);
                    tableLayoutPanel.Controls.Add(TicketAmount, 1, 0);
                    tableLayoutPanel.Controls.Add(Redeem, 2, 0);
                    PanelTable.Controls.Add(tableLayoutPanel);
                    Redeem.Click += new System.EventHandler(this.ButtonClicked);

                    ticketPending = true;
                }
            }

            if (!ticketPending)
            {
                this.Close();
            }
        }

        private void ButtonClicked_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true; 
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;

            try
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                String query = "select * from ticket_details where Ticket_No=" + currentButton.Name;
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataSet data = new DataSet();
                adapter.Fill(data);

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    DateTime now = DateTime.Now;
                    String queryInsert = "INSERT INTO transactions (User_ID, Transaction_Type, Ticket_No, Machine_No, Amount, DateAndTime)" +
                        " VALUES (@UserId, @Transaction_Type, @Ticket_No, @Machine_No, @Amount, @DateAndTime)";
                    MySqlCommand command = new MySqlCommand(queryInsert, connection);

                    command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                    command.Parameters.AddWithValue("@Transaction_Type", "Cash_Out");
                    command.Parameters.AddWithValue("@Ticket_No", currentButton.Name);

                    command.Parameters.AddWithValue("@Machine_No", row[2].ToString());
                    command.Parameters.AddWithValue("@Amount", long.Parse(row[1].ToString()));
                    command.Parameters.AddWithValue("@DateAndTime", now);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    AddShiftValue(long.Parse(row[1].ToString()));
                    String queryUpdate = "UPDATE ticket_details SET Ticket_Status = 1 Where Ticket_No = " + decimal.Parse(currentButton.Name);
                    MySqlCommand commandUpdate = new MySqlCommand(queryUpdate, connection);

                    connection.Open();
                    commandUpdate.ExecuteNonQuery();


                    Properties.Settings.Default.Balance = Properties.Settings.Default.Balance - decimal.Parse(row[1].ToString()) / 100;


                    PanelTable.Controls.Clear();
                    FetchTickets(row[2].ToString());
                    CreateHeading();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction Insertion Failed" + ex);
            }
        }

        private void AddShiftValue(long amountM)
        {
            Database dataBase = new Database();
            using (MySqlConnection connection = new MySqlConnection(dataBase.connString))
            {
                connection.Open();

                string query = $"SELECT Slot_Cash_Out_Tickets FROM shift_table WHERE Login_ID = {Properties.Settings.Default.UserID} and Status=1;";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ulong existingMatchplay = reader.GetUInt64("Slot_Cash_Out_Tickets");


                    // Calculate the updated values
                    decimal updatedMatchplay = existingMatchplay + (ulong)amountM;

                    // Update the match_play and total_in columns in the database.
                    connection.Close();
                    string updateQuery = "UPDATE shift_table SET Slot_Cash_Out_Tickets = @updatedFill WHERE Login_ID = @userId AND Status = 1";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@updatedFill", updatedMatchplay);
                    updateCommand.Parameters.AddWithValue("@userId", Properties.Settings.Default.UserID);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        Label addLabel(String text)
        {
            Label currmachineNo = new Label();
            currmachineNo.Text = text;
            currmachineNo.AutoSize = false;
            currmachineNo.Dock = DockStyle.Fill;
            currmachineNo.TextAlign = ContentAlignment.MiddleCenter;
            currmachineNo.Font = new Font("Bookman Old Style", 12F, FontStyle.Italic, GraphicsUnit.Point);
            return currmachineNo;
        }

        Button addButton(String Label)
        {
            Button currButton = new Button();
            currButton.Text = "Redeem";
            currButton.Name = Label;
            currButton.FlatAppearance.BorderSize = 0;
            currButton.Dock = DockStyle.Fill;
            currButton.Margin = new Padding(30, 5, 30, 5);
            currButton.BackColor = Color.Green;
            currButton.ForeColor = Color.White;
            currButton.TextAlign = ContentAlignment.MiddleCenter;
            currButton.UseVisualStyleBackColor = false;
            currButton.PreviewKeyDown += ButtonClicked_PreviewKeyDown;
            return currButton;
        }

        private void CreateHeading()
        {
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Height = 60;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            Label Redeem1 = addLabel("Action");
            Label TicketNo1 = addLabel("Ticket Number");
            Label TicketAmount1 = addLabel("Ticket Amount ($)");
            tableLayoutPanel1.Controls.Add(TicketNo1, 0, 0);
            tableLayoutPanel1.Controls.Add(TicketAmount1, 1, 0);
            tableLayoutPanel1.Controls.Add(Redeem1, 2, 0);
            PanelTable.Controls.Add(tableLayoutPanel1);
        }

    }
}
