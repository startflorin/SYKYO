#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DataPersistency.UI.Logging;
#endregion USING

namespace DataPersistency.DL.ServerAccess
{
    public class ServerAccessSqlServer// : ServerAccessInterface
    {
        #region Variables
        public bool AcceptSymbols { get; set; }
        public bool AcceptOperators { get; set; }
        public bool AcceptRelations { get; set; }
        static public List<string> GoodBDs = new List<string>();
        static public string DB;
        static public bool SqlServerTrace = false;
        static public bool MyResultsTrace = false;
        static SqlConnection  SqlServerConnection = null;
        SqlConnection SqlException = null;
        SqlCommand SqlServerCommand = null;

        #endregion Variables

        #region Construction

        public bool TryToOpenConnection()
        {
            return true;
        }
        
        static ServerAccessSqlServer()
        {
            SqlServerConnection = getSqlServerConnection(null);

            List<string> databaseNames = new List<string>();
            


            DB = SqlServerConnection.Database;
            if (MyResultsTrace) SQLView.LogResult(new string[] { DB });
        }



        /// <summary>
        /// Initiate connection
        /// </summary>
        public ServerAccessSqlServer()
        {
            if (SqlServerConnection == null)
            {
                SqlServerConnection = getSqlServerConnection("" + DB + "");
            }
            // TODO: Complete member initialization
        }
        //~ServerAccessOracles()
        //{
            //if (SqlServerConnection != null ) SqlServerConnection.Close();
        //}

        #endregion Construction

        #region Helpers

        public string GetProviderName() { return "SqlServer"; }

