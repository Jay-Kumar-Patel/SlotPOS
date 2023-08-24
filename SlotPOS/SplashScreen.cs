using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskScheduler;
using Hangfire;
using MySqlX.XDevAPI.Relational;
using SlotPOS.Utils;
using System.Collections;
using MySql.Data.MySqlClient;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using Table = iText.Layout.Element.Table;
using System.IO;

namespace SlotPOS
{
    public partial class SplashScreen : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public SplashScreen()
        {
            InitializeComponent();
            FindCounting();
            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);
            DateTime currentTime = DateTime.Now;
            DateTime targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, tempTime.Hour, tempTime.Minute, tempTime.Second);
            DateTime utcTime = targetTime.ToUniversalTime();
            
            RecurringJob.AddOrUpdate(() => MainScheduler(), utcTime.Minute.ToString() + " " + utcTime.Hour.ToString() + " * * *");
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            // Loading and starting the timer.
            timer.Interval = 1500;
            timer.Start();
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object? sender, EventArgs e)
        {
            //After 3 sec stop the timer.
            timer.Stop();

            //Hide this form.
            this.Hide();

            //Display LoginScreen.
            LoginScreen loginScreen = new();
            loginScreen.Show();
        }

        private void FindCounting()
        {
            Database database = new Database();
            using (MySqlConnection connection = new MySqlConnection(database.connString))
            {
                connection.Open();

                string query = "SELECT Counting FROM store_details";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string result = reader["Counting"].ToString();

                            Properties.Settings.Default.Counting = Convert.ToDateTime(result);
                        }
                        else
                        {
                            Properties.Settings.Default.Counting = Convert.ToDateTime("8:00:00");
                        }
                    }
                }
            }
        }

        [AutomaticRetry(Attempts = 3)]
        public void MainScheduler()
        {
            CreateDailyReportTable();
        }

        public void CreateDailyReportTable()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();

            String query = "select * from machine_details ORDER BY Group_ID";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            ArrayList arrayList = new ArrayList();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                String machineNumber = row[0].ToString();

                if(!machineNumber.Contains('_'))
                {
                    arrayList.Add(machineNumber);
                }
            }
            connection.Close();

            List<customRowFinal> MasterArray = new List<customRowFinal>();

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
                    String query3 = "select m.Machine_No, m.totalIn, m.totalCreditCancel, m.coinIn, m.coinOut, m.jackpotMeter, d.Game_Name, g.Group_Name, m.time from machine_" + arrayList[i] + " m, machine_details d, machine_group g where m.Type=\"daily_scheduled\" and m.Machine_No=d.Machine_No and d.Group_Id=g.Group_Id ORDER BY m.number DESC LIMIT 2";
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query3, connection);
                    DataSet data2 = new DataSet();
                    adapter2.Fill(data2);

                    connection.Close();

                    if((data2.Tables[0].Rows.Count >= 1))
                    {
                        customRowFinal MasterEntry = new customRowFinal();

                        MasterEntry.machineNumber = data2.Tables[0].Rows[0][0].ToString();
                        MasterEntry.totalIn = long.Parse(data2.Tables[0].Rows[0][1].ToString());
                        MasterEntry.totalOut = long.Parse(data2.Tables[0].Rows[0][2].ToString());
                        MasterEntry.jackPot = long.Parse(data2.Tables[0].Rows[0][5].ToString());
                        MasterEntry.net = MasterEntry.totalIn - MasterEntry.totalOut - MasterEntry.jackPot;
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

            CalculateDailyReport(MasterArray);
        }

        void CalculateDailyReport(List<customRowFinal> MasterArray)
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();
            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);

            string query = $"SELECT d.indexing, d.Date_Time, d.Machine_No, d.Total_In, d.Total_Out, " +
                $"d.Jackpot_Meter, d.Total_Net, d.Coin_In, d.Coin_Out, d.Coin_Net, d.Type, g.Group_Name, m.Game_Name FROM daily_reporting d, machine_details m, machine_group g WHERE d.Date_Time > '{DateTime.Today.AddDays(-1).AddHours(tempTime.Hour).AddMinutes(tempTime.Minute).AddSeconds(tempTime.Second):yyyy-MM-dd HH:mm:ss}' AND d.Date_Time <= '{DateTime.Today.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute).AddSeconds(tempTime.Second):yyyy-MM-dd HH:mm:ss}' and g.Group_ID = m.Group_ID and m.Machine_No = d.Machine_No ORDER BY g.Group_Name, d.Machine_No, d.Date_Time";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            connection.Close();

            List<customRowFinal> MainData = new List<customRowFinal>();
            List<customRowFinal> ClearedData = new List<customRowFinal>();
            List<customRowFinal> DeletedData = new List<customRowFinal>();

            foreach (DataRow row in data.Tables[0].Rows)
            {
                String type = row[10].ToString();

                customRowFinal temp_class = new customRowFinal(
                    row[2].ToString(),
                    long.Parse(row[3].ToString()),
                    long.Parse(row[4].ToString()),
                    long.Parse(row[6].ToString()),
                    long.Parse(row[7].ToString()),
                    long.Parse(row[8].ToString()),
                    long.Parse(row[9].ToString()),
                    row[12].ToString(),
                    row[11].ToString(),
                    long.Parse(row[5].ToString())
                    );

                if (type.Equals("daily_scheduled"))
                { 
                    MainData.Add(temp_class);
                }
                else if(type.Equals("clear_entry"))
                {
                    ClearedData.Add(temp_class);
                }
                else if(type.Equals("delete_entry"))
                {
                    DeletedData.Add(temp_class);
                }
            }

            GenerateSlotPDF(MainData, ClearedData, DeletedData, MasterArray);
        }

        void GenerateSlotPDF(List<customRowFinal> MainArray, List<customRowFinal> ClearedArray, List<customRowFinal> DeletedArray, List<customRowFinal> MasterArray)
        {
            String path = "SlotReport_"+DateTime.Today.ToString("yyyy-MM-dd")+".pdf";
            using(PdfWriter writer = new PdfWriter(path))
            {
                PdfDocument pdfDocument = new PdfDocument(writer);
                PageSize landscape = PageSize.A4.Rotate();
                pdfDocument.SetDefaultPageSize(landscape);
                Document document = new Document(pdfDocument);

                Paragraph paragraph = new Paragraph("Slot Payout");
                paragraph.SetFontSize(25f);
                paragraph.SetUnderline();
                paragraph.SetTextAlignment(TextAlignment.CENTER);
                paragraph.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());
                document.Add(paragraph);

                Paragraph paragraph3 = new Paragraph("");
                Paragraph paragraph4 = new Paragraph("");
                Paragraph paragraph5 = new Paragraph("");
                document.Add(paragraph3);
                document.Add(paragraph4);
                document.Add(paragraph5);

                DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);
                DateTime currentTime = DateTime.Today;
                String targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

                DateTime yesterdayTime = DateTime.Today.AddDays(-1);
                String targetYesterdayTime = new DateTime(yesterdayTime.Year, yesterdayTime.Month, yesterdayTime.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

                Paragraph paragraph6 = new Paragraph("From: " + yesterdayTime.DayOfWeek + " " + targetYesterdayTime);
                paragraph6.SetFontSize(14f);
                paragraph6.SetTextAlignment(TextAlignment.RIGHT);
                document.Add(paragraph6);

                Paragraph paragraph7 = new Paragraph("To: " + currentTime.DayOfWeek + " " + targetTime);
                paragraph7.SetFontSize(14f);
                paragraph7.SetTextAlignment(TextAlignment.RIGHT);
                document.Add(paragraph7);

                Paragraph paragraph2 = new Paragraph("Created On: " + DateTime.Today.DayOfWeek + " " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt"));
                paragraph2.SetFontSize(14f);
                paragraph2.SetTextAlignment(TextAlignment.LEFT);
                document.Add(paragraph2);

                iText.Kernel.Colors.Color headingColor = new DeviceRgb(180, 198, 231);
                iText.Kernel.Colors.Color dark_blue = new DeviceRgb(28, 58, 112);
                iText.Kernel.Colors.Color light_blue = new DeviceRgb(30, 144, 255);
                iText.Kernel.Colors.Color very_light_green = new DeviceRgb(192, 255, 192);
                iText.Kernel.Colors.Color dark_green = new DeviceRgb(0, 128, 0);
                iText.Kernel.Colors.Color light_green = new DeviceRgb(0, 192, 0);
                iText.Kernel.Colors.Color grey = new DeviceRgb(224, 224, 224);
                iText.Kernel.Colors.Color white = new DeviceRgb(255, 255, 255);


                float[] colwidth = { 90f, 90f, 90f, 90f, 90f, 90f, 90f, 90f, 90f, 90f };
                Table first = new Table(colwidth);

                Cell cell1 = new Cell(1, 10).Add(new Paragraph("Daily Report (Main)").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(15f)).SetBackgroundColor(headingColor);
                Cell cell2 = new Cell(1, 1).Add(new Paragraph("Machine #").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                Cell cell3 = new Cell(1, 1).Add(new Paragraph("Group").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                Cell cell4 = new Cell(1, 1).Add(new Paragraph("Game Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                Cell cell5 = new Cell(1, 1).Add(new Paragraph("Total In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                Cell cell6 = new Cell(1, 1).Add(new Paragraph("Total Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                Cell cell7 = new Cell(1, 1).Add(new Paragraph("Jackpot").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                Cell cell8 = new Cell(1, 1).Add(new Paragraph("Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                Cell cell9 = new Cell(1, 1).Add(new Paragraph("Coin In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                Cell cell10 = new Cell(1, 1).Add(new Paragraph("Coin Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                Cell cell11 = new Cell(1, 1).Add(new Paragraph("Coin Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);

                first.AddCell(cell1);
                first.AddCell(cell2);
                first.AddCell(cell3);
                first.AddCell(cell4);
                first.AddCell(cell5);
                first.AddCell(cell6);
                first.AddCell(cell7);
                first.AddCell(cell8);
                first.AddCell(cell9);
                first.AddCell(cell10);
                first.AddCell(cell11);
                
                customRowFinal grand_total = new customRowFinal();
                grand_total.machineNumber = "Grand Total";
                grand_total.gameName = "";
                grand_total.groupName = "";
                grand_total.coinIn = 0;
                grand_total.coinOut = 0;
                grand_total.coinNet = 0;
                grand_total.totalIn = 0;
                grand_total.totalOut = 0;
                grand_total.jackPot = 0;
                grand_total.net = 0;

                customRowFinal group_total = new customRowFinal();
                group_total.machineNumber = "Group Total";
                group_total.gameName = "";
                group_total.groupName = "";
                group_total.coinIn = 0;
                group_total.coinOut = 0;
                group_total.coinNet = 0;
                group_total.totalIn = 0;
                group_total.totalOut = 0;
                group_total.jackPot = 0;
                group_total.net = 0;

                Cell cell12, cell13, cell14, cell15, cell16, cell17, cell18, cell19, cell20, cell21;
                for (int i = 0; i < MainArray.Count; i++)
                {
                    customRowFinal temp = MainArray[i];

                    if (i!=0&&!MainArray[i].machineNumber.Equals(MainArray[i-1].machineNumber))
                    {
                        cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                        cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                        cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                        first.AddCell(cell12);
                        first.AddCell(cell13);
                        first.AddCell(cell14);
                        first.AddCell(cell15);
                        first.AddCell(cell16);
                        first.AddCell(cell17);
                        first.AddCell(cell18);
                        first.AddCell(cell19);
                        first.AddCell(cell20);
                        first.AddCell(cell21);

                        group_total.totalIn = 0;
                        group_total.totalOut = 0;
                        group_total.jackPot = 0;
                        group_total.net = 0;
                        group_total.coinIn = 0;
                        group_total.coinOut = 0;
                        group_total.coinNet = 0;
                        group_total.groupName = "";
                    }
                    
                    cell12 = new Cell(1, 1).Add(new Paragraph(temp.machineNumber).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell13 = new Cell(1, 1).Add(new Paragraph(temp.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell14 = new Cell(1, 1).Add(new Paragraph(temp.gameName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    

                    first.AddCell(cell12);
                    first.AddCell(cell13);
                    first.AddCell(cell14);
                    first.AddCell(cell15);
                    first.AddCell(cell16);
                    first.AddCell(cell17);
                    first.AddCell(cell18);
                    first.AddCell(cell19);
                    first.AddCell(cell20);
                    first.AddCell(cell21);

                    group_total.totalIn += temp.totalIn;
                    group_total.totalOut += temp.totalOut;
                    group_total.jackPot += temp.jackPot;
                    group_total.net += temp.net;
                    group_total.coinIn += temp.coinIn;
                    group_total.coinOut += temp.coinOut;
                    group_total.coinNet += temp.coinNet;
                    group_total.groupName = temp.groupName;

                    grand_total.totalIn += temp.totalIn;
                    grand_total.totalOut += temp.totalOut;
                    grand_total.jackPot += temp.jackPot;
                    grand_total.net += temp.net;
                    grand_total.coinIn += temp.coinIn;
                    grand_total.coinOut += temp.coinOut;
                    grand_total.coinNet += temp.coinNet;
                }

                /*Here Group Total in last.*/
                cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                first.AddCell(cell12);
                first.AddCell(cell13);
                first.AddCell(cell14);
                first.AddCell(cell15);
                first.AddCell(cell16);
                first.AddCell(cell17);
                first.AddCell(cell18);
                first.AddCell(cell19);
                first.AddCell(cell20);
                first.AddCell(cell21);

                /*Grand Total in Last*/
                cell12 = new Cell(1, 1).Add(new Paragraph("Grand Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell13 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                first.AddCell(cell12);
                first.AddCell(cell13);
                first.AddCell(cell14);
                first.AddCell(cell15);
                first.AddCell(cell16);
                first.AddCell(cell17);
                first.AddCell(cell18);
                first.AddCell(cell19);
                first.AddCell(cell20);
                first.AddCell(cell21);
                
                document.Add(first);
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));


                if(ClearedArray.Count>0)
                {
                    Table first2 = new Table(colwidth);
                    document.Add(paragraph);
                    document.Add(paragraph3);
                    document.Add(paragraph4);
                    document.Add(paragraph5);

                    /*Cleared Table*/
                    cell1 = new Cell(1, 10).Add(new Paragraph("Daily Report (Cleared Entries)").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(15f)).SetBackgroundColor(headingColor);
                    cell2 = new Cell(1, 1).Add(new Paragraph("Machine #").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell3 = new Cell(1, 1).Add(new Paragraph("Group").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell4 = new Cell(1, 1).Add(new Paragraph("Game Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell5 = new Cell(1, 1).Add(new Paragraph("Total In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell6 = new Cell(1, 1).Add(new Paragraph("Total Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell7 = new Cell(1, 1).Add(new Paragraph("Jackpot").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell8 = new Cell(1, 1).Add(new Paragraph("Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell9 = new Cell(1, 1).Add(new Paragraph("Coin In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell10 = new Cell(1, 1).Add(new Paragraph("Coin Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell11 = new Cell(1, 1).Add(new Paragraph("Coin Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);

                    first2.AddCell(cell1);
                    first2.AddCell(cell2);
                    first2.AddCell(cell3);
                    first2.AddCell(cell4);
                    first2.AddCell(cell5);
                    first2.AddCell(cell6);
                    first2.AddCell(cell7);
                    first2.AddCell(cell8);
                    first2.AddCell(cell9);
                    first2.AddCell(cell10);
                    first2.AddCell(cell11);


                    grand_total.machineNumber = "Grand Total";
                    grand_total.gameName = "";
                    grand_total.groupName = "";
                    grand_total.coinIn = 0;
                    grand_total.coinOut = 0;
                    grand_total.coinNet = 0;
                    grand_total.totalIn = 0;
                    grand_total.totalOut = 0;
                    grand_total.jackPot = 0;
                    grand_total.net = 0;

                    group_total.machineNumber = "Group Total";
                    group_total.gameName = "";
                    group_total.groupName = "";
                    group_total.coinIn = 0;
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPot = 0;
                    group_total.net = 0;

                    for (int i = 0; i < ClearedArray.Count; i++)
                    {
                        customRowFinal temp = ClearedArray[i];

                        if (i != 0 && !ClearedArray[i].machineNumber.Equals(ClearedArray[i - 1].machineNumber))
                        {
                            cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                            cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                            cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                            first2.AddCell(cell12);
                            first2.AddCell(cell13);
                            first2.AddCell(cell14);
                            first2.AddCell(cell15);
                            first2.AddCell(cell16);
                            first2.AddCell(cell17);
                            first2.AddCell(cell18);
                            first2.AddCell(cell19);
                            first2.AddCell(cell20);
                            first2.AddCell(cell21);

                            group_total.totalIn = 0;
                            group_total.totalOut = 0;
                            group_total.jackPot = 0;
                            group_total.net = 0;
                            group_total.coinIn = 0;
                            group_total.coinOut = 0;
                            group_total.coinNet = 0;
                            group_total.groupName = "";
                        }

                        cell12 = new Cell(1, 1).Add(new Paragraph(temp.machineNumber).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell13 = new Cell(1, 1).Add(new Paragraph(temp.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell14 = new Cell(1, 1).Add(new Paragraph(temp.gameName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                        cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                        cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));


                        first2.AddCell(cell12);
                        first2.AddCell(cell13);
                        first2.AddCell(cell14);
                        first2.AddCell(cell15);
                        first2.AddCell(cell16);
                        first2.AddCell(cell17);
                        first2.AddCell(cell18);
                        first2.AddCell(cell19);
                        first2.AddCell(cell20);
                        first2.AddCell(cell21);

                        group_total.totalIn += temp.totalIn;
                        group_total.totalOut += temp.totalOut;
                        group_total.jackPot += temp.jackPot;
                        group_total.net += temp.net;
                        group_total.coinIn += temp.coinIn;
                        group_total.coinOut += temp.coinOut;
                        group_total.coinNet += temp.coinNet;
                        group_total.groupName = temp.groupName;

                        grand_total.totalIn += temp.totalIn;
                        grand_total.totalOut += temp.totalOut;
                        grand_total.jackPot += temp.jackPot;
                        grand_total.net += temp.net;
                        grand_total.coinIn += temp.coinIn;
                        grand_total.coinOut += temp.coinOut;
                        grand_total.coinNet += temp.coinNet;
                    }

                    /*Here Group Total in last.*/
                    cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                    first2.AddCell(cell12);
                    first2.AddCell(cell13);
                    first2.AddCell(cell14);
                    first2.AddCell(cell15);
                    first2.AddCell(cell16);
                    first2.AddCell(cell17);
                    first2.AddCell(cell18);
                    first2.AddCell(cell19);
                    first2.AddCell(cell20);
                    first2.AddCell(cell21);

                    /*Grand Total in Last*/
                    cell12 = new Cell(1, 1).Add(new Paragraph("Grand Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell13 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                    first2.AddCell(cell12);
                    first2.AddCell(cell13);
                    first2.AddCell(cell14);
                    first2.AddCell(cell15);
                    first2.AddCell(cell16);
                    first2.AddCell(cell17);
                    first2.AddCell(cell18);
                    first2.AddCell(cell19);
                    first2.AddCell(cell20);
                    first2.AddCell(cell21);

                    document.Add(first2);
                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }


                /*Deleted Table.*/
                if(DeletedArray.Count>0)
                {
                    Table first3 = new Table(colwidth);

                    document.Add(paragraph);
                    document.Add(paragraph3);
                    document.Add(paragraph4);
                    document.Add(paragraph5);


                    /*Cleared Table*/
                    cell1 = new Cell(1, 10).Add(new Paragraph("Daily Report (Deleted Entries)").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(15f)).SetBackgroundColor(headingColor);
                    cell2 = new Cell(1, 1).Add(new Paragraph("Machine #").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell3 = new Cell(1, 1).Add(new Paragraph("Group").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell4 = new Cell(1, 1).Add(new Paragraph("Game Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell5 = new Cell(1, 1).Add(new Paragraph("Total In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell6 = new Cell(1, 1).Add(new Paragraph("Total Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                    cell7 = new Cell(1, 1).Add(new Paragraph("Jackpot").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell8 = new Cell(1, 1).Add(new Paragraph("Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell9 = new Cell(1, 1).Add(new Paragraph("Coin In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell10 = new Cell(1, 1).Add(new Paragraph("Coin Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                    cell11 = new Cell(1, 1).Add(new Paragraph("Coin Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);

                    first3.AddCell(cell1);
                    first3.AddCell(cell2);
                    first3.AddCell(cell3);
                    first3.AddCell(cell4);
                    first3.AddCell(cell5);
                    first3.AddCell(cell6);
                    first3.AddCell(cell7);
                    first3.AddCell(cell8);
                    first3.AddCell(cell9);
                    first3.AddCell(cell10);
                    first3.AddCell(cell11);


                    grand_total.machineNumber = "Grand Total";
                    grand_total.gameName = "";
                    grand_total.groupName = "";
                    grand_total.coinIn = 0;
                    grand_total.coinOut = 0;
                    grand_total.coinNet = 0;
                    grand_total.totalIn = 0;
                    grand_total.totalOut = 0;
                    grand_total.jackPot = 0;
                    grand_total.net = 0;

                    group_total.machineNumber = "Group Total";
                    group_total.gameName = "";
                    group_total.coinIn = 0;
                    group_total.groupName = "";
                    group_total.coinOut = 0;
                    group_total.coinNet = 0;
                    group_total.totalIn = 0;
                    group_total.totalOut = 0;
                    group_total.jackPot = 0;
                    group_total.net = 0;

                    for (int i = 0; i < DeletedArray.Count; i++)
                    {
                        customRowFinal temp = DeletedArray[i];

                        if (i != 0 && !DeletedArray[i].machineNumber.Equals(DeletedArray[i - 1].machineNumber))
                        {
                            cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                            cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                            cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                            cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                            cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                            first3.AddCell(cell12);
                            first3.AddCell(cell13);
                            first3.AddCell(cell14);
                            first3.AddCell(cell15);
                            first3.AddCell(cell16);
                            first3.AddCell(cell17);
                            first3.AddCell(cell18);
                            first3.AddCell(cell19);
                            first3.AddCell(cell20);
                            first3.AddCell(cell21);

                            group_total.totalIn = 0;
                            group_total.totalOut = 0;
                            group_total.jackPot = 0;
                            group_total.net = 0;
                            group_total.coinIn = 0;
                            group_total.coinOut = 0;
                            group_total.coinNet = 0;
                            group_total.groupName = "";
                        }

                        cell12 = new Cell(1, 1).Add(new Paragraph(temp.machineNumber).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell13 = new Cell(1, 1).Add(new Paragraph(temp.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell14 = new Cell(1, 1).Add(new Paragraph(temp.gameName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                        cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                        cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                        cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));


                        first3.AddCell(cell12);
                        first3.AddCell(cell13);
                        first3.AddCell(cell14);
                        first3.AddCell(cell15);
                        first3.AddCell(cell16);
                        first3.AddCell(cell17);
                        first3.AddCell(cell18);
                        first3.AddCell(cell19);
                        first3.AddCell(cell20);
                        first3.AddCell(cell21);

                        group_total.totalIn += temp.totalIn;
                        group_total.totalOut += temp.totalOut;
                        group_total.jackPot += temp.jackPot;
                        group_total.net += temp.net;
                        group_total.coinIn += temp.coinIn;
                        group_total.coinOut += temp.coinOut;
                        group_total.coinNet += temp.coinNet;
                        group_total.groupName = temp.groupName;

                        grand_total.totalIn += temp.totalIn;
                        grand_total.totalOut += temp.totalOut;
                        grand_total.jackPot += temp.jackPot;
                        grand_total.net += temp.net;
                        grand_total.coinIn += temp.coinIn;
                        grand_total.coinOut += temp.coinOut;
                        grand_total.coinNet += temp.coinNet;
                    }

                    /*Here Group Total in last.*/
                    cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                    first3.AddCell(cell12);
                    first3.AddCell(cell13);
                    first3.AddCell(cell14);
                    first3.AddCell(cell15);
                    first3.AddCell(cell16);
                    first3.AddCell(cell17);
                    first3.AddCell(cell18);
                    first3.AddCell(cell19);
                    first3.AddCell(cell20);
                    first3.AddCell(cell21);

                    /*Grand Total in Last*/
                    cell12 = new Cell(1, 1).Add(new Paragraph("Grand Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell13 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                    first3.AddCell(cell12);
                    first3.AddCell(cell13);
                    first3.AddCell(cell14);
                    first3.AddCell(cell15);
                    first3.AddCell(cell16);
                    first3.AddCell(cell17);
                    first3.AddCell(cell18);
                    first3.AddCell(cell19);
                    first3.AddCell(cell20);
                    first3.AddCell(cell21);

                    document.Add(first3);
                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }

                document.Add(paragraph);
                document.Add(paragraph3);
                document.Add(paragraph4);
                document.Add(paragraph5);

                Table first4 = new Table(colwidth);

                /*Master Table*/
                cell1 = new Cell(1, 10).Add(new Paragraph("Master Report").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(15f)).SetBackgroundColor(headingColor);
                cell2 = new Cell(1, 1).Add(new Paragraph("Machine #").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                cell3 = new Cell(1, 1).Add(new Paragraph("Group").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                cell4 = new Cell(1, 1).Add(new Paragraph("Game Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                cell5 = new Cell(1, 1).Add(new Paragraph("Total In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                cell6 = new Cell(1, 1).Add(new Paragraph("Total Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
                cell7 = new Cell(1, 1).Add(new Paragraph("Jackpot").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                cell8 = new Cell(1, 1).Add(new Paragraph("Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                cell9 = new Cell(1, 1).Add(new Paragraph("Coin In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                cell10 = new Cell(1, 1).Add(new Paragraph("Coin Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);
                cell11 = new Cell(1, 1).Add(new Paragraph("Coin Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_green).SetFontColor(white);

                first4.AddCell(cell1);
                first4.AddCell(cell2);
                first4.AddCell(cell3);
                first4.AddCell(cell4);
                first4.AddCell(cell5);
                first4.AddCell(cell6);
                first4.AddCell(cell7);
                first4.AddCell(cell8);
                first4.AddCell(cell9);
                first4.AddCell(cell10);
                first4.AddCell(cell11);

                grand_total.machineNumber = "Grand Total";
                grand_total.gameName = "";
                grand_total.groupName = "";
                grand_total.coinIn = 0;
                grand_total.coinOut = 0;
                grand_total.coinNet = 0;
                grand_total.totalIn = 0;
                grand_total.totalOut = 0;
                grand_total.jackPot = 0;
                grand_total.net = 0;

                group_total.machineNumber = "Group Total";
                group_total.gameName = "";
                group_total.coinIn = 0;
                group_total.coinOut = 0;
                group_total.coinNet = 0;
                group_total.totalIn = 0;
                group_total.totalOut = 0;
                group_total.jackPot = 0;
                group_total.net = 0;

                for (int i = 0; i < MasterArray.Count; i++)
                {
                    customRowFinal temp = MasterArray[i];

                    if (i != 0 && !MasterArray[i].machineNumber.Equals(MasterArray[i - 1].machineNumber))
                    {
                        cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                        cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                        cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                        cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                        cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                        first4.AddCell(cell12);
                        first4.AddCell(cell13);
                        first4.AddCell(cell14);
                        first4.AddCell(cell15);
                        first4.AddCell(cell16);
                        first4.AddCell(cell17);
                        first4.AddCell(cell18);
                        first4.AddCell(cell19);
                        first4.AddCell(cell20);
                        first4.AddCell(cell21);

                        group_total.totalIn = 0;
                        group_total.totalOut = 0;
                        group_total.jackPot = 0;
                        group_total.net = 0;
                        group_total.coinIn = 0;
                        group_total.coinOut = 0;
                        group_total.coinNet = 0;
                        group_total.groupName = "";
                    }

                    cell12 = new Cell(1, 1).Add(new Paragraph(temp.machineNumber).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell13 = new Cell(1, 1).Add(new Paragraph(temp.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell14 = new Cell(1, 1).Add(new Paragraph(temp.gameName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                    cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(very_light_green);
                    cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));


                    first4.AddCell(cell12);
                    first4.AddCell(cell13);
                    first4.AddCell(cell14);
                    first4.AddCell(cell15);
                    first4.AddCell(cell16);
                    first4.AddCell(cell17);
                    first4.AddCell(cell18);
                    first4.AddCell(cell19);
                    first4.AddCell(cell20);
                    first4.AddCell(cell21);

                    group_total.totalIn += temp.totalIn;
                    group_total.totalOut += temp.totalOut;
                    group_total.jackPot += temp.jackPot;
                    group_total.net += temp.net;
                    group_total.coinIn += temp.coinIn;
                    group_total.coinOut += temp.coinOut;
                    group_total.coinNet += temp.coinNet;
                    group_total.groupName = temp.groupName;

                    grand_total.totalIn += temp.totalIn;
                    grand_total.totalOut += temp.totalOut;
                    grand_total.jackPot += temp.jackPot;
                    grand_total.net += temp.net;
                    grand_total.coinIn += temp.coinIn;
                    grand_total.coinOut += temp.coinOut;
                    grand_total.coinNet += temp.coinNet;
                }

                /*Here Group Total in last.*/
                cell12 = new Cell(1, 1).Add(new Paragraph("Group Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell13 = new Cell(1, 1).Add(new Paragraph(group_total.groupName).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(group_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                first4.AddCell(cell12);
                first4.AddCell(cell13);
                first4.AddCell(cell14);
                first4.AddCell(cell15);
                first4.AddCell(cell16);
                first4.AddCell(cell17);
                first4.AddCell(cell18);
                first4.AddCell(cell19);
                first4.AddCell(cell20);
                first4.AddCell(cell21);

                /*Grand Total in Last*/
                cell12 = new Cell(1, 1).Add(new Paragraph("Grand Total").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell13 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell14 = new Cell(1, 1).Add(new Paragraph("").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell15 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell16 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell17 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.jackPot)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell18 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_green).SetFontColor(white);
                cell19 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell20 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);
                cell21 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(grand_total.coinNet)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(light_blue).SetFontColor(white);

                first4.AddCell(cell12);
                first4.AddCell(cell13);
                first4.AddCell(cell14);
                first4.AddCell(cell15);
                first4.AddCell(cell16);
                first4.AddCell(cell17);
                first4.AddCell(cell18);
                first4.AddCell(cell19);
                first4.AddCell(cell20);
                first4.AddCell(cell21);

                document.Add(first4);
                document.Close();

            }
            CalculateShiftReport();
        }


        public void CalculateShiftReport()
        {
            Database database = new Database();
            MySqlConnection connection = new MySqlConnection(database.connString);

            connection.Open();
            DateTime today = DateTime.Today;
            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);
            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime todayAt8AM = today.Date.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute).AddSeconds(tempTime.Second);
            DateTime yesterdayAt8AM = yesterday.Date.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute).AddSeconds(tempTime.Second);

            // Format the dates for the query
            string todayAt8AMFormatted = todayAt8AM.ToString("yyyy-MM-dd HH:mm:ss");
            string yesterdayAt8AMFormatted = yesterdayAt8AM.ToString("yyyy-MM-dd HH:mm:ss");

            // Construct the query
            string query = $"SELECT * FROM shift_table WHERE End_Time BETWEEN '{yesterdayAt8AMFormatted}' AND '{todayAt8AMFormatted}'";
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
                String duration = Convert.ToString(row[12]) + "-" + Convert.ToString(row[14]);
                String username = (String)row[13];

                shiftClass temp = new shiftClass(start_drawer, total_in, total_out, promo, match_play, fill, drop, expense, tickets, login_id, shift_id, username, duration, net);
                finalArray.Add(temp);
            }

            connection.Close();

            GenerateShiftPDF(finalArray);
        }

        void GenerateShiftPDF(List<shiftClass> finalArray)
        {
            String path = "ShiftReport_" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf";
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdfDocument = new PdfDocument(writer);
            PageSize landscape = PageSize.A4.Rotate();
            pdfDocument.SetDefaultPageSize(landscape);
            Document document = new Document(pdfDocument);

            Paragraph paragraph = new Paragraph("Slot Payout");
            paragraph.SetFontSize(25f);
            paragraph.SetUnderline();
            paragraph.SetTextAlignment(TextAlignment.CENTER);
            paragraph.SetWidth(document.GetPdfDocument().GetDefaultPageSize().GetWidth() - document.GetLeftMargin() - document.GetRightMargin());
            document.Add(paragraph);


            Paragraph paragraph3 = new Paragraph("");
            Paragraph paragraph4 = new Paragraph("");
            Paragraph paragraph5 = new Paragraph("");
            document.Add(paragraph3);
            document.Add(paragraph4);
            document.Add(paragraph5);

            DateTime tempTime = Convert.ToDateTime(Properties.Settings.Default.Counting);
            DateTime currentTime = DateTime.Today;
            String targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

            DateTime yesterdayTime = DateTime.Today.AddDays(-1);
            String targetYesterdayTime = new DateTime(yesterdayTime.Year, yesterdayTime.Month, yesterdayTime.Day, tempTime.Hour, tempTime.Minute, tempTime.Second).ToString("dd-MM-yyyy h:mm:ss tt");

            Paragraph paragraph6 = new Paragraph("From: " + yesterdayTime.DayOfWeek + " " + targetYesterdayTime);
            paragraph6.SetFontSize(14f);
            paragraph6.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph6);

            Paragraph paragraph7 = new Paragraph("To: " + currentTime.DayOfWeek + " " + targetTime);
            paragraph7.SetFontSize(14f);
            paragraph7.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph7);

            Paragraph paragraph2 = new Paragraph("Created On: " + DateTime.Today.DayOfWeek + " " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt"));
            paragraph2.SetFontSize(14f);
            paragraph2.SetTextAlignment(TextAlignment.LEFT);
            document.Add(paragraph2);

            iText.Kernel.Colors.Color headingColor = new DeviceRgb(180, 198, 231);

            iText.Kernel.Colors.Color dark_blue = new DeviceRgb(28, 58, 112);
            iText.Kernel.Colors.Color light_blue = new DeviceRgb(30, 144, 255);
            iText.Kernel.Colors.Color very_light_green = new DeviceRgb(192, 255, 192);
            iText.Kernel.Colors.Color dark_green = new DeviceRgb(0, 128, 0);
            iText.Kernel.Colors.Color light_green = new DeviceRgb(0, 192, 0);
            iText.Kernel.Colors.Color grey = new DeviceRgb(224, 224, 224);
            iText.Kernel.Colors.Color white = new DeviceRgb(255, 255, 255);


            float[] colwidth = { 50f, 50f, 150f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f };
            Table first = new Table(colwidth);

            Cell cell11 = new Cell(1, 14).Add(new Paragraph("Shift Report").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(15f)).SetBackgroundColor(headingColor);
            Cell cell211 = new Cell(1, 1).Add(new Paragraph("Shift_ID").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell212 = new Cell(1, 1).Add(new Paragraph("Username").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell213 = new Cell(1, 1).Add(new Paragraph("Duration").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell21 = new Cell(1, 1).Add(new Paragraph("Starting Drawer").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell22 = new Cell(1, 1).Add(new Paragraph("Total In").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell23 = new Cell(1, 1).Add(new Paragraph("Total Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell24 = new Cell(1, 1).Add(new Paragraph("Promo").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell25 = new Cell(1, 1).Add(new Paragraph("Match Play").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell26 = new Cell(1, 1).Add(new Paragraph("Fill").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell27 = new Cell(1, 1).Add(new Paragraph("Drop").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell28 = new Cell(1, 1).Add(new Paragraph("Expense").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell29 = new Cell(1, 1).Add(new Paragraph("Tickets Cash Out").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);
            Cell cell210 = new Cell(1, 1).Add(new Paragraph("Net").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(dark_blue).SetFontColor(white);

            first.AddCell(cell11);
            first.AddCell(cell211);
            first.AddCell(cell212);
            first.AddCell(cell213);
            first.AddCell(cell21);
            first.AddCell(cell22);
            first.AddCell(cell23);
            first.AddCell(cell24);
            first.AddCell(cell25);
            first.AddCell(cell26);
            first.AddCell(cell27);
            first.AddCell(cell28);
            first.AddCell(cell29);
            first.AddCell(cell210);

            for (int i = 0; i < finalArray.Count; i++)
            {
                shiftClass temp = finalArray[i];

                Cell cell311 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.shift_id)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                Cell cell312 = new Cell(1, 1).Add(new Paragraph(temp.username).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)).SetBackgroundColor(grey);
                Cell cell313 = new Cell(1, 1).Add(new Paragraph(temp.duration).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell31 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.starting_drawer)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell32 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell33 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalOut)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell34 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.totalIn)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell35 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.promo)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell36 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.match_play)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell37 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.fill)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell38 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.drop)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell39 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.expense)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                Cell cell310 = new Cell(1, 1).Add(new Paragraph(Convert.ToString(temp.net)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));


                first.AddCell(cell311);
                first.AddCell(cell312);
                first.AddCell(cell313);
                first.AddCell(cell31);
                first.AddCell(cell32);
                first.AddCell(cell33);
                first.AddCell(cell34);
                first.AddCell(cell35);
                first.AddCell(cell36);
                first.AddCell(cell37);
                first.AddCell(cell38);
                first.AddCell(cell39);
                first.AddCell(cell310);
            }


            document.Add(first);
            document.Close();


            EmailScheduler();
        }

        public void EmailScheduler()
        {
            try
            {
                MailMessage mail = new MailMessage();
                EmailHelper emailHelper = new EmailHelper();
                mail.From = new MailAddress(emailHelper.emailAddress);

                Database database = new Database();
                using (MySqlConnection connection = new MySqlConnection(database.connString))
                {
                    connection.Open();
                    string query = "SELECT Owner_Mail FROM owner_details";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string emailAddress = reader.GetString("Owner_Mail");
                            mail.To.Add(emailAddress);
                        }
                    }
                    connection.Close();
                }

                mail.Subject = "Daily Report: " + DateTime.Now;
                mail.Body = "PFA,";

                // Attach the PDF file
                mail.Attachments.Add(new Attachment("SlotReport_"+DateTime.Today.ToString("yyyy-MM-dd") + ".pdf"));
                mail.Attachments.Add(new Attachment("ShiftReport_" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf"));

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailHelper.emailAddress, emailHelper.emailPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
