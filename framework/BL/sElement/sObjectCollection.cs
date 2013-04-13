using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;
using DataModel.DL.CodeEntity;

namespace DataPersistency.DL.Collection
{
    public class SObjectCollection : AbstractCollection
    {
        //static ConditionModel.LoggingSystem.LoggingSystemOptions loggingSystemOptions = new ConditionModel.LoggingSystem.LoggingSystemOptions();

        private List<SymbolID> sObject = new List<SymbolID>();
        /// <summary>
        /// List of objects
        /// </summary>
        public List<SymbolID> SObject
        {
            get
            {
                return sObject;
            }
            set
            {
                sObject = value;
            }
        }

        private SymbolID ConvertElementToObject(SElement sElement)
        {
            SymbolID localObject = new SymbolID();
            localObject.Location.A = sElement.ObjectId;
            localObject.Names = sElement.ObjectName;
            return localObject;
        }

        #region INTERFACE

        public void AddElement(SElement sElement)
        {
            sObject.Add(ConvertElementToObject(sElement));
        }
        #endregion INTERFACE

        #region TO STRING



        internal string ToString()
        {
            StringBuilder collectionAsString = new StringBuilder();
            foreach (SymbolID element in sObject)
            {
                collectionAsString.Append(element.ToString());
            }
            return collectionAsString.ToString();
        }

        #endregion TO STRING


        #region CLEAR

        /// <summary>
        /// Clear the content of the collection
        /// </summary>
        public void Clear()
        {
            sObject = new List<SymbolID>();
        }

        #endregion CLEAR

        /// <summary>
        /// Add symbols by name: 0-Strict, 1-Begin, 2-Middle, 3-End
        /// </summary>
        /// <param name="lastString"></param>
        /// <param name="method"></param>
        public void AddByName(string lastString, int mode)
        {
            DataPersistency.DL.ServerAccess.ServerAccessInterface databaseAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL(null);
            List<SymbolID> newObjects = databaseAccess.GetSymbolsByName(lastString, mode, 10);
            sObject.AddRange(newObjects);
        }
    }
}
