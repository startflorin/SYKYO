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
using WindowsFormsApplication1.Level_Operator_From_Numbers;

namespace WindowsFormsApplication1.UI
{
    public partial class TestRelation : Form
    {
        int multiplicity = 1;
        OperatorItem currentRelation;
        OperatorCollection rels;
        SymbolCollection symbolCollection = new SymbolCollection();
        
        public TestRelation()
        {
            InitializeComponent();
            rels.GetAllOperators();
            /*foreach (OperatorCollection rel in rels)
            {
                OperatorComboBox.Items.Add(rel.ToString());
            }*/
            //currentRelation = rels[0];
            if (OperatorComboBox.Items.Count > 0)
            {
                OperatorComboBox.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SymbolID> preoperand = new List<SymbolID>(); 
            if (PreoperandTextBox.Text.Trim().Equals("*"))
            {
                preoperand.Add(SymbolID.Unknown);
            }
            else
            {
                string[] prestring = PreoperandTextBox.Text.Split(',');
                foreach (string s in prestring)
                {
                    List<SymbolID> partialPreoperand = new List<SymbolID>();
                    partialPreoperand = symbolCollection.GetSymbolCollection(s.Trim());
                    preoperand.AddRange(partialPreoperand);
                }
            }

            List<SymbolID> postoperand = new List<SymbolID>();
            if (PostoperandTextBox.Text.Trim().Equals("*"))
            {
                postoperand.Add(SymbolID.Unknown);
            }
            else
            {
                string[] poststring = PostoperandTextBox.Text.Split(',');
                foreach (string s in poststring)
                {
                    List<SymbolID> partialPostoperand = new List<SymbolID>();
                    partialPostoperand = symbolCollection.GetSymbolCollection(s.Trim());
                    postoperand.AddRange(partialPostoperand);
                }
            }

            List<SymbolID> result = new List<SymbolID>();
            if (radioButton1.Checked)
            {
                //result = relationCollection.InRelation(preoperand, new List<OperatorItem> { currentRelation }, postoperand);
            }
            if (radioButton2.Checked)
            {
                //result = relationCollection.SetRelation(preoperand, new List<OperatorItem> { currentRelation }, postoperand);
            }
            ResultRichTextBox.Text = "";
            foreach (SymbolID item in result)
            {
                    ResultRichTextBox.Text += item.ToString() + System.Environment.NewLine;
            }
        }

        private void OperatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //currentRelation = rels[OperatorComboBox.SelectedIndex];
            currentRelation.MultiplicityLevel = multiplicity;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int.TryParse(textBox1.Text.Trim(), out multiplicity);
                currentRelation.MultiplicityLevel = multiplicity;
            }
        }
    }
}
