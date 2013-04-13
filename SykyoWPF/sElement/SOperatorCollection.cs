using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;

namespace WpfApplication1.SElement
{
    class SOperatorCollection : AbstractCollection
    {
        //static ConditionModel.LoggingSystem.LoggingSystemOptions loggingSystemOptions = new ConditionModel.LoggingSystem.LoggingSystemOptions();

        static DataPersistency.DL.ServerAccess.ServerAccessInterface databaseAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL(null);
        

        private List<OperatorID> sOperator = new List<OperatorID>();
        /// <summary>
        /// List of objects
        /// </summary>
        public List<OperatorID> SOperator
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
            foreach (OperatorID element in sOperator)
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
            sOperator = new List<OperatorID>();
        }

        #endregion CLEAR


        internal void AddByName(string lastString)
        {
            List<OperatorID> newObjects = databaseAccess.GetOperatorsByName(lastString, 1, 10);
            sOperator.AddRange(newObjects);
        }

        internal void GetAllOperators()
        {
            List<OperatorID> newObjects = databaseAccess.GetAllOperators();
            sOperator.AddRange(newObjects);
        }
    }
}
