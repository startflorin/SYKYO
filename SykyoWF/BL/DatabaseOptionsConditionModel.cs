#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
#endregion USING

namespace WindowsFormsApplication1.BL
{
    class DatabaseOptionsConditionModel : INotifyPropertyChanged
    {
        #region Oracle
        private bool oracle;
        public bool Oracle
        {
            get
            {
                return oracle;
            }
            set
            {
                if (value != this.oracle)
                {
                    oracle = value;
                    NotifyPropertyChanged("Oracle");
                }
            }
        }
        #endregion Oracle

        #region MySql
        private bool mySql;
        public bool MySql
        {
            get
            {
                return mySql;
            }
            set
            {
                if (value != this.mySql)
                {
                    mySql = value;
                    NotifyPropertyChanged("MySql");
                }
            }
        }
        #endregion MySql

        #region SqlServer
        private bool sqlServer;
        public bool SqlServer
        {
            get
            {
                return sqlServer;
            }
            set
            {
                if (value != this.sqlServer)
                {
                    sqlServer = value;
                    NotifyPropertyChanged("SqlServer");
                }
            }
        }
        #endregion SqlServer

        #region Postgree
        private bool postgree;
        public bool Postgree
        {
            get
            {
                return postgree;
            }
            set
            {
                if (value != this.Postgree)
                {
                    postgree = value;
                    NotifyPropertyChanged("Postgree");
                }
            }
        }
        #endregion Postgree

        #region SqLite
        private bool sqLite;
        public bool SqLite
        {
            get
            {
                return sqLite;
            }
            set
            {
                if (value != this.SqLite)
                {
                    sqLite = value;
                    NotifyPropertyChanged("SqLite");
                }
            }
        }
        #endregion SqLite

        #region HELPER
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = info;
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion HELPER

    }
}
