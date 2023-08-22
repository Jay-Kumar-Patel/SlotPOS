using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hangfire.Storage.JobStorageFeatures;

namespace SlotPOS
{
    public partial class SlotSetup : Form
    {
        public SlotSetup()
        {
            InitializeComponent();
            TextBoxMachineNo.AutoSize = false;
            TextBoxGameName.AutoSize = false;
            TextBoxIPAddress.AutoSize = false;
            LoadGroupData();
            LoadMachines();
        }

        private void LoadMachines()
        {
            Database database = new Database();

            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                string query = "SELECT m.Status as Status, m.Machine_No as Machine_No, m.Game_Name, m.Static_IP, g.Group_Name, m.Last_Cleared_On " +
               "FROM machine_details m " +
               "JOIN machine_group g ON m.Group_ID = g.Group_ID WHERE m.Is_Deleted=0";

                connection.Open();

                DataGridViewMachines.Rows.Clear();


                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Status = reader.GetString("Status");
                            string Machine_No = reader.GetString("Machine_No");
                            string Game_name = reader.GetString("Game_Name");
                            string Static_IP = reader.GetString("Static_IP");
                            string Group_Name = reader.GetString("Group_Name");
                            string Last_Cleared = reader.GetString("Last_Cleared_On");

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DataGridViewMachines, Status, Machine_No, Game_name, Static_IP, Group_Name, Last_Cleared);
                            row.Height = 50;

