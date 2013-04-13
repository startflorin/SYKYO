using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneratedWF.Dynamics
{
    public partial class DinamicView : Form
    {
        SimulationBand dataSimulator = null;
        public DinamicView()
        {
            InitializeComponent();
            InitializeView();
        }

        private void InitializeView()
        {
            dataSimulator = new SimulationBand();

            dataSimulator.Location = new System.Drawing.Point(6, 3 * 5);
            dataSimulator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));

            this.Controls.Add(dataSimulator);

        }
    }
}
