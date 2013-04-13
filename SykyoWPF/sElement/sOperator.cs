using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.SElement
{
    public class OperatorID
    {
        #region NAMES
        private List<string> operatorNames = new List<string>();
        /// <summary>
        /// Name of the Operator
        /// </summary>
        public List<string> Names
        {
            get 
            {
                if (operatorNames.Count != 0)
                {
                    return operatorNames;
                }
                else
                {
                    return operatorNames;
                }
            }
            set
            {
                operatorNames = value;
            }
        }
        #endregion NAMES

        #region ID
        private int operatorId = -1;
        /// <summary>
        /// The line of the Operator
        /// </summary>
        public int OperatorId
        {
            get
            {
                return operatorId;
            }
            set
            {
                operatorId = value;
            }
        }
        #endregion ID

        public int IR;
        public int VAR;
        public int IR2;
        public int VAR2;

        #region TO STRING
        /// <summary>
        /// Object to string representation
        /// </summary>
        /// <returns></returns>
        internal string ToString()
        {
            List<string> names = Names;
            return names.FirstOrDefault();
        }

        #endregion TO STRING
    }
}
