using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.DL;
using WindowsFormsApplication1.BL;
using System.Threading;

namespace WindowsFormsApplication1.UI
{
    public partial class WatchParameters : Form
    {
        bool continuousValidation;
        private static int[] actualID;
        public static int[] ActualID
        {
            get { return actualID; }
            set
            {
                actualID = value; 
                if (textBox1 != null)
                {
                    textBox1.Text = actualID[0].ToString();
                }
            }
        }

        //Symbol s = new Symbol(new int[] {0,0} );
        Thread oThread = null;
        private void fillParameters()
        {
        }
        public WatchParameters()
        {
            InitializeComponent();
            InitializeParameters();
            FillGrid();
            Refresh();
            Alpha oAlpha = new Alpha();
            oThread = new Thread(new ThreadStart(oAlpha.Refresh));

        }

        private void FillGrid()
        {
            for (int i = 0; i < Alpha.parameters.Count; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells[1].Value = "units";
                dataGridView1.Rows[i].Cells[2].Value = Alpha.parameters[i].MyName;
                dataGridView1.Rows[i].Cells[4].Value = Alpha.parameters[i].Location;
            }
        }

        private void Refresh()
        {
            for (int i = 0; i < Alpha.parameters.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Alpha.parameters[i].RefreshValue();
            }
        }

        private void InitializeParameters()
        {
            Parameter parameter1 = new Parameter(new int[] { 6670, 0 });
            Alpha.parameters.Add(parameter1);
        }
        public void AddParameter()
        {
            //DataRow dataRow;
            //dataRow.ItemArray = new string[] { "aa", "ss", "dd", "ff" };
            //dataGridView1.Rows.Add(dataRow);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            continuousValidation = !continuousValidation;
            if (!oThread.IsAlive)
            {
                oThread.Start();
            }
            //backgroundWorker1_DoWork(backgroundWorker1, new DoWorkEventArgs(backgroundWorker1));
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = Alpha.parameters.Count;
            Parameter p0 = Alpha.parameters.Where(p => p.ID[0] == ActualID[0] && p.ID[1] == ActualID[1]).FirstOrDefault();
            if (p0 == null) return;
            Alpha.parameters.Add(new Parameter(new int[] { ActualID[0], 0}));
            if (Alpha.parameters.Count != count)
            {
                dataGridView1.Rows.Add(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = Alpha.parameters.Count;
            Parameter p0 = Alpha.parameters.Where(p => p.ID[0] == ActualID[0] && p.ID[1] == ActualID[1]).FirstOrDefault();
            if (p0 == null) return;
            int i = Alpha.parameters.IndexOf(p0);
            Alpha.parameters.Remove(p0);
            if (Alpha.parameters.Count != count)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //Logics.ActualSymbol = new Symbol(new int[] { ActualID[0], 0 });
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (! oThread.IsAlive)
            {
                oThread.Start();
        }
            else
            {
                //oThread.Suspend();
            }
            //while (continuousValidation)
            {
                Refresh();
            }
        }
    }
}