        public string getConnectionState() { return SqlServerConnection.State.ToString(); }

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
                    SqlServerConnection = getSqlServerConnection(DB);
                }
            }
        }

        private static bool DatabaseExists(string value)
        {
            return true;
        }

        private static SqlConnection  getSqlServerConnection(string DBc)
        {
            if (string.IsNullOrEmpty(DBc))
            {
                DBc = DB;
            }
            SqlConnection  SqlServerConnection = new SqlConnection ();
            try
            {
                if (SqlServerConnection != null && SqlServerConnection.State == ConnectionState.Open)
                {
                    SqlServerConnection.Close();
                }
                SqlServerConnection = new SqlConnection("user id=rpro0001_view;" +
                                       "password=Start1312;server=PROFIMEDICA-PC;" +
                                       "Trusted_Connection=yes;" +
                                       "database=rpro0001_cabinet; " +
                                       "connection timeout=30");
                SqlServerConnection.Open();
            }
            catch (SqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return SqlServerConnection;
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
                
            }
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        List<string> GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = SymbolID;
            List<string> ResultedSymbolNames = new List<string>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    default: // strict
                        commandString = "SELECT [EN] FROM [SIMBS] WHERE [IR] = " + a[0] + " ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                SqlDataReader myReader = mySqlCommand.ExecuteReader();
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
            catch (SqlException retrieveSymbolIndexException)
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
                        commandString = "SELECT [IR] FROM [simbs] WHERE [En] LIKE '" + SymbolNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT [IR] FROM [simbs] WHERE [En] LIKE '" + SymbolNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT [IR] FROM [simbs] WHERE [En] LIKE '%" + SymbolNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT [IR] FROM [simbs] WHERE [En] LIKE '" + SymbolNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT [IR] FROM [simbs] WHERE [En] LIKE '" + SymbolNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                SqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (myReader.GetInt32(0) > -1)
                    {
                        int[] symbolID = new int[] { 0, 0, 0, 0 };
                        symbolID[0] = myReader.GetInt32(0);
                        if (symbolID[0] != 0 || symbolID[1] != 0)
                        {
                            ResultedSymbols.Add(symbolID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (SqlException retrieveSymbolIndexException)
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
                return CreatedSymbols;
            }
            return ResultedSymbols;
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
                    string commandString = "INSERT INTO `" + DB + "`.`simbs` (`ID`, `IR`, `En`) VALUES (NULL, " + ExistentID + ", '" + SymbolAliasName + "');";
                    SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                    mySqlCommand.ExecuteNonQuery();
                    if (SqlServerTrace) SQLView.Log(mySqlCommand.CommandText);
                    
                }
                catch (SqlException retrieveSymbolIndexException)
                {
                    Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
                }
            }
            if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }    


        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        public List<int[]> CreateSymbolByName(string SymbolName, bool AcceptDuplicates)
        {
            if (! AcceptDuplicates)
            {
                List<int[]> ExistentSymbols = GetSymbolsByName(SymbolName, 0, 10);
                if (ExistentSymbols.Count > 0) return ExistentSymbols;
            }
            List<int[]> ResultedSymbols = new List<int[]>();
            int[] symbolPosition = { 0, 0, 0, 0 };
            try
            {
                //DEFAULT und NULL sind nicht als explizite Identitätswerte zulässig
                string commandString = "INSERT INTO [simbs] ([En]) OUTPUT INSERTED.ID VALUES ('" + SymbolName + "') ";
                SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                SqlDataReader myReader = mySqlCommand.ExecuteReader();
                int LastInsertedId = 0;
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (myReader.GetInt32(0) > -1)
                    {
                        int[] symbolID = new int[] { 0, 0, 0, 0 };
                        LastInsertedId = myReader.GetInt32(0);
                        symbolID[0] = LastInsertedId;
                        if (symbolID[0] != 0 || symbolID[1] != 0)
                        {
                            ResultedSymbols.Add(symbolID);
                        }
                    }
                }
                myReader.Close(); 
                //if (SqlTrace) SQLView.Log(mySqlCommand.CommandText);
                if (LastInsertedId > 0)
                {
                    string commandString2 = "UPDATE [simbs] SET [IR] = [ID] WHERE [ID] = " + LastInsertedId + " ;";
                    SqlCommand mySqlCommand2 = new SqlCommand(commandString2, SqlServerConnection);
                    mySqlCommand2.ExecuteNonQuery();
                    symbolPosition[0] = LastInsertedId;
                    //ResultedSymbols.Add(symbolPosition);
                }
            }
            catch (SqlException retrieveSymbolIndexException)
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
            return new List<int[]>();
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
                        commandString = "SELECT [IR], [VAR], [IR2], [VAR2] FROM [srel] WHERE [En] LIKE '" + OperatorNamePart + "' ";
                        break;
                    case 1: //
                        commandString = "SELECT [IR], [VAR], [IR2], [VAR2] FROM [srel] WHERE [En] LIKE '" + OperatorNamePart + "%' ";
                        break;
                    case 2: //
                        commandString = "SELECT [IR], [VAR], [IR2], [VAR2] FROM [srel] WHERE [En] LIKE '%" + OperatorNamePart + "%' ";
                        break;
                    case 3: //
                        commandString = "SELECT [IR], [VAR], [IR2], [VAR2] FROM [srel] WHERE [En] LIKE '" + OperatorNamePart + "%' ";
                        break;
                    default:
                        commandString = "SELECT [IR], [VAR], [IR2], [VAR2] FROM [srel] WHERE [En] LIKE '" + OperatorNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                SqlDataReader myReader = mySqlCommand.ExecuteReader();
                //if (MySqlTrace) SQLView.Log(mySqlCommand.CommandText);
                while (myReader.Read())
                {
                    if (myReader.GetInt32(0) > -1)
                    {
                        int[] relationID = new int[] { 0, 0, 0, 0, 0, 0 };
                        relationID[0] = (int)myReader.GetInt32(0);
                        relationID[1] = (int)myReader.GetInt32(1);
                        relationID[4] = (int)myReader.GetInt32(2);
                        relationID[5] = (int)myReader.GetInt32(3);
                        if (relationID[0] != 0 || relationID[1] != 0)
                        {
                            ResultedOperators.Add(relationID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (SqlException retrieveSymbolIndexException)
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
                CreatedSymbols = CreateSymbolByName(OperatorNamePart, true);
                return CreatedSymbols;
            }
            return ResultedOperators;
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
                        commandString = "SELECT [EN] FROM [SREL] WHERE [IR] = " + a[0] + " ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                SqlCommand mySqlCommand = new SqlCommand(commandString, SqlServerConnection);
                SqlDataReader myReader = mySqlCommand.ExecuteReader();
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
            catch (SqlException retrieveSymbolIndexException)
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

        public List<int[]> GetContent(int[] r, int[] aMul, int[] aLoc, int limit)
        {
            List<int[]> listSymbol = new List<int[]>();
            
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
            
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
            return relationPosition;
        }

        public List<int[]> IsTransitive(List<int[]> list, int[] r, int[] a, int[] b, int[] asss, int[] bsss, int limit) 
        {
            List<int[]> LocalReturnList = new List<int[]>();
            List<int[]> ReturnList = new List<int[]>();
            
            return ReturnList;
        }


        // +GetRelationsByName(RelationsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedRelations : List<int[4]>)
        public List<int[]> GetRelationOperatorByName(string RelationsNamePart, int Method = 0, int LimitOfResults = 10)
        {
            List<int[]> ResultedSymbols = new List<int[]>();
            
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
            
            return ResultedRelationNames;
        }

        public List<int[]> CreateRelation(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc) 
        {
            List<int[]> resultedIDs = new List<int[]>();
            int[] relationPosition = { 0, 0, 0, 0 };
            
            if (MyResultsTrace) SQLView.LogResult(relationPosition);
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
        private void CreateSymbolTable(int[] symbolPosition)
        {
            string commandString = "if not exists (select * from sysobjects where name='s" + symbolPosition[0].ToString("00000000") + "' and xtype='U') CREATE TABLE [s" + symbolPosition[0].ToString("00000000") + "] ( [ID] SMALLINT IDENTITY(1,1)PRIMARY KEY , [a] INT NULL , [b] INT NULL , [c] INT NULL , [d] INT NULL , [e] INT NULL , [f] INT NULL , [g] INT NULL , [h] INT NULL) ";
            SqlCommand SqlServerCommand = new SqlCommand(commandString, SqlServerConnection);
            SqlServerCommand.ExecuteNonQuery();
            //if (SqlServerTrace) SQLView.Log(SqlServerCommand.CommandText);
            commandString = "INSERT INTO [S" + symbolPosition[0].ToString("00000000") + "] ([A], [B], [C], [D], [E], [F], [G], [H]) VALUES (0,0,0,0,0,0,0,0) ;";
            SqlServerCommand = new SqlCommand(commandString, SqlServerConnection);
            SqlServerCommand.ExecuteNonQuery();
            //if (SqlServerTrace) SQLView.Log(SqlServerCommand.CommandText);
        }

        #endregion
        #endregion

            
    }
}
