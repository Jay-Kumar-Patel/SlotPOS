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
    public partial class PromoSetup : Form
    {

        String is_promo;
        String promo_amount;
        String is_matchplay;
        String matchplay_amount;

        public PromoSetup()
        {
            InitializeComponent();
            TextBoxPromo.AutoSize = false;
            TextBoxPromo.Height = 50;

            TextBoxMatchPlay.AutoSize = false;
            TextBoxMatchPlay.Height = 50;

            CheckBoxPromo.FlatAppearance.MouseOverBackColor = Color.Snow;
            CheckBoxMatchPlay.FlatAppearance.MouseOverBackColor = Color.Snow;

            fetchAndSetDetails();
        }

        private void fetchAndSetDetails()
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
                is_promo = row[0].ToString();
                promo_amount = row[1].ToString();
                is_matchplay = row[2].ToString();
                matchplay_amount = row[3].ToString();

                if (is_promo.Equals("0"))
                {
                    LabelPromoAmount.Visible = false;
                    TextBoxPromo.Visible = false;
                    ButtonPromo.Visible = false;
                    CheckBoxPromo.Checked = false;
                    TextBoxPromo.Clear();
                    CheckBoxPromo.Text = "Disabled";
                    CheckBoxPromo.FlatAppearance.BorderColor = Color.Red;
                }
                else
                {
                    LabelPromoAmount.Visible = true;
                    TextBoxPromo.Visible = true;
                    ButtonPromo.Visible = true;
                    CheckBoxPromo.Checked = true;
                    TextBoxPromo.Text = Convert.ToString(Convert.ToDecimal(promo_amount) / 100);
                    CheckBoxPromo.Text = "Enabled";
                    CheckBoxPromo.FlatAppearance.BorderColor = Color.Green;
                }

                if (is_matchplay.Equals("0"))
                {
                    LabelMatchPlayAmount.Visible = false;
                    TextBoxMatchPlay.Visible = false;
                    ButtonMatchPlay.Visible = false;
                    CheckBoxMatchPlay.Checked = false;
                    TextBoxMatchPlay.Clear();
                    CheckBoxMatchPlay.Text = "Disabled";
                    CheckBoxMatchPlay.FlatAppearance.BorderColor = Color.Red;
                }
                else
                {
                    LabelMatchPlayAmount.Visible = true;
                    TextBoxMatchPlay.Visible = true;
                    ButtonMatchPlay.Visible = true;
                    CheckBoxMatchPlay.Checked = true;
                    TextBoxMatchPlay.Text = Convert.ToString(Convert.ToDecimal(matchplay_amount) / 100);
                    CheckBoxMatchPlay.Text = "Enabled";
                    CheckBoxMatchPlay.FlatAppearance.BorderColor = Color.Green;
                }

            }
            connection.Close();
        }

        private void ButtonPromo_Click(object sender, EventArgs e)
        {
            String amount = TextBoxPromo.Text.ToString().Trim();

            if (amount.Length != 0)
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                connection.Open();
                string query = "UPDATE promo_setup SET Promo_Amount=@amount";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(amount) * 100);
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Promo Amount Updated Successfully");
            }
            else
            {
                MessageBox.Show("Please Enter Amount...");
            }
        }

        private void ButtonMatchPlay_Click(object sender, EventArgs e)
        {
            String amount = TextBoxMatchPlay.Text.ToString().Trim();

            if (amount.Length != 0)
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                connection.Open();
                string query = "UPDATE promo_setup SET Matchplay_Amount=@amount";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(amount) * 100);
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Match Play Amount Updated Successfully");
            }
            else
            {
                MessageBox.Show("Please Enter Amount...");
            }
        }

        private void changeToDatabase(String type, int status)
        {
            if (type.Equals("promo"))
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                connection.Open();
                string query = "UPDATE promo_setup SET Is_Promo=@status_changed";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@status_changed", status);
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            else
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                connection.Open();
                string query = "UPDATE promo_setup SET Is_MatchPlay=@status_changed";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@status_changed", status);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            fetchAndSetDetails();
        }

        private void CheckBoxPromo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxPromo.Checked)
            {
                changeToDatabase("promo", 1);
            }
            else
            {
                changeToDatabase("promo", 0);
            }
        }

        private void CheckBoxMatchPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxMatchPlay.Checked)
            {
                changeToDatabase("matchPlay", 1);
            }
            else
            {
                changeToDatabase("matchPlay", 0);
            }
        }

        private void TextBoxPromo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
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

        private void TextBoxMatchPlay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((System.Windows.Forms.TextBox)sender).Text.Contains('.'))
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
    }
}
