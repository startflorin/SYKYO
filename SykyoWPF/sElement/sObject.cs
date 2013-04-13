using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.SElement
{
    public class SymbolID
    {

        private List<string> objectName = new List<string>();
        /// <summary>
        /// Name of the object
        /// </summary>
        public List<string> Names
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


        private int objectId = -1;
        /// <summary>
        /// The line of the object
        /// </summary>
        public int LocationA
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
            List<string> names = Names;
            return names.FirstOrDefault();
        }
    }
}
