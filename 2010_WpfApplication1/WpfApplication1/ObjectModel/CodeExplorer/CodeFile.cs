﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
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

        private List<CodeElementNamespace> namespaces = new List<CodeElementNamespace>();
        /// <summary>
        /// File path of the code element
        /// </summary>
        public List<CodeElementNamespace> Namespaces
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
