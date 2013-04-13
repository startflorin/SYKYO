using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
{
    public class CodeElementNamespace
    {
        private Collection<FileInfo> elementFile = new Collection<FileInfo>();
        /// <summary>
        /// File path of the code element
        /// </summary>
        public Collection<FileInfo> ElementFile
        {
            get { return elementFile; }
            set { elementFile = value; }
        }

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

        private string elementName;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }

        public Collection<CodeElementClass> ElementClasses = new Collection<CodeElementClass>();

        public static CodeElementNamespace DefaultNamespace
        {
            get
            {
                CodeElementNamespace defaultElement = new CodeElementNamespace();
                defaultElement.ElementName = "DefaultNamespace";
                return defaultElement;
            }
        }
    }
}
