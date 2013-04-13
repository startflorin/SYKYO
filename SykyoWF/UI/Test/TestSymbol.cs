using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataPersistency.DL.ServerAccess;
using WindowsFormsApplication1.Level_Objects_From_Numbers;

namespace WindowsFormsApplication1.UI
{
    public partial class TestSymbol : Form
    {
        SymbolCollection SymbolCollection = new SymbolCollection();
        List<SymbolID> ResultedParonymes = new List<SymbolID>();
        public TestSymbol()
        {
            InitializeComponent();
        }

        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            ResultedParonymes.Clear();
            if (!string.IsNullOrEmpty(NameTextBox.Text.Trim()))
            {
                ResultedParonymes = SymbolCollection.GetSymbolCollection(NameTextBox.Text.Trim().ToLower());
                ParonymesListBox.Items.Clear();
                foreach (SymbolID s in ResultedParonymes)
                {
                    ParonymesListBox.Items.Add(s.ToString());
                }
            }
        }

        private void ParonymesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SymbolID> ResultedSynonimes = SymbolCollection.GetSynonimesCollection(ResultedParonymes[ParonymesListBox.SelectedIndex]);
            SynonimesTextBox.Text = "";
            foreach (SymbolID s in ResultedSynonimes)
            {
                foreach (string name in s.Names)
                {
                    SynonimesTextBox.Text += name + System.Environment.NewLine;
                }
            }
        }

        private void AcceptNewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SymbolCollection.serverAccess.AcceptSymbols = AcceptNewCheckBox.Checked;
        }
    }
}
