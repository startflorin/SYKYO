#region Composition

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using DataPersistency.UI.Logging;

#endregion Composition

namespace WindowsFormsApplication1.DL
{
    class ServerAccess
    {
        #region Variables

        static public List<string> GoodBDs = new List<string>();
        static public string DB;
        static public bool MySqlTrace = false;
        static public bool MyResultsTrace = false;
        static MySqlConnection mySQLConnection = null;
        MySqlException mysqlException = null;
        MySqlCommand mysqlCommand = null;

        #endregion Variables

        #region Construction

        static ServerAccess()
        {
            mySQLConnection = getMySQLConnection(null);

            List<string> databaseNames = new List<string>();
            try
            {
                try
                {
                    string commandString = "show databases ;";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            databaseNames.Add(myReader.GetString(0));
                        }
                    }
                    myReader.Close();
                }
                catch (InvalidOperationException ioe)
                {
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            foreach (string database in databaseNames)
            {
                List<string> tableNames = new List<string>();
                try
                {
                    try
                    {
                        mySQLConnection.ChangeDatabase(database);
                        getMySQLConnection(database);
                        string commandString = "USE " + database + "; SHOW TABLES ;";
                        MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                        mySqlCommand.CommandTimeout = 999999;
                        MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                        if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                        while (myReader.Read())
                        {
                            if (!string.IsNullOrEmpty(myReader.GetString(0)))
                            {
                                string tb = myReader.GetString(0);
                                //string db = myReader.GetString(0);
                                if (tb.Equals("simbs") || tb.Equals("srel"))
                                {
                                    tableNames.Add(tb);
                                }
                            }
                        }
                        myReader.Close();
                    }
                    catch (InvalidOperationException ioe)
                    {
                    }
                }
                catch (MySqlException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }

                if (tableNames.Contains("simbs") && tableNames.Contains("srel"))
                {
                    GoodBDs.Add(database);
                }
            }

            DB = GoodBDs[0];
            //if (MyResultsTrace) SQLView.LogResult(new string[] { name });
        }

        /// <summary>
        /// Initiate connection
        /// </summary>
        public ServerAccess()
        {
            if (mySQLConnection == null)
            {
                mySQLConnection = getMySQLConnection("" + DB + "");
            }
            // TODO: Complete member initialization
        }
        ~ServerAccess()
        {
            //if (mySQLConnection != null ) mySQLConnection.Close();
        }

        #endregion Construction

        #region Helpers

        public static string DatabaseName
        {
            get
            {
                return DB;
            }
            set
            {
                if (DatabaseExists(value))
                {
                    DB =  value;
                    mySQLConnection = getMySQLConnection(DB);
                }
            }
        }

        private static bool DatabaseExists(string value)
        {
            return true;
        }

        private static MySqlConnection getMySQLConnection(string DBc)
        {
            if (string.IsNullOrEmpty(DBc))
            {
                DBc = DB;
            }
            MySqlConnection mySQLConnection = new MySqlConnection();
            try
            {
            if (mySQLConnection != null && mySQLConnection.State == ConnectionState.Open)
            {
                mySQLConnection.Close();
            }
                mySQLConnection = new MySqlConnection("Server=localhost;Port=3306;Database=" + DBc + ";Uid=root;Pwd=Start1312;");
                mySQLConnection.Open();
            }
            catch (MySqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return mySQLConnection;
        }

        #endregion Helpers

        #region Relation (Component)

        #region get relation name by relation ID (int[4]) : List<string>
        /// <summary>
        /// Find relation by id
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string getRelationNameByID(int[] a)
        {
            string nameString = string.Empty;
            try
            {
                try
                {
                    string commandString = "SELECT `En` FROM `" + DB + "`.`srel` WHERE `IR` = " + a[0] + " AND `Var` = " + a[1] + " AND `IR2` = " + a[2] + " AND `Var2` = " + a[3] + "  ;";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            nameString = myReader.GetString(0);
                        }
                    }
                    myReader.Close();
                }
                catch (InvalidOperationException ioe)
                {
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new string[] { nameString });
            return nameString;
        }
        #endregion

        #region get relation ID by relation name (string) : List<int[4]>

        /// <summary>
        /// Get relation id by name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int[] getRelationID(string Name)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = "SELECT `IR`, `Var` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + Name.Trim() + "' ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out relationPosition[0]);
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

        #endregion

        #region get relation position

