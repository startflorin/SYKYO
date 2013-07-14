#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using DataPersistency.UI.Logging;
using DataPersistency.DL.Logging;
using DataModel.DL.CodeEntity;
#endregion USING

namespace DataPersistency.DL.ServerAccess
{
    public class ServerAccessMySQL : ServerAccessInterface
    {

        #region Variables
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions = new BL.UserOptions.LoggingSystemOptions();

        /// <summary>
        /// isHumanReadable
        /// </summary>
        public bool IsHumanReadable { get; set; }

        public bool AcceptSymbols { get; set; }
        public bool AcceptOperators { get; set; }
        public bool AcceptRelations { get; set; }
        static public List<string> GoodBDs = new List<string>();
        static public string DB = "rpro0001_cabinet";
        static public bool MySqlTrace = false;
        static public bool MyResultsTrace = false;
        static MySqlConnection mySQLConnection = null;
        MySqlException mysqlException = null;
        MySqlCommand mysqlCommand = null;

        // the logging system regerence
        //static LoggingSystem.Log loggingSystem = new LoggingSystem.Log();

        /// <summary>
        /// Search mode for looking-up for symbols and operators
        /// </summary>
        enum SearchMode { Default = -1, ExactMatch = 0, StartWith = 1, Containing = 2, EndingWith = 3 }

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
                    mySQLConnection = ConnectToDatabase(DB);
                }
            }
        }

        private static bool DatabaseExists(string value)
        {
            return true;
        }


        #endregion Variables

        #region Construction

        /// <summary>
        /// Try to open the connection if the connection is not opened
        /// </summary>
        /// <returns></returns>
        public bool TryToOpenConnection()
        {
            if (mySQLConnection.State.ToString() != "Open")
            {
                mySQLConnection.Open();
            }
            return mySQLConnection.State.ToString() == "Open";
        }
    
        /// <summary>
        /// Constructor for default connection string
        /// </summary>
        static ServerAccessMySQL()
        {
            //LoggingSystem.LogMessage = "Salut";
            mySQLConnection = ConnectToDatabase(null);

             //if (MyResultsTrace) SQLView.LogResult(new string[] { name });
        }

        /// <summary>
        /// Constructor for a given connection string
        /// </summary>
        public ServerAccessMySQL(string connectionString)
        {
            if (mySQLConnection == null)
            {
                mySQLConnection = ConnectToDatabase("rpro0001_cabinet");
            }
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Try to open an existent connection or create a new connection with the given connection string if exists or the default connection string
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private static MySqlConnection ConnectToDatabase(string databaseName)
        {
            // TODO FLORIN: The connection string does not have to be included in this file. Also the connection have to be outside this class.

            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                //System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name; fr = stackTrace.GetFrame(0);
                //LoggingSystem.LogMessage = "<div class='CodeElementNamespace'>  " + "Framework.DL.ServerAccess" + " <span class='CodeElementClass'> " + "private static MySqlConnection" + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span class='CodeElementMethod'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>");
                //LoggingSystem.LogMessage = "<span class='specialChaer'>(" + "<span class='CodeElementParameter'>string databaseName = </span> " + "<span class='CodeElementValue'>" + databaseName + "</span>)</div>";
            }
            //====================================================================================================

            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = DB;
            }
            MySqlConnection mySQLConnection = new MySqlConnection();
            try
            {
                if (mySQLConnection != null && mySQLConnection.State == ConnectionState.Open)
                {
                    mySQLConnection.Close();
                }
                mySQLConnection = new MySqlConnection("Server=localhost;Port=3306;Database=rpro0001_cabinet;Uid=sykyo_test;Pwd=start1;");
                mySQLConnection.Open();
            }
            catch (MySqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return mySQLConnection;
        }

        /// <summary>
        /// Object destructor
        /// </summary>
        ~ServerAccessMySQL()
        {
            if (mySQLConnection != null ) mySQLConnection.Close();
        }

        #endregion Construction

        #region Symbol Interface

        /// <summary>
        /// Get symbol names by symbolID
        /// </summary>
        /// <param name="symbolID">The given symbolID to search for</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: The list of names associated with the given symbolID</returns>
        public List<string> GetSymbolNamesByID(SymbolID symbolID, int limitOfResults = 10)
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<string> resultedSymbolNames = new List<string>();
            try
            {
                string commandString;
                commandString = "SELECT `En` FROM `" + DB + "`.`simbs` WHERE `IR` = " + symbolID.Location.A + " ";
                if (limitOfResults != 0) { commandString += " Limit " + limitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            resultedSymbolNames.Add(symbolName);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            // Log result
            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (string i in resultedSymbolNames)
                {
                    ResultLog += "" + i + "/";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "int[] symbolID", symbolID.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (string symbolName in resultedSymbolNames)
                {
                    result += "{" + symbolName + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return resultedSymbolNames;
        }

        /// <summary>
        /// Get symbolIDs from a partial symbol name (according to search mode options). 
        /// </summary>
        /// <param name="symbolNamePart">The partial name of the symbol to find</param>
        /// <param name="searchMode">Search mode according to the related enumeration</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>The existent symbolIDs associated with the symbol name OR the new created symbol if there was no symbol and if the permissions are right</returns>
        public List<SymbolID> GetSymbolsByName(string symbolNamePart, int searchMode, int limitOfResults = 10)
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<SymbolID> resultedSymbolIDs = new List<SymbolID>();

            try
            {
                string commandString;
                switch (searchMode)
                {
                    case (int)SearchMode.ExactMatch: // strict
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + symbolNamePart + "' ";
                        break;
                    case (int)SearchMode.StartWith: //
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + symbolNamePart + "%' ";
                        break;
                    case (int)SearchMode.Containing: //
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '%" + symbolNamePart + "%' ";
                        break;
                    case (int)SearchMode.EndingWith: //
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '%" + symbolNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT `IR` FROM `" + DB + "`.`simbs` WHERE `En` LIKE '" + symbolNamePart + "' ";
                        break;
                }
                if (limitOfResults != 0) { commandString += " Limit " + limitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0))) //TO DO null value
                    {
                        SymbolID symbolID = new SymbolID();
                        int.TryParse(myReader.GetString(0), out symbolID.Location.A);
                        if (symbolID.Exists)
                        {
                            resultedSymbolIDs.Add(symbolID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (MySqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            // If no symbol was found and there was provided the holl name of the symbol and permisions are right then create the wanted symbol name and return it`s symbolID
            if (resultedSymbolIDs.Count < 1 && searchMode == (int)SearchMode.ExactMatch && AcceptSymbols)
            {
                resultedSymbolIDs = CreateSymbolByName(symbolNamePart, true); // set AcceptDuplicates to avoid other check for existence
            }

            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    ResultLog += " [" + itemSymbolID.Location.A + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }

            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string symbolNamePart", symbolNamePart });
                thisMethod.Parameters.Add(new string[] { "int mode", searchMode.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    result += "{" + itemSymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return resultedSymbolIDs;
        }

        /// <summary>
        /// Create symbol by name. If duplicates are not accepted return the existent symbolID. If the name does not exist or duplicates are accepted insert the name  and return it`s symbolID
        /// </summary>
        /// <param name="symbolName">The exact name of the symbol to find</param>
        /// <param name="acceptDuplicates">True if duplicates are permitted</param>
        /// <returns>ResultSet: 1) The existent symbolID if it exists and duplicates are not permited 2) The new created symbolID</returns>
        public List<SymbolID> CreateSymbolByName(string symbolName, bool acceptDuplicates)
        {
            // TODO FLORIN: Create duplicates does not return also the existent IDs. But if not permited it will. Please check for consistency.

            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<SymbolID> resultedSymbolIDs = new List<SymbolID>();
            
            if (!acceptDuplicates)
            {
                // If duplicates does not exists, check if the symbol is already in the database
                resultedSymbolIDs = GetSymbolsByName(symbolName, 0, 10);
            }

            // Because creation of duplicate symbols is not permited and there are already symbols with the given name, return the existent symbols
            if (resultedSymbolIDs.Count < 1)
            {
                SymbolID symbolID = new SymbolID();
                try
                {
                    // Add symbol name to simbs table
                    string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `En`) VALUES (NULL, '" + symbolName + "');";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    mySqlCommand.ExecuteNonQuery();
                    if (mySqlCommand.LastInsertedId > 0)
                    {
                        // Update IR with the ID value
                        string commandString2 = "UPDATE `" + DB + "`.`simbs` SET `IR` = `ID` WHERE `ID` = " + (int)mySqlCommand.LastInsertedId + " ;";
                        MySql.Data.MySqlClient.MySqlCommand mySqlCommand2 = new MySqlCommand(commandString2, mySQLConnection);
                        mySqlCommand2.ExecuteNonQuery();
                        symbolID.Location.A = (int)mySqlCommand.LastInsertedId;
                        resultedSymbolIDs.Add(symbolID);
                    }
                }
                catch (MySqlException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }

                // Create the coresponding symbol table
                CreateSymbolTable(symbolID);
            }

            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    ResultLog += " [" + itemSymbolID.Location.A + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }

            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string symbolName", symbolName.ToString() });
                thisMethod.Parameters.Add(new string[] { "bool acceptDuplicates", acceptDuplicates ? "True" : "False" });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    result += "{" + itemSymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return resultedSymbolIDs;
        }

        /// <summary>
        /// Create a new alias for a given IR if the permissions are right and the given symbolID exists
        /// </summary>
        /// <param name="symbolAliasName">The new alias</param>
        /// <param name="existentID">The existent IR</param>
        /// <returns>ResultSet: 1) Empty set if IR does not exists or DB does not accept this change, 2) Existent symbol if already assigned, 3) The new symbol if DB accept this change</returns>
        public List<SymbolID> CreateSymbolAlias(string symbolAliasName, int existentID)
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // A flag to continue with log section
            bool evaluationSuspended = false;

            // Prepare result set
            List<SymbolID> resultedSymbolIDs = new List<SymbolID>();

            // Prepare given ID as a symbol ID
            SymbolID symbolID = new SymbolID();
            symbolID.Location.A = existentID;

            // TODO FLORIN: check about symbolPosition
            int[] symbolPosition = new int[] { 0, 0, 0, 1 };

            // Check if alias exists
            if (existentID != 0)
            {
                List<string> SymbolSynonimes = GetSymbolNamesByID(symbolID, 0);
                //int[] match = new int[] { 0, 0 };
                if (SymbolSynonimes.Count < 1)
                {
                    // Return empty result set because the given symbolID does not exist
                    evaluationSuspended = true;
                }
                if ((!evaluationSuspended) && SymbolSynonimes.Contains(symbolAliasName))
                {
                    resultedSymbolIDs.Add(symbolID);
                    // Return the existent symbolID already associated with the given Name
                    evaluationSuspended = true;
                }
            }

            // If the database accepts changes (accept new symbols) create the alias as the request. The symbolID exists in the database. The new alias will have the same IR
            if ((!evaluationSuspended) && AcceptSymbols)
            {
                try
                {
                    string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `IR`, `En`) VALUES (NULL, " + existentID + ", '" + symbolAliasName + "');";
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                    if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                    mySqlCommand.ExecuteNonQuery();
                    if (mySqlCommand.LastInsertedId > 0)
                    {
                        symbolPosition[0] = (int)mySqlCommand.LastInsertedId;
                        resultedSymbolIDs.Add(symbolID);
                    }
                }
                catch (MySqlException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }
            }
            if (MySqlTrace) SQLView.LogResult(resultedSymbolIDs.ToString());
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string symbolAliasName", symbolAliasName.ToString() });
                thisMethod.Parameters.Add(new string[] { "int existentID", existentID.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    result += "{" + itemSymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return resultedSymbolIDs;
        }

        #endregion Symbol Interface
        
        #region Operator Interface

        /// <summary>
        /// Get all operators known
        /// </summary>
        /// <returns>ResultSet: List of all operators stored in the database</returns>
        public List<OperatorID> GetAllOperators()
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<OperatorID> allRelations = new List<OperatorID>();
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
                        OperatorLocation operatorID = new OperatorLocation();
                        int.TryParse(myReader.GetString(0), out operatorID.IR);
                        int.TryParse(myReader.GetString(1), out operatorID.Var);
                        int.TryParse(myReader.GetString(2), out operatorID.IR2);
                        int.TryParse(myReader.GetString(3), out operatorID.Var2);
                        if (operatorID.IR != 0 || operatorID.Var != 0)
                        {
                            allRelations.Add(new OperatorID(operatorID));
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
                foreach (OperatorID operatorID in allRelations)
                {
                    ResultLog += operatorID.ToString();
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (OperatorID operatorID in allRelations)
                {
                    result += "{" + operatorID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return allRelations;
        }

        /// <summary>
        /// Get the names of the given operatorIDs
        /// </summary>
        /// <param name="operatorID">OperatorIDs to look for</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: A List of the given operator`s names</returns>
        public List<string> GetOperatorNamesByID(OperatorID operatorID, int limitOfResults = 10)
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<string> resultedOperatorNames = new List<string>();
            try
            {
                string commandString;
                commandString = "SELECT `En` FROM `" + DB + "`.`srel` WHERE `IR` = " + operatorID.Location.IR + " AND `Var` = " + operatorID.Location.Var + " AND `IR2` = " + operatorID.Location.IR2 + " AND `Var2` = " + operatorID.Location.Var2 + " ";
                if (limitOfResults != 0) { commandString += " Limit " + limitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        string symbolName;
                        symbolName = myReader.GetString(0).ToString();
                        if (!string.IsNullOrEmpty(symbolName))
                        {
                            resultedOperatorNames.Add(symbolName);
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
                foreach (string i in resultedOperatorNames)
                {
                    ResultLog += "" + i + "";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "int[] operatorID", operatorID.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (string symbolItem in resultedOperatorNames)
                {
                    result += "{" + symbolItem.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return resultedOperatorNames;
        }
        
        /// <summary>
        /// Get operatorIDs from a partial operator name (according to search mode options).
        /// </summary>
        /// <param name="operatorNamePart">The partial name of the operator to find</param>
        /// <param name="searchMode">Search method</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: The existent operatorIDs associated with the operator name OR the new created operator if there was no operator and if the permissions are right</returns>
        public List<OperatorID> GetOperatorsByName(string operatorNamePart, int searchMode, int limitOfResults = 10)
        {
            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<OperatorID> ResultedOperators = new List<OperatorID>();
            try
            {
                string commandString;
                switch (searchMode)
                {
                    case (int)SearchMode.ExactMatch: // strict
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + operatorNamePart + "' ";
                        break;
                    case (int)SearchMode.StartWith: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + operatorNamePart + "%' ";
                        break;
                    case (int)SearchMode.Containing: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '%" + operatorNamePart + "%' ";
                        break;
                    case (int)SearchMode.EndingWith: //
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '%" + operatorNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `En` LIKE '" + operatorNamePart + "' ";
                        break;
                }
                if (limitOfResults != 0) { commandString += " Limit " + limitOfResults + " "; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                TryToOpenConnection();
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        OperatorLocation relationID = new OperatorLocation();
                        int.TryParse(myReader.GetString(0), out relationID.IR);
                        int.TryParse(myReader.GetString(1), out relationID.Var);
                        int.TryParse(myReader.GetString(2), out relationID.IR2);
                        int.TryParse(myReader.GetString(3), out relationID.Var2);
                        if (relationID.IR != 0 || relationID.Var != 0)
                        {
                            ResultedOperators.Add(new OperatorID(relationID));
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
                foreach (OperatorID itemOperatorID in ResultedOperators)
                {
                    ResultLog += " [" + itemOperatorID.Location.IR + ":" + "0" + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedOperators.Count < 1 && searchMode == 0 && AcceptOperators)
            {
                // TODO FLORIN: Create expected Operator
                // ResultedOperators = CreateSymbolByName(OperatorNamePart, true); // set AcceptDuplicates to avoid other check for existence
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string operatorNamePart", operatorNamePart.ToString() });
                thisMethod.Parameters.Add(new string[] { "int mode", searchMode.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (OperatorID itemOperatorID in ResultedOperators)
                {
                    result += "{" + itemOperatorID.Location.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return ResultedOperators;
        }

        #endregion Operator Interface

        #region Relation Interface

        /// <summary>
        /// Get the proprieties of a symbol restricted to a relation type if provided
        /// </summary>
        /// <param name="operatorID">The relation to restrict the resultset if provided</param>
        /// <param name="multiplicationOrder">the multiplication order requested if provided</param>
        /// <param name="symbolID">The symbolID to expore for properties</param>
        /// <param name="limitOfResults">>Maximum capacity accepted for the result set</param>
        /// <returns>ResultSet: The descriptopn of the given symbol restricted to the relation if provided</returns>
        public List<SymbolID> GetContent(OperatorID operatorID, SymbolID symbolID, int limitOfResults)
        {
            // TODO Use multiplicationOrder parameter

            // Start logging
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            // Prepare result set
            List<SymbolID> resultedSymbolIDs = new List<SymbolID>();
            try
            {
                string commandString = string.Empty;
                if (operatorID.hasMultiplicity)
                {
                    commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + symbolID.Location.A.ToString() + "` WHERE `a` = " + symbolID.Location.A + " AND `b` = " + symbolID.Location.B + " AND `c` = " + operatorID.Location.IR + " AND `d` = " + operatorID.Location.Var + " ";
                }
                else
                {
                    commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + symbolID.Location.A.ToString() + "` WHERE `a` = " + symbolID.Location.A + " AND `b` = " + symbolID.Location.B + " AND `c` = " + operatorID.Location.IR +" AND `d` = " + operatorID.Location.Var + " ";
                //    commandString = "SELECT `e`, `f` FROM `" + DB + "`.`s" + symbolID.Location.A.ToString() + "` WHERE `a` = " + symbolID.Location.A + " AND `b` = " + symbolID.Location.B + " ";
                }
                if (limitOfResults != 0) { commandString += " Limit " + limitOfResults; }
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                MySqlDataReader myReader = mySqlCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (!string.IsNullOrEmpty(myReader.GetString(0)))
                    {
                        SymbolID newSymbolID = new SymbolID();
                        int.TryParse(myReader.GetString(0), out newSymbolID.Location.A);
                        try
                        {
                            int.TryParse(myReader.GetString(1), out newSymbolID.Location.B);
                        }
                        catch
                        {
                        }
                        //int.TryParse(myReader.GetString(2), out symbID[2]);
                        //int.TryParse(myReader.GetString(3), out symbID[3]);
                        if (newSymbolID.Exists)
                        {
                            resultedSymbolIDs.Add(newSymbolID);
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
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    ResultLog += " [" + itemSymbolID.Location.A + ":" + itemSymbolID.Location.B + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "int[] relationID", operatorID.ToString() });
                thisMethod.Parameters.Add(new string[] { "int[] symbolID", symbolID.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID itemSymbolID in resultedSymbolIDs)
                {
                    result += "{" + itemSymbolID.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return resultedSymbolIDs;
        }

        /// <summary>
        /// check if the relation is transitive
        /// </summary>
        /// <param name="path">path so far</param>
        /// <param name="operatorID">Given operator</param>
        /// <param name="symbolID1">from symbol A</param>
        /// <param name="symbolID2">to symbol B</param>
        /// <param name="limitOfResults">Maximum capacity accepted for the result set</param>
        /// <returns>the symbols succession from A to B</returns>
        public List<SymbolID> IsTransitive(List<SymbolID> path, OperatorID operatorID, SymbolID symbolID1, SymbolID symbolID2, int limitOfResults) 
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> LocalReturnList = new List<SymbolID>();
            List<SymbolID> returnList = new List<SymbolID>();
            bool solved = false;
            try
            {
                string commandString;
                if (false) // ($t1[0]=='?x')
                {
                    commandString = "SELECT `ID`, `g`, `h` FROM `" + DB + "`.`s" + symbolID1.Location.A.ToString("") + "` WHERE `a` = " + symbolID1.Location.A + " AND `c` LIKE " + operatorID.Location.Var + " AND `e` = " + symbolID2.Location.A + " ";
                }
                else
                {
                    commandString = "SELECT `ID`, `g`, `h` FROM `" + DB + "`.`s" + symbolID1.Location.A.ToString("") + "` WHERE `a` = " + symbolID1.Location.A + " AND `c` = " + operatorID.Location.Var + " AND `e` = " + symbolID2.Location.A + " ";
                }
                if (limitOfResults > 0) { commandString += " Limit " + limitOfResults; }
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
                            int[] multipplicity = new int[] {0, 0 };
                            int.TryParse(myReader.GetString(1), out multipplicity[0]);
                            int.TryParse(myReader.GetString(2), out multipplicity[1]);
                            //if (multipplicity[0] != 0 || multipplicity[1] != 0)
                            //{
                            //    ReturnList.Add(symbID);
                            //}
                            SymbolID symbID = new SymbolID();
                            symbID.Location.A = symbolID1.Location.A;
                            int.TryParse(myReader.GetString(0), out symbID.Location.A);
                            int.TryParse(myReader.GetString(1), out symbID.Location.B);
                            //int.TryParse(myReader.GetString(2), out symbID[3]);
                            if (symbID.Exists)
                            {
                                returnList.Add(symbID);
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
                        commandString = "SELECT `ID`, `e`, `f`, `g`, `h` FROM `" + DB + "`.`s" + symbolID1.Location.A.ToString("") + "` WHERE `a` = " + symbolID1.Location.A + " AND `c` = " + operatorID.Location.IR + " ";
                    }
                    else
                    {
                        commandString = "SELECT `ID`, `e`, `f`, `g`, `h` FROM `" + DB + "`.`s" + symbolID1.Location.A.ToString("") + "` WHERE `a` = " + symbolID1.Location.A + " AND `c`< 5000 ";
                    }
                    if (limitOfResults > 0) { commandString += " Limit " + limitOfResults; }
                    MySql.Data.MySqlClient.MySqlCommand mySqlCommand2 = new MySqlCommand(commandString, mySQLConnection);
                    MySqlDataReader myReader2 = mySqlCommand2.ExecuteReader();
                    if (MySqlTrace) SQLView.Log(mySqlCommand2.CommandText);
                    List<SymbolID> toTry = new List<SymbolID>();
                    while (myReader2.Read())
                    {
                        if (!string.IsNullOrEmpty(myReader2.GetString(0)))
                        {
                            SymbolID symbID = new SymbolID();
                            int.TryParse(myReader2.GetString(0), out symbID.Location.A);
                            int.TryParse(myReader2.GetString(1), out symbID.Location.B);
                            //int.TryParse(myReader2.GetString(2), out symbID[2]);
                            if (symbID.Exists)
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
                            foreach (SymbolID symbID in toTry)
                            {
                                if (symbID.Exists)
                                {
                                    SymbolID newSymbolID = symbID;
                                    LocalReturnList = IsTransitive(LocalReturnList, operatorID, newSymbolID, symbolID2, limitOfResults);
                                }
                                if (LocalReturnList.Count > 0)
                                {
                                    //int[] ToAdd = new int[] { a[0], symbID[0] };
                                    //ReturnList.Add(new int[] { symbID[1], symbID[2] });
                                    SymbolID newSymbolID = symbID;
                                    newSymbolID.Location.A = symbolID1.Location.A;
                                    newSymbolID.Location.B = symbID.Location.A;
                                    returnList.Add(newSymbolID);
                                    returnList.AddRange(LocalReturnList);
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
                foreach (SymbolID i in returnList)
                {
                    ResultLog += " [" + i.Location.A + ":" + i.Location.B + "]";
                }
                SQLView.LogResult(new string[] { ResultLog });
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                // , , int[] , int[] , int[] , int[] , int 
                thisMethod.Parameters.Add(new string[] { "List<int[]> path", path.ToString() });
                thisMethod.Parameters.Add(new string[] { "int[] relationID", operatorID.ToString() });
                thisMethod.Parameters.Add(new string[] { "int[] symbolID1", symbolID1.ToString() });
                thisMethod.Parameters.Add(new string[] { "int[] symbolID2", symbolID2.ToString() });
                thisMethod.Parameters.Add(new string[] { "int limitOfResults", limitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID symbolItem in returnList)
                {
                    result += "{" + symbolItem.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return returnList;
        }

        /// <summary>
        /// Create new relation
        /// </summary>
        /// <param name="operatorID">relation type</param>
        /// <param name="symbolID1">preoperand symbol</param>
        /// <param name="symbolID2">postoperand symbol</param>
        /// <returns>ResultSet: The new created relation</returns>
        public List<SymbolID> CreateRelation(OperatorID operatorID, SymbolID symbolID1, SymbolID symbolID2)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> relationPositions = new List<SymbolID>();
            SymbolID relationPosition = new SymbolID();
            try
            {
                string commandString = commandString = "INSERT INTO `" + DB + "`.`s" + symbolID2.Location.A.ToString("") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + symbolID2.Location.A + ", " + symbolID2.Location.B + "," + operatorID.Location.IR2 + ", " + operatorID.Location.Var2 + "," + symbolID1.Location.A + ", " + symbolID1.Location.A + ", " + operatorID.Location.IR2 + ", " + operatorID.Location.Var2 + " ) ;";
                //string commandString = commandString = "INSERT INTO `" + DB + "`.`s" + symbolID2[0].ToString() + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (" + symbolID2[0] + ", " + symbolID2[1] + "," + relationID[4] + ", " + relationID[5] + "," + symbolID1[0] + ", " + symbolID1[1] + ", " + relationID[2] + ", " + relationID[3] + " ) ;";
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                //if (mySQLConnection.
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                commandString = commandString = "INSERT INTO `" + DB + "`.`s" + symbolID1.Location.A.ToString("") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + symbolID1.Location.A + ", " + symbolID1.Location.B + ", " + operatorID.Location.IR + ", " + operatorID.Location.Var + ", " + symbolID2.Location.A + ", " + symbolID2.Location.B + ", " + operatorID.Location.IR2 + ", " + operatorID.Location.Var2 + " ) ;";
                //commandString = commandString = "INSERT INTO `" + DB + "`.`s" + symbolID1[0].ToString() + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (" + symbolID1[0] + ", " + symbolID1[1] + ", " + relationID[0] + ", " + relationID[1] + ", " + symbolID2[0] + ", " + symbolID2[1] + ", " + relationID[2] + ", " + relationID[3] + " ) ;";
                mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
                mySqlCommand.ExecuteNonQuery();
                if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (mySqlCommand.LastInsertedId > 0)
                {
                    relationPosition.Location.A = symbolID1.Location.A;
                    relationPosition.Location.B = (int)mySqlCommand.LastInsertedId;
                }
            }
            catch (MySqlException retrieveRelationIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveRelationIndexException.ToString());
            }
            if (MyResultsTrace) SQLView.LogResult(relationPosition.ToString());
            relationPositions.Add(relationPosition);
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelNumbers;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                string methodName = fr.GetMethod().ToString();
                thisMethod.ElementName = fr.GetMethod().Name;
                thisMethod.ReturnType = methodName.Substring(0, methodName.IndexOf(" "));
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "int[] relationID", operatorID.ToString() });
                // thisMethod.Parameters.Add(new string[] { "int[] multiplicityA", ArrayToString(multiplicityA) });
                thisMethod.Parameters.Add(new string[] { "int[] symbolID1", symbolID1.ToString() });
                // thisMethod.Parameters.Add(new string[] { "int[] multiplicityB", ArrayToString(multiplicityB) });
                thisMethod.Parameters.Add(new string[] { "int[] symbolID2", symbolID2.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID symbolItem in relationPositions)
                {
                    result += "{" + symbolItem.Location.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return relationPositions;
        }

        #endregion Relation Interface

        #region Interface Helpers

        #region Create symbol table

        /// <summary>
        /// Create a nonexistent table and rezerve first line with id 1
        /// </summary>
        /// <param name="symbolPosition">position of table reference in simbols table can be used to decode the table  name</param>
        private void CreateSymbolTable(SymbolID symbolID)
        {
            //=== CUSTOM LOGGER===================================================================================
            if (LoggingSystem.TraceCode)
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                LoggingSystem.LogMessage = "<div class='CodeElementNamespace'>  " + this.GetType().Namespace + " <span class='CodeElementClass'> " + this.GetType().Name + " </span>&gt; " + stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name, "<span class='CodeElementMethod'>" + stackTrace.GetFrame(0).GetMethod().Name + "</span>");
                LoggingSystem.LogMessage = "<span class='specialChaer'>(" + "<span class='CodeElementParameter'>int[] symbolPosition = </span> " + "<span class='CodeElementValue'>" + symbolID.ToString() + "</span>)</div>";
            }
            //====================================================================================================

            string commandString = "CREATE TABLE IF NOT EXISTS `" + DB + "`.`s" + symbolID.Location.A.ToString() + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , `g` INT NULL , `h` INT NULL , PRIMARY KEY (`ID`) );";
            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
            commandString = "INSERT INTO `" + DB + "`.`s" + symbolID.Location.A.ToString() + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (0,0,0,0,0,0,0,0) ;";
            mySqlCommand = new MySqlCommand(commandString, mySQLConnection);
            mySqlCommand.ExecuteNonQuery();
            if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
        }

        #endregion Create symbol table

        #endregion Interface Helpers

    }
}
