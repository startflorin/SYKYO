using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeProcessor
{
    public class FileAccess
    {
        static FileInfo fileInfo;
        static StreamWriter streamWriter;
        static StreamReader streamReader;
        

        string[] wantedExtensions = { ".cs" };
        string rootDirectory = "C:\\Users\\PROFIMEDICA\\documents\\visual studio 2010\\WindowsFormsApplication1";
        //C:\Users\PROFIMEDICA\Documents\Visual Studio 2010\WindowsFormsApplication1\CodeProcessor\CodeReader.cs
        List<string> files = new List<string>();

        /// <summary>
        /// Get the list of files of a root directory in a recursive manner
        /// </summary>
        /// <param name="rootFolder">root folder</param>
        public List<string> GetFilePathList(string rootFolder)
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                rootFolder = rootDirectory;
            }
            try
            {
                foreach (string d in Directory.GetDirectories(rootFolder))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (wantedExtensions.Contains(Path.GetExtension(f)))
                            this.files.Add(f);
                    }
                    GetFilePathList(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return this.files;
        }

        public List<FileInfo> GetFileList(List<string> paths)
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach (string filePath in paths)
            {
                if (!filePath.Contains("Designer"))
                {
                    files.Add(new FileInfo(filePath));
                }
            }
            return files;
        }

        public StringBuilder GetFileContent(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StreamReader sr = new StreamReader(filePath))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    stringBuilder.AppendLine(line);
                }
            }
            return stringBuilder;
        }
        
    }
}
