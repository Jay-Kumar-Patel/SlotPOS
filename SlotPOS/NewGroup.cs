using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
    public partial class NewGroup : Form
    {
        public String machineNo { get; set; }
        public String gameName { get; set; }
        public String ipAddress { get; set; }

        public String from { get; set; }

        public Boolean operationDone { get; set; }

        public Boolean isIpAddressChange { get; set; }

        public NewGroup()
        {
            InitializeComponent();
            TextBoxAmount.AutoSize = false;
            operationDone = false;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxAmount.Text))
            {
                InsertGroup();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Group Name.");
            }
        }

        private void InsertGroup()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO machine_group (Group_Name) VALUES (@groupName); SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@groupName", TextBoxAmount.Text.ToString());

                    int lastInsertedId = Convert.ToInt32(command.ExecuteScalar());

                    if (from.Equals("Add"))
                    {
                        AddMachines(lastInsertedId);
                    }
                    else if (from.Equals("Edit"))
                    {
                        EditMachines(lastInsertedId);
                    }
                }
            }
        }

        private void EditMachines(int lastInsertedId)
        {
            Database dataBase = new Database();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(dataBase.connString))
                {
                    String queryInsert = "UPDATE machine_details SET Game_Name = @game_name, " +
                        "Group_ID = @Group_ID, Static_IP = @Static_IP Where Machine_No = @Machine_No;";
                    MySqlCommand command = new MySqlCommand(queryInsert, conn);

                    command.Parameters.AddWithValue("@Machine_No", machineNo);
                    command.Parameters.AddWithValue("@Game_Name", gameName);
                    command.Parameters.AddWithValue("@Group_ID", lastInsertedId);
                    command.Parameters.AddWithValue("@Static_IP", ipAddress);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                    operationDone = true;

                    if (isIpAddressChange)
                    {
                        SendMqttMessageDeleteFirstMessage("machine_" + machineNo);
                        SendMqttMessageDeleteSecondMessage(ipAddress);

                        SendMqttMessageAdd(ipAddress);
                        SendMqttMessageStoreDetails();
                        SendMqttMessageDateTime();
                    }

                    MessageBox.Show("Machine edited successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void AddMachines(int lastInsertedId)
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

                    command.Parameters.AddWithValue("@Machine_No", machineNo);
                    command.Parameters.AddWithValue("@Game_Name", gameName);
                    command.Parameters.AddWithValue("@Status", 1);
                    command.Parameters.AddWithValue("@Group_ID", lastInsertedId);
                    command.Parameters.AddWithValue("@Static_IP", ipAddress);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    CreateMachineTable();
                    InsertMachineDetail();

                    SendMqttMessageAdd(ipAddress);
                    SendMqttMessageStoreDetails();
                    SendMqttMessageDateTime();

                    MessageBox.Show("Machine added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed!!! " + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void InsertMachineDetail()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string machine_name = "machine_" + machineNo;
                string type_report = "daily_scheduled";
                string createTableQuery = $"INSERT INTO {machine_name} (Machine_No, totalIn, totalCreditCancel, coinIn, coinOut, jackpotMeter, $1Bill, $5Bill, $10Bill, $20Bill, $50Bill, $100Bill, Type) " +
                    $"VALUES ({machineNo},0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '{type_report}');";

                using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateMachineTable()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                // Open the connection
                connection.Open();

                string machine_name = "machine_" + machineNo;
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

                // Create a MySqlCommand object with the SQL statement and connection
                using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
                {

                    // Execute the SQL statement
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Drop_KeyDown(object sender, KeyEventArgs e)
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
