using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity
{
    public class ElementNamespace : CodeElement
    {
        #region file
        private Collection<FileInfo> elementFile = new Collection<FileInfo>();
        /// <summary>
        /// File path of the code element
        /// </summary>
        public Collection<FileInfo> ElementFile
        {
            get { return elementFile; }
            set { elementFile = value; }
        }
        #endregion file

        #region line
        private Collection<int> elementStartLine = new Collection<int>();
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public Collection<int> ElementStartLine
        {
            get { return elementStartLine; }
            set { elementStartLine = value; }
        }

        private Collection<int> elementEndLine = new Collection<int>();
        /// <summary>
        /// End line number of the code element
        /// </summary>
        public Collection<int> ElementEndLine
        {
            get { return elementEndLine; }
            set { elementEndLine = value; }
        }
        #endregion line

        #region name
        private string elementName;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }
        #endregion name

        public Collection<ElementClass> ElementClasses = new Collection<ElementClass>();

        public static ElementNamespace DefaultNamespace {
            get {
                ElementNamespace defaultElement = new ElementNamespace();
                defaultElement.ElementName = "DefaultNamespace";
                return defaultElement;
            }
        }
    }
}