        /// <summary>
        /// Get exact relation between symbols position
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal int[] GetRelationPosition(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { -1, -1 };
            try
            {
                string commandString = "SELECT `ID` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[0] + " AND `d` = " + r[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        relationPosition[0] = a[0];
                        int.TryParse(myReader.GetString(0), out relationPosition[1]);
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

        #endregion

        #region get all relations

        internal List<int[]> getAllRelations()
        {
            List<int[]> allRelations = new List<int[]>();
            try
            {
                string commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);

                //List<string[]> list = (from IDataRecord r in myReader
                //                     select (string)r["IR"]
                //    ).ToList(); 
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[4];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[2]);
                        int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            allRelations.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in allRelations)
                {
                    //ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return allRelations;
        }

        #endregion

        #endregion Relation (Component)

        #region Symbol (Component)

        #region count symbol occurences by symbol name (string) : int

        /// <summary>
        /// Count symbols with same name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        internal int CountOccurences(string Name)
        {
            int symbolPosition = -1;
            try
            {
                string commandString = "SELECT count(`ID`) FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + Name.Trim() + "' ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out symbolPosition);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new int[] { symbolPosition });
            return symbolPosition;
        }

        #endregion

        #region count symbol occurences by symbol ID (int[2]) : int

        /// <summary>
        /// Count simbols with same name USELESS to count unique ids just to say if exists
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        internal int CountOccurences(int[] ID)
        {
            int symbolPosition = -1;
            try
            {
                string commandString = "SELECT count(`ID`) FROM `" + DB + "`.`simbs` WHERE `ID` = " + ID[0] + " ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out symbolPosition);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new int[] { symbolPosition });
            return symbolPosition;
        }

        #endregion

        #region get symbol name by symbol ID (int[2]) : List<string>

        /// <summary>
        /// Find symbol name forom id
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string getSymbolNameByID(int[] a)
        {
            string name = string.Empty;
            try
            {
                try
                {
                    string commandString = "SELECT `En` FROM `" + DB + "`.`simbs` WHERE `ID` = " + a[0] + " ;";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            name = myReader.GetString(0);
                        }
                    }
                    myReader.Close();
                }
                catch (InvalidOperationException ioe)
                {
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new string[] { name });
            return name;
        }

        #endregion

        #region get symbol ID by symbol name (string) : List<int[2]>

        /// <summary>
        /// Find id of a name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>return last found</returns>
        public int[] getSymbolIDByName(string Name)
        {
            int[] symbolPosition = { 0, 0 };
            try
            {
                try
                {
                    string commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + Name.Trim() + "' ;";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            int.TryParse(myReader.GetString(0), out symbolPosition[0]);
                        }
                    }
                    myReader.Close();
                }
                catch (InvalidOperationException ioe)
                {
                    mySQLConnection = getMySQLConnection(null);
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return symbolPosition;
        }

        #endregion

        #region get symbols by name (substring, method) : List<int[2]>
        internal List<int> getSymbsBySubstring(string substring, int method, int limit)
        {
            List<int> listSymbol = new List<int>();
            try
            {
                string commandString;
                switch (method)
                {
                    case 0: // strict
                        commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + substring + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + substring + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '%" + substring + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + substring + "%' ";
                        break;
                    default:
                        commandString = "SELECT `ID` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + substring + "' ";
                        break;
                }
                if (limit != 0) { commandString += " Limit " + limit + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int simbPos = 0;
                        int.TryParse(myReader.GetString(0), out simbPos);
                        listSymbol.Add(simbPos);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int i in listSymbol)
                {
                    ResultLog += " [" + i + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }
        #endregion

        #region create symbol ID by symbol name (string) : List<int[2]>


        /// <summary>
        /// InsertNewSymbol and generate the related SimbolTable
        /// </summary>
        /// <param name="symbolEN"></param>
        /// <returns></returns>
        public int[] CreateSymbol(string symbolEN)
        {
            int[] symbolPosition = { 0, 0 };
            try
            {
                string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `En`) VALUES (NULL, '" + symbolEN + "');";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    symbolPosition[0] = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            CreateSymbolTable(symbolPosition);
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return symbolPosition;
        }

        #endregion

        #region create symbol table

        /// <summary>
        /// Create a nonexistent table and rezerve first line with id 1
        /// </summary>
        /// <param name="symbolPosition">position of table reference in simbols table can be used to decode the table  name</param>
        private void CreateSymbolTable(int[] symbolPosition)
        {
            string commandString = "CREATE TABLE IF NOT EXISTS `" + DB + "`.`s" + symbolPosition[0].ToString("00000000") + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , PRIMARY KEY (`ID`) );";
            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
            commandString = "INSERT INTO `" + DB + "`.`s" + symbolPosition[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (0,0,0,0,0,0) ;";
            mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
        }

        #endregion

        #endregion Symbol (Component)

        #region Relation (Interaction)

        #region create interaction ID by <R> <A> <B> : List<int[4]>

        /// <summary>
        /// Create relation as new
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] CreateRelationAsNew(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                int rr = 0;
                switch (r[1])
                {
                    case 100: rr = 1; break;
                    case 1: rr = 100; break;
                    case 10: rr = -10; break;
                    case -10: rr = 10; break;
                };
                string commandString = commandString = "INSERT INTO `" + DB + "`.`s" + b[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + b[0] + ", " + b[1] + "," + r[2] + ", " + r[3] + "," + a[0] + ", " + a[1] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`s" + a[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + ", " + r[0] + ", " + r[1] + ", " + b[0] + ", " + b[1] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = a[0];
                    relationPosition[1] = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        #endregion Relation (Interaction)

        #region is reflexive

        /// <summary>
        /// Is inverse relation true
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal List<int[]> IsReflexive(int[] r, int[] a, int[] b, int limit)
        {
            return IsTransitive(new List<int[]>(), r, b, a, limit);
        }

        #endregion

        #region is transitive

        /// <summary>
        /// Is transitive
        /// </summary>
        /// <param name="result"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal List<int[]> IsTransitive(List<int[]> result, int[] r, int[] a, int[] b, int limit)
        {
            List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            bool solved = false;
            try
            {
                string commandString;
                if (false) // ($t1[0]=='?x')
                {
                    commandString = "SELECT `ID` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` LIKE " + r[0] + " AND `e` = " + b[0] + " ";
                }
                else
                {
                    commandString = "SELECT `ID` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` = " + r[0] + " AND `e` = " + b[0] + " ";
                }
                if (limit > 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (myReader.HasRows)
                {
                    solved = true;
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            int[] symbID = new int[] { a[0], 0 };
                            int.TryParse(myReader.GetString(0), out symbID[1]);
                            if (symbID[0] != 0 || symbID[1] != 0)
                            {
                                ReturnList.Add(symbID);
                            }
                        }
                    }
                    myReader.Close();
                }
                else
                {
                    myReader.Close();
                    if (true) // ($t1[0]=='?x')
                    {
                        commandString = "SELECT `ID`, `e`, `f` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` = " + r[0] + " ";
                    }
                    else
                    {
                        commandString = "SELECT `ID`, `e`, `f` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c`< 5000 ";
                    }
                    if (limit > 0) { commandString += " Limit " + limit; }
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand2 = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader2 = mySqlCommand2.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand2.CommandText);
                    List<int[]> toTry = new List<int[]>();
                    while (myReader2.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader2.GetString(0)))
                        {
                            int[] symbID = new int[] { 0, 0, 0 };
                            int.TryParse(myReader2.GetString(0), out symbID[0]);
                            int.TryParse(myReader2.GetString(1), out symbID[1]);
                            int.TryParse(myReader2.GetString(2), out symbID[2]);
                            if (symbID[1] != 0 || symbID[2] != 0)
                            {
                                toTry.Add(symbID);
                            }
                        }

                    }
                    myReader2.Close();
                    for (int i = 0; i < toTry.Count; i++)
                    {
                        if (!solved)
                        {
                            foreach (int[] symbID in toTry)
                            {
                                if (symbID[1] != 0 || symbID[2] != 0)
                                {
                                    LocalReturnList = IsTransitive(LocalReturnList, r, new int[] { symbID[1], symbID[2] }, b, limit);
                                }
                                if (LocalReturnList.Count > 0)
                                {
                                    //int[] ToAdd = new int[] { a[0], symbID[0] };
                                    //ReturnList.Add(new int[] { symbID[1], symbID[2] });
                                    ReturnList.Add(new int[] { a[0], symbID[0] });
                                    ReturnList.AddRange(LocalReturnList);
                                    solved = true;
                                }
                            }
                        }
                    }

                }


            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in ReturnList)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return ReturnList;
        }

        #endregion

        #region find container

        /// <summary>
        /// find container
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal List<int[]> FindContainer(int[] r, int[] a, int[] b, int limit) // TO DO Fond container for more then just a=b=c but also for a=b=
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[0] + " ";
                if (limit > 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[2];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            listSymbol.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in listSymbol)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }

        #endregion

        #region find content

        /// <summary>
        /// Find content
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal List<int[]> FindContent(int[] r, int[] a, int[] b, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[0] + " ";
                if (limit > 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[2];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            listSymbol.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in listSymbol)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }

        #endregion

        #region solve

        public List<int[]> SolveRelation(int[] r, int[] a, int[] b, int limit)
        {
            // Algebric logics
            // null        == [ 0,  0]   // nothing, ignorance
            // measure     == [ 0,+/-]   // levels, needs, satisfacrion, states
            // event       == [ +,  -]   // decisions, state modifiers
            // object      == [ +,0/+]   // static objects, existence
            // incertitude == [ -,  +]   // cumulator, unoriented, unknown
            // collision   == [ -,  -]   // collision, boundary, buffer, meta
            List<int[]> resultNULL = new List<int[]>();


            // CASE 0:
            // relation is unknown
            if (r[0] == 0 && r[1] == 0 && r[2] == 0 && r[3] == 0) // SOMETHING ? SOMETHING
            {
                List<int[]> result = new List<int[]>();
                return result;
            }

            // CASE 1:
            // both operands are nulls
            if (a[0] == 0 && a[1] == 0 && b[0] == 0 && b[1] == 0) // [ 0,  0] => [ 0,  0]  // NULL => NULL
            {
                List<int[]> result = new List<int[]>();
                result.Add(new int[] { 0, 0 });
                return result; // NULL
            }

            // CASE 2:
            // first operand is null
            if ((a[0] == 0 && a[1] == 0) && (b[0] > 0 && b[1] >= 0)) // [ 0,  0] => [ +,  +]  // NULL => SOMETHING
            {
                List<int[]> result = new List<int[]>();
                result.Add(new int[] { 0, 0 });
                return result; // NULL
            }

            // CASE 3:
            // second operand is null
            if ((a[0] > 0 && a[1] >= 0) && (b[0] == 0 && b[1] == 0)) // [ +,  +] => [ 0,  0] // SOMETHING => NULL
            {
                List<int[]> result = new List<int[]>();
                result.Add(new int[] { 0, 0 });
                return result; // NULL
            }

            // CASE 4:
            // both operands are not-nulls
            if ((a[0] < 0 && a[1] > 0) && (b[0] < 0 || b[1] > 0)) // [ -,  +] => [ -,  +]  // ? => ?
            {
                List<int[]> result = new List<int[]>();
                result.Add(new int[] { 0, 0 });
                return result; // NULL
            }

            // CASE 5:
            // unknown preoperand
            if ((a[0] < 0 && a[1] > 0) && (b[0] > 0 || b[1] >= 0)) // [ -,  +] => [ +,  +]  // ? => SOMETHING
            {
                return FindContainer(r, a, b, limit);
            }

            // CASE 6:
            // both unknown postoperand
            if ((a[0] > 0 && a[1] >= 0) && (b[0] < 0 && b[1] > 0)) // [ +,  +] => [ -,  +]  // SOMETHING => ?
            {
                return FindContent(r, a, b, limit);
            }

            // CASE 4:
            // both operands are not-nulls
            if ((a[0] > 0 && a[1] >= 0) && (b[0] > 0 || b[1] >= 0)) // [ +,  +] => [ -,  +]  // SOMETHING => SOMETHING
            {
                return IsTransitive(new List<int[]>(), r, a, b, 10);
            }

            return resultNULL;
        }

        #endregion

        #region get leafs by relation

        public List<int[]> getLeafsByRelation(int[] r, int[] a, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[0] + " AND `d` = " + r[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[2];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            listSymbol.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in listSymbol)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }

        #endregion

        #region exists relation
        
        internal int ExistsRelation(int[] r, int[] a, int[] b)
        {
            int relationCount = 0;
            try
            {
                string commandString = "SELECT count(`ID`) FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[0] + " AND `d` = " + r[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out relationCount);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new int[] { relationCount });
            return relationCount;
        }

        #endregion

        #endregion Relation (Interaction)

        #region Multiplicity-Reference

        #region get association position

        /// <summary>
        /// Get exact value position
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal int[] GetAssociationPosition(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { -1, -1 };
            try
            {
                string commandString = "SELECT `ID` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` = " + r[3] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        relationPosition[0] = a[0];
                        int.TryParse(myReader.GetString(0), out relationPosition[1]);
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

        #endregion

        #region get association value

        /// <summary>
        /// Get last value !!! it should be list
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal int[] GetAssociatedValues(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
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

        #endregion

        #region find specified property

        /// <summary>
        /// Find content PLEASE DELETE ME
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal List<int[]> FindSpecifiedProperty(int[] r, int[] a, int[] b, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT `e`, `f,m ` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = 2 AND `d` = 100 AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                if (limit > 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[2];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            listSymbol.Add(symbID);
                        }
                    }
                }

                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in listSymbol)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }

        #endregion

        #region solve comparation

        /// <summary>
        /// Alter value and return resultant value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<int[]> SolveComparation(int[] r, int[] a, int[] b, int limit)
        {
            CreateValueTable(a);
            CreateValueTable(b);
            List<int[]> resultNULL = new List<int[]>();
            try
            {
                string commandString1 = "";
                switch (r[1])
                {
                    case 5: // ==
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` = " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                    //case 0:
                    //    commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `c` = `c` + " + r[2] + " SET `d`= `d` + " + r[3] + " WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `d` =" + a[1] + " ) ;";
                    //    break;
                    case -5: // !=
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` != " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                    case 100: // >
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` > " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                    case -100: // <
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` < " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                    case 110: // >=
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` >= " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                    case -110: // <=
                        commandString1 = "SELECT `c`, `d` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + r[2] + " AND `d` <= " + r[3] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ";
                        break;
                }
                if (limit != 0) { commandString1 += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString1, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[4];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        //int.TryParse(myReader.GetString(2), out symbID[2]);
                        //int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (symbID[2] == 0 || symbID[3] == 0)
                        {
                            resultNULL.Add(new int[] { symbID[0], symbID[1] });
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in resultNULL)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return resultNULL;
        }

        #endregion

        #region get value

        /// <summary>
        /// Get All Values of a symbol by passing the object(subobject)
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<int[]> GetValue(int[] r, int[] a, int[] b, int limit)
        {
            /// r==0 get numeric value
            /// r==1 get reference /// de fapt nu am nevoie :)
            CreateValueTable(a);
            CreateValueTable(b);
            List<int[]> resultNULL = new List<int[]>();
            try
            {
                string commandString = "SELECT `c`, `d`, `e`, `f` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[4];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[2]);
                        int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (symbID[2] == 0 || symbID[3] == 0)
                        {
                            resultNULL.Add(new int[] { symbID[0], symbID[1] });
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in resultNULL)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return resultNULL;
        }

        /// <summary>
        /// Get All Values of a symbol by passing the object(subobject)
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<int[]> GetReferince(int[] r, int[] a, int[] b, int limit)
        {
            /// r==0 get numeric value
            /// r==1 get reference /// de fapt nu am nevoie :)
            CreateValueTable(a);
            CreateValueTable(b);
            List<int[]> resultNULL = new List<int[]>();
            try
            {
                string commandString = "SELECT `c`, `d`, `e`, `f` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[4];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[2]);
                        int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (symbID[2] == 0 || symbID[3] == 0)
                        {
                            resultNULL.Add(new int[] { symbID[0], symbID[1] });
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in resultNULL)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return resultNULL;
        }

        #endregion

        #region exists values

        internal int ExistsValues(int[] r, int[] a, int[] b)
        {
            CreateValueTable(a);
            CreateValueTable(b);

            int relationCount = 0;
            try
            {
                string commandString = "SELECT count(`ID`) FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out relationCount);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new int[] { relationCount });
            return relationCount;
        }

        #endregion

        #region exists associated values

        internal int ExistsAssociatedValues(int[] r, int[] a, int[] b)
        {
            CreateValueTable(a);
            CreateValueTable(b);

            int relationCount = 0;
            try
            {
                string commandString = "SELECT count(`ID`) FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` = " + b[1] + " ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int.TryParse(myReader.GetString(0), out relationCount);
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(new int[] { relationCount });
            return relationCount;
        }
        #endregion

        #region assign multiplicity
        /// <summary>
        /// Create value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] AssignValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = commandString = "INSERT INTO `" + DB + "`.`v" + b[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + "," + r[2] + ", " + r[3] + "," + b[0] + ", " + b[1] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                if (b[0] != 0 && b[1] != 0) mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`v" + a[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + ", " + r[2] + ", " + r[3] + ", " + b[0] + ", " + b[1] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = a[0];
                    relationPosition[1] = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        /// <summary>
        /// Create value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] AssignNewReferince(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = commandString = "INSERT INTO `" + DB + "`.`v" + b[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + "," + r[2] + ", " + r[3] + "," + b[0] + ", " + b[1] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                if (b[0] != 0 && b[1] != 0) mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`v" + a[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + ", " + r[2] + ", " + r[3] + ", " + b[0] + ", " + b[1] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = a[0];
                    relationPosition[1] = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        #endregion

        #region reassign multiplicity
        /// <summary>
        /// Alter value 
        /// Now, this is only one overwritable value type. = > < >= <=>
        /// </summary>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] ReAssignValue(int[] r, int[] a, int[] b)
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString1 = "";
                string commandString2 = "";
                switch (r[1])
                {
                    case 0: // =
                        commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `d`= " + r[3] + " WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `f` =" + a[1] + "  ;";
                        commandString2 = "UPDATE `" + DB + "`.`v" + a[0].ToString("00000000") + "` SET `d`= " + r[3] + " WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ;";
                        break;
                    //case 0:
                    //    commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `c` = `c` + " + r[2] + " SET `d`= `d` + " + r[3] + " WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `d` =" + a[1] + " ) ;";
                    //    break;
                    case 1: // ++
                        commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `d`= `d` + 1 WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `f` =" + a[1] + "  ;";
                        commandString2 = "UPDATE `" + DB + "`.`v" + a[0].ToString("00000000") + "` SET `d`= `d` + 1 WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ;";
                        break;
                    case -1: // --
                        commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `d`= `d` - 1 WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `f` =" + a[1] + "  ;";
                        commandString2 = "UPDATE `" + DB + "`.`v" + a[0].ToString("00000000") + "` SET `d`= `d` - 1 WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ;";
                        break;
                    case 10: // +=
                        commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `d`= `d` + " + r[3] + " WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `f` =" + a[1] + "  ;";
                        commandString2 = "UPDATE `" + DB + "`.`v" + a[0].ToString("00000000") + "` SET `d`= `d` + " + r[3] + " WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ;";
                        break;
                    case -10: // -=
                        commandString1 = "UPDATE `" + DB + "`.`v" + b[0].ToString("00000000") + "` SET `d`= `d` - " + r[3] + " WHERE `a` = " + b[0] + " AND `b` = " + b[1] + " AND `e` = " + a[0] + " AND `f` =" + a[1] + "  ;";
                        commandString2 = "UPDATE `" + DB + "`.`v" + a[0].ToString("00000000") + "` SET `d`= `d` - " + r[3] + " WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `e` = " + b[0] + " AND `f` =" + b[1] + "  ;";
                        break;
                }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString1, mySQLConnection);
                //if (mySQLConnection.
                if (b[0] != 0 && b[1] != 0) mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);

                mySqlCommand = new MySqlCommand(commandString2, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                //if (mySqlCommand.LastInsertedId > 0)
                //{
                int[] MultiplicityReference = GetValue(r, a, b, 1).FirstOrDefault();
                //relationPosition = MultiplicityReference;
                relationPosition = new int[] { MultiplicityReference[0], MultiplicityReference[1] };
                //}
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        #endregion

        #region create multiplicity-referince table

        /// <summary>
        /// Create a nonexistent table and rezerve first line with id 1
        /// </summary>
        /// <param name="symbolPosition">position of table reference in simbols table can be used to decode the table  name</param>
        private void CreateValueTable(int[] symbolPosition)
        {
            string commandString = "CREATE TABLE IF NOT EXISTS `" + DB + "`.`v" + symbolPosition[0].ToString("00000000") + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , PRIMARY KEY (`ID`) );";
            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
            commandString = "INSERT INTO `" + DB + "`.`s" + symbolPosition[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (0,0,0,0,0,0) ;";
            mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            //mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
        }

        #endregion

        #region get refered symbol

        /// <summary>
        /// Get refered symbol of a relation
        /// </summary>
        /// <returns></returns>
        internal List<int[]> getReferedSymbol(int[] r, int[] a, int[] b, int limit)
        {
            CreateValueTable(a);
            CreateValueTable(b);
            List<int[]> resultNULL = new List<int[]>();
            try
            {
                string commandString = "SELECT `c`, `d`, `e`, `f` FROM `" + DB + "`.`v" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `b` = " + a[1] + " AND `c` = " + b[0] + " AND `d` = " + b[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[4];
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[2]);
                        int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (symbID[2] == 0 || symbID[3] == 0)
                        {
                            resultNULL.Add(new int[] { symbID[2], symbID[3] });
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in resultNULL)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return resultNULL;
        }

        #endregion

        #endregion Multiplicity-Referince
    }
}
