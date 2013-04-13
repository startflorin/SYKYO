using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataPersistency.DL.FileAccess
{
    public enum UserFile{ LogingSystemOptions, DatabaseConnectionOptions, LogingSystemTrace  }
    class TextFileAccess
    {
        static FileInfo fileInfo;
        static StreamWriter streamWriter;
        static StreamReader streamReader;
        public static void SaveModel(UserFile userFile, object model)
        {
            DataPersistency.BL.UserOptions.UserOptions userOptions = new DataPersistency.BL.UserOptions.UserOptions();
            if (fileInfo == null)
            {
                fileInfo = userOptions.GetFileInfo(userFile);
            }
            if (streamWriter == null)
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                    streamReader = null;
                }
                streamWriter = fileInfo.CreateText();
                streamWriter.AutoFlush = true;
            }

            DataPersistency.BL.UserOptions.LoggingSystemOptions mdl = (DataPersistency.BL.UserOptions.LoggingSystemOptions)model;

            streamWriter.WriteLine("LogNumbersNone" + "\t" + mdl.LogNumbersNone.GetType() + "\t" + mdl.LogNumbersNone);
            streamWriter.WriteLine("LogNumbersResults" + "\t" + mdl.LogNumbersResults.GetType() + "\t" + mdl.LogNumbersResults);
            streamWriter.WriteLine("LogNumbersParameters" + "\t" + mdl.LogNumbersParameters.GetType() + "\t" + mdl.LogNumbersParameters);
            streamWriter.WriteLine("LogNumbersCode" + "\t" + mdl.LogNumbersCode.GetType() + "\t" + mdl.LogNumbersCode);

            streamWriter.WriteLine("LogObjectsNone" + "\t" + mdl.LogObjectsNone.GetType() + "\t" + mdl.LogObjectsNone);
            streamWriter.WriteLine("LogObjectsResults" + "\t" + mdl.LogObjectsResults.GetType() + "\t" + mdl.LogObjectsResults);
            streamWriter.WriteLine("LogObjectsParameters" + "\t" + mdl.LogObjectsParameters.GetType() + "\t" + mdl.LogObjectsParameters);
            streamWriter.WriteLine("LogObjectsCode" + "\t" + mdl.LogObjectsCode.GetType() + "\t" + mdl.LogObjectsCode);

            streamWriter.WriteLine("LogRelationsNone" + "\t" + mdl.LogRelationsNone.GetType() + "\t" + mdl.LogRelationsNone);
            streamWriter.WriteLine("LogRelationsResults" + "\t" + mdl.LogRelationsResults.GetType() + "\t" + mdl.LogRelationsResults);
            streamWriter.WriteLine("LogRelationsParameters" + "\t" + mdl.LogRelationsParameters.GetType() + "\t" + mdl.LogRelationsParameters);
            streamWriter.WriteLine("LogRelationsCode" + "\t" + mdl.LogRelationsCode.GetType() + "\t" + mdl.LogRelationsCode);

            streamWriter.WriteLine("LogLogicsNone" + "\t" + mdl.LogLogicsNone.GetType() + "\t" + mdl.LogLogicsNone);
            streamWriter.WriteLine("LogLogicsResults" + "\t" + mdl.LogLogicsResults.GetType() + "\t" + mdl.LogLogicsResults);
            streamWriter.WriteLine("LogLogicsParameters" + "\t" + mdl.LogLogicsParameters.GetType() + "\t" + mdl.LogLogicsParameters);
            streamWriter.WriteLine("LogLogicsCode" + "\t" + mdl.LogLogicsCode.GetType() + "\t" + mdl.LogLogicsCode);
            streamWriter.Close();
            streamWriter = null;
        }
        public static void RestoreModel(UserFile userFile, ref DataPersistency.BL.UserOptions.LoggingSystemOptions model)
        {
            DataPersistency.BL.UserOptions.UserOptions userOptions = new DataPersistency.BL.UserOptions.UserOptions();
            if (fileInfo == null)
            {
                fileInfo = userOptions.GetFileInfo(userFile);
            }
            if (streamWriter == null)
            {
                streamWriter = fileInfo.AppendText();
                streamWriter.AutoFlush = true;
            }
            DataPersistency.BL.UserOptions.LoggingSystemOptions mdl = (DataPersistency.BL.UserOptions.LoggingSystemOptions)model;
            List<string[]> filtredLines = new List<string[]>();
            if (streamWriter != null)
            {
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = null;
            }
            streamReader = fileInfo.OpenText();
            string line = string.Empty;
            while ((line = streamReader.ReadLine()) != null)
            {
                filtredLines.Add(line.Split('\t'));
            }
            streamReader.Close();
            streamReader.Dispose();
            streamReader = null;

            mdl.LogNumbersNone = filtredLines[0][2].Equals("True") ? true : false;
            mdl.LogNumbersResults = filtredLines[1][2].Equals("True") ? true : false;
            mdl.LogNumbersParameters = filtredLines[2][2].Equals("True") ? true : false;
            mdl.LogNumbersCode = filtredLines[3][2].Equals("True") ? true : false;

            mdl.LogObjectsNone = filtredLines[4][2].Equals("True") ? true : false;
            mdl.LogObjectsResults = filtredLines[5][2].Equals("True") ? true : false;
            mdl.LogObjectsParameters = filtredLines[6][2].Equals("True") ? true : false;
            mdl.LogObjectsCode = filtredLines[7][2].Equals("True") ? true : false;

            mdl.LogRelationsNone = filtredLines[8][2].Equals("True") ? true : false;
            mdl.LogRelationsResults = filtredLines[9][2].Equals("True") ? true : false;
            mdl.LogRelationsParameters = filtredLines[10][2].Equals("True") ? true : false;
            mdl.LogRelationsCode = filtredLines[11][2].Equals("True") ? true : false;

            mdl.LogLogicsNone = filtredLines[12][2].Equals("True") ? true : false;
            mdl.LogLogicsResults = filtredLines[13][2].Equals("True") ? true : false;
            mdl.LogLogicsParameters = filtredLines[14][2].Equals("True") ? true : false;
            mdl.LogLogicsCode = filtredLines[15][2].Equals("True") ? true : false;

        }
    }
}
