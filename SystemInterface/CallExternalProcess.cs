using System.Diagnostics;
using System.IO;

namespace SystemInterface
{
    /// <summary>
    /// External process (system calls)
    /// </summary>
    public class ExternalProcess
    {
        // External Process
        private readonly Process process = new Process();
        private readonly string OutputFilePath;

        /// <summary>
        /// Prepare external program call
        /// </summary>
        /// <param name="filePath">path to executable</param>
        /// <param name="processName">executable name</param>
        /// <param name="arguments">command line arguments</param>
        public ExternalProcess(string filePath, string processName, string arguments)
        {
            process.StartInfo.FileName = filePath + processName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            //process.StartInfo.RedirectStandardOutput = true;
        }

        /// <summary>
        /// Start this external program
        /// </summary>
        /// <returns>execution output</returns>
        public string Execute(string outputFile)
        {
            process.Start();
            process.WaitForExit();
            string error = process.StandardError.ReadToEnd();
            string output = "";
            //string output = process.StandardOutput.ReadToEnd();
            /*
            Stream OutputStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
            System.IO.BinaryWriter file = new System.IO.BinaryWriter(OutputStream);
            file.Write(process.StandardOutput.ReadToEnd());
            file.Close();
             * */
            return output;
        }
    }
}
