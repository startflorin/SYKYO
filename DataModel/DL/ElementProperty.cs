using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity
{
    public class ElementProperty : CodeElement
    {
        #region file
        private FileInfo elementFile;
        /// <summary>
        /// File path of the code element
        /// </summary>
        public FileInfo ElementFile
        {
            get { return elementFile; }
            set { elementFile = value; }
        }
        #endregion file

        #region type
        private string propertyType;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }
        #endregion type

        #region namdatory
        private bool isMandatory;
        /// <summary>
        /// If need Validation
        /// </summary>
        public bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }
        #endregion namdatory

        #region static
        private bool isStatic;
        /// <summary>
        /// The static flag for the code element
        /// </summary>
        public bool IsStatic
        {
            get { return isStatic; }
            set { isStatic = value; }
        }
        #endregion static

        #region namespace
        private string elementNamespaceName = string.Empty;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string ElementNamespaceName
        {
            get { return elementNamespaceName; }
            set { elementNamespaceName = value; }
        }
        #endregion namespace

        #region class
        private string elementClassName = string.Empty;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string ElementClassName
        {
            get { return elementClassName; }
            set { elementClassName = value; }
        }
        #endregion class

        #region result is=type?
        private string result;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        #endregion result

        #region line
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
        #endregion line

        #region name
        private string elementName = string.Empty;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }
        #endregion name

        #region class
        private ElementClass elementClass;
        /// <summary>
        /// Name of the code element
        /// </summary>
        public ElementClass ElementClass
        {
            get { return elementClass; }
            set { elementClass = value; }
        }
        #endregion class

        public enum DataAccessType { isPublic, isProtected, isPrivate }

        #region access
        private DataAccessType elementAccessType;
        /// <summary>
        /// Access type of the code element
        /// </summary>
        public DataAccessType ElementAccessType
        {
            get { return elementAccessType; }
            set { elementAccessType = value; }
        }
        #endregion access


        public Collection<ElementParameter> ElementParameters = new Collection<ElementParameter>();
        public Collection<string> CalledMethods = new Collection<string>();

        public static ElementMethod DefaultMethod
        {
            get
            {
                ElementMethod defaultElement = new ElementMethod();
                defaultElement.ElementName = "DefaultMethod";
                return defaultElement;
            }
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
            foreach (ElementParameter parameter in this.ElementParameters)
            {
                result += ", " + parameter.ElementName;
            }
            result += ")";
            return result;
        }

        public string ToBlock()
        {
            string result = string.Empty;
            result += "     private ";
            if (this.isStatic)
            {
                result += "static ";
            }
            if (this.PropertyType.StartsWith(" "))
            {
                string[] descr = this.PropertyType.Split(' ');
                result += descr + "[]";
            }
            else
            {
                result += this.PropertyType;
            }
            result += " " + ToLow(this.ElementName) + "";
            if (this.PropertyType.StartsWith("List<"))
            {
                result += " = new " + this.PropertyType + "()";
            }
            if (this.PropertyType.StartsWith(" "))
            {
                string[] descr = this.PropertyType.Split(' ');
                result += " = new " + descr[0] + "[" + descr[1] + "]";
            }
            result += ";" + Environment.NewLine;

            result += "     public " + this.PropertyType + " " + ToUp(this.ElementName) + Environment.NewLine;
            result += "     {" + Environment.NewLine;
            result += "         get { return " + ToLow(this.ElementName) + "; }" + Environment.NewLine;
            result += "         set { " + ToLow(this.ElementName) + " = value; }" + Environment.NewLine;
            result += "     }" + Environment.NewLine;
            return result;
        }

        private string ToUp(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return string.Empty;
            }
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return string.Empty;
            }
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }


    }
}
