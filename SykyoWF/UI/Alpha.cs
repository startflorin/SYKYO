using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1.DL;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DataPersistency.UI.Logging;

namespace WindowsFormsApplication1.UI
{
    class Alpha
    {
        public Alpha()
        {
            getMySQLConnection();
        }
        public static List<Parameter> parameters = new List<Parameter>();
        
        public void Refresh()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                List<int[]> values = new List<int[]>();
                for (int i = 0; i < parameters.Count; i++)
                {
                    if ((WatchParameters.dataGridView1.Rows.Count > i))
                    //WatchParameters.dataGridView1.Rows[i].Cells[0].Value = parameters[i].RefreshValue();
                    WatchParameters.dataGridView1.Rows[i].Cells[0].Value = GetAssociatedValues(new int[] {-100, 100, 0, 0}, parameters[i].ID, new int []{0, 0} )[1];
                }
            }
        }


        static MySqlConnection mySQLConnection = null;
        static string DB = "rpro0001_cabinet";
        private static MySqlConnection getMySQLConnection()
        {
            /*if (this.mySQLConnection != null && mySQLConnection.State == ConnectionState.Open)
            {
                mySQLConnection.Close();
            }*/
            MySqlConnection mySQLConnection = new MySqlConnection();
            try
            {
                mySQLConnection = new MySqlConnection("Server=localhost;Port=3306;Database=" + DB + ";Uid=rpro0001_view;Pwd=undermind;");
                mySQLConnection.Open();
            }
            catch (MySqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return mySQLConnection;
        }

        internal int[] GetAssociatedValues(int[] r, int[] a, int[] b)
        {
            if (mySQLConnection == null)
            {
                mySQLConnection = getMySQLConnection();
            }

            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = "SELECT `c`, `d` FROM `rpro0001_cabinet`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        relationPosition[0] = 0;
                        int.TryParse(myReader.GetString(1), out relationPosition[1]);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }
    
public  bool MyResultsTrace { get; set; }}

    
}
