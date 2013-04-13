#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data;
using WpfApplication1.SElement;
#endregion USING

namespace WpfApplication1.sDataAccessBase
{
    class ServerAccessPostgree : ServerAccessInterface
    {
        #region Variables
        public bool AcceptSymbols { get; set; }
        public bool AcceptOperators { get; set; }
        public bool AcceptRelations { get; set; }
        static public List<string> GoodBDs = new List<string>();
        static public string DB;
        static public bool PostgreeTrace = false;
        static public bool MyResultsTrace = false;
        static NpgsqlConnection NpgsqlConnection = null;
        NpgsqlException PostgreeException = null;
        NpgsqlCommand PostgreeCommand = null;

        #endregion Variables

        #region Construction

        static ServerAccessPostgree()
        {
            NpgsqlConnection = getNpgsqlConnection(null);

            List<string> databaseNames = new List<string>();



            DB = "";// GoodBDs[0];
            //if (MyResultsTrace) SQLView.LogResult(new string[] { databaseNames[0] });
        }



        /// <summary>
        /// Initiate connection
        /// </summary>
        public ServerAccessPostgree()
        {
            if (NpgsqlConnection == null)
            {
                NpgsqlConnection = getNpgsqlConnection("" + DB + "");
            }
            // TODO: Complete member initialization
        }
        //~ServerAccessOracles()
        //{
        //if (NpgsqlConnection != null ) NpgsqlConnection.Close();
        //}

        #endregion Construction

        #region Helpers

        public string GetProviderName() { return "Postgree"; }

