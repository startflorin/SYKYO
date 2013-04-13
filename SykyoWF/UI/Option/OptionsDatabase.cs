using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataPersistency.UI.Logging;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.Level_Objects_From_Numbers;

namespace WindowsFormsApplication1.UI
{
    public partial class DatabaseOptions : Form
    {
        //Last connection result
        private bool successful;
        SymbolCollection symbolCollection;
        private enum Providers { Oracle, MySQL, SqlServer, Postgree, SqLite };

        public DatabaseOptions(SymbolCollection cymbolCollection)
        {
            this.symbolCollection = symbolCollection;
            InitializeComponent();
            if (string.IsNullOrEmpty(SymbolCollection.serverAccess.GetProviderName()))
            {
                string provider = SymbolCollection.serverAccess.GetProviderName();
                switch (provider)
                {
                    case "Oracle":
                        OracleRadioButton.Checked = true;
                        break;
                }
            }
        }

        //Log connections events
        private void Log(string currentEvent, bool historicalEvent, object focuse)
        {
            string separator = System.Environment.NewLine;
            if (historicalEvent)
            {
                HistoryTextBox.Text += "===================" + separator;
                SQLView.LogResult(currentEvent);
                HistoryTextBox.Text += separator;
            }
            HistoryTextBox.Focus();
            HistoryTextBox.Text += currentEvent + separator;
            HistoryTextBox.Select(HistoryTextBox.Text.Length-1, 0);
            if (focuse is RadioButton)
            {
                ((RadioButton)focuse).Focus();
            }
        }

        //Switch connection to Oracle
        private void OracleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OracleRadioButton.Checked)
            {
                Log("Try to connect to " + Providers.Oracle.ToString(), false, null);
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = Providers.Oracle.ToString();
                if (SymbolCollection.serverAccess.GetProviderName().Equals(Providers.Oracle.ToString()))
                {
                    Log("Successfuly connected to " + SymbolCollection.serverAccess.GetProviderName(), false, null);
                }
                else
                {
                    Log("Faild to connect to " + Providers.Oracle.ToString(), false, null);
                }
                Log("Curent connection is:  " + SymbolCollection.serverAccess.GetProviderName()+" (" + SymbolCollection.serverAccess.getConnectionState() + ")", true, OracleRadioButton);
            }
        }

        //Switch connection to MySQL
        private void MySqlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MySqlRadioButton.Checked)
            {
                Log("Try to connect to " + Providers.MySQL.ToString(), false, null);
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = Providers.MySQL.ToString();
                if (SymbolCollection.serverAccess.GetProviderName().Equals(Providers.MySQL.ToString()))
                {
                    Log("Successfuly connected to " + SymbolCollection.serverAccess.GetProviderName(), false, null);
                }
                else
                {
                    string o = SymbolCollection.serverAccess.GetProviderName();
                    Log("Faild to connect to " + Providers.MySQL.ToString(), false, null);
                }
                Log("Curent connection is:  " + SymbolCollection.serverAccess.GetProviderName() + " (" + SymbolCollection.serverAccess.getConnectionState() + ")", true, MySqlRadioButton);
            }
        }

        //Switch connection to SqlServer
        private void SqlServerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SqlServerRadioButton.Checked)
            {
                Log("Try to connect to " + Providers.SqlServer.ToString(), false, null);
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = Providers.SqlServer.ToString();
                if (SymbolCollection.serverAccess.GetProviderName().Equals(Providers.SqlServer.ToString()))
                {
                    Log("Successfuly connected to " + SymbolCollection.serverAccess.GetProviderName(), false, null);
                }
                else
                {
                    Log("Faild to connect to " + Providers.SqlServer.ToString(), false, null);
                }
                Log("Curent connection is:  " + SymbolCollection.serverAccess.GetProviderName() + " (" + SymbolCollection.serverAccess.getConnectionState() + ")", true, SqlServerRadioButton);
            }
        }

        //Switch connection to Postgree
        private void PostgreeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PostgreeRadioButton.Checked)
            {
                Log("Try to connect to " + Providers.Postgree.ToString(), false, null);
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = Providers.Postgree.ToString();
                if (SymbolCollection.serverAccess.GetProviderName().Equals(Providers.Postgree.ToString()))
                {
                    Log("Successfuly connected to " + SymbolCollection.serverAccess.GetProviderName(), false, null);
                }
                else
                {
                    Log("Faild to connect to " + Providers.Postgree.ToString(), false, null);
                }
                Log("Curent connection is:  " + SymbolCollection.serverAccess.GetProviderName() + " (" + SymbolCollection.serverAccess.getConnectionState() + ")", true, PostgreeRadioButton);
            }
        }

        //Switch connection to SqLite
        private void SqLiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SqLiteRadioButton.Checked)
            {
                Log("Try to connect to " + Providers.SqLite.ToString(), false, null);
                DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = Providers.SqLite.ToString();
                if (SymbolCollection.serverAccess.GetProviderName().Equals(Providers.SqLite.ToString()))
                {
                    Log("Successfuly connected to " + SymbolCollection.serverAccess.GetProviderName(), false, null);
                }
                else
                {
                    Log("Faild to connect to " + Providers.SqLite.ToString(), false, null);
                }
                Log("Curent connection is:  " + SymbolCollection.serverAccess.GetProviderName() + " (" + SymbolCollection.serverAccess.getConnectionState() + ")", true, SqLiteRadioButton);
            }
        }
    }
}
