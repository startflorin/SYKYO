using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DL.CodeEntity;
using DataPersistency.DL.ServerAccess;

namespace DataPersistency.DL.Collection
{
    public class SOperatorCollection : AbstractCollection
    {
        private static DataPersistency.DL.ServerAccess.ServerAccessInterface databaseAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL(null);
        public SOperatorCollection()
        {
            databaseAccess.IsHumanReadable = true;
        }

        
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

        public string ToString()
        {
            StringBuilder collectionAsString = new StringBuilder();
            foreach (OperatorID element in sOperator)
            {
                if (databaseAccess.IsHumanReadable)
                {
                    List<string> names = databaseAccess.GetOperatorNamesByID(element);
                    foreach (string name in names)
                    {
                        collectionAsString.Append(name + " ");
                    }
                }
                else
                {
                    collectionAsString.Append(element.ToString() + "; ");
                }
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
            sOperator = new List<OperatorID>();
        }

        #endregion CLEAR


        public void AddByName(string lastString)
        {
            List<OperatorID> newObjects = databaseAccess.GetOperatorsByName(lastString, 1, 10);
            sOperator.AddRange(newObjects);
        }

        public void GetAllOperators()
        {
            List<OperatorID> newObjects = databaseAccess.GetAllOperators();
            sOperator.AddRange(newObjects);
        }
    }
}
