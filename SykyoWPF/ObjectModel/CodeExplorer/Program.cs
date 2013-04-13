using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1.ObjectModel.CodeExplorer
{
    public class Program
    {
        private List<CodeFile> codeFiles = new List<CodeFile>();
        /// <summary>
        /// Files of the program
        /// </summary>
        public List<CodeFile> CodeFiles
        {
            get { return codeFiles; }
            set { codeFiles = value; }
        }


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
