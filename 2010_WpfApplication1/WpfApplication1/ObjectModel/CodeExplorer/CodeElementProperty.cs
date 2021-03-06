﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
{
    public class CodeElementProperty
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

        private string propertyType;
        /// <summary>
        /// Start line number of the code element
        /// </summary>
        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        private bool isMandatory;
        /// <summary>
        /// If need Validation
        /// </summary>
        public bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
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
        public string ToString()
        {
            string result = string.Empty;
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
            if (this.isStatic)
            {
                result += "static ";
            }
            result += this.elementName;

            result += " (";
            
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
            result += "     {"+Environment.NewLine;
            result += "         get { return " + ToLow(this.ElementName) + "; }" + Environment.NewLine;
            result += "         set { " + ToLow(this.ElementName) + " = value; }" + Environment.NewLine;
            result += "     }" + Environment.NewLine;
            return result;
        }

        private string ToUp(string identifier)
        {
            return identifier[0].ToString().ToUpper() + identifier.Substring(1);
        }

        private string ToLow(string identifier)
        {
            return identifier[0].ToString().ToLower() + identifier.Substring(1);
        }
    }
}
