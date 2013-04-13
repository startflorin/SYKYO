#region Composition
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using NaturalLanguageProcessor;
using WindowsFormsApplication1.DL;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.UI;
using WindowsFormsApplication1.Level_Objects_From_Numbers;
using WindowsFormsApplication1.Level_Operator_From_Numbers;
using WindowsFormsApplication1.CD;
using DataPersistency.DL.ServerAccess;
using DataPersistency.UI.Logging;
#endregion Composition
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Variables
        // Loging Model
        static DataPersistency.BL.UserOptions.LoggingSystemOptions logingOptionsModel = new DataPersistency.BL.UserOptions.LoggingSystemOptions();

        public static ServerAccessInterface serverAccess = new ServerAccessMySQL(null);
        SymbolCollection symbolCollection = new SymbolCollection(serverAccess, logingOptionsModel);
        OperatorCollection operatorCollection = new OperatorCollection(serverAccess);
        SQLView loggingWindow = new SQLView();
     //=//   WebView webWindow = new WebView();
        //ServerAccessInterface dataAccessinterface = new server
        //ServerAccess serverAccess = null;
        //FileAccess fileAccess = new FileAccess();
        //CodeAccess codeAccess = new CodeAccess();
        QueryEvaluator queryEvaluator = new QueryEvaluator(logingOptionsModel);

        List<string> files = new List<string>();
        //Logics logics = new Logics();
        bool replaceLastWord = false;
        int RealTimeEvaluation = 0;
        public static string CodeTextBoxText;
        #endregion variables

        DecodePhrase decodePhrase = new DecodePhrase();
        #region Construction

        /// <summary>
        /// Preset option variables
        /// </summary>
        private void InitializeSettings()
        {
            DataPersistency.DL.DynamicConfiguration.DatabaseOptionsController.SelectedDataProvider = "MySql";
            //DisplayTestConsole();
            //DisplayMSDNReader();
            modeToolStripMenuItem.SelectedText = "Evaluate";
            EvaluateButton.BackColor = Color.Green;
            RealTimeEvaluation = 1;
            TraceOptions traceOptions = new TraceOptions(logingOptionsModel);
        }

        private void DisplayMSDNReader()
        {
            MSDNReader mSDNReader =new MSDNReader();
            mSDNReader.Show();
        }

        /// <summary>
        /// Attach connection string
        /// </summary>
        /// <returns></returns>

        private void InitializeDatabaseSelector()
        {
            foreach (string database in ServerAccess.GoodBDs)
            {
                DatabaseSelection.Items.Add(database.Substring(9).ToUpper());
            }
            DatabaseSelection.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //InitializeDatabaseSelector();
            InitializeSettings();
            //files = fileAccess.GetFileList("");
            ReadFile textFile = new ReadFile("C:\\Users\\PROFIMEDICA\\Downloads\\ocx_sfv_030a.ctl", "C:\\Users\\PROFIMEDICA\\Downloads\\ocx_sfv_030a.csv");
        
            //serverAccess.CreateSymbol("rrrrrrr");
        }

        #endregion Construction

        #region Logics

        /// <summary>
        /// Start Evaluation
        /// </summary>
        public void EvaluateInput()
        {
            bool trueRelation = true;
            string HumanReadable = "";
            string result = queryEvaluator.Evaluate(QueryTextBox.Text.Substring(0, QueryTextBox.Text.Length-2).Trim() , 2);
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Replace("[0:0]","").Trim()))
            {
                if (!QueryTextBox.Text.Contains("!"))
                    QueryTextBox.BackColor = Color.Orange;
                answaeTF.Text = "True\n";
                List<int[]> ints = decodePhrase.Decode(result);
                HumanReadable = queryEvaluator.Decode(result);
            }
            else
            {
                if (!QueryTextBox.Text.Contains("!"))
                    QueryTextBox.BackColor = Color.Magenta;
                answaeTF.Text = "False\n";
            }
            answaeTF.Text += result + "\n >>> " + HumanReadable + " <<< ";
        }

        /// <summary>
        /// Prepare input for evaluation
        /// </summary>
        public void PrepareInput()
        {
            bool evaluated = false;
            answaeTF.Text = ">> Answare <<";
            string substring = QueryTextBox.Text;
            if (substring.Length > 0)
            {
                if (substring[substring.Length - 1] == '?')
                {
                    substring = substring.Substring(0, substring.Length - 2).Trim();
                    EvaluateInput();
                    evaluated = true;
                }
                if (substring[substring.Length - 1] == '!')
                {
                    substring = substring.Substring(0, substring.Length - 2).Trim();
                    bool RelationCreateUnknown = OperatorCollection.serverAccess.AcceptRelations;
                    bool SymbolCreateUnknown = SymbolCollection.serverAccess.AcceptSymbols;
                    //QueryTextBox.BackColor = Color.Red;
                    OperatorCollection.serverAccess.AcceptRelations = true;
                    SymbolCollection.serverAccess.AcceptSymbols = true;
                    EvaluateInput();
                    evaluated = true;
                    OperatorCollection.serverAccess.AcceptRelations = RelationCreateUnknown;
                    SymbolCollection.serverAccess.AcceptSymbols = SymbolCreateUnknown;
                }
            }
            if (QueryTextBox.Text.LastIndexOf(' ') + 1 > 0)
            {
                substring = QueryTextBox.Text.Substring(QueryTextBox.Text.LastIndexOf(' ') + 1);
            }
            RegenerateSugestionList(substring, 1);
            if (RealTimeEvaluation > 0 && (!evaluated))
            {
                queryEvaluator.Evaluate(QueryTextBox.Text, 1);
                CodeTextBox.Text = CodeTextBoxText;
            }
        }

        /// <summary>
        /// KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (QueryTextBox.Text.Contains("!"))
            {
                QueryTextBox.BackColor = Color.Red;
            }
            if (QueryTextBox.Text.Contains("?"))
            {
                QueryTextBox.BackColor = Color.Blue;
            }
            else {
                if (QueryTextBox.BackColor != Color.White)
                QueryTextBox.BackColor = Color.White; 
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (SugestionListView.Items.Count > 0)
                {
                    AutocompleteInput(SugestionListView.Items[0].Text);
                    e.Handled = true;
                }
            }
            else
            {
                replaceLastWord = false;
            }
            PrepareInput();
        }

        #endregion Logics

        #region Helpers

        /// <summary>
        /// Regenerate suggestion list
        /// </summary>
        /// <param name="substring"></param>
        /// <param name="method"></param>
        private void RegenerateSugestionList(string substring, int method)
        {
            SugestionListView.Items.Clear();
            List<SymbolID> listSymbols = new List<SymbolID>();
            listSymbols = new List<SymbolID>();//=// symbolCollection.GetSymbolCollectionByString(substring, method, 30);
            for (int i = 0; i < listSymbols.Count; i++)
            {
                //=// SugestionListView.Items.Add(listSymbols[i].Names);
                SugestionListView.Items[i].Tag = listSymbols[i];
            }
        }

        /// <summary>
        /// Inject selected item into the expresion and set as actual symbol
        /// </summary>
        /// <param name="selection"></param>
        private void AutocompleteInput(string selection)
        {
            if (SugestionListView.SelectedItems.Count > 0)
            {
                //Logics.ActualSymbol = (Symbol)SugestionListView.SelectedItems[0].Tag; // Set actual Symbol for all windows
            }
        
            int lastSpace = 0;
            if (replaceLastWord)
            {
                lastSpace = QueryTextBox.Text.TrimEnd().LastIndexOf(' ');
            }
            else
            {
                lastSpace = QueryTextBox.Text.LastIndexOf(' ');
            }
            replaceLastWord = true;
            QueryTextBox.Text = QueryTextBox.Text.Substring(0, lastSpace + 1) + selection + " ";
            QueryTextBox.SelectionStart = QueryTextBox.Text.Length;
        }

        #endregion Helpers

        #region Events

        private void symbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestSymbol testSymbol = new TestSymbol();
            testSymbol.Show();
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestServer testServer = new TestServer();
            testServer.Show();
        }

        private void relationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestRelation testRelation = new TestRelation();
            testRelation.Show();
        }

        #region TEMP

        /// <summary>
        /// Jump into evaluation ( no automatic evaluation !? )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            EvaluateInput();
        }

        /// <summary>
        /// Open SQL View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sQLViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggingWindow.Show();
        }

        /// <summary>
        /// Open web window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wEBViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //=//webWindow.Show();
        }

        /// <summary>
        /// Set if evaluate on demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox2.SelectedItem.ToString())
            {
                case "Evaluation on Demand":
                    RealTimeEvaluation = 0;
                    break;
                case "Real Time Evaluation":
                    RealTimeEvaluation = 1;
                    break;
            }
        }

        /// <summary>
        /// Open window for direct code input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCode codeView = new MyCode();
            codeView.Show();
        }

        /// <summary>
        /// Lunch web extractor for MSDN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nSDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //URLIncrementalReader incrementalReader = new URLIncrementalReader(true);
        }
        #endregion TEMP

        private void SugestionListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SugestionListView.SelectedItems.Count > 0)
            {
                AutocompleteInput(SugestionListView.SelectedItems[0].Text);
            }
            else
            {
                AutocompleteInput(SugestionListView.FocusedItem.Text);
            }
        }
 
        #region Configuration
        /// <summary>
        /// Switch to advanced window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyCode codeView = new MyCode();
            codeView.Show();
            //SQLView sqlView = new SQLView();
            //sqlView.Show();
            WebView webView = new WebView("System");
            webView.Show();
            WatchParameters watchParameters = new WatchParameters();
            watchParameters.Show();
        }

        /// <summary>
        /// Set default intention intention ?! trough menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modeToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            EvaluateButton.Text = modeToolStripMenuItem.SelectedItem.ToString();
            RelationCollection.ProcessMode = EvaluateButton.Text;
            switch (EvaluateButton.Text)
            {
                case "Evaluate":
                    EvaluateButton.BackColor = Color.Green;
                    break;
                case "Adopt":
                    EvaluateButton.BackColor = Color.Orange;
                    break;
                case "Force Adopt":
                    EvaluateButton.BackColor = Color.Red;
                    break;
            }
        }

        /// <summary>
        /// Set default intention intention ?! trough button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int i = modeToolStripMenuItem.SelectedIndex;
            if (i < modeToolStripMenuItem.Items.Count - 1)
            {
                modeToolStripMenuItem.SelectedIndex++;
            }
            else
            {
                modeToolStripMenuItem.SelectedIndex = 0;
            }
            button2.Text = (modeToolStripMenuItem.SelectedItem.ToString().Substring(0, 1));
        }

        /// <summary>
        /// Set the level of new knowledge acceptance trough menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Only Existent": // Use only recognized symbols and relations
                    //Symbol.CreateUnknown = false;
                    //Relation.CreateUnknown = false;
                    //QueryTextBox.BackColor = Color.White;
                    break;
                case "Symbols Only": // Assimilate unknown symbols
                    //Symbol.CreateUnknown = true;
                    //Relation.CreateUnknown = false;
                    //QueryTextBox.BackColor = Color.Yellow;
                    break;
                case "Both Symbols and Relations": // Assimilate unknown symbols and relations
                    //Symbol.CreateUnknown = true;
                    //Relation.CreateUnknown = true;
                    //QueryTextBox.BackColor = Color.Red;
                    break;
                case "Relations Only": // Assimilate unknown relations
                    //Symbol.CreateUnknown = false;
                    //Relation.CreateUnknown = true;
                    //QueryTextBox.BackColor = Color.Blue;
                    break;
            }
        }

        /// <summary>
        /// Set the level of new knowledge acceptance trough ui button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            int i = toolStripComboBox1.SelectedIndex;
            if (i < toolStripComboBox1.Items.Count - 1)
            {
                toolStripComboBox1.SelectedIndex++;
            }
            else
            {
                toolStripComboBox1.SelectedIndex = 0;
            }
            button1.Text = (toolStripComboBox1.SelectedItem.ToString().Substring(0, 1));
        }

        /// <summary>
        /// Set actual database for all windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
           // ServerAccess.DatabaseName = DatabaseName.Text.Trim();
        }
        #endregion Configuration

        private void DatabaseSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ServerAccess.DatabaseName = ServerAccess.GoodBDs[DatabaseSelection.SelectedIndex];
        }

        #endregion Events

        #region TEST

        void DisplayTestConsole()
        {
            TestServer testServer = new TestServer();
            testServer.Show();
            TestRelation testRelation = new TestRelation();
            testRelation.Show();
            TestSymbol testSymbols = new TestSymbol();
            testSymbols.Show();
        }

        #endregion TEST

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReadFile textFile = new ReadFile("C:\\Users\\PROFIMEDICA\\Downloads\\ocx_sfv_030a.ctl", "C:\\Users\\PROFIMEDICA\\Downloads\\ocx_sfv_030a.csv");
        }

        private void databaseOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseOptions databaseOptions = new DatabaseOptions(symbolCollection);
            databaseOptions.Show();
        }

        private void traceOprtionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TraceOptions traceOptions = new TraceOptions(logingOptionsModel);
            traceOptions.Show();
        }

        private void frameworkManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPersistency.Form1 frameworkSettings = new DataPersistency.Form1();
            frameworkSettings.Show();
        }

        private void naturalLanguageProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NaturalLanguageProcessor.Form1 frameworkSettings = new NaturalLanguageProcessor.Form1();
            //frameworkSettings.Show();
        }

        private void readCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void createGraphVizDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramCreator.Form1 diagramCreator = new DiagramCreator.Form1();
            diagramCreator.Show();
        }

        private void naturalWriterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NaturalLanguageProcessor.Form1 naturalWriter = new NaturalLanguageProcessor.Form1();
            naturalWriter.Show();
        }

        private void dataExlorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExplorer.Form1 dataExplorer = new DataExplorer.Form1();
            dataExplorer.Show();
        }

        private void codeProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeProcessor.Form1 codeProcessor = new CodeProcessor.Form1();
            codeProcessor.Show();
            DiagramCreator.Form1 diagramCreator = new DiagramCreator.Form1();
            diagramCreator.Show();
        }
    }
}
