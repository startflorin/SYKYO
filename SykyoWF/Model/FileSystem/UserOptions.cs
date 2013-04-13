using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1.Model.FileSystem
{
    class UserOptions
    {
        private string logingSystemOptionsPath = ".\\Files\\Options\\LogingSystemOptions.txt";
        private FileInfo logingSystemOptions = null;
        public FileInfo LogingSystemOptionsFile
        {
            get
            {
                if (logingSystemOptions == null)
                {
                    logingSystemOptions = new FileInfo(logingSystemOptionsPath);
                    if (!logingSystemOptions.Exists)
                    {
                        File.Create(logingSystemOptionsPath);
                        logingSystemOptions = new FileInfo(logingSystemOptionsPath);
                    }
                }
                return logingSystemOptions;
            }
        }        
        
        private string logingSystemTracePath = ".\\Files\\Logs\\LogingSystemTrace.txt";
        private FileInfo logingSystemTrace = null;
        public FileInfo LogingSystemTraceFile
        {
            get
            {
                if (logingSystemTrace == null)
                {
                    logingSystemTrace = new FileInfo(logingSystemTracePath);
                    if (!logingSystemTrace.Exists)
                    {
                        File.Create(logingSystemTracePath);
                        logingSystemOptions = new FileInfo(logingSystemTracePath);
                    }
                }
                return logingSystemTrace;
            }
        }

        public FileInfo GetFileInfo(DataAccess.FileAccess.UserFile userFile)
        {
            switch (userFile)
            {
                case DataAccess.FileAccess.UserFile.LogingSystemOptions:
                    return LogingSystemOptionsFile;
                    break;
            }
            return LogingSystemOptionsFile;
        }
    }
}