        public string getConnectionState() { return NpgsqlConnection.State.ToString(); }

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
                    NpgsqlConnection = getNpgsqlConnection(DB);
                }
            }
        }

        private static bool DatabaseExists(string value)
        {
            return true;
        }

        private static NpgsqlConnection getNpgsqlConnection(string DBc)
        {
            if (string.IsNullOrEmpty(DBc))
            {
                DBc = DB;
            }
            NpgsqlConnection NpgsqlConnection = new NpgsqlConnection();
            try
            {
                if (NpgsqlConnection != null && NpgsqlConnection.State == ConnectionState.Open)
                {
                    NpgsqlConnection.Close();
                }
                NpgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=Start1312;Database=rpro0001_cabinet;");
                NpgsqlConnection.Open();
            }
            catch (NpgsqlException connectionException)
            {
                Console.WriteLine("Error: {0}", connectionException.ToString());
            }
            return NpgsqlConnection;
        }
        
        #endregion Helpers

        #region Symbol Interface
        public List<int[]> getRecord(int symbNr, int offset)
        {
            return new List<int[]>();
        }
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
            //if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }

        // +GetSymbolNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
        public List<string> GetSymbolNamesByID(int[] SymbolID, int Mode = 0, int LimitOfResults = 10)
        {
            int[] a = SymbolID;
            List<string> ResultedSymbolNames = new List<string>();

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
                        commandString = "SELECT \"EN\" FROM \"SIMBS\" WHERE \"IR\" = " + a[0] + " ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                NpgsqlCommand OracleCommand = new NpgsqlCommand(commandString, NpgsqlConnection);
                NpgsqlDataReader myReader = OracleCommand.ExecuteReader();
                //if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (NpgsqlException retrieveSymbolIndexException)
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
                        commandString = "SELECT \"IR\" FROM \"SIMBS\" WHERE \"EN\" LIKE '" + SymbolNamePart + "' ";
                        break;
                    case 1: // starting with
                        commandString = "SELECT \"IR\" FROM \"SIMBS\" WHERE \"EN\" LIKE '" + SymbolNamePart + "%' ";
                        break;
                    case 2: // containing
                        commandString = "SELECT \"IR\" FROM \"SIMBS\" WHERE \"EN\" LIKE '%" + SymbolNamePart + "%' ";
                        break;
                    case 3: // ending with
                        commandString = "SELECT \"IR\" FROM \"SIMBS\" WHERE \"EN\" LIKE '%" + SymbolNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT \"IR\" FROM \"SIMBS\" WHERE \"EN\" LIKE '" + SymbolNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                NpgsqlCommand OracleCommand = new NpgsqlCommand(commandString, NpgsqlConnection);
                NpgsqlDataReader myReader = OracleCommand.ExecuteReader();
                //if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
                while (myReader.Read())
                {
                    if (!myReader.IsDBNull(0))
                    {
                        SObject symbolID = new SObject();
                        //int.TryParse(myReader.GetString(0), out symbolID[0]);
                        symbolID.ObjectId = (int)myReader.GetDecimal(0);
                        if (symbolID.ObjectId != 0 || symbolID.ObjectId != 0)
                        {
                            ResultedSymbols.Add(symbolID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (NpgsqlException retrieveSymbolIndexException)
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
                CreatedSymbols = CreateSymbolByName(SymbolNamePart, true);
                return CreatedSymbols;
            }
            return ResultedSymbols;
        }

        // +GetOperatorNamesByID(SymbolID : int [2] = {0,0}, int Mode; LimitOfResults : int = 10, ResultedSymbols : List<string>)
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
                        commandString = "SELECT \"EN\" FROM \"SREL\" WHERE \"IR\" = " + a[0] + " AND \"VAR\" = " + a[1] + " AND \"IR2\" = " + a[4] + " AND \"VAR2\" = " + a[5] + "  ;";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                NpgsqlCommand OracleCommand = new NpgsqlCommand(commandString, NpgsqlConnection);
                NpgsqlDataReader myReader = OracleCommand.ExecuteReader();
                //if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
            catch (NpgsqlException retrieveSymbolIndexException)
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

        // +GetOperatorsByName(SymbolsNamePart : string = "", Method : int = 0, LimitOfResults : int = 10, ResultedSymbols : List<int[2]>)
        public List<SOperator> GetOperatorsByName(string OperatorNamePart, int Mode, int LimitOfResults = 10)
        {
            List<SOperator> ResultedSymbols = new List<SOperator>();
            try
            {
                string commandString;
                switch (Mode)
                {
                    case 0: // strict "SELECT `IR`, `Var`, `IR2`, `Var2` FROM `" + DB + "`.`srel` WHERE `EN` LIKE `" + OperatorName + "` ";
                        commandString = "SELECT \"IR\", \"VAR\", \"IR2\", \"VAR2\" FROM \"SREL\" WHERE \"EN\" LIKE '" + OperatorNamePart + "' ";
                        break;
                    case 1: // starting with
                        commandString = "SELECT \"IR\", \"VAR\", \"IR2\", \"VAR2\" FROM \"SREL\" WHERE \"EN\" LIKE '" + OperatorNamePart + "%' ";
                        break;
                    case 2: // containing
                        commandString = "SELECT \"IR\", \"VAR\", \"IR2\", \"VAR2\" FROM \"SREL\" WHERE \"EN\" LIKE '%" + OperatorNamePart + "%' ";
                        break;
                    case 3: // ending with
                        commandString = "SELECT \"IR\", \"VAR\", \"IR2\", \"VAR2\" FROM \"SREL\" WHERE \"EN\" LIKE '%" + OperatorNamePart + "' ";
                        break;
                    default:
                        commandString = "SELECT \"IR\", \"VAR\", \"IR2\", \"VAR2\" FROM \"SREL\" WHERE \"EN\" LIKE '" + OperatorNamePart + "' ";
                        break;
                }
                //if (LimitOfResults != 0) { commandString += " Limit " + LimitOfResults + " "; }
                NpgsqlCommand OracleCommand = new NpgsqlCommand(commandString, NpgsqlConnection);
                NpgsqlDataReader myReader = OracleCommand.ExecuteReader();
                //if (OracleTrace) SQLView.Log(OracleCommand.CommandText);
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
                            //ResultedSymbols.Add(relationID);
                        }
                    }
                }
                myReader.Close();
            }
            catch (NpgsqlException retrieveSymbolIndexException)
            {
                Console.WriteLine("Error: {0}", retrieveSymbolIndexException.ToString());
            }

            if (MyResultsTrace)
            {
                string ResultLog = "";
                foreach (SOperator i in ResultedSymbols)
                {
                    ResultLog += " [" + i.OperatorId + ":" + "0" + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            if (ResultedSymbols.Count < 1 && Mode == 0 && AcceptSymbols)
            {
                List<SOperator> CreatedSymbols = new List<SOperator>();
                CreatedSymbols = CreateOperatorByName(OperatorNamePart, true);
                return CreatedSymbols;
            }
            return ResultedSymbols;
        }
        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        public List<SObject> CreateSymbolByName(string SymbolName, bool AcceptDuplicates)
        {
            if (!AcceptDuplicates)
            {
                List<SObject> ExistentSymbols = GetSymbolsByName(SymbolName, 0, 10);
                if (ExistentSymbols.Count > 0) return ExistentSymbols;
            }
            List<SObject> ResultedSymbols = new List<SObject>();
            int[] symbolPosition = { 0, 0, 0, 0 };


            CreateSymbolTable(symbolPosition);
            //if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }


        // +CreateSymbolByName(SymbolName : string = "UNKNOWN", AcceptDuplicates : boolean = 0, ResultedSymbol : List<int[2]>)
        public List<SOperator> CreateOperatorByName(string OperatorName, bool AcceptDuplicates)
        {
            if (!AcceptDuplicates)
            {
                List<SOperator> ExistentOperators = GetOperatorsByName(OperatorName, 0, 10);
                if (ExistentOperators.Count > 0) return ExistentOperators;
            }
            List<SOperator> ResultedSymbols = new List<SOperator>();
            int[] symbolPosition = { 0, 0, 0, 0 };


            CreateSymbolTable(symbolPosition);
            //if (MyResultsTrace) SQLView.LogResult(symbolPosition);
            return ResultedSymbols;
        }

        #endregion Symbol Interface

        #region Relation Interface


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
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return listSymbol;
        }

        public List<SOperator> GetOperatorsByName(string OperatorName)
        {
            List<SOperator> Operators = new List<SOperator>();

            if (MyResultsTrace)
            {
                string ResultLog = "";
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
            List<SOperator> allRelations = new List<SOperator>();

            if (MyResultsTrace)
            {
                string ResultLog = "";
                //foreach (int[] i in allRelations)
                {
                    //ResultLog += " [" + i[0] + ":" + i[1] + "]";
                }
                //SQLView.LogResult(new string[] { ResultLog });
            }
            return allRelations;
        }

        public int[] CreateRelationAsNew(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc)
        {
            int[] relationPosition = { 0, 0 };

            //if (MyResultsTrace) SQLView.LogResult(relationPosition);
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

            return ResultedRelationNames;
        }

        public List<int[]> CreateRelation(int[] r, int[] aMul, int[] aLoc, int[] bMul, int[] bLoc)
        {
            List<int[]> resultedIDs = new List<int[]>();
            int[] relationPosition = { 0, 0, 0, 0 };

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
        private void CreateSymbolTable(int[] symbolPosition)
        {
            string commandString = "CREATE TABLE IF NOT EXISTS `" + DB + "`.`s" + symbolPosition[0].ToString("00000000") + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , `g` INT NULL , `h` INT NULL , PRIMARY KEY (`ID`) );";
            //Postgree.Data.PostgreeClient.PostgreeCommand PostgreeCommand = new PostgreeCommand(commandString, NpgsqlConnection);
            //PostgreeCommand.ExecuteNonQuery();
            //if (PostgreeTrace) SQLView.Log(PostgreeCommand.CommandText);
            commandString = "INSERT INTO `" + DB + "`.`s" + symbolPosition[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`, `g`, `h`) VALUES (0,0,0,0,0,0,0,0) ;";
            //PostgreeCommand = new PostgreeCommand(commandString, NpgsqlConnection);
            //PostgreeCommand.ExecuteNonQuery();
            //if (PostgreeTrace) SQLView.Log(PostgreeCommand.CommandText);
        }

        #endregion
        #endregion


    }
}
