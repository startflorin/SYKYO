#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.DL.CodeEntity;
using Oracle.DataAccess.Client;
using DataPersistency.DL.Logging;
using DataPersistency.UI.Logging;
#endregion USING

namespace DataPersistency.DL.ServerAccess
{
    
    public class ServerAccessOracle// : ServerAccessInterface
    {
        #region Variables
        // log options model
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions;
        enum Log { None = 0, Results , Parameters, Code }
        public bool AcceptSymbols { get; set; }
        public bool AcceptOperators { get; set; }
        public bool AcceptRelations { get; set; }
        static public List<string> GooDBDs = new List<string>();
        static public string OracleDB;
        static public bool OracleTrace = false;
        static public bool MyResultsTrace = false;
        static OracleConnection OracleConnection = null;
        OracleException OracleException = null;
        OracleCommand OracleCommand = null;

        #endregion Variables

        #region Construction

        public bool TryToOpenConnection()
        {
            return true;
        }
        
        static ServerAccessOracle()
        {
            if (LoggingSystem.TraceCode) LoggingSystem.LogMessage = "static ServerAccessOracle()";
            OracleConnection = getOracleConnection(null);

            List<string> databaseNames = new List<string>();
            try
            {
                try
                {
                    string commandString = "show databases ;";
                    OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                    OracleDataReader myReader = OracleCommand.ExecuteReader();
                    if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
                        OracleConnection.ChangeDatabase(database);
                        getOracleConnection(database);
                        string commandString = "USE " + database + "; SHOW TABLES ;";
                        OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                        OracleDataReader myReader = OracleCommand.ExecuteReader();
                        if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                        while (myReader.Read())
                        {
                            if (!string.IsNullOrEmpty(myReader.GetString(0)))
                            {
                                string tb = myReader.GetString(0);
                                string DB = myReader.GetString(0);
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
                catch (OracleException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }

                if (tableNames.Contains("simbs") && tableNames.Contains("srel"))
                {
                    GooDBDs.Add(database);
                }
            }

            OracleDB = "SYSTEM";
            //if (MyResultsTrace) SQLView.LogResult(new string[] { name });
        }

        /// <summary>
        /// Initiate connection
        /// </summary>
        public ServerAccessOracle(DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsReferince)
        {
            logingOptions = logingOptionsReferince;

            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; string myLog; System.IO.StreamWriter log; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div>  " + this.GetType().Namespace + " <span style='color:green'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span style='color:blue'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>") + "</div>";
            }
            //====================================================================================================
            if (OracleConnection == null)
            {
                OracleConnection = getOracleConnection("" + OracleDB + "");
            }
            // TODO: Complete member initialization
        }
        

        #endregion Construction

        #region Helpers

        public string GetProviderName() { return "Oracle"; }

        public string getConnectionState() { return OracleConnection.State.ToString(); }

        public static string DatabaseName
        {
            get
            {
                return OracleDB;
            }
            set
            {
                if (DatabaseExists(value))
                {
                    OracleDB = value;
                    OracleConnection = getOracleConnection(OracleDB);
                }
            }
        }

        private static bool DatabaseExists(string value)
        {
            return true;
        }

        private static OracleConnection getOracleConnection(string DBc)
        {
            if (string.IsNullOrEmpty(DBc))
            {
                DBc = OracleDB;
            }
            OracleConnection OracleConnection = new OracleConnection();
            try
            {
                if (OracleConnection != null && OracleConnection.State == ConnectionState.Open)
                {
                    OracleConnection.Close();
                }
                OracleConnection = new OracleConnection("DATA SOURCE=;PERSIST SECURITY INFO=True;USER ID=SYSTEM; PASSWORD=Start1312");
                //OracleConnection = new OracleConnection("user id=SYSTEM;password=Start1312;data source=Local Database;");
                //OracleConnection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)"+"(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=SYSTEM;Password=Start1312;");
                //OracleConnection = new OracleConnection("Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = PROFIMEDICA-PC)(PORT = 1287)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orclXDB.168.201.64))); User Id = SYSTEM; Password = Start1312;");
                //OracleConnection.HostName = "localhost";
                OracleConnection.Open();
            }
            catch (OracleException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return OracleConnection;
        }

        #endregion Helpers

        #region Symbol Interface

        internal List<int[]> CreateSymbolAlias(string SymbolAliasName, int[] ExistentID)
        {
            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div class='CodeElementNamespace'>  " + this.GetType().Namespace + " <span class='CodeElementClass'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span class='CodeElementMethod'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>");
                LoggingSystem.LogMessage = "<span class='specialChaer'>(" + "<span class='CodeElementParameter'>string</span> " + "<span class='CodeElementValue'>" + SymbolAliasName + "</span>" + ", <span class='CodeElementParameter'>int[]</span> " + "<span class='CodeElementValue'>" + ExistentID + "</span>" + ")</div>";
            }
            //====================================================================================================

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
                    string commandString = "INSERT INTO " + OracleDB + ".SIMBS (ID, IR, EN) VALUES (NULL, " + ExistentID[0] + ", '" + SymbolAliasName + "') RETURNING ID INTO :LASTID ;";
                    OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                    OracleParameter lastId = new OracleParameter("LASTID", OracleDbType.Decimal, ParameterDirection.Output);
                    OracleCommand.Parameters.Add(lastId);
                    OracleCommand.ExecuteNonQuery();
                    int LastInsertedId = (int)lastId.Value;
                    if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                    //if (OracleCommand.LastInsertedId > 0)
                    {
                        //symbolPosition[0] = (int)OracleCommand.LastInsertedId;
                        ResultedSymbols.Add(ExistentID);
                    }
                }
                catch (OracleException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }
            }
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }    

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        public List<string> GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10)
        {
            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div class='CodeElementNamespace'>  " + this.GetType().Namespace + " <span class='CodeElementClass'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span class='CodeElementMethod'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>");
                LoggingSystem.LogMessage = "<span class='specialChaer'>(" + "<span class='CodeElementParameter'>int[]</span> " + "<span class='CodeElementValue'>" + SymbolID + "</span>" + ", <span class='CodeElementParameter'>int[]</span> " + "<span class='CodeElementValue'>" + Mode + "</span>" + ", <span class='CodeElementParameter'>int[]</span> " + "<span class='CodeElementValue'>" + LimitOfResults + "</span>" + ")</div>";
            }
            //====================================================================================================