                            DataGridViewMachines.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void LoadGroupData()
        {
            DataGridViewImageCell imageCellTemplate = (DataGridViewImageCell)DataGridViewMachines.Columns["Edit"].CellTemplate;
            imageCellTemplate.Style.Padding = new Padding(13);

            DataGridViewImageCell clearCellTemplate = (DataGridViewImageCell)DataGridViewMachines.Columns["Clear"].CellTemplate;
            clearCellTemplate.Style.Padding = new Padding(8);

            DataGridViewImageCell deleteCellTemplate = (DataGridViewImageCell)DataGridViewMachines.Columns["Delete"].CellTemplate;
            deleteCellTemplate.Style.Padding = new Padding(12);

            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                ComboBoxGroup.Items.Clear();

                ComboBoxGroup.Items.Add("+ New Group");
                string query = "SELECT Group_ID, Group_Name FROM machine_group";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string groupName = reader.GetString("Group_ID") + ". " + reader.GetString("Group_Name");
                            ComboBoxGroup.Items.Add(groupName);
                        }
                    }
                }

                if (ComboBoxGroup.Items.Count > 0)
                {
                    ComboBoxGroup.SelectedIndex = 0;
                }
            }
        }
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxMachineNo.Text))
            {
                MessageBox.Show("Please enter proper Machine number.");
            }
            else if (string.IsNullOrEmpty(TextBoxGameName.Text))
            {
                MessageBox.Show("Please enter proper Game Name.");
            }
            else if (string.IsNullOrEmpty(TextBoxIPAddress.Text))
            {
                MessageBox.Show("Please enter proper IP Address.");
            }
            else if (!CheckMachineNumber())
            {
                MessageBox.Show("Machine already exists. Please change the previous one.");
            }
            else if (!CheckGameName())
            {

            }
            else if (!CheckIPAddress())
            {
                MessageBox.Show("IP_Address already exists. Please change the previous one.");
            }
            else
            {
                if (ComboBoxGroup.SelectedIndex == 0)
                {
                    Form popUp = new Form();
                    try
                    {
                        using (NewGroup pop = new NewGroup())
                        {
                            pop.machineNo = TextBoxMachineNo.Text.ToString();
                            pop.gameName = TextBoxGameName.Text.ToString();
                            pop.ipAddress = TextBoxIPAddress.Text.ToString();
                            pop.from = "Add";

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
                            pop.FormClosed += NewGroup_FormClosed;
                            pop.ShowDialog();

                            popUp.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { popUp.Dispose(); }
                    TextBoxIPAddress.Clear();
                    TextBoxGameName.Clear();
                    TextBoxMachineNo.Clear();
                    ComboBoxGroup.SelectedIndex = 0;
                }
                else
                {
                    AddMachine();
                    CreateMachineTable(TextBoxMachineNo.Text.ToString());
                    InsertMachineDetail(TextBoxMachineNo.Text.ToString());
                    TextBoxIPAddress.Clear();
                    TextBoxGameName.Clear();
                    TextBoxMachineNo.Clear();
                    ComboBoxGroup.SelectedIndex = 0;
                }

                SendMqttMessageAdd(TextBoxIPAddress.Text.ToString());
                SendMqttMessageStoreDetails();
                SendMqttMessageDateTime();
            }
        }

        private void NewGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadGroupData();
            LoadMachines();
        }

        private void InsertMachineDetail(string machineNo)
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string machine_name = "Machine_" + machineNo;
                string type_report = "daily_scheduled";
                string createTableQuery = $"INSERT INTO {machine_name} (Machine_No, totalIn, totalcreditcancel, coinIn, coinOut, jackpotmeter, $1Bill, $5Bill, $10Bill, $20Bill, $50Bill, $100Bill, Type) " +
                    $"VALUES ({machineNo},0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '{type_report}');";

                using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateMachineTable(string machineNo)
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                // Open the connection
                connection.Open();

                string machine_name = "Machine_" + machineNo;
                string createTableQuery = $"CREATE TABLE {machine_name} (" +
                            "time TIMESTAMP DEFAULT CURRENT_TIMESTAMP, " +
                            "number int(255) AUTO_INCREMENT, " +
                            "Machine_No VARCHAR(256) NOT NULL, " +
                            "totalIn int(255) NOT NULL, " +
                            "totalCreditCancel int(255) NOT NULL, " +
                            "coinIn int(255) NOT NULL, " +
                            "coinOut int(255) NOT NULL, " +
                            "jackpotMeter int(255) NOT NULL, " +
                            "$1Bill int(255) DEFAULT NULL, " +
                            "$5Bill int(255) DEFAULT NULL, " +
                            "$10Bill int(255) DEFAULT NULL, " +
                            "$20Bill int(255) DEFAULT NULL, " +
                            "$50Bill int(255) DEFAULT NULL, " +
                            "$100Bill int(255) DEFAULT NULL, " +
                            "Type VARCHAR(256) NOT NULL, " +
                            "PRIMARY KEY(number), " +
                            "FOREIGN KEY(Machine_No) REFERENCES machine_details(Machine_No) ON UPDATE CASCADE" +
                          ")";

                using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddMachine()
        {
            Database dataBase = new Database();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                {
                    String queryInsert = "INSERT INTO machine_details (Machine_No, Game_Name, Status, Group_ID, Static_IP)" +
                        " VALUES (@Machine_No, @Game_Name, @Status, @Group_ID, @Static_IP)";
                    MySqlCommand command = new MySqlCommand(queryInsert, conn);

                    command.Parameters.AddWithValue("@Machine_No", TextBoxMachineNo.Text);
                    command.Parameters.AddWithValue("@Game_Name", TextBoxGameName.Text);
                    command.Parameters.AddWithValue("@Status", 1);
                    command.Parameters.AddWithValue("@Group_ID", ComboBoxGroup.SelectedItem.ToString().Split(".")[0]);
                    command.Parameters.AddWithValue("@Static_IP", TextBoxIPAddress.Text);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Machine added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadMachines();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckIPAddress()
        {
            Database dataBase = new Database();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                {
                    string query = $"SELECT * FROM machine_details WHERE Static_IP = \"{TextBoxIPAddress.Text}\" and Is_Deleted=0";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        private bool CheckGameName()
        {
            return true;
        }

        private bool CheckMachineNumber()
        {
            Database dataBase = new Database();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                {
                    string query = $"SELECT * FROM machine_details WHERE Machine_No = \"{TextBoxMachineNo.Text}\";";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        private void TextBoxMachineNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxGameName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow backspace and colon (used in MAC addresses)
            if (!char.IsControl(e.KeyChar) && e.KeyChar != ':')
            {
                // Allow only hexadecimal characters (0-9, A-F)
                if (!char.IsDigit(e.KeyChar) && !((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')))
                {
                    e.Handled = true; // Suppress the character
                }
            }
        }

        private void TextBoxIPAddress_TextChanged(object sender, EventArgs e)
        {
            string input = TextBoxIPAddress.Text; 
            TextBoxIPAddress.Text = input.ToUpper();
            TextBoxIPAddress.SelectionStart = TextBoxIPAddress.Text.Length;
        }


        private void DataGridViewMachines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewMachines.Columns[e.ColumnIndex].HeaderText == "Edit"&&e.RowIndex>=0)
            {
                Form popUp = new Form();
                try
                {
                    using (EditMachinePopUp pop = new EditMachinePopUp(DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString(),
                        DataGridViewMachines.Rows[e.RowIndex].Cells["Game_Name"].Value.ToString(),
                        DataGridViewMachines.Rows[e.RowIndex].Cells["Static_IP"].Value.ToString(),
                        DataGridViewMachines.Rows[e.RowIndex].Cells["Group"].Value.ToString()
                        ))
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
                LoadMachines();
            }
            else if (DataGridViewMachines.Columns[e.ColumnIndex].HeaderText == "Clear" && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to clear machine "
                    + DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString() +
                    "?", "Confirming Clear...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SendMqttMessageClear(DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString());

                    ClearMachine(DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString());
                }
            }
            else if (DataGridViewMachines.Columns[e.ColumnIndex].HeaderText == "Delete" && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete machine "
                    + DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString() +
                    "?", "Confirming Deletion...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SendMqttMessageDeleteFirstMessage(DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString());
                    SendMqttMessageDeleteSecondMessage(DataGridViewMachines.Rows[e.RowIndex].Cells["Static_IP"].Value.ToString());

                    DeleteMachine(DataGridViewMachines.Rows[e.RowIndex].Cells["Machine_No"].Value.ToString());
                }
            }
        }

        private void ClearMachine(string machineNo)
        {
            int currentClearCounter = 0;
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                string fetchQuery = "SELECT Clear_Counter FROM machine_details WHERE Machine_No = @machineNo";
                using (MySqlCommand fetchCommand = new MySqlCommand(fetchQuery, connection))
                {
                    fetchCommand.Parameters.AddWithValue("@machineNo", machineNo);
                    connection.Open();
                    currentClearCounter = Convert.ToInt32(fetchCommand.ExecuteScalar());
                }

                // Increment the clear counter value
                currentClearCounter = currentClearCounter + 1;

                RenameAndCreateTable(machineNo, currentClearCounter);

                // Query to update the clear counter value by incrementing it by 1
                string updateQuery = "UPDATE machine_details SET Clear_Counter = @newClearCounter, Last_Cleared_On = @datenow WHERE Machine_No = @machineNo";
                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newClearCounter", currentClearCounter);
                    updateCommand.Parameters.AddWithValue("@machineNo", machineNo);
                    updateCommand.Parameters.AddWithValue("@datenow", DateTime.Now);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private void RenameAndCreateTable(string machineNo, int counter)
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = $"ALTER TABLE {"machine_" + machineNo} RENAME TO {"machine_" + machineNo + "_c" + counter}";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                CreateMachineTable(machineNo);
                InsertMachineDetail(machineNo);

                MessageBox.Show("Machine " + machineNo + " cleared successfully.");
                connection.Close();
            }
        }

        private void DeleteMachine(string machineNo)
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = "UPDATE machine_details SET Machine_No = @newMachineNo, Is_Deleted = 1 WHERE Machine_No = @oldMachineNo";

                string selectQuery = $"SELECT Clear_Counter FROM machine_details WHERE Machine_No=@machineNo;";
                using (MySqlCommand fetchCommand = new MySqlCommand(query, connection))
                {

                    String currentTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fetchCommand.Parameters.AddWithValue("@newMachineNo", machineNo + "_" + currentTimestamp);
                    fetchCommand.Parameters.AddWithValue("@oldMachineNo", machineNo);

                    long clearCounterValue = 0;

                    int rowsAffected = fetchCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Fetch the clear_counter value
                        using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                        {
                            selectCommand.Parameters.AddWithValue("@machineNo", machineNo + "_" + currentTimestamp);
                            using (MySqlDataReader reader = selectCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    clearCounterValue = Convert.ToInt64(reader["Clear_Counter"]);
                                }
                            }
                        }
                    }

                    ChangeAllTables(machineNo, clearCounterValue, currentTimestamp);
                    LoadMachines();
                }

                MessageBox.Show("Machine " + machineNo + " deleted successfully.");
                connection.Close();
            }

        }

        private void ChangeAllTables(string machineNo, long clear_Counter, String currentTimeStamp)
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = $"ALTER TABLE {"machine_" + machineNo} RENAME TO {"machine_" + machineNo + "_" + currentTimeStamp}";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            for (int i = 1; i <= clear_Counter; i++)
            {
                using (MySqlConnection connection = new MySqlConnection(database.connString))
                {
                    connection.Open();

                    string query = $"ALTER TABLE {"machine_" + machineNo + "_c" + i} RENAME TO {"machine_" + machineNo + "_c" + i + "_" + currentTimeStamp}";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }

        private void EditGroup_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (EditGroupName pop = new EditGroupName())
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
            LoadGroupData();
        }

        private async Task SendMqttMessageAdd(String ipAddress)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                operation = "add",
                macAddress = ipAddress
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("deviceOperation")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }

        private async Task SendMqttMessageDateTime()
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                dateTime = DateTime.Now.ToString()
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("setDateTime")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }


        private async Task SendMqttMessageStoreDetails()
        {

            String name = "", address = "", city = "", state = "", zipCode = "";

            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            String query = "select * from store_details";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow row in data.Tables[0].Rows)
            {
                name = row[0].ToString();
                address = row[1].ToString();
                city = row[2].ToString();
                state = row[3].ToString();
                zipCode = row[4].ToString();
            }
            connection.Close();

            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                location = name,
                address1 = address,
                address2 = city + ", " + state + ", " + zipCode
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("locationInfo")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }


        private async Task SendMqttMessageClear(String machineName)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                Machine_Number = machineName,
                Time = DateTime.Now.ToString()
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("machine_clear")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }



        private async Task SendMqttMessageDeleteFirstMessage(String machineName)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                Machine_Number = machineName,
                Time = DateTime.Now.ToString()
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("machine_delete")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }


        private async Task SendMqttMessageDeleteSecondMessage(String ipAddress)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var messageData = new
            {
                operation = "delete",
                macAddress = ipAddress
            };

            var messagePayload = JsonConvert.SerializeObject(messageData);

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.254", 1883)
                .WithCredentials("pos", "@lphaBeta01")
                .Build();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("deviceOperation")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(false)
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.PublishAsync(message);
            await mqttClient.DisconnectAsync();
        }

        
    }

}
