using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using ESCPOS_NET;


namespace SlotPOS.Utils
{
    public class Printer
    {
        static public String Receipt(String Amount, String LocationName)
        {
            string TicketNumber = GenerateRandomTicketNumber(18);
            byte[] ticketNumberBytes = Encoding.ASCII.GetBytes(TicketNumber);

            try
            {
                var printer = new SerialPrinter(portName: "COM10", baudRate: 9600);

                // Display the generated ticket information
                Debug.WriteLine($"Ticket Number: {TicketNumber}");

                string dateTime = DateTime.Now.ToString("dd MMMM yyyy HH:mm");
                var esc = new EPSON();
                byte[] Landscape = new byte[] { 0x1D, 0x56, 0x01 };

                byte[] Portart = new byte[] { 0x1D, 0x56, 0x00 };
                byte[] FormFeed = new byte[] { 0x0C };
                byte[] SetCharPerLine = new byte[] { 0x1D, 0x74, 0xFF };// to set wedth of page
                byte[] Barcode_byte = new byte[] { 0x1D, 0x6B, 0x07, 0x12 };
                byte[] Barcode_byte1 = new byte[] { 0x1D, 0x6B, 0x07, 0x12, 0x37, 0x32, 0x36, 0x32, 0x31, 0x37, 0x32, 0x34, 0x36, 0x38, 0x33, 0x39, 0x31, 0x37, 0x35, 0x32, 0x32, 0x39 };
                byte[] Font20cpi = new byte[] { 0x1B, 0x4D };
                byte[] doubleHeightBold = new byte[] { 0x1D, 0x12, 0x0E };
                byte[] doubleWidth = new byte[] { 0x0E };
                byte[] setField = new byte[] { 0x1D, 0x46, 0x01, 0x00, 0x00, 0x00, 0x00 };
                byte[] ESCStar = new byte[] { 0x1B, 0x2A };
                byte[] LineFeed = new byte[] { 0x0a };
                byte[] GST = new byte[] { 0x1d, 0x54, 0x03 };

                var commands = ByteSplicer.Combine(
                    ESCStar,
                    SetCharPerLine,
                    Landscape,
                    // Portart,
                    setField,
                    esc.CenterAlign(),
                    esc.PrintLine(""),
                    esc.SetStyles(PrintStyle.None),
                    Font20cpi,
                    doubleHeightBold,
                    esc.PrintLine(""),
                    esc.PrintLine(LocationName),

                    esc.SetStyles(PrintStyle.None),
                    esc.PrintLine(""),
                    //esc.PrintLine("Jamni Baju Pelo State Aa city 420420 Zip Code US"),

                    Font20cpi,
                    doubleWidth,
                    doubleWidth,
                    doubleWidth,
                    doubleWidth,
                    doubleHeightBold,
                    doubleHeightBold,
                    doubleHeightBold,
                    esc.PrintLine("           PLAYABLE VOUCHER           "),
                    //esc.SetStyles(PrintStyle.None),
                    esc.SetBarWidth(BarWidth.Default),
                    Barcode_byte,
                    ticketNumberBytes,
                    esc.SetStyles(PrintStyle.None),
                    esc.PrintLine(""),
                    esc.Print("VALIDATION      "),
                    esc.PrintLine($"{TicketNumber}"),
                    esc.Print(dateTime),
                    Font20cpi,
                    doubleHeightBold,
                    doubleHeightBold,

                    esc.PrintLine(""),

                    doubleHeightBold,
                    esc.Print("$"),
                    esc.PrintLine(Amount),

                    FormFeed
                    );

                printer.Write(commands);
                Thread.Sleep(1000);
                printer.Dispose();


                Debug.WriteLine("Receipt printed successfully.");


                // send mqtt payload with ticketnumber and amount


                return TicketNumber;


            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Printer connection error: {ex.Message}");

                return "Print failed";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");

                return "Print failed";
            }

        }



        public void PrinterStatus()
        {

        }

        // generate teckit number
        static private string GenerateRandomTicketNumber(int count)
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(count);

            for (int i = 0; i < count; i++)
            {
                int randomDigit = random.Next(10); // Generates a random number between 0 and 9
                stringBuilder.Append(randomDigit);
            }
            Debug.WriteLine(stringBuilder.ToString());

            return stringBuilder.ToString();
        }
    }
}
