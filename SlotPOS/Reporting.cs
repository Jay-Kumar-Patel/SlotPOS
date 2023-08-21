using iText.Layout.Element;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
using System.Windows.Forms.Design.Behavior;

namespace SlotPOS
{
    public partial class Reporting : Form
    {
        public Reporting()
        {
            InitializeComponent();
            fetchAndSetDetails();

            int totalWidth = DataGridViewReporting.Width;
            int column1Width = (int)(totalWidth * 0.6);
            int otherColumnsWidth = (totalWidth - column1Width) / 2;

            DataGridViewReporting.Columns[0].Width = column1Width;
            DataGridViewReporting.Columns[1].Width = otherColumnsWidth;
            DataGridViewReporting.Columns[2].Width = otherColumnsWidth;
        }

        private void fetchAndSetDetails()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            DataGridViewReporting.Rows.Clear();

            String query = "select * from owner_details";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow eachrow in data.Tables[0].Rows)
            {
                String email = eachrow[1].ToString();
                String is_daily = eachrow[2].ToString();
                String is_shift = eachrow[3].ToString();

                DataGridViewRow row;
                if (is_daily.Equals("1") && is_shift.Equals("1"))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewReporting, email, true, true);
                    row.Height = 50;
                }
                else if (is_daily.Equals("1") && is_shift.Equals("0"))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewReporting, email, true, false);
                    row.Height = 50;
                }
                else if (is_daily.Equals("0") && is_shift.Equals("1"))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewReporting, email, false, true);
                    row.Height = 50;
                }
                else
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewReporting, email, false, false);
                    row.Height = 50;
                }

                DataGridViewReporting.Rows.Add(row);

            }
            connection.Close();
        }

        private void DataGridViewReporting_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell_email = DataGridViewReporting.Rows[e.RowIndex].Cells[0];
            bool cell_daily = (bool)DataGridViewReporting.Rows[e.RowIndex].Cells[1].Value;
            bool cell_shift = (bool)DataGridViewReporting.Rows[e.RowIndex].Cells[2].Value;

            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);
            connection.Open();

            if (DataGridViewReporting.Columns[e.ColumnIndex].HeaderText == "Daily Report")
            {
                if (cell_daily)
                {
                    try
                    {
                        string query = "UPDATE owner_details SET isSlotReport=@is_daily where Owner_Mail=@owner_id";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@is_daily", 0);
                        cmd.Parameters.AddWithValue("@owner_id", cell_email.Value.ToString().Trim());
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }
                else
                {
                    string query = "UPDATE owner_details SET isSlotReport=@is_daily where Owner_Mail=@owner_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@owner_id", cell_email.Value.ToString());
                    cmd.Parameters.AddWithValue("@is_daily", 1);
                    cmd.ExecuteNonQuery();
                }
            }
            else if (DataGridViewReporting.Columns[e.ColumnIndex].HeaderText == "Shift Report")
            {
                if (cell_shift)
                {
                    string query = "UPDATE owner_details SET isShiftReport=@is_slot where Owner_Mail=@owner_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@owner_id", cell_email.Value.ToString());
                    cmd.Parameters.AddWithValue("@is_slot", 0);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query = "UPDATE owner_details SET isShiftReport=@is_slot where Owner_Mail=@owner_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@owner_id", cell_email.Value.ToString());
                    cmd.Parameters.AddWithValue("@is_slot", 1);
                    cmd.ExecuteNonQuery();
                }
            }

            connection.Close();
            fetchAndSetDetails();
        }

        private void ButtonAddEmail_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (AddOwner pop = new AddOwner())
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
            fetchAndSetDetails();
        }
    }
}
