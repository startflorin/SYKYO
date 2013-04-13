#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;
using WindowsFormsApplication1.BL;
using DataPersistency.DL.Logging;
#endregion USING

namespace WindowsFormsApplication1.Level_Objects_From_Numbers
{
    public class SymbolCollection
    {

        #region VARIABLES
        // log options model
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptions;

        List<SymbolID> symbolCollection = new List<SymbolID>();

        static string provider = "MySql";
        public static ServerAccessInterface serverAccess ;
        bool CreateUnknown = false;
        SymbolID ID = new SymbolID();
        string Name = string.Empty;
        #endregion VARIABLES

        #region CONSTRUCTOR

        #endregion CONSTRUCTOR

        #region ELEMENT SET

        #endregion ELEMENT SET

        #region ELEMENT GET

        public SymbolCollection(ServerAccessInterface serverAccessReference, DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsReferince)
        {
            logingOptions = logingOptionsReferince;
            serverAccess = serverAccessReference;
        }

        public SymbolCollection(SymbolID ID)
        {
            if (ID.Exists)
            {
                List<string> ReturnedElements = serverAccess.GetSymbolNamesByID(ID, 1);
                if (ReturnedElements.Count > 0)
                {
                    this.ID = ID;
                    this.Name = ReturnedElements[0];
                }
            }
        }

        public SymbolCollection()
        {
            
        }

        public void AddRange(List<SymbolID> SymbolIDs)
        {
            symbolCollection = SymbolIDs;
        }

        internal List<SymbolID> GetSymbolByID(SymbolID ID)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> ReturnedElements = new List<SymbolID>();
            if (ID.Exists)
            {
                List<string> ReturnedStrings = serverAccess.GetSymbolNamesByID(ID, 1);
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
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "int[] ID", ID.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID rl in ReturnedElements)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ReturnedElements;
        }

        internal List<SymbolID> GetSymbolCollection(string name)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                List<SymbolID> ReturnedElements = serverAccess.GetSymbolsByName(name, 0, 10001);
                if (ReturnedElements.Count > 0)
                {
                    foreach (SymbolID ints in ReturnedElements)
                    {
                        if (ints.Exists)
                        {
                            ints.Names.Add(name);
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
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string name", name.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID rl in ReturnedSymbols)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ReturnedSymbols;
        }

        internal List<SymbolID> ForceSymbolCollection(string name)
        {
                        System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            List<SymbolID> ReturnedElements = serverAccess.CreateSymbolByName(name, true);
            if (ReturnedElements.Count > 0)
            {
                foreach (SymbolID IDs in ReturnedElements)
                {
                    if (IDs.Exists)
                    {
                        ReturnedSymbols.Add(new SymbolID(IDs.Location, IDs.Multiplicity, name));
                    }
                }
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string name", name.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID rl in ReturnedSymbols)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ReturnedSymbols;
        }

        internal List<SymbolID> GetSynonimesCollection(SymbolID symbol)
        {
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (symbol.Exists)
            {
                List<string> ReturnedElements = serverAccess.GetSymbolNamesByID(symbol, 0);
                if (ReturnedElements.Count > 0)
                {
                    foreach (string Name in ReturnedElements)
                    {
                        if (symbol.Exists)
                        {
                            symbol.Names.Add(Name);
                            ReturnedSymbols.Add(symbol);
                        }
                    }
                }
            }
            #region LOG
            //=== CUSTOM LOGGER===================================================================================
            ElementMethod thisMethod = new ElementMethod();
            int logLevel = -1; if (logingOptions != null) logLevel = logingOptions.levelRelations;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "SymbolID symbol", symbol.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID rl in ReturnedSymbols)
                {
                    result += "{" + rl.ToString() + "}";
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG
            return ReturnedSymbols;
        }

         #endregion ELEMENT GET

        #region ELEMENT COUNT

        // Count Sinonymes
        public int Count(SymbolID ID)
        {
            List<string> ReturnedElements = serverAccess.GetSymbolNamesByID(ID, 10001);
            return ReturnedElements.Count;
        }

        // Count Paronymes
        public int Count(string Name)
        {
            List<SymbolID> ReturnedElements = serverAccess.GetSymbolsByName(Name, 0, 10001);
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
            if (ID.Location.A != 0 || ID.Location.B != 0)
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

        public string ToString()
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
            System.Diagnostics.StackTrace stackTrace0 = new System.Diagnostics.StackTrace(); LoggingSystem.BranchName = stackTrace0.GetFrame(0).GetMethod().Name;

            List<SymbolID> ReturnedSymbols = new List<SymbolID>();
            if (!string.IsNullOrWhiteSpace(SymbolNamePart))
            {
                List<SymbolID> ReturnedElements = new List<SymbolID>();
                if (serverAccess != null)
                {
                    ReturnedElements = serverAccess.GetSymbolsByName(SymbolNamePart, Mode, 30);
                }
               if (ReturnedElements.Count > 0)
                {
                    foreach (SymbolID ints in ReturnedElements)
                    {
                        if (ID.Exists)
                        {
                            ID.Names = serverAccess.GetSymbolNamesByID(ID, 10);
                            //ReturnedSymbols.Add(new SymbolID(ID, Multiplicity, Name));
                            ReturnedSymbols.Add(ID);
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
            logLevel = 3;
            if (logLevel >= 3) // code
            {
                System.Diagnostics.StackTrace stackTrace; System.Diagnostics.StackFrame fr; stackTrace = new System.Diagnostics.StackTrace(); fr = stackTrace.GetFrame(0);
                thisMethod.ElementNamespaceName = this.GetType().Namespace;
                thisMethod.ElementClassName = this.GetType().Name;
                thisMethod.ElementName = stackTrace.GetFrame(0).GetMethod().ToString().Replace(stackTrace.GetFrame(0).GetMethod().Name.ToString(), "<span style='color: red'>" + stackTrace.GetFrame(0).GetMethod().Name.ToString() + "</span>");
            }
            if (logLevel >= 2) // parameters
            {
                thisMethod.Parameters.Add(new string[] { "string SymbolNamePart", SymbolNamePart.ToString() });
                thisMethod.Parameters.Add(new string[] { "int Mode", Mode.ToString() });
                thisMethod.Parameters.Add(new string[] { "int LimitOfResults", LimitOfResults.ToString() });
            }
            if (logLevel >= 1) // results
            {
                string result = string.Empty;
                foreach (SymbolID symbol in ReturnedSymbols)
                {
                    foreach (string name in symbol.Names)
                    {
                        result += "{" + name + "}";
                    }
                }
                thisMethod.Result = result;
            }
            LoggingSystem.LogMethod = thisMethod;

            //====================================================================================================
            #endregion LOG

            return ReturnedSymbols;
        }

        private string ArrayToString(int[] array)
        {
            string result = string.Empty;
            result += "[";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
                if (i < array.Length - 1)
                {
                    result += ":";
                }
            }
            result += "]";
            return result;
        }
    }
}