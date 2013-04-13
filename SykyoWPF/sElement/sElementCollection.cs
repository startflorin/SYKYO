using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;

namespace WpfApplication1.SElement
{
    class SElementCollection : AbstractCollection
    {
        public SElementCollection()
        {

        }

        // static ConditionModel.LoggingSystem.LoggingSystemOptions loggingSystemOptions = new ConditionModel.LoggingSystem.LoggingSystemOptions();

        private List<AbstractCollection> sElement = new List<AbstractCollection>();
        /// <summary>
        /// List of objects
        /// </summary>
        public List<AbstractCollection> SElement
        {
            get
            {
                return sElement;
            }
            set
            {
                sElement = value;
            }
        }

        #region TO STRING

        internal string ToString()
        {
            StringBuilder collectionAsString = new StringBuilder();
            foreach (SymbolID element in sElement)
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
        internal void Clear()
        {
            //sElement = new List<e>();
        }

        #endregion CLEAR


        internal void AddByName(string lastString)
        {
            DataPersistency.DL.ServerAccess.ServerAccessInterface databaseAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL(null);
            List<SymbolID> newObjects = databaseAccess.GetSymbolsByName(lastString, 1, 10);
            //sElement.AddRange(newObjects);
        }
    }
}
