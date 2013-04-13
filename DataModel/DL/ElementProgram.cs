using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DataModel.DL.CodeEntity
{
    public class ElementProgram
    {
        
        /// <summary>
        /// Files of the program
        /// </summary>
        public List<CodeFile> CodeFiles
        {
            get { return codeFiles; }
            set { codeFiles = value; }
        }
        private List<CodeFile> codeFiles = new List<CodeFile>();

        /// <summary>
        /// Namespaces in the program
        /// </summary>
        public List<ElementNamespace> CodeNamespaces
        {
            get { return codeNamespaces; }
            set { codeNamespaces = value; }
        }
        private List<ElementNamespace> codeNamespaces = new List<ElementNamespace>();


        public string programName;
        /// <summary>
        /// Nane of the program
        /// </summary>
        public string ProgramName
        {
            get { return programName; }
            set { programName = value; }
        }

    }
}
