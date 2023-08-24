
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using PdfSharp.Pdf.IO;
using SlotPOS.Utils;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SlotPOS
{
    public partial class ReportScreen : Form
    {
        private PrintDocument printDocument;
        private int currentGridIndex = 0;

        private DataGridView[] dataGridViews;

        DataGridView dataGridViewDaily = new DataGridView();
        DataGridView dataGridViewMaster = new DataGridView();
        DataGridView dataGridViewDeleted = new DataGridView();
        DataGridView dataGridViewCleared = new DataGridView();
        DataGridView dataGridViewShift = new DataGridView();

        public ReportScreen()
        {
            InitializeComponent();
            ComboBoxReport.Items.Add("Slot Report");
            ComboBoxReport.Items.Add("Shift Report");


            // Set the initial selected item
            ComboBoxReport.SelectedIndex = 0;

            DTPto.Value = DateTime.Now;
            DTPFrom.Value = DateTime.Now.AddDays(-1);
            DTPFrom.MaxDate = DateTime.Today;
            DTPto.MaxDate = DateTime.Today.AddDays(1);

            DTPto.Format = DateTimePickerFormat.Custom;
            DTPto.CustomFormat = "dd-MMM-yyyy";

            DTPFrom.Format = DateTimePickerFormat.Custom;
            DTPFrom.CustomFormat = "dd-MMM-yyyy";

            // Initialize the PrintDocument and its event handler.
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            PanelPDF.Controls.Clear();
            int selectedIndex = ComboBoxReport.SelectedIndex;
            DateTime fromDate = DTPFrom.Value;
            DateTime toDate = DTPto.Value;

            DateTime fromTime = Convert.ToDateTime(Properties.Settings.Default.Counting);

            if (selectedIndex == 0)
            {
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, fromTime.Hour, fromTime.Minute, fromTime.Second);
                toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, fromTime.Hour, fromTime.Minute, fromTime.Second);

                GenerateSlotPDF(fromDate, toDate);
            }
            else
            {
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, fromTime.Hour, fromTime.Minute, fromTime.Second);
                toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, fromTime.Hour, fromTime.Minute, fromTime.Second);

                GenerateShiftPDF(fromDate, toDate);
            }
        }



        private void GenerateSlotPDF(DateTime start, DateTime end)
        {
            String startDate = start.ToString("yyyy-MM-dd HH:mm:ss");
            String endDate = end.ToString("yyyy-MM-dd HH:mm:ss");

            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            List<customRowDailyReport> dailyReport = new List<customRowDailyReport>();
            List<customRowDailyReport> dailyReportCleared = new List<customRowDailyReport>();
            List<customRowDailyReport> dailyReportDeleted = new List<customRowDailyReport>();

            String query = "SELECT d.indexing, d.Date_Time, d.Machine_No, d.Total_In, d.Total_Out, " +
                "d.jackpot_Meter, d.Total_Net, d.Coin_In, d.Coin_Out, d.Coin_Net, d.Type, g.Group_Name, m.Game_Name FROM daily_reporting d, machine_details m, machine_group g WHERE g.Group_ID = m.Group_ID and m.Machine_No = d.Machine_No and d.Date_Time > '" + startDate + "' AND d.Date_Time <= '" + endDate + "' ORDER BY g.Group_Name, d.Machine_No, d.Date_Time";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            connection.Close();
            customRowDailyReport localData = new customRowDailyReport(
                "", "", "", 0, 0, 0, 0, 0, 0, 0
                );

            if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 1)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow currentRow = data.Tables[0].Rows[i];
                    DataRow nextRow = data.Tables[0].Rows[i + 1];

                    String currMachine = (String)currentRow[2];
                    String nextMachine = (String)nextRow[2];

                    localData.machineNo = currMachine;
                    localData.totalIn += long.Parse(currentRow[3].ToString());
                    localData.totalOut += long.Parse(currentRow[4].ToString());
                    localData.jackPotMeter += long.Parse(currentRow[5].ToString());
                    localData.totalNet += long.Parse(currentRow[6].ToString());
                    localData.coinIn += long.Parse(currentRow[7].ToString());
                    localData.coinOut += long.Parse(currentRow[8].ToString());
                    localData.coinNet += long.Parse(currentRow[9].ToString());
                    localData.gameName = (String)currentRow[12];
                    localData.groupName = (String)currentRow[11];

                    if (currentRow[10].ToString().Equals("clear_entry"))
                    {
                        dailyReportCleared.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));

                        localData.machineNo = "";
                        localData.totalIn = 0;
                        localData.totalOut = 0;
                        localData.jackPotMeter = 0;
                        localData.totalNet = 0;
                        localData.coinIn = 0;
                        localData.coinOut = 0;
                        localData.coinNet = 0;
                        localData.gameName = "";
                        localData.groupName = "";
                    }
                    else if (currentRow[10].ToString() == "delete_entry")
                    {
                        dailyReportDeleted.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));
                        localData.machineNo = "";
                        localData.totalIn = 0;
                        localData.totalOut = 0;
                        localData.jackPotMeter = 0;
                        localData.totalNet = 0;
                        localData.coinIn = 0;
                        localData.coinOut = 0;
                        localData.coinNet = 0;
                        localData.gameName = "";
                        localData.groupName = "";
                    }
                    else if (!currMachine.Equals(nextMachine))
                    {
                        dailyReport.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));
                        localData.machineNo = "";
                        localData.totalIn = 0;
                        localData.totalOut = 0;
                        localData.jackPotMeter = 0;
                        localData.totalNet = 0;
                        localData.coinIn = 0;
                        localData.coinOut = 0;
                        localData.coinNet = 0;
                        localData.gameName = "";
                        localData.groupName = "";
                    }
                }


                DataRow row = data.Tables[0].Rows[data.Tables[0].Rows.Count - 1];

                localData.machineNo = row[2].ToString();
                localData.totalIn += long.Parse(row[3].ToString());
                localData.totalOut += long.Parse(row[4].ToString());
                localData.jackPotMeter += long.Parse(row[5].ToString());
                localData.totalNet += long.Parse(row[6].ToString());
                localData.coinIn += long.Parse(row[7].ToString());
                localData.coinOut += long.Parse(row[8].ToString());
                localData.coinNet += long.Parse(row[9].ToString());
                localData.gameName = (String)row[12];
                localData.groupName = (String)row[11];

                if (row[10].ToString() == "daily_scheduled")
                {
                    dailyReport.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));
                }
                else if (row[10].ToString() == "clear_entry")
                {
                    dailyReportCleared.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));
                }
                else
                {
                    dailyReportDeleted.Add(new customRowDailyReport(
                            localData.machineNo,
                            localData.groupName,
                            localData.gameName,
                            localData.totalIn,
                            localData.totalOut,
                            localData.jackPotMeter,
                            localData.totalNet,
                            localData.coinIn,
                            localData.coinOut,
                            localData.coinNet
                            ));
                }
            }
            else if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count == 1)
            {
                DataRow row = data.Tables[0].Rows[0];

                customRowDailyReport temp_class = new customRowDailyReport(
                    row[2].ToString(),
                    row[12].ToString(),
                    row[11].ToString(),
                    long.Parse(row[3].ToString()),
                    long.Parse(row[4].ToString()),
                    long.Parse(row[5].ToString()),
                    long.Parse(row[6].ToString()),
                    long.Parse(row[7].ToString()),
                    long.Parse(row[8].ToString()),
                    long.Parse(row[9].ToString())
                    );

                if (data.Tables[0].Rows[0][10].ToString() == "daily_scheduled")
                {
                    dailyReport.Add(temp_class);
                }
                else if (data.Tables[0].Rows[0][10].ToString() == "clear_entry")
                {
                    dailyReportCleared.Add(temp_class);
                }
                else
                {
                    dailyReportDeleted.Add(temp_class);
                }
            }


            connection.Open();

            String exquery = "select * from machine_details ORDER BY Group_ID";
            MySqlDataAdapter adapterex = new MySqlDataAdapter(exquery, connection);
            DataSet dataex = new DataSet();
            adapterex.Fill(dataex);

            ArrayList arrayList = new ArrayList();
            foreach (DataRow row in dataex.Tables[0].Rows)
            {
                String machineNumber = row[0].ToString();

                if (!machineNumber.Contains('_'))
                {
                    arrayList.Add(machineNumber);
                }
            }
            connection.Close();

            List<customRowDailyReport> MasterArray = new List<customRowDailyReport>();

            for (int i = 0; i < arrayList.Count; i++)
            {
                connection.Open();
                string tableName = "machine_" + arrayList[i];
                string query2 = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'slotpayout' AND table_name = @tableName";
                MySqlCommand command = new MySqlCommand(query2, connection);
                command.Parameters.AddWithValue("@tableName", tableName);

                int tableCount = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                if (tableCount > 0)
                {
                    connection.Open();
                    String query3 = "select m.Machine_No, m.totalIn, m.totalCreditCancel, m.coinIn, m.coinOut, m.jackpotMeter, d.Game_Name, g.Group_Name, m.time from machine_" + arrayList[i] + " m, machine_details d, machine_group g where m.Type=\"daily_scheduled\" and m.Machine_No=d.Machine_No and d.Group_ID=g.Group_ID ORDER BY m.number DESC LIMIT 1";
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query3, connection);
                    DataSet data2 = new DataSet();
                    adapter2.Fill(data2);

                    connection.Close();

                    if ((data2.Tables[0].Rows.Count >= 1))
                    {
                        customRowDailyReport MasterEntry = new customRowDailyReport();

                        MasterEntry.machineNo = data2.Tables[0].Rows[0][0].ToString();
                        MasterEntry.totalIn = long.Parse(data2.Tables[0].Rows[0][1].ToString());
                        MasterEntry.totalOut = long.Parse(data2.Tables[0].Rows[0][2].ToString());
                        MasterEntry.jackPotMeter = long.Parse(data2.Tables[0].Rows[0][5].ToString());
                        MasterEntry.totalNet = MasterEntry.totalIn - MasterEntry.totalOut - MasterEntry.jackPotMeter;
                        MasterEntry.coinIn = long.Parse(data2.Tables[0].Rows[0][3].ToString());
                        MasterEntry.coinOut = long.Parse(data2.Tables[0].Rows[0][4].ToString());
                        MasterEntry.coinNet = MasterEntry.coinIn - MasterEntry.coinOut;
                        MasterEntry.gameName = data2.Tables[0].Rows[0][6].ToString();
                        MasterEntry.groupName = data2.Tables[0].Rows[0][7].ToString();

                        MasterArray.Add(MasterEntry);
                    }
                }
                else
                {
                    MessageBox.Show("Machine Number : " + tableName + "'s table not found. Please contact the department.");
                }
            }

            CalculateGroupGrandTotal(dailyReport, dailyReportCleared, dailyReportDeleted, MasterArray);

        }

        private void CalculateGroupGrandTotal(List<customRowDailyReport> dailyReport, List<customRowDailyReport> dailyReportCleared, List<customRowDailyReport> dailyReportDeleted, List<customRowDailyReport> masterArray)
        {
            List<customRowDailyReport> customDailyReports = new List<customRowDailyReport>();
            customRowDailyReport grand_total = new customRowDailyReport();
            grand_total.machineNo = "Grand Total";
            grand_total.gameName = "";
            grand_total.groupName = "";
            grand_total.coinIn = 0;
            grand_total.coinOut = 0;
            grand_total.coinNet = 0;
            grand_total.totalIn = 0;
            grand_total.totalOut = 0;
            grand_total.jackPotMeter = 0;
            grand_total.totalNet = 0;

            customRowDailyReport group_total = new customRowDailyReport();
            group_total.machineNo = "Group Total";
            group_total.gameName = "";
            group_total.groupName = "";
            group_total.coinIn = 0;
            group_total.coinOut = 0;
            group_total.coinNet = 0;
            group_total.totalIn = 0;
            group_total.totalOut = 0;
            group_total.jackPotMeter = 0;
            group_total.totalNet = 0;

            for (int i = 0; i < dailyReport.Count; i++)
            {
                customRowDailyReport temp = dailyReport[i];


                if (i != 0 && !dailyReport[i].groupName.Equals(dailyReport[i - 1].groupName))
                {
                    customRowDailyReport group_tempd = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

                    customDailyReports.Add(group_tempd);

                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPotMeter = 0;
                    group_total.totalNet = 0;
                    group_total.coinIn = 0;
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.groupName = "";
                }

                customDailyReports.Add(temp);

                group_total.totalIn += temp.totalIn;
                group_total.totalOut += temp.totalOut;
                group_total.jackPotMeter += temp.jackPotMeter;
                group_total.totalNet += temp.totalNet;
                group_total.coinIn += temp.coinIn;
                group_total.coinOut += temp.coinOut;
                group_total.coinNet += temp.coinNet;
                group_total.groupName = temp.groupName;

                grand_total.totalIn += temp.totalIn;
                grand_total.totalOut += temp.totalOut;
                grand_total.jackPotMeter += temp.jackPotMeter;
                grand_total.totalNet += temp.totalNet;
                grand_total.coinIn += temp.coinIn;
                grand_total.coinOut += temp.coinOut;
                grand_total.coinNet += temp.coinNet;
            }

            customRowDailyReport group_temp = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

            customDailyReports.Add(group_temp);

            customRowDailyReport grand_temp = new customRowDailyReport(
                        "Grand Total",
                        "",
                        "",
                        grand_total.totalIn,
                        grand_total.totalOut,
                        grand_total.jackPotMeter,
                        grand_total.totalNet,
                        grand_total.coinIn,
                        grand_total.coinOut,
                        grand_total.coinNet
                        );

            customDailyReports.Add(grand_temp);


            /*Cleared Reports.*/
            List<customRowDailyReport> customClearReports = new List<customRowDailyReport>();
            grand_total.machineNo = "Grand Total";
            grand_total.gameName = "";
            grand_total.groupName = "";
            grand_total.coinIn = 0;
            grand_total.coinOut = 0;
            grand_total.coinNet = 0;
            grand_total.totalIn = 0;
            grand_total.totalOut = 0;
            grand_total.jackPotMeter = 0;
            grand_total.totalNet = 0;

            group_total.machineNo = "Group Total";
            group_total.gameName = "";
            group_total.groupName = "";
            group_total.coinIn = 0;
            group_total.coinOut = 0;
            group_total.coinNet = 0;
            group_total.totalIn = 0;
            group_total.totalOut = 0;
            group_total.jackPotMeter = 0;
            group_total.totalNet = 0;

            for (int i = 0; i < dailyReportCleared.Count; i++)
            {
                customRowDailyReport temp = dailyReportCleared[i];


                if (i != 0 && !dailyReportCleared[i].groupName.Equals(dailyReportCleared[i - 1].groupName))
                {
                    customRowDailyReport group_tempc = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

                    customClearReports.Add(group_tempc);

                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPotMeter = 0;
                    group_total.totalNet = 0;
                    group_total.coinIn = 0;
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.groupName = "";
                }

                customClearReports.Add(temp);

                group_total.totalIn += temp.totalIn;
                group_total.totalOut += temp.totalOut;
                group_total.jackPotMeter += temp.jackPotMeter;
                group_total.totalNet += temp.totalNet;
                group_total.coinIn += temp.coinIn;
                group_total.coinOut += temp.coinOut;
                group_total.coinNet += temp.coinNet;
                group_total.groupName = temp.groupName;

                grand_total.totalIn += temp.totalIn;
                grand_total.totalOut += temp.totalOut;
                grand_total.jackPotMeter += temp.jackPotMeter;
                grand_total.totalNet += temp.totalNet;
                grand_total.coinIn += temp.coinIn;
                grand_total.coinOut += temp.coinOut;
                grand_total.coinNet += temp.coinNet;
            }

            customRowDailyReport group_tempx = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

            customClearReports.Add(group_tempx);

            customRowDailyReport grand_tempx = new customRowDailyReport(
                        "Grand Total",
                        "",
                        "",
                        grand_total.totalIn,
                        grand_total.totalOut,
                        grand_total.jackPotMeter,
                        grand_total.totalNet,
                        grand_total.coinIn,
                        grand_total.coinOut,
                        grand_total.coinNet
                        );

            customClearReports.Add(grand_tempx);


            /*Deleted Reports.*/
            List<customRowDailyReport> customDeleteReports = new List<customRowDailyReport>();
            grand_total.machineNo = "Grand Total";
            grand_total.gameName = "";
            grand_total.groupName = "";
            grand_total.coinIn = 0;
            grand_total.coinOut = 0;
            grand_total.coinNet = 0;
            grand_total.totalIn = 0;
            grand_total.totalOut = 0;
            grand_total.jackPotMeter = 0;
            grand_total.totalNet = 0;

            group_total.machineNo = "Group Total";
            group_total.gameName = "";
            group_total.groupName = "";
            group_total.coinIn = 0;
            group_total.coinOut = 0;
            group_total.coinNet = 0;
            group_total.totalIn = 0;
            group_total.totalOut = 0;
            group_total.jackPotMeter = 0;
            group_total.totalNet = 0;

            for (int i = 0; i < dailyReportDeleted.Count; i++)
            {
                customRowDailyReport temp = dailyReportDeleted[i];


                if (i != 0 && !dailyReportDeleted[i].groupName.Equals(dailyReportDeleted[i - 1].groupName))
                {
                    customRowDailyReport group_tempde = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

                    customDeleteReports.Add(group_tempde);

                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPotMeter = 0;
                    group_total.totalNet = 0;
                    group_total.coinIn = 0;
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.groupName = "";
                }

                customDeleteReports.Add(temp);

                group_total.totalIn += temp.totalIn;
                group_total.totalOut += temp.totalOut;
                group_total.jackPotMeter += temp.jackPotMeter;
                group_total.totalNet += temp.totalNet;
                group_total.coinIn += temp.coinIn;
                group_total.coinOut += temp.coinOut;
                group_total.coinNet += temp.coinNet;
                group_total.groupName = temp.groupName;

                grand_total.totalIn += temp.totalIn;
                grand_total.totalOut += temp.totalOut;
                grand_total.jackPotMeter += temp.jackPotMeter;
                grand_total.totalNet += temp.totalNet;
                grand_total.coinIn += temp.coinIn;
                grand_total.coinOut += temp.coinOut;
                grand_total.coinNet += temp.coinNet;
            }

            customRowDailyReport group_temp2 = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

            customDeleteReports.Add(group_temp2);

            customRowDailyReport grand_temp2 = new customRowDailyReport(
                        "Grand Total",
                        "",
                        "",
                        grand_total.totalIn,
                        grand_total.totalOut,
                        grand_total.jackPotMeter,
                        grand_total.totalNet,
                        grand_total.coinIn,
                        grand_total.coinOut,
                        grand_total.coinNet
                        );

            customDeleteReports.Add(grand_temp2);

            /*Master Reports.*/
            List<customRowDailyReport> customMasterReports = new List<customRowDailyReport>();
            grand_total.machineNo = "Grand Total";
            grand_total.gameName = "";
            grand_total.groupName = "";
            grand_total.coinIn = 0;
            grand_total.coinOut = 0;
            grand_total.coinNet = 0;
            grand_total.totalIn = 0;
            grand_total.totalOut = 0;
            grand_total.jackPotMeter = 0;
            grand_total.totalNet = 0;

            group_total.machineNo = "Group Total";
            group_total.gameName = "";
            group_total.groupName = "";
            group_total.coinIn = 0;
            group_total.coinOut = 0;
            group_total.coinNet = 0;
            group_total.totalIn = 0;
            group_total.totalOut = 0;
            group_total.jackPotMeter = 0;
            group_total.totalNet = 0;

            for (int i = 0; i < masterArray.Count; i++)
            {
                customRowDailyReport temp = masterArray[i];


                if (i != 0 && !masterArray[i].groupName.Equals(masterArray[i - 1].groupName))
                {
                    customRowDailyReport group_tempde = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

                    customMasterReports.Add(group_tempde);

                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPotMeter = 0;
                    group_total.totalNet = 0;
                    group_total.coinIn = 0;
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.groupName = "";
                }

                customMasterReports.Add(temp);

                group_total.totalIn += temp.totalIn;
                group_total.totalOut += temp.totalOut;
                group_total.jackPotMeter += temp.jackPotMeter;
                group_total.totalNet += temp.totalNet;
                group_total.coinIn += temp.coinIn;
                group_total.coinOut += temp.coinOut;
                group_total.coinNet += temp.coinNet;
                group_total.groupName = temp.groupName;

                grand_total.totalIn += temp.totalIn;
                grand_total.totalOut += temp.totalOut;
                grand_total.jackPotMeter += temp.jackPotMeter;
                grand_total.totalNet += temp.totalNet;
                grand_total.coinIn += temp.coinIn;
                grand_total.coinOut += temp.coinOut;
                grand_total.coinNet += temp.coinNet;
            }

            customRowDailyReport group_tempm = new customRowDailyReport(
                        "Group Total",
                        group_total.groupName,
                        "",
                        group_total.totalIn,
                        group_total.totalOut,
                        group_total.jackPotMeter,
                        group_total.totalNet,
                        group_total.coinIn,
                        group_total.coinOut,
                        group_total.coinNet
                        );

            customMasterReports.Add(group_tempm);

            customRowDailyReport grand_tempm = new customRowDailyReport(
                        "Grand Total",
                        "",
                        "",
                        grand_total.totalIn,
                        grand_total.totalOut,
                        grand_total.jackPotMeter,
                        grand_total.totalNet,
                        grand_total.coinIn,
                        grand_total.coinOut,
                        grand_total.coinNet
                        );

            customMasterReports.Add(grand_tempm);

            CreateSlotView(customDailyReports, customClearReports, customDeleteReports, customMasterReports);
        }

        private void CreateSlotView(List<customRowDailyReport> dailyReport, List<customRowDailyReport> dailyReportCleared, List<customRowDailyReport> dailyReportDeleted, List<customRowDailyReport> masterArray)
        {
            PanelPDF.Controls.Clear();


            DataGridViewCellStyle dataGridViewCellStyleMaster1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleMaster2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleMaster3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleMaster4 = new DataGridViewCellStyle();

            dataGridViewMaster.Columns.Clear();
            dataGridViewMaster.CellFormatting += DataGridViewMaster_CellFormatting;
            dataGridViewMaster.Dock = DockStyle.Top;
            dataGridViewMaster.AllowUserToAddRows = false;
            dataGridViewMaster.AllowUserToDeleteRows = false;
            dataGridViewMaster.AllowUserToResizeColumns = false;
            dataGridViewMaster.AllowUserToResizeRows = false;
            dataGridViewMaster.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMaster.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewMaster.BorderStyle = BorderStyle.None;
            dataGridViewMaster.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyleMaster1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleMaster1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleMaster1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleMaster1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleMaster1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster1.WrapMode = DataGridViewTriState.False;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyleMaster1;

            dataGridViewMaster.ColumnHeadersHeight = 50;
            dataGridViewMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyleMaster2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleMaster2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleMaster2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyleMaster2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyleMaster2.WrapMode = DataGridViewTriState.False;
            dataGridViewMaster.DefaultCellStyle = dataGridViewCellStyleMaster2;
            dataGridViewMaster.EnableHeadersVisualStyles = false;
            dataGridViewMaster.GridColor = System.Drawing.Color.LightGray;
            dataGridViewMaster.MultiSelect = false;
            dataGridViewMaster.ReadOnly = true;
            dataGridViewMaster.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyleMaster3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleMaster3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleMaster3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleMaster3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleMaster3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleMaster3.WrapMode = DataGridViewTriState.False;
            dataGridViewMaster.RowHeadersDefaultCellStyle = dataGridViewCellStyleMaster3;
            dataGridViewMaster.RowHeadersVisible = false;
            dataGridViewMaster.RowHeadersWidth = 51;
            dataGridViewMaster.RowsDefaultCellStyle = dataGridViewCellStyleMaster4;
            dataGridViewMaster.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridViewMaster.RowTemplate.Height = 50;
            dataGridViewMaster.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridViewMaster.ScrollBars = ScrollBars.None;
            dataGridViewMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMaster.ShowEditingIcon = false;
            dataGridViewMaster.TabIndex = 0;
            dataGridViewMaster.TabStop = false;
            dataGridViewMaster.AutoGenerateColumns = false;

            dataGridViewMaster.Columns.Add("machineNumber", "Machine #");
            dataGridViewMaster.Columns.Add("groupName", "Group");
            dataGridViewMaster.Columns.Add("gameName", "Game Name");
            dataGridViewMaster.Columns.Add("totalIn", "Total In");
            dataGridViewMaster.Columns.Add("totalOut", "Total Out");
            dataGridViewMaster.Columns.Add("jackPot", "Jackpot");
            dataGridViewMaster.Columns.Add("net", "Net");
            dataGridViewMaster.Columns.Add("coinIn", "Coin In");
            dataGridViewMaster.Columns.Add("coinOut", "Coin Out");
            dataGridViewMaster.Columns.Add("coinNet", "Coin Net");

            dataGridViewMaster.Columns["machineNumber"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["groupName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["gameName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["totalIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["totalOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["jackPot"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["net"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["coinIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["coinOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaster.Columns["coinNet"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dataGridViewMaster.Rows.Clear();
            foreach (customRowDailyReport custom in masterArray)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewMaster, custom.machineNo, custom.groupName, custom.gameName,
                    custom.totalIn, custom.totalOut, custom.jackPotMeter, custom.totalNet,
                    custom.coinIn, custom.coinOut, custom.coinNet);
                row.Height = 50;

                dataGridViewMaster.Rows.Add(row);
            }
            dataGridViewMaster.Height = 50 * masterArray.Count + dataGridViewMaster.ColumnHeadersHeight + 50;


            PanelPDF.Controls.Add(dataGridViewMaster);

            Label MasterReport = new Label();
            MasterReport.Text = "Master Report";
            MasterReport.Dock = DockStyle.Top;
            MasterReport.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MasterReport.Height = 50;
            MasterReport.TextAlign = ContentAlignment.MiddleCenter;
            MasterReport.BackColor = System.Drawing.Color.FromArgb(180, 198, 231);
            MasterReport.ForeColor = System.Drawing.Color.Black;
            PanelPDF.Controls.Add(MasterReport);


            if (dailyReportDeleted.Count > 2)
            {
                dataGridViewDeleted.Columns.Clear();
                DataGridViewCellStyle dataGridViewCellStyleDelete1 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleDelete2 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleDelete3 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleDelete4 = new DataGridViewCellStyle();
                dataGridViewDeleted.CellFormatting += DataGridViewDeleted_CellFormatting;

                dataGridViewDeleted.Dock = DockStyle.Top;
                dataGridViewDeleted.AllowUserToAddRows = false;
                dataGridViewDeleted.AllowUserToDeleteRows = false;
                dataGridViewDeleted.AllowUserToResizeColumns = false;
                dataGridViewDeleted.AllowUserToResizeRows = false;
                dataGridViewDeleted.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewDeleted.BackgroundColor = System.Drawing.Color.AliceBlue;
                dataGridViewDeleted.BorderStyle = BorderStyle.None;
                dataGridViewDeleted.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dataGridViewCellStyleDelete1.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleDelete1.BackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleDelete1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleDelete1.ForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleDelete1.SelectionForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete1.WrapMode = DataGridViewTriState.False;
                dataGridViewDeleted.ColumnHeadersDefaultCellStyle = dataGridViewCellStyleDelete1;

                dataGridViewDeleted.ColumnHeadersHeight = 50;
                dataGridViewDeleted.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewCellStyleDelete2.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleDelete2.BackColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleDelete2.ForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyleDelete2.SelectionBackColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete2.SelectionForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyleDelete2.WrapMode = DataGridViewTriState.False;
                dataGridViewDeleted.DefaultCellStyle = dataGridViewCellStyleDelete2;
                dataGridViewDeleted.EnableHeadersVisualStyles = false;
                dataGridViewDeleted.GridColor = System.Drawing.Color.LightGray;
                dataGridViewDeleted.MultiSelect = false;
                dataGridViewDeleted.ReadOnly = true;
                dataGridViewDeleted.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dataGridViewCellStyleDelete3.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleDelete3.BackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleDelete3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleDelete3.ForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleDelete3.SelectionForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleDelete3.WrapMode = DataGridViewTriState.False;
                dataGridViewDeleted.RowHeadersDefaultCellStyle = dataGridViewCellStyleDelete3;
                dataGridViewDeleted.RowHeadersVisible = false;
                dataGridViewDeleted.RowHeadersWidth = 51;
                dataGridViewDeleted.RowsDefaultCellStyle = dataGridViewCellStyleDelete4;
                dataGridViewDeleted.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dataGridViewDeleted.RowTemplate.Height = 50;
                dataGridViewDeleted.RowTemplate.Resizable = DataGridViewTriState.False;
                dataGridViewDeleted.ScrollBars = ScrollBars.None;
                dataGridViewDeleted.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewDeleted.ShowEditingIcon = false;
                dataGridViewDeleted.TabIndex = 0;
                dataGridViewDeleted.TabStop = false;
                dataGridViewDeleted.AutoGenerateColumns = false;

                dataGridViewDeleted.Columns.Add("machineNumber", "Machine #");
                dataGridViewDeleted.Columns.Add("groupName", "Group");
                dataGridViewDeleted.Columns.Add("gameName", "Game Name");
                dataGridViewDeleted.Columns.Add("totalIn", "Total In");
                dataGridViewDeleted.Columns.Add("totalOut", "Total Out");
                dataGridViewDeleted.Columns.Add("jackPot", "Jackpot");
                dataGridViewDeleted.Columns.Add("net", "Net");
                dataGridViewDeleted.Columns.Add("coinIn", "Coin In");
                dataGridViewDeleted.Columns.Add("coinOut", "Coin Out");
                dataGridViewDeleted.Columns.Add("coinNet", "Coin Net");


                dataGridViewDeleted.Columns["machineNumber"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["groupName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["gameName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["totalIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["totalOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["jackPot"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["net"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["coinIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["coinOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewDeleted.Columns["coinNet"].SortMode = DataGridViewColumnSortMode.NotSortable;


                dataGridViewDeleted.Rows.Clear();
                foreach (customRowDailyReport custom in dailyReportDeleted)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewDeleted, custom.machineNo, custom.groupName, custom.gameName,
                        custom.totalIn, custom.totalOut, custom.jackPotMeter, custom.totalNet,
                        custom.coinIn, custom.coinOut, custom.coinNet);
                    row.Height = 50;

                    dataGridViewDeleted.Rows.Add(row);
                }
                dataGridViewDeleted.Height = 50 * dailyReportDeleted.Count + dataGridViewDeleted.ColumnHeadersHeight + 50;

                PanelPDF.Controls.Add(dataGridViewDeleted);

                Label DeleteReport = new Label();
                DeleteReport.Text = "Deleted Entries";
                DeleteReport.Dock = DockStyle.Top;
                DeleteReport.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point);
                DeleteReport.Height = 50;
                DeleteReport.TextAlign = ContentAlignment.MiddleCenter;
                DeleteReport.BackColor = System.Drawing.Color.FromArgb(180, 198, 231);
                DeleteReport.ForeColor = System.Drawing.Color.Black;
                PanelPDF.Controls.Add(DeleteReport);
            }

            if (dailyReportCleared.Count > 2)
            {
                dataGridViewCleared.Columns.Clear();
                DataGridViewCellStyle dataGridViewCellStyleClear1 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleClear2 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleClear3 = new DataGridViewCellStyle();
                DataGridViewCellStyle dataGridViewCellStyleClear4 = new DataGridViewCellStyle();

                dataGridViewCleared.CellFormatting += DataGridViewCleared_CellFormatting;

                dataGridViewCleared.Dock = DockStyle.Top;
                dataGridViewCleared.AllowUserToAddRows = false;
                dataGridViewCleared.AllowUserToDeleteRows = false;
                dataGridViewCleared.AllowUserToResizeColumns = false;
                dataGridViewCleared.AllowUserToResizeRows = false;
                dataGridViewCleared.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewCleared.BackgroundColor = System.Drawing.Color.AliceBlue;
                dataGridViewCleared.BorderStyle = BorderStyle.None;
                dataGridViewCleared.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dataGridViewCellStyleClear1.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleClear1.BackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleClear1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleClear1.ForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleClear1.SelectionForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear1.WrapMode = DataGridViewTriState.False;
                dataGridViewCleared.ColumnHeadersDefaultCellStyle = dataGridViewCellStyleClear1;

                dataGridViewCleared.ColumnHeadersHeight = 50;
                dataGridViewCleared.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewCellStyleClear2.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleClear2.BackColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleClear2.ForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyleClear2.SelectionBackColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear2.SelectionForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyleClear2.WrapMode = DataGridViewTriState.False;
                dataGridViewCleared.DefaultCellStyle = dataGridViewCellStyleClear2;
                dataGridViewCleared.EnableHeadersVisualStyles = false;
                dataGridViewCleared.GridColor = System.Drawing.Color.LightGray;
                dataGridViewCleared.MultiSelect = false;
                dataGridViewCleared.ReadOnly = true;
                dataGridViewCleared.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dataGridViewCellStyleClear3.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCellStyleClear3.BackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleClear3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
                dataGridViewCellStyleClear3.ForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
                dataGridViewCellStyleClear3.SelectionForeColor = System.Drawing.Color.White;
                dataGridViewCellStyleClear3.WrapMode = DataGridViewTriState.False;
                dataGridViewCleared.RowHeadersDefaultCellStyle = dataGridViewCellStyleClear3;
                dataGridViewCleared.RowHeadersVisible = false;
                dataGridViewCleared.RowHeadersWidth = 51;
                dataGridViewCleared.RowsDefaultCellStyle = dataGridViewCellStyleClear4;
                dataGridViewCleared.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dataGridViewCleared.RowTemplate.Height = 50;
                dataGridViewCleared.RowTemplate.Resizable = DataGridViewTriState.False;
                dataGridViewCleared.ScrollBars = ScrollBars.None;
                dataGridViewCleared.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewCleared.ShowEditingIcon = false;
                dataGridViewCleared.TabIndex = 0;
                dataGridViewCleared.TabStop = false;
                dataGridViewCleared.AutoGenerateColumns = false;

                dataGridViewCleared.Columns.Add("machineNumber", "Machine #");
                dataGridViewCleared.Columns.Add("groupName", "Group");
                dataGridViewCleared.Columns.Add("gameName", "Game Name");
                dataGridViewCleared.Columns.Add("totalIn", "Total In");
                dataGridViewCleared.Columns.Add("totalOut", "Total Out");
                dataGridViewCleared.Columns.Add("jackPot", "Jackpot");
                dataGridViewCleared.Columns.Add("net", "Net");
                dataGridViewCleared.Columns.Add("coinIn", "Coin In");
                dataGridViewCleared.Columns.Add("coinOut", "Coin Out");
                dataGridViewCleared.Columns.Add("coinNet", "Coin Net");

                dataGridViewCleared.Columns["machineNumber"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["groupName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["gameName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["totalIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["totalOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["jackPot"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["net"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["coinIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["coinOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewCleared.Columns["coinNet"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridViewCleared.Rows.Clear();

                foreach (customRowDailyReport custom in dailyReportCleared)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewCleared, custom.machineNo, custom.groupName, custom.gameName,
                        custom.totalIn, custom.totalOut, custom.jackPotMeter, custom.totalNet,
                        custom.coinIn, custom.coinOut, custom.coinNet);
                    row.Height = 50;

                    dataGridViewCleared.Rows.Add(row);
                }
                dataGridViewCleared.Height = 50 * dailyReportCleared.Count + dataGridViewCleared.ColumnHeadersHeight + 50;

                PanelPDF.Controls.Add(dataGridViewCleared);


                Label ClearReport = new Label();
                ClearReport.Text = "Cleared Entries";
                ClearReport.Dock = DockStyle.Top;
                ClearReport.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point);
                ClearReport.Height = 50;
                ClearReport.TextAlign = ContentAlignment.MiddleCenter;
                ClearReport.BackColor = System.Drawing.Color.FromArgb(180, 198, 231);
                ClearReport.ForeColor = System.Drawing.Color.Black;
                PanelPDF.Controls.Add(ClearReport);
            }

            dataGridViewDaily.Columns.Clear();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();

            dataGridViewDaily.CellFormatting += DataGridViewDaily_CellFormatting;

            dataGridViewDaily.Dock = DockStyle.Top;
            dataGridViewDaily.AllowUserToAddRows = false;
            dataGridViewDaily.AllowUserToDeleteRows = false;
            dataGridViewDaily.AllowUserToResizeColumns = false;
            dataGridViewDaily.AllowUserToResizeRows = false;
            dataGridViewDaily.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDaily.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewDaily.BorderStyle = BorderStyle.None;
            dataGridViewDaily.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewDaily.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

            dataGridViewDaily.ColumnHeadersHeight = 50;
            dataGridViewDaily.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewDaily.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewDaily.EnableHeadersVisualStyles = false;
            dataGridViewDaily.GridColor = System.Drawing.Color.LightGray;
            dataGridViewDaily.MultiSelect = false;
            dataGridViewDaily.ReadOnly = true;
            dataGridViewDaily.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewDaily.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewDaily.RowHeadersVisible = false;
            dataGridViewDaily.RowHeadersWidth = 51;
            dataGridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewDaily.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridViewDaily.RowTemplate.Height = 50;
            dataGridViewDaily.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridViewDaily.ScrollBars = ScrollBars.None;
            dataGridViewDaily.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDaily.ShowEditingIcon = false;
            dataGridViewDaily.TabIndex = 0;
            dataGridViewDaily.TabStop = false;
            dataGridViewDaily.AutoGenerateColumns = false;

            dataGridViewDaily.Columns.Add("machineNumber", "Machine #");
            dataGridViewDaily.Columns.Add("groupName", "Group");
            dataGridViewDaily.Columns.Add("gameName", "Game Name");
            dataGridViewDaily.Columns.Add("totalIn", "Total In");
            dataGridViewDaily.Columns.Add("totalOut", "Total Out");
            dataGridViewDaily.Columns.Add("jackPot", "Jackpot");
            dataGridViewDaily.Columns.Add("net", "Net");
            dataGridViewDaily.Columns.Add("coinIn", "Coin In");
            dataGridViewDaily.Columns.Add("coinOut", "Coin Out");
            dataGridViewDaily.Columns.Add("coinNet", "Coin Net");


            dataGridViewDaily.Columns["machineNumber"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["groupName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["gameName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["totalIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["totalOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["jackPot"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["net"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["coinIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["coinOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewDaily.Columns["coinNet"].SortMode = DataGridViewColumnSortMode.NotSortable;


            dataGridViewDaily.Rows.Clear();
            foreach (customRowDailyReport custom in dailyReport)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewDaily, custom.machineNo, custom.groupName, custom.gameName,
                    custom.totalIn, custom.totalOut, custom.jackPotMeter, custom.totalNet,
                    custom.coinIn, custom.coinOut, custom.coinNet);
                row.Height = 50;

                dataGridViewDaily.Rows.Add(row);
            }
            dataGridViewDaily.Height = 50 * dailyReport.Count + dataGridViewDaily.ColumnHeadersHeight + 50;


            PanelPDF.Controls.Add(dataGridViewDaily);

            Label DailyReport = new Label();
            DailyReport.Text = "Daily Report";
            DailyReport.Dock = DockStyle.Top;
            DailyReport.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point);
            DailyReport.Height = 50;
            DailyReport.TextAlign = ContentAlignment.MiddleCenter;
            DailyReport.BackColor = System.Drawing.Color.FromArgb(180, 198, 231);
            DailyReport.ForeColor = System.Drawing.Color.Black;
            PanelPDF.Controls.Add(DailyReport);

            DateTime fromDate = DTPFrom.Value;
            DateTime toDate = DTPto.Value;
            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);
            
            String targetTime = new DateTime(toDate.Year, toDate.Month, toDate.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");
            String targetYesterdayTime = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

            Label DateTo = new Label();
            DateTo.Text = "To: " + toDate.DayOfWeek + " " + targetTime;
            DateTo.Dock = DockStyle.Top;
            DateTo.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateTo.Padding = new Padding(30, 0, 0, 0);
            DateTo.Height = 50;
            DateTo.TextAlign = ContentAlignment.MiddleRight;
            PanelPDF.Controls.Add(DateTo);

            Label DateFrom = new Label();
            DateFrom.Text = "From: " + fromDate.DayOfWeek + " " + targetYesterdayTime;
            DateFrom.Dock = DockStyle.Top;
            DateFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateFrom.Padding = new Padding(30, 0, 0, 0);
            DateFrom.Height = 50;
            DateFrom.TextAlign = ContentAlignment.MiddleRight;
            PanelPDF.Controls.Add(DateFrom);

            Label DateCreated = new Label();
            DateCreated.Text = "Created On: " + DateTime.Today.DayOfWeek + " " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt");
            DateCreated.Dock = DockStyle.Top;
            DateCreated.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateCreated.Padding = new Padding(30, 0, 0, 0);
            DateCreated.Height = 100;
            DateCreated.TextAlign = ContentAlignment.MiddleLeft;
            PanelPDF.Controls.Add(DateCreated);

        }


        private void DataGridViewMaster_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 4 && e.ColumnIndex < 7)
            {
                if (dataGridViewMaster.Rows[e.RowIndex].Cells[0].Value == "Grand Total" || dataGridViewMaster.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
                else
                {
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewMaster.Rows[e.RowIndex].Cells[0].Value == "Grand Total")
                {
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewMaster.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    if (e.ColumnIndex <= 2)
                    {
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(244, 244, 244);
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);

                    }
                    else
                    {
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        dataGridViewMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);

                    }
                }
            }

        }

        private void DataGridViewCleared_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 4 && e.ColumnIndex < 7)
            {
                if (dataGridViewCleared.Rows[e.RowIndex].Cells[0].Value == "Grand Total" || dataGridViewCleared.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
                else
                {
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewCleared.Rows[e.RowIndex].Cells[0].Value == "Grand Total")
                {
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewCleared.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    if (e.ColumnIndex <= 2)
                    {
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(244, 244, 244);
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);

                    }
                    else
                    {
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        dataGridViewCleared.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);

                    }
                }
            }

        }

        private void DataGridViewDeleted_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 4 && e.ColumnIndex < 7)
            {
                if (dataGridViewDeleted.Rows[e.RowIndex].Cells[0].Value == "Grand Total" || dataGridViewDeleted.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
                else
                {
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewDeleted.Rows[e.RowIndex].Cells[0].Value == "Grand Total")
                {
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewDeleted.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    if (e.ColumnIndex <= 2)
                    {
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(244, 244, 244);
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);

                    }
                    else
                    {
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        dataGridViewDeleted.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);

                    }
                }
            }

        }

        private void DataGridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 4 && e.ColumnIndex < 7)
            {
                if (dataGridViewDaily.Rows[e.RowIndex].Cells[0].Value == "Grand Total" || dataGridViewDaily.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
                else
                {
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewDaily.Rows[e.RowIndex].Cells[0].Value == "Grand Total")
                {
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex <= 4 || e.ColumnIndex >= 7))
            {
                if (dataGridViewDaily.Rows[e.RowIndex].Cells[0].Value == "Group Total")
                {
                    if (e.ColumnIndex <= 2)
                    {
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(244, 244, 244);
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);

                    }
                    else
                    {
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        dataGridViewDaily.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = System.Drawing.Color.FromArgb(255, 255, 255);

                    }
                }
            }

        }


        private void GenerateShiftPDF(DateTime start, DateTime end)
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            // Format the dates for the query
            string todayAt8AMFormatted = start.ToString("yyyy-MM-dd HH:mm:ss");
            string yesterdayAt8AMFormatted = end.ToString("yyyy-MM-dd HH:mm:ss");


            // Construct the query
            string query = $"SELECT * FROM shift_table WHERE End_Time BETWEEN '{todayAt8AMFormatted}' AND '{yesterdayAt8AMFormatted}'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            List<shiftClass> finalArray = new List<shiftClass>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                long shift_id = Convert.ToInt64(row[0]);
                long start_drawer = Convert.ToInt64(row[1]);
                long total_in = Convert.ToInt64(row[2]);
                long total_out = Convert.ToInt64(row[3]);
                long promo = Convert.ToInt64(row[4]);
                long match_play = Convert.ToInt64(row[5]);
                long fill = Convert.ToInt64(row[6]);
                long drop = Convert.ToInt64(row[7]);
                long expense = Convert.ToInt64(row[8]);
                long tickets = Convert.ToInt64(row[9]);
                long login_id = Convert.ToInt64(row[10]);
                long net = (start_drawer + total_in + fill) - (total_out + drop + expense);
                String duration = Convert.ToString(row[12]) + " \nto\n" + Convert.ToString(row[14]);
                String username = (String)row[13];

                shiftClass temp = new shiftClass(start_drawer, total_in, total_out, promo, match_play, fill, drop, expense, tickets, login_id, shift_id, username, duration, net);
                finalArray.Add(temp);
            }

            connection.Close();

            CalculateShiftPDF(finalArray);
        }

        private void CalculateShiftPDF(List<shiftClass> finalArray)
        {
            PanelPDF.Controls.Clear();

            dataGridViewShift.Columns.Clear();
            DataGridViewCellStyle dataGridViewCellStyleShift1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleShift2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleShift3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyleShift4 = new DataGridViewCellStyle();

            dataGridViewShift.Dock = DockStyle.Top;
            dataGridViewShift.AllowUserToAddRows = false;
            dataGridViewShift.AllowUserToDeleteRows = false;
            dataGridViewShift.AllowUserToResizeColumns = false;
            dataGridViewShift.AllowUserToResizeRows = false;
            dataGridViewShift.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewShift.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewShift.BorderStyle = BorderStyle.None;
            dataGridViewShift.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyleShift1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleShift1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleShift1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleShift1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleShift1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift1.WrapMode = DataGridViewTriState.False;
            dataGridViewShift.ColumnHeadersDefaultCellStyle = dataGridViewCellStyleShift1;

            dataGridViewShift.ColumnHeadersHeight = 50;
            dataGridViewShift.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyleShift2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleShift2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleShift2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyleShift2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyleShift2.WrapMode = DataGridViewTriState.False;
            dataGridViewShift.DefaultCellStyle = dataGridViewCellStyleShift2;
            dataGridViewShift.EnableHeadersVisualStyles = false;
            dataGridViewShift.GridColor = System.Drawing.Color.LightGray;
            dataGridViewShift.MultiSelect = false;
            dataGridViewShift.ReadOnly = true;
            dataGridViewShift.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyleShift3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyleShift3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleShift3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyleShift3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyleShift3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyleShift3.WrapMode = DataGridViewTriState.False;
            dataGridViewShift.RowHeadersDefaultCellStyle = dataGridViewCellStyleShift3;
            dataGridViewShift.RowHeadersVisible = false;
            dataGridViewShift.RowHeadersWidth = 51;
            dataGridViewShift.RowsDefaultCellStyle = dataGridViewCellStyleShift4;
            dataGridViewShift.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridViewShift.RowTemplate.Height = 50;
            dataGridViewShift.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridViewShift.ScrollBars = ScrollBars.None;
            dataGridViewShift.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewShift.ShowEditingIcon = false;
            dataGridViewShift.TabIndex = 0;
            dataGridViewShift.TabStop = false;
            dataGridViewShift.AutoGenerateColumns = false;

            dataGridViewShift.Columns.Add("shiftID", "Shift ID");
            dataGridViewShift.Columns.Add("Username", "Username");
            dataGridViewShift.Columns.Add("Duration", "Duration");
            dataGridViewShift.Columns.Add("startdrawer", "Starting Drawer");
            dataGridViewShift.Columns.Add("totalIn", "Total In");
            dataGridViewShift.Columns.Add("totalOut", "Total Out");
            dataGridViewShift.Columns.Add("promo", "Promo");
            dataGridViewShift.Columns.Add("matchplay", "Match Play");
            dataGridViewShift.Columns.Add("fill", "Fill");
            dataGridViewShift.Columns.Add("drop", "Drop");
            dataGridViewShift.Columns.Add("expense", "Expenses");
            dataGridViewShift.Columns.Add("tickets_cash_out", "Tickets Cash Out");
            dataGridViewShift.Columns.Add("net", "Net");


            dataGridViewShift.Columns["shiftID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["Username"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["Duration"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["startdrawer"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["totalIn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["totalOut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["promo"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["matchplay"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["fill"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["drop"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["expense"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["tickets_cash_out"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewShift.Columns["net"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewShift.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridViewShift.Rows.Clear();
            foreach (shiftClass custom in finalArray)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.CreateCells(dataGridViewShift, custom.shift_id, custom.username, custom.duration, custom.starting_drawer,
                    custom.totalIn, custom.totalOut, custom.promo, custom.match_play, custom.fill,
                    custom.drop, custom.expense, custom.total_tickets_out, custom.net);
                row.Height = 100;

                dataGridViewShift.Rows.Add(row);
            }
            dataGridViewShift.Height = 100 * finalArray.Count + dataGridViewShift.ColumnHeadersHeight + 50;


            PanelPDF.Controls.Add(dataGridViewShift);

            Label DailyReport = new Label();
            DailyReport.Text = "Shift Report";
            DailyReport.Dock = DockStyle.Top;
            DailyReport.Font = new Font("Bookman Old Style", 10F, FontStyle.Regular, GraphicsUnit.Point);
            DailyReport.Height = 50;
            DailyReport.TextAlign = ContentAlignment.MiddleCenter;
            DailyReport.BackColor = System.Drawing.Color.FromArgb(180, 198, 231);
            DailyReport.ForeColor = System.Drawing.Color.Black;
            PanelPDF.Controls.Add(DailyReport);

            DateTime fromDate = DTPFrom.Value;
            DateTime toDate = DTPto.Value;
            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);

            String targetTime = new DateTime(toDate.Year, toDate.Month, toDate.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");
            String targetYesterdayTime = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

            Label DateTo = new Label();
            DateTo.Text = "To: " + toDate.DayOfWeek + " " + targetTime;
            DateTo.Dock = DockStyle.Top;
            DateTo.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateTo.Padding = new Padding(30, 0, 0, 0);
            DateTo.Height = 50;
            DateTo.TextAlign = ContentAlignment.MiddleRight;
            PanelPDF.Controls.Add(DateTo);

            Label DateFrom = new Label();
            DateFrom.Text = "From: " + fromDate.DayOfWeek + " " + targetYesterdayTime;
            DateFrom.Dock = DockStyle.Top;
            DateFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateFrom.Padding = new Padding(30, 0, 0, 0);
            DateFrom.Height = 50;
            DateFrom.TextAlign = ContentAlignment.MiddleRight;
            PanelPDF.Controls.Add(DateFrom);

            Label DateCreated = new Label();
            DateCreated.Text = "Created On: " + DateTime.Today.DayOfWeek + " " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt");
            DateCreated.Dock = DockStyle.Top;
            DateCreated.Font = new System.Drawing.Font("Bookman Old Style", 11F, FontStyle.Regular, GraphicsUnit.Point);
            DateCreated.Padding = new Padding(30, 0, 0, 0);
            DateCreated.Height = 100;
            DateCreated.TextAlign = ContentAlignment.MiddleLeft;
            PanelPDF.Controls.Add(DateCreated);

        }

        class customRowDailyReport
        {
            public String machineNo;
            public String groupName;
            public String gameName;
            public long totalIn;
            public long totalOut;
            public long jackPotMeter;
            public long totalNet;
            public long coinIn;
            public long coinOut;
            public long coinNet;

            public customRowDailyReport()
            {

            }

            public customRowDailyReport(string machineNo, String groupName, string gameName, long totalIn, long totalOut, long jackPotMeter, long totalNet, long coinIn, long coinOut, long coinNet)
            {
                this.machineNo = machineNo;
                this.groupName = groupName;
                this.gameName = gameName;
                this.totalIn = totalIn;
                this.totalOut = totalOut;
                this.jackPotMeter = jackPotMeter;
                this.totalNet = totalNet;
                this.coinIn = coinIn;
                this.coinOut = coinOut;
                this.coinNet = coinNet;
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            /*int gridViewCount = 0;

            if (dataGridViewCleared.RowCount > 0 && dataGridViewDeleted.RowCount > 0)
            {
                gridViewCount = 4;
                dataGridViews = new DataGridView[] { dataGridViewDaily, dataGridViewCleared, dataGridViewDeleted, dataGridViewMaster };
            }
            else if (dataGridViewCleared.RowCount > 0)
            {
                gridViewCount = 3;
                dataGridViews = new DataGridView[] { dataGridViewDaily, dataGridViewCleared, dataGridViewMaster };
            }
            else if (dataGridViewDeleted.RowCount > 0)
            {
                gridViewCount = 3;
                dataGridViews = new DataGridView[] { dataGridViewDaily, dataGridViewDeleted, dataGridViewMaster };
            }
            else
            {
                gridViewCount = 2;
                dataGridViews = new DataGridView[] { dataGridViewDaily, dataGridViewMaster };
            }

            // Show the print dialog and start printing.
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                currentGridIndex = 0;
                printDocument.Print();   
            }*/
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            
        }


    }
}





/*



*/