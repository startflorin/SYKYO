using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPersistency.DL.ServerAccess;

namespace DataPersistency.DL.DynamicConfiguration
{
    public class DatabaseOptionsController
    {
        // transfere provider properties
        private static bool inheritedAcceptSymbols = false;
        static DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel;

        private static string selectedDataProvider = string.Empty;
        public static string SelectedDataProvider
        {
            get
            {
                return selectedDataProvider;
            }
            set
            {
                if (value != selectedDataProvider)
                {
                    try
                    {
                        SetDataProvider(value);
                        selectedDataProvider = value;
                    }
                    catch(Exception d)
                    {
                    }
                }
            }
        }

        public static DataPersistency.DL.ServerAccess.ServerAccessInterface serverAccess;// = new ServerAccessOracle();
        public static void SetDataProvider(string provider)
        {
            switch(provider)
            {
                case "Oracle":
                    if (serverAccess != null) 
                    {
                        inheritedAcceptSymbols = serverAccess.AcceptSymbols;
                    }
                    // serverAccess = new ServerAccessOracle(logingOptionsModel);
                    serverAccess.AcceptSymbols = inheritedAcceptSymbols;
                    break;
                case "MySQL":
                    if (serverAccess != null) 
                    {
                        inheritedAcceptSymbols = serverAccess.AcceptSymbols;
                    }
                    string connectionString = UpdateConnectionString(provider);
                    serverAccess = new ServerAccessMySQL(connectionString);
                    serverAccess.AcceptSymbols = inheritedAcceptSymbols;
                    break;
                case "SqlServer":
                    if (serverAccess != null) 
                    {
                        inheritedAcceptSymbols = serverAccess.AcceptSymbols;
                    }
                    // serverAccess = new ServerAccessSqlServer();
                    serverAccess.AcceptSymbols = inheritedAcceptSymbols;
                    break;
                case "Postgree":
                    if (serverAccess != null) 
                    {
                        inheritedAcceptSymbols = serverAccess.AcceptSymbols;
                    }
                    // serverAccess = new ServerAccessPostgree();
                    serverAccess.AcceptSymbols = inheritedAcceptSymbols;
                    break;
                case "SqLite":
                    if (serverAccess != null) 
                    {
                        inheritedAcceptSymbols = serverAccess.AcceptSymbols;
                    }
                    // serverAccess = new ServerAccessSqLite();
                    serverAccess.AcceptSymbols = inheritedAcceptSymbols;
                    break;

            }
        }

        public static void SetConnectionString(string provider)
        {
            switch(provider)
            {
                case "Oracle":
                    if (serverAccess != null)
                    {
                        //serverAccess.Get
                    }
                    break;
            }
        }
       // "Server=localhost;Port=3306;Database=" + DBc + ";Uid=sykyo_test;Pwd=start1;"



        internal static string UpdateConnectionString(string ServerAccessInstanceType)
        {
            if (string.IsNullOrWhiteSpace(ServerAccessInstanceType))
            {
                ServerAccessInstanceType = serverAccess.GetProviderName();
            }
            string connectionString = string.Empty;
            switch (ServerAccessInstanceType)
            {
                case "MySQL":
                    connectionString = "Server=" + ConnectionString.ConnectionStringServer + ";Port=" + ConnectionString.ConnectionStringPort + ";Database=" + ConnectionString.ConnectionStringDatabase + ";Uid=" + ConnectionString.ConnectionStringUsername + ";Pwd=" + ConnectionString.ConnectionStringPassword + ";";
                    break;
            }
            return connectionString;
        }
    }

    static class ConnectionString
    {
        public static string ConnectionStringServer = "localhost";
        public static string ConnectionStringPort = "3306";
        public static string ConnectionStringDatabase = "rpro0001_cabinet";
        public static string ConnectionStringUsername = "sykyo_test";
        public static string ConnectionStringPassword = "start1";
    }

}
