using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataPersistency.DL.ServerAccess;

namespace NaturalLanguageProcessor.Test
{
    public partial class NaturalFromInternal : Form
    {
        DataPersistency.DL.ServerAccess.ServerAccessInterface serverAccess = new DataPersistency.DL.ServerAccess.ServerAccessMySQL("Server=localhost;Port=3306;Database=rpro0001_cabinet;Uid=sykyo_test;Pwd=start1;");
                
        public NaturalFromInternal()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            EvaluateExpression(richTextBox1.Text);
        }

        private void EvaluateExpression(string expression)
        {
            string[] sentences = expression.Split(';');
            StringBuilder sb = new StringBuilder();
            foreach (string sentence in sentences)
            {
                string[] simbolStrings = sentence.Split(' ');
                List<SymbolID> symbols = new List<SymbolID>();
                foreach (string simbolString in simbolStrings)
                {
                    if (simbolString.StartsWith("[") && simbolString.EndsWith("]") && simbolString.Length > 2)
                    {
                        string[] components = simbolString.Substring(1, simbolString.Length-2).Split(':');
                        SymbolID symbolID = new SymbolID(new Location(1));
                        if (components.Length > 0)
                        {
                            int.TryParse(components[0], out symbolID.Location.A);
                        }
                        symbolID.Names = serverAccess.GetSymbolNamesByID(symbolID);
                        symbols.Add(symbolID);
                    }
                }
                foreach (SymbolID symbolID in symbols)
                {
                    if (symbolID.Names.Count > 0)
                    {
                        sb.Append(symbolID.Names[0]);
                    }
                    else
                    {
                        sb.Append(symbolID.ToString());
                    }
                }
            }
            richTextBox2.Text = sb.ToString();
        }
    }
}
