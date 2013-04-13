using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DataPersistency.DL.Logging;
using DataPersistency.DL.ServerAccess;
using DataModel.DL.CodeEntity;

namespace DataPersistency.UI.Logging
{
    public partial class SQLView : Form
    {
        #region VARIABLES

        // the logging system regerence
        //LoggingSystem.LoggingSystem. = new LoggingSystem.LoggingSystem();
        static string uri = "file:///C:/Users/PROFIMEDICA/Documents/Visual%20Studio%202010/WindowsFormsApplication1/Framework/Files/Logs/FrameworkLog.html";
        static string fileName = "../../../Framework/Files/Logs/FrameworkLog.html";
        static TextWriter tw = new StreamWriter(fileName);
        
        #endregion VARIABLES

        #region CONSTRUCT

        static System.Windows.Forms.WebBrowser SQLViewTextBox0 = new System.Windows.Forms.WebBrowser();
        public SQLView()
        {
            // Initialize default layout
            InitializeComponent();
            // initialize custom layout
            InitializeCustomLayout();
            LoggingSystem.log.PropertyChanged += new PropertyChangedEventHandler(Log_PropertyChanged);
            //LoggingSystem.LoggingSystem. += new PropertyChangedEventHandler(Log_PropertyChanged);
            tw.WriteLine("<style>");
            tw.WriteLine(".critical {width:100%; text-align:center; color:blue; background-color: yellow;} ");
            tw.WriteLine(".CodeElementNamespace{color:silver} ");
            tw.WriteLine(".CodeElementClass{color:red} ");
            tw.WriteLine(".CodeElementMethod{color:yellow}");
            tw.WriteLine(".CodeElementParameter{color:orange}");
            tw.WriteLine(".CodeElementValue{color:green}");
            tw.WriteLine(".value{color:magenta}");
            tw.WriteLine(".punctuation{color:darkBlue}");
            tw.WriteLine("</style><span class='method'>Text</span>");
            tw.Flush();

        }

        void Log_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BranchNo")
            {
                string branchName = DataPersistency.DL.Logging.LoggingSystem.BranchName;
                int lastIndexOfpoint = branchName.LastIndexOf(".");
                if (lastIndexOfpoint > -1)
                {
                    branchName = branchName.Substring(lastIndexOfpoint);
                }
                else
                {
                }
                tw.WriteLine("<div onclick=\"element = document.getElementById('branch" + DataPersistency.DL.Logging.LoggingSystem.BranchNo + "') ; element.style.display = element.style.display=='block' ? 'none' : 'block'\" style='border: 1 solid green; padding: 5'>BranchNo: " + DataPersistency.DL.Logging.LoggingSystem.BranchNo + ") " + branchName + "</div>");
                tw.WriteLine("<div id='branch" + DataPersistency.DL.Logging.LoggingSystem.BranchNo + "' style='display:none; border: 1 solid red; padding: 5'>");
                return;
            }

            //tw.WriteLine("======================" + "<br>" + Environment.NewLine);

            if (e.PropertyName == "LogMethod")
            {
                tw.WriteLine(ColorizeMethod(DataPersistency.DL.Logging.LoggingSystem.LogMethod));
                tw.Flush();
                //=// SQLViewTextBox0.Url = new Uri(uri); 
                return;
            }
            
            foreach (string mesage in LoggingSystem.LogTrace)
            {
                //tw.WriteLine(mesage + "<br>" + Environment.NewLine);
                tw.Flush();
                SQLViewTextBox0.Update();
            }
            tw.WriteLine(LoggingSystem.LogMessage + "<br>" + Environment.NewLine);
                tw.Flush();
                SQLViewTextBox0.Update();
                SQLViewTextBox.Update();
             //=//   SQLViewTextBox0.Url = new Uri(uri);

             //   SQLViewTextBox0.DocumentText += SQLViewTextBox0.DocumentText.Substring(6, SQLViewTextBox0.DocumentText.Length - 6) + LoggingSystem.LoggingSystem.LogMessage + Environment.NewLine;
                //tw.WriteLine(SQLViewTextBox0.DocumentText + SQLViewTextBox0.DocumentText.Substring(6, SQLViewTextBox0.DocumentText.Length - 6) + LoggingSystem.LoggingSystem.LogMessage + Environment.NewLine);
            
        }

