using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataPersistency.DL.CommenAccess.ObjectsFromNumbers;
using DataPersistency.DL.ServerAccess;

namespace DataPersistency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel = new DataPersistency.BL.UserOptions.LoggingSystemOptions();
        DataPersistency.UI.Logging.SQLView loggingWindow = new DataPersistency.UI.Logging.SQLView();
        static string connectionString = DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.UpdateConnectionString("MySQL");
        public static DataPersistency.DL.ServerAccess.ServerAccessInterface serverAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL(connectionString);


        private void databaseOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataPersistency.UI.UserOptions.DatabaseOptions databaseOptions = new DataPersistency.UI.UserOptions.DatabaseOptions();
            databaseOptions.Focus();
            databaseOptions.Show();
        }

        private void traceOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPersistency.UI.UserOptions.TraceOptions traceOptions = new DataPersistency.UI.UserOptions.TraceOptions(new DataPersistency.BL.UserOptions.LoggingSystemOptions());
            traceOptions.Focus();
            traceOptions.Show();
        }

        private void sQLViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPersistency.UI.Logging.SQLView sqlView = new DataPersistency.UI.Logging.SQLView();
            sqlView.Focus();
            sqlView.Show();
        }
    }
}
