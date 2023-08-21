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
    public partial class ShiftManagement : Form
    {
        public ShiftManagement()
        {
            InitializeComponent();
            fetchAndSetDetails();
        }

        private void fetchAndSetDetails()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            DataGridViewShift.Rows.Clear();

            String query = "select * from shift_table where Status=1";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach (DataRow eachrow in data.Tables[0].Rows)
            {
                String shiftId = eachrow[0].ToString();
                String loginId = eachrow[10].ToString();
                String userName = eachrow[13].ToString();
                String startTime = eachrow[12].ToString();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DataGridViewShift,shiftId, loginId, userName, startTime, txt_btn_endShift.Text = "End Shift");
                row.Height = 50;

                DataGridViewShift.Rows.Add(row);
            }

            connection.Close();
        }


        private void DataGridViewShift_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewShift.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            e.RowIndex >= 0) // Exclude header row
            {
                DataGridViewButtonCell cell = DataGridViewShift.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                // Apply padding to the button cell
                cell.Style.Padding = new Padding(10, 5, 10, 5); // Replace with your desired padding values
            }
        }

        private void DataGridViewShift_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell_loginId = DataGridViewShift.Rows[e.RowIndex].Cells[1];
            DataGridViewCell cell_shiftId = DataGridViewShift.Rows[e.RowIndex].Cells[0];

            if (DataGridViewShift.Columns[e.ColumnIndex].HeaderText == "Click Here")
            {
                Form popUp = new Form();
                try
                {
                    using (CloseShift pop = new CloseShift())
                    {

                        pop.fromShiftManagementScreen = true;
                        pop.shiftId = cell_shiftId.Value.ToString().Trim();
                        pop.loginId = cell_loginId.Value.ToString().Trim();

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
}
