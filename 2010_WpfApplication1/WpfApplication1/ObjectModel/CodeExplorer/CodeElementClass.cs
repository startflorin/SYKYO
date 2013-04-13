using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
{
    public class CodeElementClass
    {



        private FileInfo elementFile;
        /// <summary>
        /// File path of the code element
        /// </summary>
        public FileInfo ElementFile
        {
            get { return elementFile; }
            set { elementFile = value; }
        }

        private int elementStartLine;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public int ElementStartLine
        {
            get { return elementStartLine; }
            set { elementStartLine = value; }
        }

        private int elementEndLine;
        /// <summary>
        /// End line number of the code element
        /// </summary>
        public int ElementEndLine
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
        public enum DataAccessType { isPublic, isProtected, isPrivate }

        private DataAccessType elementAccessType;
        /// <summary>
        /// Access type of the code element
        /// </summary>
        public DataAccessType ElementAccessType
        {
            get { return elementAccessType; }
            set { elementAccessType = value; }
        }

        private bool isStatic;
        /// <summary>
        /// The static flag for the code element
        /// </summary>
        public bool IsStatic
        {
            get { return isStatic; }
            set { isStatic = value; }
        }


        private CodeElementNamespace elementNamespace;
        /// <summary>
        /// The namespace of this class
        /// </summary>
        public CodeElementNamespace ElementNamespace
        {
            get { return elementNamespace; }
            set { elementNamespace = value; }
        }

        private CodeElementClass parent;
        /// <summary>
        /// The static flag for the code element
        /// </summary>
        public CodeElementClass Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Collection<CodeElementProperty> ElementProperties = new Collection<CodeElementProperty>();
        
        public Collection<CodeElementMethod> ElementMethods = new Collection<CodeElementMethod>();

        public static CodeElementClass DefaultClass
        {
            get
            {
                CodeElementClass defaultElement = new CodeElementClass();
                defaultElement.ElementName = "DefaultClass";
                return defaultElement;
            }
        }

    }
}
