using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataPersistency.UI.UserOptions
{
    public partial class TraceOptions : Form
    {
        //Last connection result
        private bool successful;
        private enum Providers { Oracle, MySQL, SqlServer, Postgree, SqLite };
        private enum Levels { undefined=-1, None=0, Result=1, Parameters=2, Code=3 };
        DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel;
        DataPersistency.BL.UserOptions.LoggingSystemOptions lastLogingOptionsModel;

        public TraceOptions(DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModelReferince)
        {
            this.lastLogingOptionsModel = this.logingOptionsModel = logingOptionsModelReferince;
            InitializeComponent();
            ShowLastModel();
            bindingSourceOptionsTrace.ResetBindings(true);

            bindingSourceOptionsTrace.DataSource = logingOptionsModel;
            logingOptionsModelReferince.PropertyChanged += new PropertyChangedEventHandler(logingOptionsModelReferince_PropertyChanged);
        }

        private string GetOptionName(int level)
        {
            string optionName;
            switch (level)
            {
                case 0:
                    optionName = "None";
                    break;
                case 1:
                    optionName = "Result";
                    break;
                case 2:
                    optionName = "Parameters";
                    break;
                case 3:
                    optionName = "Code";
                    break;
                default:
                    optionName = "undefined";
                    break;
            }
                    return optionName;
        }

        private void ShowLastModel()
        {
            StringBuilder lastConfiguration = new StringBuilder();
            lastConfiguration.Append("Log Numbers from SQL:\n" + GetOptionName(lastLogingOptionsModel.levelNumbers));
            lastConfiguration.Append("\n\nLog Objects from Numbers:\n" + GetOptionName(lastLogingOptionsModel.levelObjects));
            lastConfiguration.Append("\n\nLog Relations from Objects:\n" + GetOptionName(lastLogingOptionsModel.levelRelations));
            lastConfiguration.Append("\n\nLog Logics from Relations:\n" + GetOptionName(lastLogingOptionsModel.levelLogics));
            HistoryTextBox.Text = lastConfiguration.ToString();
        }

        void logingOptionsModelReferince_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Save the trace options
        /// </summary>
        /// <param name="sender">Save button</param>
        /// <param name="e">Save event</param>
        private void SaveToDefaultsTextButton_Click(object sender, EventArgs e)
        {
            DataPersistency.DL.FileAccess.TextFileAccess.SaveModel(DataPersistency.DL.FileAccess.UserFile.LogingSystemOptions, logingOptionsModel);
            lastLogingOptionsModel = logingOptionsModel;
            ShowLastModel();
        }

        /// <summary>
        /// Reset the trace options to the last saved trace options
        /// </summary>
        /// <param name="sender">Reset button</param>
        /// <param name="e">Reset event</param>
        private void ResetFromDefaultsTextButton_Click(object sender, EventArgs e)
        {
            DataPersistency.DL.FileAccess.TextFileAccess.RestoreModel(DataPersistency.DL.FileAccess.UserFile.LogingSystemOptions, ref logingOptionsModel);
        }

    }
}
