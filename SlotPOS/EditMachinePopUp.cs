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
    public partial class EditMachinePopUp : Form
    {
        public String machineNo { get; set; }
        public String gameName { get; set; }
        public String ipAddress { get; set; }
        public String groupName { get; set; }


        public EditMachinePopUp(String mNo, String game, String ip, String group)
        {
            InitializeComponent();
            TextBoxGameName.AutoSize = false;
            TextBoxGameName.Height = 40;
            TextBoxStaticIP.AutoSize = false;
            TextBoxStaticIP.Height = 40;
            ComboBoxGroup.AutoSize = false;
            ComboBoxGroup.Height = 40;

            machineNo = mNo;
            gameName = game;
            ipAddress = ip;
            groupName = group;
            LabelMachineNumber.Text = machineNo;
            TextBoxGameName.Text = gameName;
            TextBoxStaticIP.Text = ipAddress;

            LoadGroupData();
        }

        private void LoadGroupData()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                ComboBoxGroup.Items.Clear();

                ComboBoxGroup.Items.Add("+ New Group");
                int i = 0;
                string query = "SELECT Group_ID, Group_Name FROM machine_group";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string groupNaming = reader.GetString("Group_ID") + ". " + reader.GetString("Group_Name");
                            ComboBoxGroup.Items.Add(groupNaming);
                            i++;
                            if (reader.GetString("Group_Name").Equals(groupName))
                            {
                                ComboBoxGroup.SelectedIndex = i;
                            }
                        }
                    }
                }
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (TextBoxGameName.Text.ToString() == gameName &&
                TextBoxStaticIP.Text.ToString() == ipAddress &&
                (ComboBoxGroup.SelectedIndex != 0 && groupName == ComboBoxGroup.SelectedItem.ToString().Split(". ")[1]))
            {
                MessageBox.Show("Nothing Changed, can't edited.");
            }
            else if (string.IsNullOrEmpty(TextBoxGameName.Text))
            {
                MessageBox.Show("Please enter proper Game Name.");
            }
            else if (string.IsNullOrEmpty(TextBoxStaticIP.Text))
            {
                MessageBox.Show("Please enter proper IP Address.");
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
                            pop.machineNo = LabelMachineNumber.Text.ToString();
                            pop.gameName = TextBoxGameName.Text.ToString();
                            pop.ipAddress = TextBoxStaticIP.Text.ToString();
                            pop.from = "Edit";

                            if (TextBoxStaticIP.Text.ToString() != ipAddress)
                            {
                                pop.isIpAddressChange = true;
                            }

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

                            if (pop.operationDone == true)
                            {
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { popUp.Dispose(); }

                }
                else
                {
                    if (TextBoxStaticIP.Text.ToString() == ipAddress)
                    {
                        EditMachine(false);
                    }
                    else
                    {
                        EditMachine(true);
                    }

                    this.Close();
                }
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
                    string query = $"SELECT * FROM machine_details WHERE Static_IP = \"{TextBoxStaticIP.Text}\" and Machine_No != \"{LabelMachineNumber.Text}\";";

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

        private void EditMachine(bool isIpAddressChange)
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

                    command.Parameters.AddWithValue("@Machine_No", LabelMachineNumber.Text);
                    command.Parameters.AddWithValue("@Game_Name", TextBoxGameName.Text);
                    command.Parameters.AddWithValue("@Group_ID", ComboBoxGroup.SelectedItem.ToString().Split(".")[0]);
                    command.Parameters.AddWithValue("@Static_IP", TextBoxStaticIP.Text);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    if (isIpAddressChange)
                    {
                        SendMqttMessageDeleteFirstMessage("machine_" + LabelMachineNumber.Text.ToString());
                        SendMqttMessageDeleteSecondMessage(TextBoxStaticIP.Text.ToString());

                        SendMqttMessageAdd(TextBoxStaticIP.Text.ToString());
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



        private void TextBoxGameName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxStaticIP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TextBoxStaticIP_TextChanged(object sender, EventArgs e)
        {
            string input = TextBoxStaticIP.Text;
            TextBoxStaticIP.Text = input.ToUpper();
            TextBoxStaticIP.SelectionStart = TextBoxStaticIP.Text.Length;
        }
    }
}
