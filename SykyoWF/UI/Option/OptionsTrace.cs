using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.Level_Objects_From_Numbers;

namespace WindowsFormsApplication1.UI
{
    public partial class TraceOptions : Form
    {
        //Last connection result
        private bool successful;
        SymbolCollection symbolCollection;
        private enum Providers { Oracle, MySQL, SqlServer, Postgree, SqLite };
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel;

        public TraceOptions(DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModelReferince)
        {
            this.logingOptionsModel = logingOptionsModelReferince;
            this.symbolCollection = symbolCollection;
            InitializeComponent();
            //bindingSourceOptionsTrace.ResetBindings(true);

            //logingOptionsModel.
            bindingSourceOptionsTrace.DataSource = logingOptionsModel;
            logingOptionsModelReferince.PropertyChanged += new PropertyChangedEventHandler(logingOptionsModelReferince_PropertyChanged);
        }

        void logingOptionsModelReferince_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void SaveToDefaultsTextButton_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.DataAccess.FileAccess.TextFileAccess.SaveModel(DataAccess.FileAccess.UserFile.LogingSystemOptions, logingOptionsModel);
        }

        private void ResetFromDefaultsTextButton_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.DataAccess.FileAccess.TextFileAccess.RestoreModel(DataAccess.FileAccess.UserFile.LogingSystemOptions, ref logingOptionsModel);
        }

    }
}
