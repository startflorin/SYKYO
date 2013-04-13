using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;

namespace WpfApplication1.SElement
{
    class SOperatorCollection : AbstractCollection
    {
        static ConditionModel.LoggingSystem.LoggingSystemOptions loggingSystemOptions = new ConditionModel.LoggingSystem.LoggingSystemOptions();

        static ServerAccessInterface databaseAccess = new ServerAccessMySQL();
        

        private List<SOperator> sOperator = new List<SOperator>();
        /// <summary>
        /// List of objects
        /// </summary>
        public List<SOperator> SOperator
        {
            get
            {
                return sOperator;
            }
            set
            {
                sOperator = value;
            }
        }


        #region TO STRING

        internal string ToString()
        {
            StringBuilder collectionAsString = new StringBuilder();
            foreach (SOperator element in sOperator)
            {
                collectionAsString.Append(element.ToString()+"; ");
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
            sOperator = new List<SOperator>();
        }

        #endregion CLEAR


        internal void AddByName(string lastString)
        {
            List<SOperator> newObjects = databaseAccess.GetOperatorsByName(lastString, 1, 10);
            sOperator.AddRange(newObjects);
        }

        internal void GetAllOperators()
        {
            List<SOperator> newObjects = databaseAccess.GetAllOperators();
            sOperator.AddRange(newObjects);
        }
    }
}
