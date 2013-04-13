using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity 
{
    public class ElementClass : CodeElement
    {



        private string extend;
        /// <summary>
        /// File path of the code element
        /// </summary>
        public string Extend
        {
            get { return extend; }
            set { extend = value; }
        }

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


        private ElementNamespace elementNamespace;
        /// <summary>
        /// The namespace of this class
        /// </summary>
        public ElementNamespace ElementNamespace
        {
            get { return elementNamespace; }
            set { elementNamespace = value; }
        }

        private ElementClass parent;
        /// <summary>
        /// The static flag for the code element
        /// </summary>
        public ElementClass Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Collection<ElementMethod> ElementMethods = new Collection<ElementMethod>();
        
        public static ElementClass DefaultClass
        {
            get
            {
                ElementClass defaultElement = new ElementClass();
                defaultElement.Name = "DefaultClass";
                return defaultElement;
            }
        }
    
    }
}
