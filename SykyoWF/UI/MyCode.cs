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
using WindowsFormsApplication1.Level_Operator_From_Numbers;

namespace WindowsFormsApplication1
{
    public partial class MyCode : Form
    {
        RelationCollection logics = new RelationCollection();
        public MyCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] code = myCodeTextBox.Text.Split(';');
            foreach (string instruction in code)
            {
                string[] args = instruction.Split(' ');
                string[] IDA = args[0].Substring(1, args[0].Length-2).Split(':');
                string[] IDR = args[1].Substring(1, args[1].Length-2).Split(':');
                string[] IDB = args[2].Substring(1, args[2].Length-2).Split(':');

                int[] symAID = new int[2];
                int[] relID = new int[4];
                int[] symBID = new int[2];

                int.TryParse(IDA[0], out symAID[0]);
                int.TryParse(IDA[1], out symAID[1]);
                int.TryParse(IDR[0], out relID[0]);
                int.TryParse(IDR[1], out relID[1]);
                int.TryParse(IDR[2], out relID[2]);
                int.TryParse(IDR[3], out relID[3]);
                int.TryParse(IDB[0], out symBID[0]);
                int.TryParse(IDB[1], out symBID[1]);

                ////SymbolItem symA = new SymbolItem(symAID, new int[]{}, "");
                ////OperatorItem relation = new OperatorItem(relID, "");
                ////SymbolItem symB = new SymbolItem(symBID, new int[] { }, "");

                //relation.CreateRelationIfNotExists(symA.Location, symB.Location);
            }
        }
    }
}
