using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DL.CodeEntity
{
    public class ElementParameter : CodeElement
    {
        private string elementName;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }
    }
}
