using Microsoft.AspNetCore.Components.Forms;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Tsp;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class StoreInformation : Form
    {
        private Regex storeNameRegex = new Regex(@"^[A-Za-z0-9\s-]+$");

        public StoreInformation()
        {
            InitializeComponent();
            fetchAndSetDetails();

            DateTimePickerCounting.Format = DateTimePickerFormat.Custom;
            DateTimePickerCounting.CustomFormat = "HH:mm";
            DateTimePickerCounting.ShowUpDown = true;

            TextBoxName.AutoSize = false;
            TextBoxAddress.AutoSize = false;
            TextBoxCity.AutoSize = false;
            TextBoxState.AutoSize = false;
            TextBoxZipCode.AutoSize = false;
            DateTimePickerCounting.AutoSize = false;

        }

        private void fetchAndSetDetails()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            String query = "select * from store_details";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow row in data.Tables[0].Rows)
            {
                TextBoxName.Text = row[0].ToString();
                TextBoxAddress.Text = row[1].ToString();
                TextBoxCity.Text = row[2].ToString();
                TextBoxState.Text = row[3].ToString();
                TextBoxZipCode.Text = row[4].ToString();
                DateTimePickerCounting.Text = row[5].ToString();
            }
            connection.Close();
        }

        private void btn_confirm_storeDetails_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);
            connection.Open();

            // Check if any rows exist in the store_details table
            string countQuery = "SELECT COUNT(*) FROM store_details";
            MySqlCommand countCmd = new MySqlCommand(countQuery, connection);
            long rowCount = (long)countCmd.ExecuteScalar();

            if (rowCount == 0)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                // No rows exist, perform insertion
                string insertQuery = "INSERT INTO store_details (Name, Street_Address, City, State, Zip_Code, Counting) VALUES (@Store_Name, @Store_Address, @Store_City, @Store_State, @Store_ZipCode, @Store_Variable_Counting)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@Store_Name", string.Join(" ", TextBoxName.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                insertCmd.Parameters.AddWithValue("@Store_Address", string.Join(" ", TextBoxAddress.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                insertCmd.Parameters.AddWithValue("@Store_City", string.Join(" ", TextBoxCity.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                insertCmd.Parameters.AddWithValue("@Store_State", string.Join(" ", TextBoxState.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                insertCmd.Parameters.AddWithValue("@Store_ZipCode", TextBoxZipCode.Text.ToString());
                insertCmd.Parameters.AddWithValue("@Store_Variable_Counting", DateTimePickerCounting.Text.ToString());
                insertCmd.ExecuteNonQuery();
                Properties.Settings.Default.Counting = Convert.ToDateTime(DateTimePickerCounting.Text.ToString());
            }
            else
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                // Rows exist, perform update
                string updateQuery = "UPDATE store_details SET Name=@Store_Name, Street_Address=@Store_Address, City=@Store_City ,State=@Store_State, Zip_Code=@Store_ZipCode, Counting=@Store_Variable_Counting";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@Store_Name", string.Join(" ", TextBoxName.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                updateCmd.Parameters.AddWithValue("@Store_Address", string.Join(" ", TextBoxAddress.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                updateCmd.Parameters.AddWithValue("@Store_City", string.Join(" ", TextBoxCity.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                updateCmd.Parameters.AddWithValue("@Store_State", string.Join(" ", TextBoxState.Text.ToString().Split().Select(word => textInfo.ToTitleCase(word))));
                updateCmd.Parameters.AddWithValue("@Store_ZipCode", TextBoxZipCode.Text.ToString());
                updateCmd.Parameters.AddWithValue("@Store_Variable_Counting", DateTimePickerCounting.Text.ToString());
                updateCmd.ExecuteNonQuery();
                Properties.Settings.Default.Counting = Convert.ToDateTime(DateTimePickerCounting.Text.ToString());
            }

            connection.Close();

            SendMqttMessageStoreDetails(TextBoxName.Text.ToString(), TextBoxAddress.Text.ToString(), TextBoxCity.Text.ToString(), TextBoxState.Text.ToString(), TextBoxZipCode.Text.ToString());
            SendMqttMessageDateTime();

            this.Close();
        }

        private async Task SendMqttMessageStoreDetails(String name, String address, String city, String state, String zipCode)
        {
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
                .WithTcpServer("localhost", 1883)
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
                .WithTcpServer("localhost", 1883)
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

        private void TextBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') // Check if the key pressed is the backspace key
                return;

            if (!storeNameRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true; // Suppress the character input
            }
        }

        private void TextBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') // Check if the key pressed is the backspace key
                return;

            Regex streetAddressRegex = new Regex(@"^[A-Za-z0-9\s\-.,]+$");
            if (!streetAddressRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true; // Suppress the character input
            }
        }

        private void TextBoxCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') // Check if the key pressed is the backspace key
                return;

            Regex cityRegex = new Regex(@"^[A-Za-z\s\-]+$");
            if (!cityRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true; // Suppress the character input
            }
        }

        private void TextBoxState_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') // Check if the key pressed is the backspace key
                return;

            Regex stateRegex = new Regex(@"^[A-Za-z\s]+$");
            if (!stateRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true; // Suppress the character input
            }
        }

        private void TextBoxZipCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the character input
            }
        }
    }
}
