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

namespace SlotPOS
{
    public partial class EditGroupName : Form
    {
        public EditGroupName()
        {
            InitializeComponent();
            LoadGroupData();
            ComboBoxOldGroup.AutoSize = false;
        }

        private void LoadGroupData()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                ComboBoxOldGroup.Items.Clear();

                string query = "SELECT Group_ID, Group_Name FROM machine_group";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string groupName = reader.GetString("Group_ID") + ". " + reader.GetString("Group_Name");
                            ComboBoxOldGroup.Items.Add(groupName);
                        }
                    }
                }

                if (ComboBoxOldGroup.Items.Count > 0)
                {
                    ComboBoxOldGroup.SelectedIndex = 0;
                }
            }
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxNewGroup.Text))
            {
                Database database = new Database();
                using (MySqlConnection connection = new MySqlConnection(database.connString))
                {
                    connection.Open();

                    string updateQuery = $"UPDATE machine_group SET Group_Name = @OldGroupName WHERE Group_ID = @NewGroupName";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {

                        updateCommand.Parameters.AddWithValue("@NewGroupName", ComboBoxOldGroup.SelectedItem.ToString().Split(".")[0]);
                        updateCommand.Parameters.AddWithValue("@OldGroupName", TextBoxNewGroup.Text);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Group name updated successfully!");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void TextBoxNewGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ComboBoxOldGroup_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBoxNewGroup_KeyDown(object sender, KeyEventArgs e)
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

        private void ButtonConfirm_KeyDown(object sender, KeyEventArgs e)
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
