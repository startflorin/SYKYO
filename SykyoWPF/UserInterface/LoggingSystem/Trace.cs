using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WpfApplication1.UserInterface.LoggingSystem
{
    public partial class Trace : Form
    {
        #region VARIABLES

        // the logging system regerence
        //LoggingSystem.LoggingSystem. = new LoggingSystem.LoggingSystem();
        static string uri = "file:///D:/Users/PROFIMEDICA/Documents/Visual%20Studio%202010/WindowsFormsApplication1/WindowsFormsApplication1/bin/Debug/Log.html";
        static string fileName = "Log.html";
        TextWriter tw = new StreamWriter(fileName);
        
        #endregion VARIABLES

        #region CONSTRUCT

        static System.Windows.Forms.WebBrowser SQLViewTextBox0 = new System.Windows.Forms.WebBrowser();
        public Trace()
        {
            // Initialize default layout
            InitializeComponent();
            // initialize custom layout
            InitializeCustomLayout();
            Controller.LoggingSystem.LoggingSystem.log.PropertyChanged += new PropertyChangedEventHandler(Log_PropertyChanged);
            //LoggingSystem.LoggingSystem.PropertyChanged += new PropertyChangedEventHandler(Log_PropertyChanged);
            tw.WriteLine("<style>");
            tw.WriteLine(".CodeElementNamespace{color:silver} ");
            tw.WriteLine(".CodeElementClass{color:red} ");
            tw.WriteLine(".CodeElementMethod{color:blue}");
            tw.WriteLine(".CodeElementParameter{color:orange}");
            tw.WriteLine(".CodeElementValue{color:green}");
            tw.WriteLine(".value{color:magenta}");
            tw.WriteLine(".punctuation{color:darkBlue}");
            tw.WriteLine("</style><span class='method'>Text</span>");
            tw.Flush();

        }

        void Log_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {


            //tw.WriteLine("======================" + "<br>" + Environment.NewLine);

            if (!string.IsNullOrEmpty(Controller.LoggingSystem.LoggingSystem.LogMethod.Result)) tw.WriteLine(ColorizeMethod(Controller.LoggingSystem.LoggingSystem.LogMethod));
            /*
            foreach (string mesage in LoggingSystem.LoggingSystem.LogTrace)
            {
                tw.WriteLine(mesage + "<br>" + Environment.NewLine);
                tw.Flush();
                SQLViewTextBox0.Update();
            }
            tw.WriteLine(LoggingSystem.LoggingSystem.LogMessage + "<br>" + Environment.NewLine);
                tw.Flush();
                SQLViewTextBox0.Update();
             //   SQLViewTextBox0.DocumentText += SQLViewTextBox0.DocumentText.Substring(6, SQLViewTextBox0.DocumentText.Length - 6) + LoggingSystem.LoggingSystem.LogMessage + Environment.NewLine;
                //tw.WriteLine(SQLViewTextBox0.DocumentText + SQLViewTextBox0.DocumentText.Substring(6, SQLViewTextBox0.DocumentText.Length - 6) + LoggingSystem.LoggingSystem.LogMessage + Environment.NewLine);
            */
        }

        private string ColorizeMethod(ObjectModel.CodeExplorer.CodeElementMethod method)
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
            if (!string.IsNullOrEmpty(res)) result.Append("<span class='value'>" + res + "</span><br>" + Environment.NewLine);
            return result.ToString();
        }

        #endregion CONSTRUCT

        #region HELPERS

        #region LAYOUT
        /// <summary>
        /// Ad the main text box as static variable so that it is visible from outsde
        /// </summary>
        private void InitializeCustomLayout()
        {/*

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
            //Controls.Remove(this.SQLViewTextBox);
            Controls.Add(SQLViewTextBox0);
            this.ResumeLayout(false);
            this.PerformLayout();*/
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
        {/*
            if (true)//BL.ServerAccessInterface.Equals(null);)
            {
                if (BL.ServerAccessMySQL.MyResultsTrace)
                {
                    BL.ServerAccessMySQL.MySqlTrace = BL.ServerAccessMySQL.MyResultsTrace = !BL.ServerAccessMySQL.MyResultsTrace;
                    button2.Text = "no-SQL";
                }
                else
                {
                    BL.ServerAccessMySQL.MyResultsTrace = !BL.ServerAccessMySQL.MyResultsTrace;
                    button2.Text = "SQL+Results";
                }
            }
            else
            {
                BL.ServerAccessMySQL.MySqlTrace = true;
                button2.Text = "SQL";
            }

            if (BL.ServerAccessOracle.OracleTrace)
            {
                if (BL.ServerAccessOracle.MyResultsTrace)
                {
                    BL.ServerAccessOracle.OracleTrace = BL.ServerAccessOracle.MyResultsTrace = !BL.ServerAccessOracle.MyResultsTrace;
                    button2.Text = "no-SQL";
                }
                else
                {
                    BL.ServerAccessOracle.MyResultsTrace = !BL.ServerAccessOracle.MyResultsTrace;
                    button2.Text = "SQL+Results";
                }
            }
            else
            {
                BL.ServerAccessOracle.OracleTrace = true;
                button2.Text = "SQL";
            }*/
        }

        #endregion EVENTS
        internal static void Log(string SqlCommand)
        {
            SQLViewTextBox0.DocumentText += System.Environment.NewLine + SqlCommand;
        }

        internal static void LogResult(int[] result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + parseResult(result);
        }

        internal static void LogHumanResult(string result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + result;
        }

        internal static void LogResult(string[] result)
        {
            SQLViewTextBox0.Text += System.Environment.NewLine + parseResult(result);
        }

        internal static void LogResult(string ResultLog)
        {
            Log(ResultLog);
        }
    }
}

