using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataPersistency.DL.CommenAccess.ObjectsFromNumbers;
using DataPersistency.DL.DynamicConfiguration;
using DataPersistency.UI.Logging;

namespace DataPersistency.UI.UserOptions
{
    public partial class DatabaseOptions : Form
    {
        #region VARIABLES
        /// <summary>
        /// Supported database providers enumeration
        /// </summary>
        private enum Provider { Oracle, MySQL, SqlServer, Postgree, SqLite };
        #endregion VARIABLES

        #region CONSTRUCT

        /// <summary>
        /// Create database options view
        /// </summary>
        public DatabaseOptions()
        {
            InitializeComponent();
            CustomInitialize();
        }

        /// <summary>
        /// Set UI to defaults
        /// </summary>
        private void CustomInitialize()
        {
            if (DatabaseOptionsController.serverAccess == null)
            {
                DatabaseOptionsController.SelectedDataProvider = Provider.MySQL.ToString();
            }

            string provider = DatabaseOptionsController.serverAccess.GetProviderName();
            switch (provider)
            {
                case "Oracle":
                    OracleRadioButton.Checked = true;
                    break;
                case "MySQL":
                    MySqlRadioButton.Checked = true;
                    break;
                case "SqlServer":
                    SqlServerRadioButton.Checked = true;
                    break;
                case "Postgree":
                    PostgreeRadioButton.Checked = true;
                    break;
                case "SqLite":
                    SqLiteRadioButton.Checked = true;
                    break;
            }
        }

        #endregion CONSTRUCT

        #region BUSSINESS

        /// <summary>
        /// Try to open current connection
        /// </summary>
        private void TryToOpenConnection()
        {
            if (DatabaseOptionsController.serverAccess. TryToOpenConnection())
            {
                DisplayMessage(">> " + DatabaseOptionsController.serverAccess.getConnectionState().ToUpperInvariant() + " <<");
                button1.Text = "Ready to Use !";
            }
            else
            {
                DisplayMessage(">> " + DatabaseOptionsController.serverAccess.getConnectionState().ToUpperInvariant() + " <<");
            }
            string userMessage = "\nCurent connection is:\n  " + DatabaseOptionsController.serverAccess.GetProviderName() + " (" + DatabaseOptionsController.serverAccess.getConnectionState() + ")";
            DisplayMessage(userMessage);
            SQLView.LogResult(userMessage, 0);
        }

        /// <summary>
        /// Connect to a given provider
        /// </summary>
        /// <param name="provider">name of the provider to connect to</param>
        private void ChangeDatabaseProvider(Provider provider)
        {
            DisplayMessage("Try to connect to " + provider.ToString());
            DatabaseOptionsController.SelectedDataProvider = provider.ToString();

            if (DatabaseOptionsController.serverAccess.GetProviderName().Equals(provider.ToString()))
            {
                DisplayMessage(">> " + DatabaseOptionsController.serverAccess.getConnectionState().ToUpperInvariant() + " <<");
            }
            else
            {
                DisplayMessage(">> FAIL <<");
            }
            string userMessage = "\nCurent connection is:\n  " + DatabaseOptionsController.serverAccess.GetProviderName() + " (" + DatabaseOptionsController.serverAccess.getConnectionState() + ")";
            DisplayMessage(userMessage);
            SQLView.LogResult(userMessage, 0);

            switch (provider)
            {

                case Provider.MySQL:
                    MySqlRadioButton.Focus();
                    break;
                case Provider.Oracle:
                    OracleRadioButton.Focus();
                    break;
                case Provider.Postgree:
                    PostgreeRadioButton.Focus();
                    break;
                case Provider.SqLite:
                    SqLiteRadioButton.Focus();
                    break;
                case Provider.SqlServer:
                    SqlServerRadioButton.Focus();
                    break;
            }
        }

        #endregion BUSSINESS

        #region LOG
        /// <summary>
        /// Display connections events. Historical significant events will be loggged to file.
        /// </summary>
        /// <param name="resultMessage">message to display</param>
        /// <param name="historicalEvent">if significant for connectivity history </param>
        private void DisplayMessage(string resultMessage)
        {
            string separator = System.Environment.NewLine;
            HistoryTextBox.Focus();
            HistoryTextBox.Text += resultMessage + separator;
            HistoryTextBox.Select(HistoryTextBox.Text.Length - 1, 0);
        }


        #endregion LOG

        #region EVENTS

        #region Try OPEN

        /// <summary>
        /// Try to OPEN connection
        /// </summary>
        /// <param name="sender">action button</param>
        /// <param name="e">default parameter</param>
        private void button1_Click(object sender, EventArgs e)
        {
            TryToOpenConnection();
        }

        #endregion Try OPEN

        #region Change PROVIDER

        //Switch connection to Oracle
        private void OracleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OracleRadioButton.Checked)
            {
                ChangeDatabaseProvider(Provider.Oracle);
            }
        }

        //Switch connection to MySQL
        private void MySqlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MySqlRadioButton.Checked)
            {
                ChangeDatabaseProvider(Provider.MySQL);
            }
        }

        //Switch connection to SqlServer
        private void SqlServerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SqlServerRadioButton.Checked)
            {
                ChangeDatabaseProvider(Provider.SqlServer);
            }
        }

        //Switch connection to Postgree
        private void PostgreeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PostgreeRadioButton.Checked)
            {
                ChangeDatabaseProvider(Provider.Postgree);
            }
        }

        //Switch connection to SqLite
        private void SqLiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SqLiteRadioButton.Checked)
            {
                ChangeDatabaseProvider(Provider.SqLite);
            }
        }

        #endregion Change PROVIDER

        #region Edit Connection
        private void button3_Click(object sender, EventArgs e)
        {
            DataPersistency.UI.UserOptions.ConnectionStringEditor connectionStringEditor = new DataPersistency.UI.UserOptions.ConnectionStringEditor();
            connectionStringEditor.Focus();
            connectionStringEditor.Show();
        }
        #endregion Edit Connection

        #region Switch/Create  DATABASE
        private void button2_Click(object sender, EventArgs e)
        {
            //createDatabase();
        }
        #endregion Switch/Create  DATABASE

        #endregion EVENTS
    }
}
