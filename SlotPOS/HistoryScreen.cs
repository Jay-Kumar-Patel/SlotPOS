using iText.Layout.Element;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using SlotPOS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class HistoryScreen : Form
    {
        public HistoryScreen()
        {
            InitializeComponent();
            DTPto.Value = DateTime.Now;
            DTPFrom.Value = DateTime.Now.AddDays(-1);
            DTPFrom.MaxDate = DateTime.Today;
            DTPto.MaxDate = DateTime.Today;

            DTPto.Format = DateTimePickerFormat.Custom;
            DTPto.CustomFormat = "dd-MMM-yyyy";

            DTPFrom.Format = DateTimePickerFormat.Custom;
            DTPFrom.CustomFormat = "dd-MMM-yyyy";

        }

        private void HistoryScreen_Load(object sender, EventArgs e)
        {

        }

        private void HistoryScreen_Shown(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void LoadHistory()
        {
            FetchHistory();
        }

        private void FetchHistory()
        {
            Database dataBase = new Database();
            MySqlConnection connection = new MySqlConnection(dataBase.connString);

            DateTime fromDate = DTPFrom.Value;
            DateTime toDate = DTPto.Value;

            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 1);
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);

            // Build the SQL query
            String query = "SELECT t.DateAndTime AS Date_Time, u.User_Name as Username, t.Transaction_Type, FORMAT(t.Amount / 100, 2) AS Amount, t.Ticket_No, t.Machine_No " +
               "FROM Transactions t " +
               "JOIN users u ON t.User_ID = u.User_Id " +
               "WHERE t.DateAndTime BETWEEN @fromDate AND @toDate " +
               "ORDER BY t.DateAndTime DESC";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet data = new DataSet();
            adapter.Fill(data);


            DataGridViewHistory.DataSource = data.Tables[0];
            DataGridViewHistory.Columns["Date_Time"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewHistory.Columns["Username"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewHistory.Columns["Transaction_Type"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewHistory.Columns["Amount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewHistory.Columns["Ticket_No"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewHistory.Columns["Machine_No"].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void DataGridViewHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
            {
                e.Value = "-";
            }

            if (e.ColumnIndex == 3 && e.Value != null && e.Value != DBNull.Value)
            {
                decimal amount = Convert.ToDecimal(e.Value);
                e.Value = amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = DataGridViewHistory.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.BackColor = Color.FromArgb(224, 224, 224);
                cell.Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
            }

            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.Value.ToString().Contains("_"))
            {
                DataGridViewRow row = DataGridViewHistory.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[e.ColumnIndex];

                string[] parts = cell.Value.ToString().Split('_');

                string deletedTimestamp = parts[1];
                DateTime timestamp;
                if (DateTime.TryParseExact(deletedTimestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp))
                {
                    string formattedValue = $"{parts[0]} (Deleted: {timestamp.ToString("dd/MM/yy HH:mm:ss")})";
                    cell.Value = formattedValue;
                }
                cell.Style.ForeColor = Color.Red;
                cell.Style.SelectionForeColor = Color.Red;
            }
        }
    }
}
