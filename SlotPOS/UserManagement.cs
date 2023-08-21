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
    public partial class UserManagement : Form
    {
        public UserManagement()
        {
            InitializeComponent();
            fetchAndSetDetails();
        }

        private void fetchAndSetDetails()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            DataGridViewUsers.Rows.Clear();

            String query = "select * from users where User_Type = \"Admin\" or User_Type = \"Manager\" or User_Type = \"Cashier\"";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow eachrow in data.Tables[0].Rows)
            {

                String id = eachrow[0].ToString();
                String name = eachrow[1].ToString();
                String type = eachrow[3].ToString();
                String status = eachrow[4].ToString();

                btn_user_editPassword.Text = "Edit Password";

                DataGridViewRow row;
                if (status.Equals("1"))
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewUsers, id, name, type, btn_user_editPassword.Text = "Edit Password", btn_user_status.Text = "Activated");
                    row.Height = 50;
                }
                else
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DataGridViewUsers, id, name, type, btn_user_editPassword.Text = "Edit Password", btn_user_status.Text = "De-Activated");
                    row.Height = 50;
                }

                DataGridViewUsers.Rows.Add(row);
            }
            connection.Close();
        }

        private void DataGridViewUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewUsers.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            e.RowIndex >= 0) // Exclude header row
            {
                DataGridViewButtonCell cell = DataGridViewUsers.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                // Apply padding to the button cell
                cell.Style.Padding = new Padding(10, 5, 10, 5); // Replace with your desired padding values
            }

            
        }

        private void DataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell clickedCellId = DataGridViewUsers.Rows[e.RowIndex].Cells[0];
            DataGridViewCell clickedCellStatus = DataGridViewUsers.Rows[e.RowIndex].Cells[4];


            if (DataGridViewUsers.Columns[e.ColumnIndex].HeaderText == "Edit Password")
            {

                Form popUp = new Form();
                try
                {
                    using (EditPassword pop = new EditPassword())
                    {
                        pop.user_id = clickedCellId.Value.ToString();

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
            else if (DataGridViewUsers.Columns[e.ColumnIndex].HeaderText == "Status")
            {
                Database database = new Database();
                MySqlConnection connection = new MySqlConnection(database.connString);
                connection.Open();
                string query = "UPDATE users SET Is_Active=@is_Active where User_ID=@user_id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", clickedCellId.Value.ToString());

                if (clickedCellStatus.Value.ToString().Equals("Activated"))
                {
                    cmd.Parameters.AddWithValue("@is_Active", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@is_Active", 1);
                }
                cmd.ExecuteNonQuery();
                connection.Close();
                fetchAndSetDetails();
            }
        }

        private void ButtonAddNewUser_Click(object sender, EventArgs e)
        {
            Form popUp = new Form();
            try
            {
                using (AddNewUser pop = new AddNewUser())
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
