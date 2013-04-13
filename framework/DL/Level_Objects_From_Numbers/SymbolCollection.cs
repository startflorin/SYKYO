#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;
#endregion USING

namespace DataPersistency.DL.CommenAccess.ObjectsFromNumbers
{
    public class SymbolCollection
    {

        #region VARIABLES
        // log options model
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions;

        List<SymbolID> symbolCollection = new List<SymbolID>();

        static string provider = "Oracle";
        private static ServerAccessInterface serverAccess;
        public static ServerAccessInterface ServerAccess
        {
            get
            {
                if (serverAccess == null)
                {
                    serverAccess = new ServerAccessMySQL("");
                    serverAccess.TryToOpenConnection();
                }
                return serverAccess;
            }
            set
            {
            }
        }
        bool CreateUnknown = false;
        SymbolID ID = new SymbolID();
        string Name = string.Empty;
        #endregion VARIABLES

        #region CONSTRUCTOR

        #endregion CONSTRUCTOR

        #region ELEMENT SET

        #endregion ELEMENT SET

        #region ELEMENT GET

        public SymbolCollection(DataPersistency.DL.ServerAccess.ServerAccessInterface serverAccessReference, DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsReferince)
        {
            logingOptions = logingOptionsReferince;
            ServerAccess = serverAccessReference;
        }

        public SymbolCollection(SymbolID ID)
        {
            if (ID.Location.A != 0 || ID.Location.B != 0)
            {
                List<string> ReturnedElements = ServerAccess.GetSymbolNamesByID(ID, 1);
                if (ReturnedElements.Count > 0)
                {
                    this.ID = ID;
                    this.Name = ReturnedElements[0];
                }
            }
        }

        public SymbolCollection()
        {
            // TODO: Complete member initialization
        }

        public void AddRange(List<SymbolID> SymbolIDs)
        {
            symbolCollection = SymbolIDs;
        }

        internal List<SymbolID> GetSymbolByID(SymbolID ID)
        {
            List<SymbolID> ReturnedElements = new List<SymbolID>();
            if (ID.Exists)
            {
                List<string> ReturnedStrings = ServerAccess.GetSymbolNamesByID(ID, 1);
                if (ReturnedStrings.Count > 0)
                {
                    foreach (string Name in ReturnedStrings)
                    {
                        ReturnedElements.Add(new SymbolID(ID.Location, ID.Multiplicity, Name));// this.ID = ID;
                        //this.Name = ReturnedElements[0];
                    }
                }
                else
                {
                    ReturnedElements.Add(SymbolID.Null);
                }
            }
            return ReturnedElements;
        }

        internal List<SymbolID> GetSymbolCollection(string Name)
        {
            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                List<SymbolID> ReturnedElements = ServerAccess.GetSymbolsByName(Name, 0, 10001);
                if (ReturnedElements.Count > 0)
                {
                    foreach (SymbolID ints in ReturnedElements)
                    {
                        if (ints.Exists)
                        {
                            ints.Multiplicity = ID.Multiplicity;
                            ints.Names.Add(Name);
                            ReturnedSymbols.Add(ints);
                        }
                    }
                }
            }
            if (ReturnedSymbols.Count < 1)
            {
                ReturnedSymbols.Add(SymbolID.Null);
            }
            return ReturnedSymbols;
        }

        internal List<SymbolID> ForceSymbolCollection(string Name)
        {
            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            List<SymbolID> ReturnedElements = ServerAccess.CreateSymbolByName(Name, true);
            if (ReturnedElements.Count > 0)
            {
                foreach (SymbolID IDs in ReturnedElements)
                {
                    if (IDs.Exists)
                    {
                        IDs.Multiplicity = ID.Multiplicity;
                        IDs.Names.Add(Name);
                        ReturnedSymbols.Add(IDs);
                    }
                }
            } return ReturnedSymbols;
        }

        internal List<SymbolID> GetSynonimesCollection(SymbolID symbol)
        {
            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (symbol.Exists)
            {
                List<string> ReturnedElements = ServerAccess.GetSymbolNamesByID(symbol, 0);
                if (ReturnedElements.Count > 0)
                {
                    foreach (string Name in ReturnedElements)
                    {
                        if (symbol.Exists)
                        {
                            ReturnedSymbols.Add(new SymbolID(symbol.Location, symbol.Multiplicity, Name));
                        }
                    }
                }
            }
            return ReturnedSymbols;
        }

         #endregion ELEMENT GET

        #region ELEMENT COUNT

        // Count Sinonymes
        public int Count(SymbolID ID)
        {
            List<string> ReturnedElements = ServerAccess.GetSymbolNamesByID(ID, 10001);
            return ReturnedElements.Count;
        }

        // Count Paronymes
        public int Count(string Name)
        {
            List<SymbolID> ReturnedElements = ServerAccess.GetSymbolsByName(Name, 10001);
            return ReturnedElements.Count;
        }

        // Count
        public int Count()
        {
            if (ID.Exists)
            {
                return Count(ID);
            }

            else
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    return Count(Name);
                }
            return 0;
        }

        #endregion ELEMENT COUNT

        #region ELEMENT EXIST
        public bool Exists()
        {
            if (ID.Exists)
            {
                if (Count(ID) > 0)
                {
                    return true;
                }
            }

            else
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (Count(Name) > 0)
                    {
                        return true;
                    }
                }
            return false;
        }
        #endregion ELEMENT EXIST

        public override string ToString()
        {
            StringBuilder symbolCollectionAsString = new StringBuilder();
            foreach (SymbolID SymbolID in symbolCollection)
            {
                symbolCollectionAsString.Append(SymbolID.ToString()+" ");
            }
            return symbolCollectionAsString.ToString();
        }

        internal List<SymbolID> GetSymbolCollectionByString(string SymbolNamePart, int Mode = 0, int LimitOfResults = 10)
        {
            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (!string.IsNullOrWhiteSpace(SymbolNamePart))
            {
                List<SymbolID> ReturnedElements = new List<SymbolID>();
                if (ServerAccess != null)
                {
                    ReturnedElements = ServerAccess.GetSymbolsByName(SymbolNamePart, 30);
                }
               if (ReturnedElements.Count > 0)
                {
                    foreach (SymbolID ints in ReturnedElements)
                    {
                        //SymbolID ID = new SymbolID( ints.Location.A, ints.Location.B );
                        //int[] Multiplicity = { ints[2], ints[3] };
                        if (ints.Exists)
                        {
                            ints.Names = ServerAccess.GetSymbolNamesByID(ID, 10);
                            //ReturnedSymbols.Add(new SymbolID(ID, Multiplicity, Name));
                            ReturnedSymbols.Add(ints);
                        }
                    }
                }
            }
            if (ReturnedSymbols.Count < 1)
            {
                ReturnedSymbols.Add(SymbolID.Null);
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel=logingOptions.levelObjects;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
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
                foreach (SymbolID symbol in ReturnedSymbols)
                {
                    result += "[" + symbol.Names[0] + "]";
                }
                thisMethod.Result = result;
            }
            DataPersistency.DL.Logging.LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ReturnedSymbols;
        }
    }
}