            int[] a = SymbolID;
            List<string> ResultedSymbolNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT EN FROM " + OracleDB + ".SIMBS WHERE IR = " + a[0] + " ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
                SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedSymbolNames;
        }

        public List<int[]> CreateSymbolAlias(string SymbolAliasName, int ExistentID)
        {
            List<int[]> ResultedSymbols = new List<int[]>();
            int[] symbolPosition = new int[] { 0, 0, 0, 1 };
            // Check if alias exists
            if (ExistentID != 0)
            {
                List<string> SymbolSynonimes = GetSymbolNamesByID(new int[] { ExistentID, 0 }, 0, 0);
                int[] match = new int[] { 0, 0 };
                if (SymbolSynonimes.Count < 1)
                {
                    return ResultedSymbols;
                }
                if (SymbolSynonimes.Contains(SymbolAliasName))
                {
                    ResultedSymbols.Add(new int[] { ExistentID, 0 });
                    return ResultedSymbols;
                }
            }
            if (AcceptSymbols)
            {
                try
                {
                    string commandString = "INSERT INTO `simbs` (`ID`, `IR`, `En`) VALUES (NULL, " + ExistentID + ", '" + SymbolAliasName + "');";
                    /*OracleCommand oracleCommand = new OracleCommand(commandString, OracleConnection);
                    oracleCommand.ExecuteNonQuery();
                    if (OracleTrace) SQLView.Log(oracleCommand.CommandText);
                    if (oracleCommand.LastInsertedId > 0)
                    {
                        symbolPosition[0] = (int)oracleCommand.LastInsertedId;
                        ResultedSymbols.Add(new int[] { ExistentID, 0 });
                    }*/
                }
                catch (OracleException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }
            }
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }    


        // +GetSymbolsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        public List<int[]> GetSymbolsByName(string SymbolNamePart, int Mode, int LimitOfResults = 10)
        { 
            List<int[]> ResultedSymbols = new List<int[]>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    case 0: // strict
                        commandString = "SELECT IR FROM " + OracleDB + ".SIMBS WHERE EN LIKE '" + SymbolNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT IR FROM " + OracleDB + ".SIMBS WHERE EN LIKE '" + SymbolNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT IR FROM " + OracleDB + ".SIMBS WHERE EN LIKE '" + SymbolNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT IR FROM " + OracleDB + ".SIMBS WHERE EN LIKE '" + SymbolNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT IR FROM " + OracleDB + ".SIMBS WHERE EN LIKE '" + SymbolNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                while (myReader.Read())
                {
                    if (!myReader.IsDBNull(0))
                    {
                        int[] symbolID = new int[] {0, 0, 0, 0};
                        //int.TryParse(myReader.GetString(0), out symbolID[0]);
                        symbolID[0] = (int)myReader.GetDecimal(0);
                        if (symbolID[0] != 0 || symbolID[1] != 0)
                        {
                            ResultedSymbols.Add(symbolID);
                        }
                    }
                myReader.Close();
                }
            }
            catch (OracleException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in ResultedSymbols)
                {
                    ResultLog += " [" + i[0] + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedSymbols.Count < 1 && Mode == 0 && AcceptSymbols)
            {
                List<int[]> CreatedSymbols = new List<int[]>();
                CreatedSymbols = CreateSymbolByName(SymbolNamePart, true); 
                ResultedSymbols = CreatedSymbols;
            }

            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString()+"</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "SymbolNamePart", SymbolNamePart.ToString() });
                thisMethod.Parameters.Add(new string[] { "Mode", Mode.ToString() });
                thisMethod.Parameters.Add(new string[] { "LimitOfResults", LimitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (int[] id in ResultedSymbols)
                {
                    result += "[";
                    string nrs = string.Empty;
                    foreach (int nr in id)
                    {
                        nrs += ":" + nr;
                    }
                    if (nrs.Length > 2)
                    {
                        result += nrs.Substring(1);
                    }
                    result += "]";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ResultedSymbols;
        }

        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        public List<int[]> CreateSymbolByName(string SymbolName, bool AcceptDuplicates)
        {
            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div class='CodeElementNamespace'>  " + this.GetType().Namespace + " <span class='CodeElementClass'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span class='CodeElementMethod'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>");
                LoggingSystem.LogMessage = "<span class='specialChaer'>(" + "<span class='CodeElementParameter'>string</span> " + "<span class='CodeElementValue'>" + SymbolName + "</span>" + ", <span class='CodeElementParameter'>bool</span> " + "<span class='CodeElementValue'>" + AcceptDuplicates + "</span>)</span></div>";
            }
            //====================================================================================================

            if (!AcceptDuplicates)
            {
                List<int[]> ExistentSymbols = GetSymbolsByName(SymbolName, 0, 10);
                if (ExistentSymbols.Count > 0) return ExistentSymbols;
            }
            List<int[]> ResultedSymbols = new List<int[]>();
            int[] symbolPosition = { 0, 0, 0, 0 };
            try
            {
                string commandString = "INSERT INTO " + OracleDB + ".SIMBS (ID, EN) VALUES (NULL, '" + SymbolName + "') RETURNING SIMBS.ID INTO :LastID ";
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleParameter LastId = new OracleParameter("LastID", OracleDbType.Decimal, ParameterDirection.ReturnValue);
                OracleCommand.Parameters.Add(LastId);
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                int LastInsertedId = 0;
                int.TryParse(LastId.Value.ToString(), out LastInsertedId);
                if (LastInsertedId > 0)
                {
                    string commandString2 = "UPDATE " + OracleDB + ".SIMBS SET IR = ID WHERE ID = " + LastInsertedId + " ";
                    OracleCommand mySqlCommand2 = new OracleCommand(commandString2, OracleConnection);
                    mySqlCommand2.ExecuteNonQuery();
                    symbolPosition[0] = LastInsertedId;
                    ResultedSymbols.Add(symbolPosition);
                }
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
            }
            catch (OracleException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            CreateSymbolTable(symbolPosition);
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }

        #endregion Symbol Interface

        #region Relation Interface
        public List<int[]> getRecord(int symbNr, int offset)
        {
            //return new List<int[]>();
         List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            bool solved = false;
            try
            {
                string commandString="";
                //if (r[0] == 0 && r[1] == 0 && r[4] == 0 && r[5] == 0)
                
                    //if (symbNr == -100 && r[3] == 5)
                    
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + symbNr.ToString("00000000") + " WHERE A = " + symbNr + " AND ID = " + offset + " ";
                    
                                    OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader[0].ToString()))
                        {
                            int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                            rel[0] = (int)myReader.GetDecimal(1);
                            rel[1] = (int)myReader.GetDecimal(2);
                            rel[2] = (int)myReader.GetDecimal(3);
                            rel[3] = (int)myReader.GetDecimal(4);
                            rel[4] = (int)myReader.GetDecimal(5);
                            rel[5] = (int)myReader.GetDecimal(6);
                            rel[6] = (int)myReader.GetDecimal(7);
                            rel[7] = (int)myReader.GetDecimal(8);
                            rel[8] = (int)myReader.GetDecimal(0);

                            if (rel[0] != 0 || rel[1] != 0)
                            {
                                ReturnList.Add(new int[] { rel[0], rel[1] });
                                //ReturnList.Add(new int[] { rel[2], 0 });
                                ReturnList.Add(new int[] { rel[4], rel[5] });
                            }
                        }
                    }
                    myReader.Close();
                }
            }
            catch (OracleException retrieveSymbolIndexException)
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

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        List<string> GetOperatorNamesByID(int[] OperatorID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = OperatorID;
            List<string> ResultedOperatorNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT EN FROM " + OracleDB + ".SREL WHERE IR = " + a[0] + " ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
                SQLView.LogResult(new string[] { ResultLog });
            }
            return ResultedOperatorNames;
        }

        // +GetSymbolsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        public List<int[]> GetOperatorsByName(string OperatorNamePart, int Mode, int LimitOfResults = 10)
        {
            List<int[]> ResultedOperators = new List<int[]>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    case 0: // strict
                        commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                    if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                while (myReader.Read())
                {
                    if (!myReader.IsDBNull(0))
                    {
                        int[] relationID = new int[] { 0, 0, 0, 0, 0, 0 };
                        relationID[0] = (int)myReader.GetDecimal(0);
                        relationID[1] = (int)myReader.GetDecimal(1);
                        relationID[4] = (int)myReader.GetDecimal(2);
                        relationID[5] = (int)myReader.GetDecimal(3);
                        if (relationID[0] != 0 || relationID[1] != 0)
                        {
                            ResultedOperators.Add(relationID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (OracleException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in ResultedOperators)
                {
                    ResultLog += " [" + i[0] + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedOperators.Count < 1 && Mode == 0 && AcceptSymbols)
            {
                List<int[]> CreatedSymbols = new List<int[]>();
                //CreatedSymbols = CreateSymbolByName(OperatorNamePart, true);
                return CreatedSymbols;
            }
            return ResultedOperators;
        }

        public List<int[]> GetLeafsByRelation(int[] r, int[] a, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            try
            {
                string commandString = "SELECT E, F, G, H FROM " + OracleDB + ".S" + a[0].ToString("00000000") + " WHERE A = " + a[0] + " AND B = " + a[1] + " AND C = " + r[0] + " AND D = " + r[1] + " ";
                if (limit != 0) { commandString += " Limit " + limit; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
        
        public List<int[]> GetOperatorsByName(string OperatorName)
        {
            List<int[]> Operators = new List<int[]>();
            try
            {
                string commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL WHERE EN LIKE '" + OperatorName + "' ";
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);

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
                            Operators.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (OracleException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (int[] i in Operators)
                {
                    //ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            return Operators;
        }
        
        public List<int[]> GetAllOperators() 
        {
            List<int[]> allRelations = new List<int[]>();
            try
            {
                string commandString = "SELECT IR, VAR, IR2, VAR2 FROM " + OracleDB + ".SREL ";
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);

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
                            allRelations.Add(symbID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (OracleException retrieveSymbolIndexException)
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

        public int[] CreateRelationAsNew(int[] r, int[] a, int[] b) 
        {
            int[] relationPosition = { 0, 0 };
            try
            {
                string commandString = commandString = "INSERT INTO " + OracleDB + ".s" + b[0].ToString("00000000") + " (a, b, c, d, e, f) VALUES (" + b[0] + ", " + b[1] + "," + r[4] + ", " + r[5] + "," + a[0] + ", " + a[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                //if (OracleConnection.
                OracleCommand.ExecuteNonQuery();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                commandString = commandString = "INSERT INTO " + OracleDB + ".s" + a[0].ToString("00000000") + " (a, b, c, d, e, f) VALUES (" + a[0] + ", " + a[1] + ", " + r[0] + ", " + r[1] + ", " + b[0] + ", " + b[1] + ", " + r[2] + ", " + r[3] + " ) ;";
                OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleCommand.ExecuteNonQuery();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                //if (OracleCommand.LastInsertedId > 0)
                {
                    relationPosition[0] = a[0];
                    //relationPosition[1] = (int)OracleCommand.LastInsertedId;
                }
            }
            catch (OracleException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        public List<int[]> IsTransitive(List<int[]> list, int[] r, int[] aLoc , int[] aMul, int[] bLoc, int[] bMul, int limit)
        {
            List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            bool solved = false;
            try
            {
                string commandString="";
                if (r[0] == 0 && r[1] == 0 && r[4] == 0 && r[5] == 0)
                {
                    if (r[2] == -100 && r[3] == 5)
                    {
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND C = " + r[2] + " AND G = " + bMul[0] + " AND H =  " + bMul[1] + " ";
                    }
                    if (r[2] == -100 && r[3] == -5)
                    {
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND C = " + r[2] + " AND G != " + bMul[0] + " AND H =  " + bMul[1] + " ";
                    }
                }
                else
                {
                    //brother
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + bLoc[0].ToString("00000000") + " WHERE A = " + bLoc[0] + " AND C = " + r[2] + " AND E = " + aLoc[0] + " ";
                    // commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND C = " + r[0] + " AND E = " + bLoc[0] + " ";
                }
                //if (limit > 0) { commandString += " Limit " + limit; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                if (myReader.HasRows)
                {
                    solved = true;
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader[0].ToString()))
                        {
                            int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                            // Symbol A table
                            rel[0] = (int)myReader.GetDecimal(1);
                            // A Line
                            rel[1] = (int)myReader.GetDecimal(2);
                            rel[2] = (int)myReader.GetDecimal(3);
                            rel[3] = (int)myReader.GetDecimal(4);
                            rel[4] = (int)myReader.GetDecimal(5);
                            rel[5] = (int)myReader.GetDecimal(6);
                            rel[6] = (int)myReader.GetDecimal(7);
                            // Symbol B table
                            rel[7] = (int)myReader.GetDecimal(8);
                            // B Line
                            rel[8] = (int)myReader.GetDecimal(0);

                            if (rel[0] != 0 || rel[1] != 0)
                            {
                                ReturnList.Add(rel);
                            }
                       }
                    }
                    myReader.Close();
                }
                else
                {
                    myReader.Close();
                    if (!(r[0] == 0 && r[1] == 0 && r[4] == 0 && r[5] == 0))
                    {
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND C = " + r[0] + " ";
                    }
                    else
                    {
                        commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND C < 5000 ";
                    }
                    //if (limit > 0) { commandString += " Limit " + limit; }
                    OracleCommand OracleCommand2 = new OracleCommand(commandString, OracleConnection);
                    OracleDataReader myReader2 = OracleCommand2.ExecuteReader();
                    if (OracleTrace) SQLView.Log(OracleCommand2.CommandText);
                    List<int[]> toTry = new List<int[]>();
                    while (myReader2.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader2[0].ToString()))
                        {
                            int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                            rel[0] = (int)myReader2.GetDecimal(1);
                            rel[1] = (int)myReader2.GetDecimal(2);
                            rel[2] = (int)myReader2.GetDecimal(3);
                            rel[3] = (int)myReader2.GetDecimal(4);
                            rel[4] = (int)myReader2.GetDecimal(5);
                            rel[5] = (int)myReader2.GetDecimal(6);
                            rel[6] = (int)myReader2.GetDecimal(7);
                            rel[7] = (int)myReader2.GetDecimal(8);
                            rel[8] = (int)myReader2.GetDecimal(0);

                            if (rel[0] != 0 || rel[1] != 0)
                            {
                                toTry.Add(rel);
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
                                    LocalReturnList = IsTransitive(LocalReturnList, r, new int[] { symbID[4], symbID[5] }, new int[] { symbID[5], symbID[6] }, bLoc, bMul, limit);
                                }
                                if (LocalReturnList.Count > 0)
                                {
                                    //int[] ToAdd = new int[] { a[0], symbID[0] };
                                    //ReturnList.Add(new int[] { symbID[1], symbID[2] });
                                    ReturnList.Add(new int[] { aLoc[0], symbID[0] });
                                    ReturnList.AddRange(LocalReturnList);
                                    solved = true;
                                }
                            }
                        }
                    }

                }
            }
            catch (OracleException retrieveSymbolIndexException)
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

        public List<int[]> GetContent(int[] r, int[] aMul, int[] aLoc, int limit)
        {
            List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            try
            {
                string commandString;
                if (false) // ($t1[0]=='?x')
                {
                    commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND B = " + aLoc[1] + " AND E LIKE " + r[0] + " ";
                }
                else
                {
                    commandString = "SELECT ID, A, B, C, D, E, F, G, H FROM " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " WHERE A = " + aLoc[0] + " AND B = " + aLoc[1] + " AND C = " + r[0] + " ";
                }
                //if (limit > 0) { commandString += " Limit " + limit; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader[0].ToString()))
                        {
                            int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                            rel[0] = (int)myReader.GetDecimal(1);
                            rel[1] = (int)myReader.GetDecimal(2);
                            rel[2] = (int)myReader.GetDecimal(3);
                            rel[3] = (int)myReader.GetDecimal(4);
                            rel[4] = (int)myReader.GetDecimal(5);
                            rel[5] = (int)myReader.GetDecimal(6);
                            rel[6] = (int)myReader.GetDecimal(7);
                            rel[7] = (int)myReader.GetDecimal(8);
                            rel[8] = (int)myReader.GetDecimal(0);

                            if (rel[0] != 0 || rel[1] != 0)
                            {
                                ReturnList.Add(rel);
                            }
                        }
                    }
                    myReader.Close();
                }
            }
            catch (OracleException retrieveSymbolIndexException)
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
                        commandString = "SELECT IR, Var, IR2, Var2 FROM " + OracleDB + ".srel WHERE En LIKE '" + RelationsNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT IR, Var, IR2, Var2 FROM " + OracleDB + ".srel WHERE En LIKE '" + RelationsNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT IR, Var, IR2, Var2 FROM " + OracleDB + ".srel WHERE En LIKE '%" + RelationsNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT IR, Var, IR2, Var2 FROM " + OracleDB + ".srel WHERE En LIKE '" + RelationsNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT IR, Var, IR2, Var2 FROM " + OracleDB + ".srel WHERE En LIKE '" + RelationsNamePart + "' ";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
                SQLView.LogResult(new string[] { ResultLog });
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
                        commandString = "SELECT En FROM " + OracleDB + ".srel WHERE IR = " + a[0] + " AND Var = " + a[1] + " AND IR2 = " + a[4] + " AND Var2 = " + a[5] + "  ;";
                        break;
                }
                if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                OracleDataReader myReader = OracleCommand.ExecuteReader();
                if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (OracleException retrieveSymbolIndexException)
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
                SQLView.LogResult(ResultLog);
            }
            return ResultedRelationNames;
        }

        public List<int[]> CreateRelation(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc) 
        {
            List<int[]> resultedIDs = new List<int[]>();
            int[] relationPosition = { 0, 0, 0, 0, 0, 0 };
            try
            {
                string commandString = string.Empty;
                

                if (bLoc[0] == 0 && bLoc[1] == 0)
                {
                    commandString = commandString = "INSERT INTO " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " (a, b, c, d, e, f, g, h) VALUES (" + aLoc[0] + ", " + aLoc[1] + ", " + r[2] + ", " + r[3] + ", " + bLoc[0] + ", " + bLoc[1] + ", " + bMul[0] + ", " + bMul[1] + " )  RETURNING ID INTO :LastID ";
                    OracleCommand OracleCommand2 = new OracleCommand(commandString, OracleConnection);
                    OracleParameter LastId = new OracleParameter("LastID", OracleDbType.Decimal, ParameterDirection.ReturnValue);
                    OracleCommand2.Parameters.Add(LastId);
                    //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    OracleDataReader myReader = OracleCommand2.ExecuteReader();
                    
                    int LastInsertedId = 0;
                    int.TryParse(LastId.Value.ToString(), out LastInsertedId);
                    //OracleCommand = new OracleCommand(commandString, OracleConnection);
                    //OracleCommand.ExecuteNonQuery();
                    if (OracleTrace) SQLView.Log(OracleCommand2.CommandText);
                    if (LastInsertedId > 0)
                    {
                        int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        rel[0] = aLoc[0];
                        rel[1] = aLoc[1];
                        rel[2] = r[2];
                        rel[3] = r[3];
                        rel[4] = bLoc[0];
                        rel[5] = bLoc[1];
                        rel[6] = bMul[0];
                        rel[7] = bMul[1];
                        rel[8] = LastInsertedId;

                        if (rel[0] != 0 || rel[1] != 0)
                        {
                            resultedIDs.Add(rel);
                        }
                    }
                }

                if (bLoc[0] != 0)
                {
                    commandString = commandString = "INSERT INTO " + OracleDB + ".S" + bLoc[0].ToString("00000000") + " (a, b, c, d, e, f, g, h) VALUES (" + bLoc[0] + ", " + bLoc[1] + "," + r[4] + ", " + r[5] + "," + aLoc[0] + ", " + aLoc[1] + ", " + r[2] + ", " + r[3] + " ) ";
                    OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
                    //if (OracleConnection.
                    OracleCommand.ExecuteNonQuery();
                    if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                    commandString = commandString = "INSERT INTO " + OracleDB + ".S" + aLoc[0].ToString("00000000") + " (a, b, c, d, e, f, g, h) VALUES (" + aLoc[0] + ", " + aLoc[1] + ", " + r[0] + ", " + r[1] + ", " + bLoc[0] + ", " + bLoc[1] + ", " + r[2] + ", " + r[3] + " )  RETURNING ID INTO :LastID ";
                    OracleCommand OracleCommand2 = new OracleCommand(commandString, OracleConnection);
                    OracleParameter LastId = new OracleParameter("LastID", OracleDbType.Decimal, ParameterDirection.ReturnValue);
                    OracleCommand2.Parameters.Add(LastId);
                    //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    OracleDataReader myReader = OracleCommand2.ExecuteReader();

                    int LastInsertedId = 0;
                    int.TryParse(LastId.Value.ToString(), out LastInsertedId);
                    //OracleCommand = new OracleCommand(commandString, OracleConnection);
                    //OracleCommand.ExecuteNonQuery();
                    if (OracleTrace) SQLView.Log(OracleCommand2.CommandText);
                    if (LastInsertedId > 0)
                    {
                        int[] rel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        rel[0] = aLoc[0];
                        rel[1] = aLoc[1];
                        rel[2] = r[2];
                        rel[3] = r[3];
                        rel[4] = bLoc[0];
                        rel[5] = bLoc[1];
                        rel[6] = bMul[0];
                        rel[7] = bMul[1];
                        rel[8] = LastInsertedId;

                        if (rel[0] != 0 || rel[1] != 0)
                        {
                            resultedIDs.Add(rel);
                        }
                    }
                }
            }
            catch (OracleException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            //resultedIDs.Add(relationPosition);
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
        private void CreateSymbolTable(int[] symbolPosition)
        {
            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; string myLog; System.IO.StreamWriter log; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div>  " + this.GetType().Namespace + " <span style='color:green'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span style='color:blue'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>") + "</div>";
            }
            //====================================================================================================

            string commandString  = " CREATE TABLE \"" + OracleDB + "\".\"S" + symbolPosition[0].ToString("00000000") + "\" (	\"ID\" NUMBER, 	\"A\" NUMBER, 	\"B\" NUMBER, 	\"C\" NUMBER, 	\"D\" NUMBER, 	\"E\" NUMBER, 	\"F\" NUMBER, 	\"G\" NUMBER, 	\"H\" NUMBER   ) ";
            OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);
            OracleCommand.ExecuteNonQuery();

            commandString  = " CREATE SEQUENCE  \"" + OracleDB + "\".\"S" + symbolPosition[0].ToString("00000000") + "_SEQ_ID\"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 161 CACHE 20 NOORDER  NOCYCLE  ";
            OracleCommand OracleCommandSEQ = new OracleCommand(commandString, OracleConnection);
            OracleCommandSEQ.ExecuteNonQuery();
            
            commandString  = " create or replace TRIGGER S" + symbolPosition[0].ToString("00000000") + "_AUTOINCREMENT ";
            commandString += " BEFORE INSERT ON S" + symbolPosition[0].ToString("00000000") + " ";
            commandString += " REFERENCING OLD AS OLD NEW AS NEW  ";
            commandString += " FOR EACH ROW  ";
            commandString += " WHEN (NEW.ID IS NULL)  ";
            commandString += " BEGIN ";
            commandString += "   select S" + symbolPosition[0].ToString("00000000") + "_SEQ_ID.NEXTVAL ";
            commandString += "   INTO :NEW.ID FROM dual; ";
            commandString += " END ; ";
            OracleCommand OracleCommandTRG = new OracleCommand(commandString, OracleConnection);
            OracleCommandTRG.ExecuteNonQuery();
            
            if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
            commandString = "INSERT INTO \"" + OracleDB + "\".\"S" + symbolPosition[0].ToString("00000000") + "\" (\"A\", \"B\", \"C\", \"D\", \"E\", \"F\", \"G\", \"H\") VALUES (0,0,0,0,0,0,0,0) ";
            OracleCommand = new OracleCommand(commandString, OracleConnection);
            OracleCommand.ExecuteNonQuery();
            if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
        }

        #endregion
        #endregion

        #region PROCEDURE CALL
        private void CallOracleProcedure(string procame, string retVal, string[] inVals, string[] outVals)
        {
            if (LoggingSystem.TraceCode) LoggingSystem.LogMessage = "private void CallOracleProcedure(string procame, string retVal, string[] inVals, string[] outVals)";
            
            OracleCommand cmd = new OracleCommand(procame, OracleConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter retval = new OracleParameter("retval", OracleDbType.Varchar2, 50);
            retval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retval);
              

            foreach (string inVal in inVals)
            {
                int i = 0;
                OracleParameter inval = new OracleParameter("inval"+i, OracleDbType.Varchar2);
                inval.Direction = ParameterDirection.Input;
                inval.Value = inVal;
                cmd.Parameters.Add(inval);
                i++;
            }

            foreach (string outVal in outVals)
            {
                int i = 0;
                OracleParameter outval = new OracleParameter("outval"+i, OracleDbType.Varchar2);
                outval.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outVal);
                i++;
            }

            cmd.ExecuteNonQuery();
            //Console.WriteLine("return value is {0}, out value is {1}", retval.Value, outval.Value);
        }
        #endregion PROCEDURE CALL
    }
}
