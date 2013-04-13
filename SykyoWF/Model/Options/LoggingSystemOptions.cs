﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WindowsFormsApplication1.DataAccess.Options
{
    public class LoggingSystemOptions : INotifyPropertyChanged
    {
        #region PROPERTIES

        #region NUMBERS

        private static bool logNumbersNone = false;
        public bool LogNumbersNone
        {
            get { return logNumbersNone; }
            set
            {
                if (value)
                {
                    logNumbersNone = value;
                    logNumbersResults = !value;
                    logNumbersParameters = !value;
                    logNumbersCode = !value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("LogNumbersNone");
                }
            }
        }


        private static bool logNumbersResults = false;
        public bool LogNumbersResults
        {
            get { return logNumbersResults; }
            set
            {
                if (value)
                {
                    logNumbersResults = value;
                    logNumbersNone = !value;
                    logNumbersParameters = !value;
                    logNumbersCode = !value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("LogNumbersResults");
                }            
            }

        }


        private static bool logNumbersParameters = false;
        public bool LogNumbersParameters
        {
            get { return logNumbersParameters; }
            set
            {
                if (value)
                {
                    logNumbersParameters = value;
                    logNumbersNone = !value;
                    logNumbersResults = !value;
                    logNumbersCode = !value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("LogNumbersParameters");
                }
            }
        }


        private static bool logNumbersCode = true;
        public bool LogNumbersCode
        {
            get { return logNumbersCode; }
            set
            {
                if (value)
                {
                    logNumbersCode = value;
                    logNumbersNone = !value;
                    logNumbersResults = !value;
                    logNumbersParameters = !value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("LogNumbersCode");
                }
            }
        }

        #endregion PROPERTY

        #region OBJECTS

        private static bool logObjectsNone = false;
        public bool LogObjectsNone
        {
            get { return logObjectsNone; }
            set
            {
                logObjectsNone = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogObjectsNone");
            }
        }


        private static bool logObjectsResults = false;
        public bool LogObjectsResults
        {
            get { return logObjectsResults; }
            set
            {
                logObjectsResults = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogObjectsResults");
            }
        }


        private static bool logObjectsParameters = false;
        public bool LogObjectsParameters
        {
            get { return logObjectsParameters; }
            set
            {
                logObjectsParameters = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogObjectsParameters");
            }
        }


        private static bool logObjectsCode = false;
        public bool LogObjectsCode
        {
            get { return logObjectsCode; }
            set
            {
                logObjectsCode = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogObjectsCode");
            }
        }

        #endregion OBJECTS

        #region RELATIONS

        private static bool logRelationsNone = false;
        public bool LogRelationsNone
        {
            get { return logRelationsNone; }
            set
            {
                logRelationsNone = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogRelationsNone");
            }
        }


        private static bool logRelationsResults = false;
        public bool LogRelationsResults
        {
            get { return logRelationsResults; }
            set
            {
                logRelationsResults = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogRelationsResults");
            }
        }


        private static bool logRelationsParameters = false;
        public bool LogRelationsParameters
        {
            get { return logRelationsParameters; }
            set
            {
                logRelationsParameters = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogRelationsParameters");
            }
        }


        private static bool logRelationsCode = false;
        public bool LogRelationsCode
        {
            get { return logRelationsCode; }
            set
            {
                logRelationsCode = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogRelationsCode");
            }
        }

        #endregion RELATIONS

        #region LOGICS

        private static bool logLogicsNone = false;
        public bool LogLogicsNone
        {
            get { return logLogicsNone; }
            set
            {
                logLogicsNone = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogLogicsNone");
            }
        }


        private static bool logLogicsResults = false;
        public bool LogLogicsResults
        {
            get { return logLogicsResults; }
            set
            {
                logLogicsResults = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogLogicsResults");
            }
        }


        private static bool logLogicsParameters = false;
        public bool LogLogicsParameters
        {
            get { return logLogicsParameters; }
            set
            {
                logLogicsParameters = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogLogicsParameters");
            }
        }


        private static bool logLogicsCode = false;
        public bool LogLogicsCode
        {
            get { return logLogicsCode; }
            set
            {
                logLogicsCode = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("LogLogicsCode");
            }
        }

        #endregion LOGICS

        #endregion PROPERTIES

        #region LEVELS

        public int levelNumbers 
        {
            get { return logNumbersNone ? 0 : logNumbersResults ? 1 : logNumbersParameters ? 2 : logNumbersCode ? 3 : -1; }
        }

        public int levelObjects
        {
            get { return logObjectsNone ? 0 : logObjectsResults ? 1 : logObjectsParameters ? 2 : logObjectsCode ? 3 : -1; }
        }

        public int levelRelations
        {
            get { return logRelationsNone ? 0 : logRelationsResults ? 1 : logRelationsParameters ? 2 : logRelationsCode ? 3 : -1; }
        }

        public int levelLogics
        {
            get { return logLogicsNone ? 0 : logLogicsResults ? 1 : logLogicsParameters ? 2 : logLogicsCode ? 3 : -1; }
        }

        #endregion LEVELS

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
}

