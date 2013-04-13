using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity 
{
    public class CodeFile
    {
        private string fileName;
        /// <summary>
        /// File name of the code element
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private List<ElementNamespace> namespaces = new List<ElementNamespace>();
        /// <summary>
        /// File path of the code element
        /// </summary>
        public List<ElementNamespace> Namespaces
        {
            get { return namespaces; }
            set { namespaces = value; }
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
    }
}