        private string ColorizeMethod(ElementMethod method)
        {
            StringBuilder result = new StringBuilder();
            
            //if(!string.IsNullOrEmpty(method.Result)) result.Append("======================" + "<br>" + Environment.NewLine);
            if (method.ElementClassName.Length>2) result.Append("<span class='CodeElementNamespace'>" + method.ElementNamespaceName + "</span>&gt; <span class='CodeElementClass'>" + method.ElementClassName + "</span> <span class='CodeElementMethod'>" + method.ElementName + "</span><br>" + Environment.NewLine);
            string parameters = string.Empty;
            if (method.Parameters != null)
            foreach (string[] parameter in method.Parameters)
            {
                 parameters += ", <span class='CodeElementParameter'>" + parameter[0] + "</span> = " + "<span class='CodeElementValue'>" + parameter[1] + "</span>";
            }
            if (parameters.Length > 2) { result.Append("<span class='specialChaer'>(" + parameters.Substring(2) + ")" + "<br>"); }
            string res = method.Result;
            string colorRes = method.Result;
            bool beforePunctuation = false;
            if (!string.IsNullOrEmpty(res))
            for(int i = 0; i<res.Length; i++)
            {
                char c = res[i];
                    
                if (!char.IsLetterOrDigit(res[i]))
                {
                    if (!beforePunctuation)
                    {
                        res = res.Substring(0, i) + "<span class='punctuation'>" + res.Substring(i); i = i + 26;
                    }
                    beforePunctuation = true;
                }
                else 
                {
                    if (beforePunctuation)
                    {
                        res = res.Substring(0, i) + "</span>" + res.Substring(i); i = i + 7;
                    }
                    beforePunctuation = false;
                }
            }
            res += "</span>";
            if (!string.IsNullOrEmpty(res))
            {
                result.Append("<span class='value'>" + res + "</span><br>" + Environment.NewLine);
            }
            result.Append("</div>"); // FOLDING SyStEm DIV

            return result.ToString();
        }

        #endregion CONSTRUCT

        #region HELPERS

        #region LAYOUT
        /// <summary>
        /// Ad the main text box as static variable so that it is visible from outsde
        /// </summary>
        private void InitializeCustomLayout()
        {

            SQLViewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
| System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
            SQLViewTextBox0.Location = new System.Drawing.Point(12, 12);
            //SQLViewTextBox0.Multiline = true;
            SQLViewTextBox0.Name = "SQLViewTextBox";
            //SQLViewTextBox0.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            SQLViewTextBox0.Size = new System.Drawing.Size(792, 209);
            SQLViewTextBox0.TabIndex = 0;
            SQLViewTextBox0.Url = new Uri(uri);
            Controls.Remove(this.SQLViewTextBox);
            Controls.Add(SQLViewTextBox0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion LAYOUT

        #region ARRAY TO STRING

        /// <summary>
        /// Convert array of string into string
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string parseResult(string[] result)
        {
            string parsedResult = "";
            foreach (string s in result)
            {
                parsedResult += " " + s;
            }
            return parsedResult;
        }

        /// <summary>
        /// Convert array og integers into string
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string parseResult(int[] result)
        {
            string parsedResult = "";
            foreach (int s in result)
            {
                parsedResult += " " + s;
            }
            return parsedResult;
        }

        #endregion ARRAY TO STRING

        #endregion HELPERS

        #region EVENTS
        private void button1_Click(object sender, EventArgs e)
        {
            SQLViewTextBox0.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (true)//BL.ServerAccessInterface.Equals(null);)
            {
                if (ServerAccessMySQL.MyResultsTrace)
                {
                    ServerAccessMySQL.MySqlTrace = ServerAccessMySQL.MyResultsTrace = !ServerAccessMySQL.MyResultsTrace;
                    button2.Text = "no-SQL";
                }
                else
                {
                    ServerAccessMySQL.MyResultsTrace = !ServerAccessMySQL.MyResultsTrace;
                    button2.Text = "SQL+Results";
                }
            }
            else
            {
                ServerAccessMySQL.MySqlTrace = true;
                button2.Text = "SQL";
            }

            if (ServerAccessOracle.OracleTrace)
            {
                if (ServerAccessOracle.MyResultsTrace)
                {
                    ServerAccessOracle.OracleTrace = ServerAccessOracle.MyResultsTrace = !ServerAccessOracle.MyResultsTrace;
                    button2.Text = "no-SQL";
                }
                else
                {
                    ServerAccessOracle.MyResultsTrace = !ServerAccessOracle.MyResultsTrace;
                    button2.Text = "SQL+Results";
                }
            }
            else
            {
                ServerAccessOracle.OracleTrace = true;
                button2.Text = "SQL";
            }
        }

        #endregion EVENTS
        public static void Log(string SqlCommand)
        {
            SQLViewTextBox0.DocumentText += System.Environment.NewLine + SqlCommand;
        }

        public static void LogResult(int[] result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + parseResult(result);
        }

        public static void LogHumanResult(string result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + result;
        }

        public static void LogResult(string[] result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + parseResult(result);
        }

        public static void LogResult(string ResultLog)
        {
            Log(ResultLog);
        }

        public static void LogResult(string currentEvent, int level)
        {
            string eventClass = string.Empty;
            switch (level)
            {
                case 0:
                    eventClass = "critical";
                    break;
            }
            if (string.IsNullOrWhiteSpace(eventClass))
            {
                SQLViewTextBox0.DocumentText += "<br>" + currentEvent;
            }
            else
            {
                SQLViewTextBox0.DocumentText += "<br>" + "<span class='" + eventClass + "'>" + currentEvent + "</span>";
            }
        }
    }
}
