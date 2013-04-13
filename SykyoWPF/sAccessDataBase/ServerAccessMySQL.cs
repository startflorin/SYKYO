#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.InteropServices;
using WpfApplication1.SElement;
#endregion USING

namespace WpfApplication1.sDataAccessBase
{
    public class ServerAccessMySQL : ServerAccessInterface
    {

        #region Variables
        public bool AcceptSymbols { get; set; }
        public bool AcceptOperators { get; set; }
        public bool AcceptRelations { get; set; }
        static public List<string> GoodBDs = new List<string>();
        static public string DB;
        static public bool MySqlTrace = false;
        static public bool MyResultsTrace = false;
        static MySqlConnection mySQLConnection = null;
        MySqlException mysqlException = null;
        MySqlCommand mysqlCommand = null;

        // the logging system regerence
        //static LoggingSystem.Log loggingSystem = new LoggingSystem.Log();

        #endregion Variables

        #region Construction

        static ServerAccessMySQL()
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
                    //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
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
                        MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                        //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                        while (myReader.Read())
                        {
                            if (!string.IsNullOrEmpty(myReader.GetString(0)))
                            {
                                string tb = myReader.GetString(0);
                                string db = myReader.GetString(0);
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
        public ServerAccessMySQL()
        {
            if (mySQLConnection == null)
            {
                mySQLConnection = getMySQLConnection("" + DB + "");
            }
            // TODO: Complete member initialization
        }
        //~ServerAccessOracles()
        //{
            //if (mySQLConnection != null ) mySQLConnection.Close();
        //}

        #endregion Construction

        #region Helpers

        public string GetProviderName() { return "MySQL"; }

        public string getConnectionState() { return mySQLConnection.State.ToString(); }

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
                    DB = value;
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

        #region Symbol Interface

        internal List<int[]> CreateSymbolAlias(string SymbolAliasName, int[] ExistentID)
        {
            List<int[]> ResultedSymbols = new List<int[]>();
            int[] symbolPosition = new int[] { 0, 0, 0, 1 };
            // Check if alias exists
            if (ExistentID[0] != 0 || ExistentID[1] != 0)
            {
                List<string> SymbolSynonimes = GetSymbolNamesByID(ExistentID, 0, 0);
                int[] match = new int[] { 0, 0 };
                if (SymbolSynonimes.Count < 1)
                {
                    return ResultedSymbols;
                }
                if (SymbolSynonimes.Contains(SymbolAliasName))
                {
                    ResultedSymbols.Add(ExistentID);
                    return ResultedSymbols;
                }
            }
            if (AcceptSymbols)
            {
                try
                {
                    string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `IR`, `En`) VALUES (NULL, " + ExistentID[0] + ", '" + SymbolAliasName + "');";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    mySqlCommand.ExecuteNonQuery();
                    //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    if (mySqlCommand.LastInsertedId > 0)
                    {
                        symbolPosition[0] = (int)mySqlCommand.LastInsertedId;
                        ResultedSymbols.Add(ExistentID);
                    }
                }
                catch (MySqlException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }
            }
            //if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }    

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        public List<string> GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = SymbolID;
            List<string> ResultedSymbolNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT `En` FROM `" + DB + "`.`simbs` WHERE `IR` = " + a[0] + " ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            ResultedSymbolNames.Add(symbolName);
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
                foreach (string i in ResultedSymbolNames)
                {
                    ResultLog += "" + i + "";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedSymbolNames;
        }

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        List<string> ServerAccessInterface.GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = SymbolID;
            List<string> ResultedSymbolNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT `En` FROM `" + DB + "`.`simbs` WHERE `IR` = " + a[0] + " ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            ResultedSymbolNames.Add(symbolName);
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
                foreach (string i in ResultedSymbolNames)
                {
                    ResultLog += "" + i + "";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedSymbolNames;
        }

        // +GetSymbolsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        public List<SObject> GetSymbolsByName(string SymbolNamePart, int Mode, int LimitOfResults = 10)
        {
            List<SObject> ResultedSymbols = new List<SObject>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    case 0: // strict
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + SymbolNamePart + "' ";
                        break;
                    case 1: // starting with
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + SymbolNamePart + "%' ";
                        break;
                    case 2: // containing
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '%" + SymbolNamePart + "%' ";
                        break;
                    case 3: // ending with
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '%" + SymbolNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + SymbolNamePart + "' ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0))) //TO DO null value
                    {
                        SObject symbolID = new SObject();
                        int temporarId = -1;
                        int.TryParse(myReader.GetString(0), out temporarId);
                        symbolID.ObjectId = temporarId;
                        if (symbolID.ObjectId != 0 || symbolID.ObjectId != 0)
                        {
                            ResultedSymbols.Add(symbolID);
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
                foreach (SObject i in ResultedSymbols)
                {
                    ResultLog += " [" + i.ObjectId + ":" + "0" + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedSymbols.Count < 1 && Mode == 0 && AcceptSymbols)
            {
                List<SObject> CreatedSymbols = new List<SObject>();
                CreatedSymbols = CreateSymbolByName(SymbolNamePart, true); // set AcceptDuplicates to avoid other check for existence
                return CreatedSymbols;
            }
            return ResultedSymbols;
        }

        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        public List<SObject> CreateSymbolByName(string SymbolName, bool AcceptDuplicates)
        {
            if (! AcceptDuplicates)
            {
                List<SObject> ExistentSymbols = GetSymbolsByName(SymbolName, 0, 10);
                if (ExistentSymbols.Count > 0) return ExistentSymbols;
            }
            List<SObject> ResultedSymbols = new List<SObject>();
            SObject symbolPosition = new SObject();
            try
            {
                string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `En`) VALUES (NULL, '" + SymbolName + "');";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    string commandString2 = "UPDATE `" + DB + "`.`simbs` SET `IR` = `ID` WHERE `ID` = " + (int)mySqlCommand.LastInsertedId + " ;";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand2 = new MySqlCommand(commandString2, mySQLConnection);
                    mySqlCommand2.ExecuteNonQuery();
                    symbolPosition.ObjectId = (int)mySqlCommand.LastInsertedId;
                    ResultedSymbols.Add(symbolPosition);
                }
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            CreateSymbolTable(symbolPosition);
            //if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }

        #endregion Symbol Interface

        #region Relation Interface
        public List<int[]> getRecord(int symbNr, int offset)
        {
            return new List<int[]>();
        }
        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        List<string> ServerAccessInterface.GetOperatorNamesByID(int[] OperatorID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = OperatorID;
            List<string> ResultedOperatorNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT `En` FROM `" + DB + "`.`srel` WHERE `IR` = " + a[0] + " AND `Var` = " + a[1] + " AND `IR2` = " + a[4] + " AND `Var2` = " + a[5] + " ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            ResultedOperatorNames.Add(symbolName);
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
                foreach (string i in ResultedOperatorNames)
                {
                    ResultLog += "" + i + "";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedOperatorNames;
        }

        // +GetSymbolsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        public List<SOperator> GetOperatorsByName(string OperatorNamePart, int Mode, int LimitOfResults = 10)
        {
            List<SOperator> ResultedOperators = new List<SOperator>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    case 0: // strict
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + OperatorNamePart + "' ";
                        break;
                    case 1: // starting with
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + OperatorNamePart + "%' ";
                        break;
                    case 2: // containing
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '%" + OperatorNamePart + "%' ";
                        break;
                    case 3: // ending with
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '%" + OperatorNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + OperatorNamePart + "' ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] relationID = new int[] { 0, 0, 0, 0, 0, 0 };
                        int.TryParse(myReader.GetString(0), out relationID[0]);
                        int.TryParse(myReader.GetString(1), out relationID[1]);
                        int.TryParse(myReader.GetString(2), out relationID[4]);
                        int.TryParse(myReader.GetString(3), out relationID[5]);
                        if (relationID[0] != 0 || relationID[1] != 0)
                        {
                            //ResultedOperators.Add(relationID);
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
                foreach (SOperator i in ResultedOperators)
                {
                    ResultLog += " [" + i.OperatorId + ":" + "0" + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedOperators.Count < 1 && Mode == 0 && AcceptSymbols)
            {
                List<SOperator> CreatedSymbols = new List<SOperator>();
                //CreatedSymbols = CreateSymbolByName(OperatorNamePart, true); // set AcceptDuplicates to avoid other check for existence
                return CreatedSymbols;
            }
            return ResultedOperators;
        }


        public List<int[]> GetContent(int[] r, int[] aMul, int[] aLoc, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT `e`, `f`, `g`, `h` FROM `" + DB + "`.`s" + aLoc[0].ToString("00000000") + "` WHERE `a` = " + aLoc[0] + " AND `b` = " + aLoc[1] + " AND `c` = " + r[0] + " AND `d` = " + r[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
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
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }
    
        public List<SOperator> GetOperatorsByName(string OperatorName)
        {
            List<SOperator> Operators = new List<SOperator>();
            try
            {
                string commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `EN` LIKE `" + OperatorName + "` ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);

                //List<string[]> list = (from IDataRecord r in myReader
                //                     select (string)r["IR"]
                //    ).ToList(); 
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[] { 0, 0, 0, 0, 0, 0 };
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[4]);
                        int.TryParse(myReader.GetString(3), out symbID[5]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            //Operators.Add(symbID);
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
                foreach (SOperator i in Operators)
                {
                    //ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return Operators;
        }
    
        public List<SOperator> GetAllOperators() 
        {
            List<SOperator> resultedOperators = new List<SOperator>();
            try
            {
                string commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` ";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);

                //List<string[]> list = (from IDataRecord r in myReader
                //                     select (string)r["IR"]
                //    ).ToList(); 
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] symbID = new int[]{0,0,0,0,0,0};
                        int.TryParse(myReader.GetString(0), out symbID[0]);
                        int.TryParse(myReader.GetString(1), out symbID[1]);
                        int.TryParse(myReader.GetString(2), out symbID[4]);
                        int.TryParse(myReader.GetString(3), out symbID[5]);
                        if (symbID[0] != 0 || symbID[1] != 0)
                        {
                            SOperator sOperator = new SOperator();
                            sOperator.IR = symbID[0];
                            sOperator.VAR = symbID[1];
                            sOperator.IR2 = symbID[2];
                            sOperator.VAR2 = symbID[3];
                            if (sOperator.IR != 0 || sOperator.VAR != 0)
                            {
                                resultedOperators.Add(sOperator);
                            }
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
                //foreach (int[] i in allRelations)
                {
                    //ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return resultedOperators;
        }

        public List<int[]> CreateRelation(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc) 
        {
            List<int[]> relationPositions = new List<int[]>();
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = commandString = "INSERT INTO `" + DB + "`.`s" + bLoc[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + bLoc[0] + ", " + bLoc[1] + "," + r[4] + ", " + r[5] + "," + aLoc[0] + ", " + aLoc[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                mySqlCommand.ExecuteNonQuery();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`s" + aLoc[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + aLoc[0] + ", " + aLoc[1] + ", " + r[0] + ", " + r[1] + ", " + bLoc[0] + ", " + bLoc[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = aLoc[0];
                    relationPosition[1] = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            //if (MyResultsTrace) SQLView.LogResult(relationPosition);
            relationPositions.Add(relationPosition);
            return relationPositions;
        }

        public List<int[]> IsTransitive(List<int[]> list, int[] r, int[] a, int[] b, int[] asss, int[] bsss, int limit) 
        {
            List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            bool solved = false;
            try
            {
                string commandString;
                if (false) // ($t1[0]=='?x')
                {
                    commandString = "SELECT `ID`, `g`, `h` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` LIKE " + r[0] + " AND `e` = " + b[0] + " ";
                }
                else
                {
                    commandString = "SELECT `ID`, `g`, `h` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` = " + r[0] + " AND `e` = " + b[0] + " ";
                }
                if (limit > 0) { commandString += " Limit " + limit; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (myReader.HasRows)
                {
                    solved = true;
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader.GetString(0)))
                        {
                            int[] multipplicity = new int[] {0, 0 };
                            int.TryParse(myReader.GetString(1), out multipplicity[0]);
                            int.TryParse(myReader.GetString(2), out multipplicity[1]);
                            //if (multipplicity[0] != 0 || multipplicity[1] != 0)
                            //{
                            //    ReturnList.Add(symbID);
                            //}
                            int[] symbID = new int[] { a[0], 0, 0, 0 };
                            int.TryParse(myReader.GetString(0), out symbID[1]);
                            int.TryParse(myReader.GetString(1), out symbID[2]);
                            int.TryParse(myReader.GetString(2), out symbID[3]);
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
                        commandString = "SELECT `ID`, `e`, `f`, `g`, `h` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c` = " + r[0] + " ";
                    }
                    else
                    {
                        commandString = "SELECT `ID`, `e`, `f`, `g`, `h` FROM `" + DB + "`.`s" + a[0].ToString("00000000") + "` WHERE `a` = " + a[0] + " AND `c`< 5000 ";
                    }
                    if (limit > 0) { commandString += " Limit " + limit; }
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand2 = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader2 = mySqlCommand2.ExecuteReader();
                    //if (MySqlTrace) SQLView.Log(mySqlCommand2.CommandText);
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
                                    LocalReturnList = IsTransitive(LocalReturnList, r, new int[] { symbID[1], symbID[2] }, b, b, b, limit);
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
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return ReturnList;
        }


        // +GetRelationsByName(RelationsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedRelations : List<int[4]>)
        public List<int[]> GetRelationOperatorByName(string RelationsNamePart, int Method = 0, int LimitOfResults = 10)
        {
            List<int[]> ResultedSymbols = new List<int[]>();
            try
            {
                string commandString;
                switch (Method)
                {
                    case 0: // strict
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + RelationsNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + RelationsNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '%" + RelationsNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + RelationsNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + RelationsNamePart + "' ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        int[] relationID = new int[] { 0, 0, 0, 0, 0, 0 };
                        int.TryParse(myReader.GetString(0), out relationID[0]);
                        int.TryParse(myReader.GetString(1), out relationID[1]);
                        int.TryParse(myReader.GetString(2), out relationID[4]);
                        int.TryParse(myReader.GetString(3), out relationID[5]);
                        if (relationID[0] != 0 || relationID[1] != 0)
                        {
                            ResultedSymbols.Add(relationID);
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
                foreach (int[] i in ResultedSymbols)
                {
                    ResultLog += " [" + i[0] + ":" + i[1] + ":" + i[4] + ":" + i[5] + "] ";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedSymbols;
        }

        //+GetRelationNamesByID(RelationlID : int [4] = {0,0,0,0}, int Mode = 0; LimitOfResults : int = 10, ResultedRelationNames : List<string>)
        public List<string> GetRelationOperatorNameByID(int[] RelationID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = RelationID;
            List<string> ResultedRelationNames = new List<string>();
            //if () // return for invalid ids
            try
            {
                string commandString = "";
                switch (Mode)
                {
                    case 0: // strict
                        commandString = "SELECT `En` FROM `" + DB + "`.`srel` WHERE `IR` = " + a[0] + " AND `Var` = " + a[1] + " AND `IR2` = " + a[4] + " AND `Var2` = " + a[5] + "  ;";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            ResultedRelationNames.Add(symbolName);
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
                foreach (string i in ResultedRelationNames)
                {
                    ResultLog += " " + i + " ";
                }
                //SQLView.LogResult(ResultLog);
            }
            return ResultedRelationNames;
        }

        public List<int[]> CreateRelation2(int[] r, int[] a, int[] b) 
        {
            List<int[]> resultedIDs = new List<int[]>();
            int[] relationPosition = { 0, 0, 0, 0 };
            try
            {
                string commandString = commandString = "INSERT INTO `" + DB + "`.`s" + b[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (" + b[0] + ", " + b[1] + "," + r[4] + ", " + r[5] + "," + a[0] + ", " + a[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                mySqlCommand.ExecuteNonQuery();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`s" + a[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (" + a[0] + ", " + a[1] + ", " + r[0] + ", " + r[1] + ", " + b[0] + ", " + b[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = a[0];
                    relationPosition[1] = (int)mySqlCommand.LastInsertedId;
                    relationPosition[2] = 0;
                    relationPosition[3] = 0;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            //if (MyResultsTrace) SQLView.LogResult(relationPosition);
            resultedIDs.Add(relationPosition);
            return resultedIDs;
        }

        #endregion Relation Interface

        #region Adapter
        //public List<string> GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10);
        //GetSymbolNamesByID = ServerAccessInterface.GetSymbolNamesByID;
        #endregion Adapter

        #region Interface Helpers
        #region create symbol table

        /// <summary>
        /// Create a nonexistent table and rezerve first line with id 1
        /// </summary>
        /// <param name="symbolPosition">position of table reference in simbols table can be used to decode the table  name</param>
        private void CreateSymbolTable(SObject symbolPosition)
        {
            string commandString = "CREATE TABLE IF NOT EXISTS `" + DB + "`.`s" + symbolPosition.ObjectId.ToString("00000000") + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , `g` INT NULL , `h` INT NULL , PRIMARY KEY (`ID`) );";
            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
            commandString = "INSERT INTO `" + DB + "`.`s" + symbolPosition.ObjectId.ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (0,0,0,0,0,0,0,0) ;";
            mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
        }

        #endregion
        #endregion
            
    }
}
