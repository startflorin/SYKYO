using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataPersistency.UI.UserOptions
{
    public partial class ConnectionStringEditor : Form
    {
        public ConnectionStringEditor()
        {
            InitializeComponent();
            ServerTextBox.Text = DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringServer;
            PortTextBox.Text = DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringPort;
            UsernameTextBox.Text = DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringUsername;
            PasswordTextBox.Text = DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringPassword;
            DatabaseTextBox.Text = DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringDatabase;
        }


        private void button1_Click(object  sender, EventArgs e)
        {
            DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringServer = ServerTextBox.Text;
            DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringPort = PortTextBox.Text;
            DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringUsername = UsernameTextBox.Text;
            DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringPassword = PasswordTextBox.Text;
            DataPersistency.DL.DynamicConfiguration.ConnectionString.ConnectionStringDatabase = DatabaseTextBox.Text;
            DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.UpdateConnectionString(null);
            if (DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.serverAccess.TryToOpenConnection())
            {
                this.Close();
            }
        }
    }
}
