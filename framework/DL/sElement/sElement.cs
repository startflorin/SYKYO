using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.DL.Collection
{
    public class SElement
    {
        public SElement precedent = null;
        public SElement successor = null;


        private List<string> objectName = new List<string>();
        /// <summary>
        /// Name of the object
        /// </summary>
        public List<string> ObjectName
        {
            get 
            {
                if (objectName.Count != 0)
                {
                    return objectName;
                }
                else
                {
                    return objectName;
                }
            }
            set
            {
                objectName = value;
            }
        }

        private string inputSequence = string.Empty;
        /// <summary>
        /// The input sequence that this element is trying to represent
        /// </summary>
        public string InputSequence
        {
            get
            {
                return inputSequence;
            }
            set
            {
                inputSequence = value;
            }
        }


        private int objectId = -1;
        /// <summary>
        /// The line of the object
        /// </summary>
        public int ObjectId
        {
            get
            {
                return objectId;
            }
            set
            {
                objectId = value;
            }
        }

        /// <summary>
        /// Object to string representation
        /// </summary>
        /// <returns></returns>
        internal string ToString()
        {
            List<string> names = ObjectName;
            return names.FirstOrDefault();
        }
    }
}
