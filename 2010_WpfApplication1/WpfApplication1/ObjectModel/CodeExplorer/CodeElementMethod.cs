using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
{
    public class CodeElementMethod
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

        private List<string[]> parameters = new List<string[]>();
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public List<string[]> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private string elementNamespaceName = string.Empty;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string ElementNamespaceName
        {
            get { return elementNamespaceName; }
            set { elementNamespaceName = value; }
        }

        private string result;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        private string elementClassName = string.Empty;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string ElementClassName
        {
            get { return elementClassName; }
            set { elementClassName = value; }
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

        private string elementName = string.Empty;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }

        private CodeElementClass elementClass;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public CodeElementClass ElementClass
        {
            get { return elementClass; }
            set { elementClass = value; }
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

        public string ToBlock()
        {
            string result = string.Empty;
            result += "     public ";
            if (this.isStatic)
            {
                result += "static ";
            }
            result += "bool can"+this.ElementName + "F = true;"+Environment.NewLine;
            

            result += "     public ";
            if (this.isStatic)
            {
                result += "static ";
            }
            result += "void ";
            result += this.ElementName + "F()" + Environment.NewLine;
            result += "     {" + Environment.NewLine;
            result += "         if (!can"+this.ElementName + "F) return;" + Environment.NewLine;
            result += "     }" + Environment.NewLine;
            


            return result;
        }

        public string ToString()
        {
            string result = string.Empty;
            if (this.isStatic)
            {
                result += "static ";
            }
            switch (this.elementAccessType)
            {
                case DataAccessType.isPrivate:
                    result += "private ";
                    break;
                case DataAccessType.isProtected:
                    result += "protected ";
                    break;
                case DataAccessType.isPublic:
                    result += "public ";
                    break;
            }
            result += this.elementName;

            result += " (";
            foreach (CodeElementParameter parameter in this.ElementParameters)
            {
                result += ", " + parameter.ElementName;
            }
            result += ")";
            return result;
        }
        public Collection<CodeElementParameter> ElementParameters = new Collection<CodeElementParameter>();
        public Collection<string> CalledMethods = new Collection<string>();

        public static CodeElementMethod DefaultMethod
        {
            get
            {
                CodeElementMethod defaultElement = new CodeElementMethod();
                defaultElement.ElementName = "DefaultMethod";
                return defaultElement;
            }
        }
    }
}
