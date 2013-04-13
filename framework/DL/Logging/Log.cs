using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DataModel.DL.CodeEntity;

namespace DataPersistency.DL.Logging
{
    #region STATIC

    public static class LoggingSystem
    {
        private static bool logAdministrativeTasks = false;
        public static bool LogAdministrativeTasks { get { return logAdministrativeTasks; } set { logAdministrativeTasks = value; } }
        private static bool traceCode = true;
        public static bool TraceCode { get { return traceCode; } set { traceCode = value; } }

        public static Log log = new Log();

        public static string LogMessage
        {
            get { return log.LogMessage; }
            set { log.LogMessage = value; }
        }

        public static int BranchNo
        {
            get { return log.BranchNo; }
            set { log.BranchNo = value; }
        }

        public static string BranchName
        {
            get { return log.BranchName; }
            set { log.BranchName = value; BranchNo++; }
        }

        public static List<string> LogTrace
        {
            get { return log.LogTrace; }
            set { log.LogTrace = value; }
        }

        public static ElementMethod LogMethod
        {
            get { return log.LogMethod; }
            set { log.LogMethod = value; }
        }
        public static event PropertyChangedEventHandler PropertyChanged;

    }

    #endregion STATIC

    #region NOTIFY
    public class    Log : INotifyPropertyChanged
    {

        #region PROPERTY

        private string logMessage = string.Empty;
        public string LogMessage
        {
            get { return logMessage; }
            set
            {
                logMessage = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogMessage");
            }
        }

        private List<string> logTrace = new List<string>();
        public List<string> LogTrace
        {
            get { return logTrace; }
            set
            {
                logTrace = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogTrace");
            }
        }

        private string branchName = string.Empty;
        public string BranchName
        {
            get { return branchName; }
            set
            {
                branchName = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("BranchName");
            }
        }

        private int branchNo = 0;
        public int BranchNo
        {
            get { return branchNo; }
            set
            {
                branchNo = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("BranchNo");
            }
        }

        private static ElementMethod logMethod = new ElementMethod();
        public ElementMethod LogMethod
        {
            get { return logMethod; }
            set
            {
                logMethod = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogMethod");
            }
        }
        
            
        #endregion PROPERTY

        #region EVENT

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion EVENT

    }
#endregion NOIFY
}
