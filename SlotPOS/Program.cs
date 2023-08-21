using Hangfire;
using Hangfire.MySql;
using MySql.Data.MySqlClient;
using SlotPOS.Utils;

namespace SlotPOS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Database dataBase = new Database();
            using (MySqlConnection connection = new MySqlConnection(dataBase.connString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS `hangfireslots`", connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            GlobalConfiguration.Configuration
                .UseStorage(
                new MySqlStorage(
                "server=127.0.0.1;port=3306;username = slotposuser;password = Slot@POSUSA;database = hangfireslots;Allow User Variables=True",
                new MySqlStorageOptions
                {
                    TablesPrefix = "Hangfire"
                }
            ));

            ApplicationConfiguration.Initialize();
            using (var server = new BackgroundJobServer())
            {
                // Run your WinForms application
                Application.Run(new SplashScreen());
            }
        }
    }
}