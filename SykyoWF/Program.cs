using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WindowsFormsApplication1.DL;

namespace WindowsFormsApplication1
{
    static class Program
    {
        private static MySqlConnection getMySQLConnection()
        {
            MySqlConnection mySQLConnection = new MySqlConnection();
            try
            {
                mySQLConnection = new MySqlConnection("Server=localhost;Port=3306;Database=rpro0001_cabinet;Uid=rpro0001_view;Pwd=undermind;");
                mySQLConnection.Open();
            }
            catch (MySqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return mySQLConnection;
        }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //ServerAccess serverAccess = new ServerAccess();
            Application.Run(new MainForm());
        }


    }
}
