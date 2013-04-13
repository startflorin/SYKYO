using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeneratedWF
{
    public partial class SimulationBand : UserControl
    {
            //
            DataTable dtGraph = new DataTable ();
            int Cntr = 0;
            int freezedCntr = 0;
            double freezedRar = 0;
            int freezedInterval = 0;
            double Rar = 0;
        // If animation is ok
            bool animate = true;
            //

        DataTable dataTable = new DataTable("Source");
        public SimulationBand()
        {
            InitializeComponent();

            /*
            for (int i = 0; i < 100; i++)
			{
			  dataTable.Columns.Add(new DataColumn("Column"+i, typeof(decimal)));
			}
            for (int i = 0; i < 10; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int j = 0; j < 50; j++)
                {
                   dataRow[j] = j;
                }
                dataTable.Rows.Add(dataRow);
            }
             * */
            bindingSource1.DataSource = dataTable;
            this.chart1.DataSource = bindingSource1;
            /*
            chart1.Series["Series1"].XValueMember = Convert.ToString(dataTable.Columns[1]);
            chart1.Series["Series1"].YValueMembers = Convert.ToString(dataTable.Columns[2]);
            chart1.Series.Add("Series2");
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Series2"].XValueMember = Convert.ToString(dataTable.Columns[1]);
            chart1.Series["Series2"].YValueMembers = Convert.ToString(dataTable.Columns[3]);
            chart1.Series.Add("Series3");
            chart1.Series["Series3"].XValueMember = Convert.ToString(dataTable.Columns[1]);
            chart1.Series["Series3"].YValueMembers = Convert.ToString(dataTable.Columns[4]);
            */

            SimulationBand_Load();

            chart1.DataBind();
            chart1.Visible = true;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            int offset = (800 - trackBar1.Value);
            Evaluate(freezedCntr + offset);
            Sumulate(((TrackBar)sender).Value);
        }

        private void Sumulate(int p)
        {
            decimal offset = 20 - p;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (animate)
            {
                Evaluate(Cntr);
                freezedCntr = Cntr;
            }
            Cntr ++;//= (int)(Convert.ToDecimal(domainUpDown2.SelectedItem.ToString())+1000);
        }


        private void AddSegment(double time, double amplitude)
        {
            double remainingTime = AddSegment(valueOnX, valueOnY, valueOnX + time, amplitude);
            valueOnY = amplitude;
            valueOnX += remainingTime;
        }
        static double valueOnX = 0;
        static double valueOnY = 0;
        static int seq = 0;

        private double AddSegment(double fromX, double fromY, double toX, double toY)
        {
            double interval = 0;
            double height = 0;
            int segments = 0;
            double stepX = 0;
            double stepY = 0;
            double positionX = 0;
            double positionY = 0;
            // First Down
            interval = toX - fromX;
            height = toY - fromY;
            segments = (int)Math.Round(interval / 0.01);
            for (int z = 0; z <= segments; z++)
            {
                stepX = interval / segments;
                stepY = height / segments;
                positionX = fromX + z * stepX;
                positionY = fromY + z * stepY;
                seq++;
                if (seq > Cntr)
                {
                    dataTable.Rows.Add(positionX, positionY, 0);
                }
                else
                {
                    return 0;
                }
                
            }
            return toX - fromX;
        }

        void Evaluate(int Cntr)
        {
            if (Cntr < 0)
            {
                Cntr = freezedCntr;
            }
            dataTable.Clear();
            valueOnX = 0;
            valueOnY = 0;
            seq = 0;
            double x = 0;
            double toX = 0.042969;
            double toY = -0.314364;

            for (int t = 0; t < 20; t++)
            {
                /** */
                toX = 0.13906;
                toY = 1.031029;
                //AddSegment(toX, toY);
                toX = 0.042969;
                toX = 0.039063;
                toY = -0.314364;
                AddSegment(toX, toY);
                toX = 0.070312;
                toX = 0.027343;
                toY = -0.088692;
                AddSegment(toX, toY);
                toX = 0.109375;
                toX = 0.039063;
                toY = -0.001795;
                AddSegment(toX, toY);
                toX = 0.242188;
                toX = 0.132813;
                toY = 0.400901;
                AddSegment(toX, toY);
                toX = 0.445312;
                toX = 0.203124;
                toY = -0.044416;
                AddSegment(toX, toY);
                toX = 0.714844;
                toX = 0.269532;
                toY = 0.020444;
                AddSegment(toX, toY);
                toX = 0.816406;
                toX = 0.101562;
                toY = 0.300815;
                AddSegment(toX, toY);
                toX = 0.886719;
                toX = 0.070313;
                toY = 0.068501;
                AddSegment(toX, toY);
                toX = 0.914062;
                toX = 0.027343;
                toY = -0.028893;
                AddSegment(toX, toY);
                toX = 0.933594;
                toX = 0.046875;
                toY = -0.134135;
                AddSegment(toX, toY);
                toX = 0.933594;
                toX = 0.054688;
                toY = 0.958345;
                AddSegment(toX, toY);
            }

            for (int b = 0; b < 0; b++)
            {
                dataTable.Rows.Add(x + 0.003906, 1.031029, 0);
                dataTable.Rows.Add(x + 0.007812, 0.916540, 0);
                dataTable.Rows.Add(x + 0.011719, 0.748307, 0);
                dataTable.Rows.Add(x + 0.015625, 0.549199, 0);
                dataTable.Rows.Add(x + 0.019531, 0.342897, 0);
                dataTable.Rows.Add(x + 0.023438, 0.149521, 0);
                dataTable.Rows.Add(x + 0.027344, -0.016643, 0);
                dataTable.Rows.Add(x + 0.031250, -0.147374, 0);
                dataTable.Rows.Add(x + 0.035156, -0.239553, 0);
                dataTable.Rows.Add(x + 0.039062, -0.293963, 0);
                dataTable.Rows.Add(x + 0.042969, -0.314364, 4);

                dataTable.Rows.Add(x + 0.046875, -0.306833, 0);
                dataTable.Rows.Add(x + 0.050781, -0.279048, 0);
                dataTable.Rows.Add(x + 0.054688, -0.239311, 0);
                dataTable.Rows.Add(x + 0.058594, -0.195373, 0);
                dataTable.Rows.Add(x + 0.062500, -0.153381, 0);
                dataTable.Rows.Add(x + 0.066406, -0.117263, 0);
                dataTable.Rows.Add(x + 0.070312, -0.088692, 0);

                dataTable.Rows.Add(x + 0.074219, -0.067541, 0);
                dataTable.Rows.Add(x + 0.078125, -0.052578, 0);
                dataTable.Rows.Add(x + 0.082031, -0.042145, 0);
                dataTable.Rows.Add(x + 0.085938, -0.034640, 0);
                dataTable.Rows.Add(x + 0.089844, -0.028777, 0);
                dataTable.Rows.Add(x + 0.093750, -0.023647, 0);
                dataTable.Rows.Add(x + 0.097656, -0.018664, 0);
                dataTable.Rows.Add(x + 0.101562, -0.013481, 0);
                dataTable.Rows.Add(x + 0.105469, -0.007896, 0);
                dataTable.Rows.Add(x + 0.109375, -0.001795, 0);

                dataTable.Rows.Add(x + 0.113281, 0.004896, 0);
                dataTable.Rows.Add(x + 0.117188, 0.012224, 0);
                dataTable.Rows.Add(x + 0.121094, 0.020230, 0);
                dataTable.Rows.Add(x + 0.125000, 0.028942, 0);
                dataTable.Rows.Add(x + 0.128906, 0.038385, 0);
                dataTable.Rows.Add(x + 0.132812, 0.048577, 0);
                dataTable.Rows.Add(x + 0.136719, 0.059526, 0);
                dataTable.Rows.Add(x + 0.140625, 0.071235, 0);
                dataTable.Rows.Add(x + 0.144531, 0.083696, 0);
                dataTable.Rows.Add(x + 0.148438, 0.096889, 0);
                dataTable.Rows.Add(x + 0.152344, 0.110786, 0);
                dataTable.Rows.Add(x + 0.156250, 0.125345, 0);
                dataTable.Rows.Add(x + 0.160156, 0.140510, 0);
                dataTable.Rows.Add(x + 0.164062, 0.156217, 0);
                dataTable.Rows.Add(x + 0.167969, 0.172385, 0);
                dataTable.Rows.Add(x + 0.171875, 0.188922, 0);
                dataTable.Rows.Add(x + 0.175781, 0.205723, 0);
                dataTable.Rows.Add(x + 0.179688, 0.222674, 0);
                dataTable.Rows.Add(x + 0.183594, 0.239647, 0);
                dataTable.Rows.Add(x + 0.187500, 0.256509, 0);
                dataTable.Rows.Add(x + 0.191406, 0.273116, 0);
                dataTable.Rows.Add(x + 0.195312, 0.289320, 0);
                dataTable.Rows.Add(x + 0.199219, 0.304970, 0);
                dataTable.Rows.Add(x + 0.203125, 0.319914, 0);
                dataTable.Rows.Add(x + 0.207031, 0.333999, 0);
                dataTable.Rows.Add(x + 0.210938, 0.347078, 0);
                dataTable.Rows.Add(x + 0.214844, 0.359010, 0);
                dataTable.Rows.Add(x + 0.218750, 0.369660, 0);
                dataTable.Rows.Add(x + 0.222656, 0.378909, 0);
                dataTable.Rows.Add(x + 0.226562, 0.386646, 0);
                dataTable.Rows.Add(x + 0.230469, 0.392780, 0);
                dataTable.Rows.Add(x + 0.234375, 0.397235, 0);
                dataTable.Rows.Add(x + 0.238281, 0.399955, 0);
                dataTable.Rows.Add(x + 0.242188, 0.400901, 5);


                dataTable.Rows.Add(x + 0.246094, 0.400060, 0);
                dataTable.Rows.Add(x + 0.250000, 0.397434, 0);
                dataTable.Rows.Add(x + 0.253906, 0.393051, 0);
                dataTable.Rows.Add(x + 0.257812, 0.386958, 0);
                dataTable.Rows.Add(x + 0.261719, 0.379220, 0);
                dataTable.Rows.Add(x + 0.265625, 0.369923, 0);
                dataTable.Rows.Add(x + 0.269531, 0.359168, 0);
                dataTable.Rows.Add(x + 0.273438, 0.347073, 0);
                dataTable.Rows.Add(x + 0.277344, 0.333767, 0);
                dataTable.Rows.Add(x + 0.281250, 0.319390, 0);
                dataTable.Rows.Add(x + 0.285156, 0.304092, 0);
                dataTable.Rows.Add(x + 0.289062, 0.288025, 0);
                dataTable.Rows.Add(x + 0.292969, 0.271347, 0);
                dataTable.Rows.Add(x + 0.296875, 0.254215, 0);
                dataTable.Rows.Add(x + 0.300781, 0.236784, 0);
                dataTable.Rows.Add(x + 0.304688, 0.219206, 0);
                dataTable.Rows.Add(x + 0.308594, 0.201624, 0);
                dataTable.Rows.Add(x + 0.312500, 0.184177, 0);
                dataTable.Rows.Add(x + 0.316406, 0.166990, 0);
                dataTable.Rows.Add(x + 0.320312, 0.150179, 0);
                dataTable.Rows.Add(x + 0.324219, 0.133847, 0);
                dataTable.Rows.Add(x + 0.328125, 0.118087, 0);
                dataTable.Rows.Add(x + 0.332031, 0.102974, 0);
                dataTable.Rows.Add(x + 0.335938, 0.088573, 0);
                dataTable.Rows.Add(x + 0.339844, 0.074935, 0);
                dataTable.Rows.Add(x + 0.343750, 0.062099, 0);
                dataTable.Rows.Add(x + 0.347656, 0.050090, 0);
                dataTable.Rows.Add(x + 0.351562, 0.038923, 0);
                dataTable.Rows.Add(x + 0.355469, 0.028601, 0);
                dataTable.Rows.Add(x + 0.359375, 0.019118, 0);
                dataTable.Rows.Add(x + 0.363281, 0.010459, 0);
                dataTable.Rows.Add(x + 0.367188, 0.002604, 0);
                dataTable.Rows.Add(x + 0.371094, -0.004478, 0);
                dataTable.Rows.Add(x + 0.375000, -0.010818, 0);
                dataTable.Rows.Add(x + 0.378906, -0.016456, 0);
                dataTable.Rows.Add(x + 0.382812, -0.021430, 0);
                dataTable.Rows.Add(x + 0.386719, -0.025785, 0);
                dataTable.Rows.Add(x + 0.390625, -0.029564, 0);
                dataTable.Rows.Add(x + 0.394531, -0.032811, 0);
                dataTable.Rows.Add(x + 0.398438, -0.035571, 0);
                dataTable.Rows.Add(x + 0.402344, -0.037886, 0);
                dataTable.Rows.Add(x + 0.406250, -0.039798, 0);
                dataTable.Rows.Add(x + 0.410156, -0.041347, 0);
                dataTable.Rows.Add(x + 0.414062, -0.042572, 0);
                dataTable.Rows.Add(x + 0.417969, -0.043507, 0);
                dataTable.Rows.Add(x + 0.421875, -0.044187, 0);
                dataTable.Rows.Add(x + 0.425781, -0.044640, 0);
                dataTable.Rows.Add(x + 0.429688, -0.044896, 0);
                dataTable.Rows.Add(x + 0.433594, -0.044980, 0);
                dataTable.Rows.Add(x + 0.437500, -0.044914, 0);
                dataTable.Rows.Add(x + 0.441406, -0.044720, 0);
                dataTable.Rows.Add(x + 0.445312, -0.044416, 0);

                dataTable.Rows.Add(x + 0.449219, -0.044018, 0);
                dataTable.Rows.Add(x + 0.453125, -0.043540, 0);
                dataTable.Rows.Add(x + 0.457031, -0.042994, 0);
                dataTable.Rows.Add(x + 0.460938, -0.042392, 0);
                dataTable.Rows.Add(x + 0.464844, -0.041743, 0);
                dataTable.Rows.Add(x + 0.468750, -0.041055, 0);
                dataTable.Rows.Add(x + 0.472656, -0.040335, 0);
                dataTable.Rows.Add(x + 0.476562, -0.039588, 0);
                dataTable.Rows.Add(x + 0.480469, -0.038819, 0);
                dataTable.Rows.Add(x + 0.484375, -0.038033, 0);
                dataTable.Rows.Add(x + 0.488281, -0.037234, 0);
                dataTable.Rows.Add(x + 0.492188, -0.036385, 0);
                dataTable.Rows.Add(x + 0.496094, -0.035528, 0);
                dataTable.Rows.Add(x + 0.500000, -0.034673, 0);
                dataTable.Rows.Add(x + 0.503906, -0.033819, 0);
                dataTable.Rows.Add(x + 0.507812, -0.032966, 0);
                dataTable.Rows.Add(x + 0.511719, -0.032115, 0);
                dataTable.Rows.Add(x + 0.515625, -0.031265, 0);
                dataTable.Rows.Add(x + 0.519531, -0.030416, 0);
                dataTable.Rows.Add(x + 0.523438, -0.029569, 0);
                dataTable.Rows.Add(x + 0.527344, -0.028723, 0);
                dataTable.Rows.Add(x + 0.531250, -0.027878, 0);
                dataTable.Rows.Add(x + 0.535156, -0.027034, 0);
                dataTable.Rows.Add(x + 0.539062, -0.026192, 0);
                dataTable.Rows.Add(x + 0.542969, -0.025351, 0);
                dataTable.Rows.Add(x + 0.546875, -0.024512, 0);
                dataTable.Rows.Add(x + 0.550781, -0.023674, 0);
                dataTable.Rows.Add(x + 0.554688, -0.022837, 0);
                dataTable.Rows.Add(x + 0.558594, -0.022002, 0);
                dataTable.Rows.Add(x + 0.562500, -0.021168, 0);
                dataTable.Rows.Add(x + 0.566406, -0.020335, 0);
                dataTable.Rows.Add(x + 0.570312, -0.019504, 0);
                dataTable.Rows.Add(x + 0.574219, -0.018674, 0);
                dataTable.Rows.Add(x + 0.578125, -0.017846, 0);
                dataTable.Rows.Add(x + 0.582031, -0.017019, 0);
                dataTable.Rows.Add(x + 0.585938, -0.016194, 0);
                dataTable.Rows.Add(x + 0.589844, -0.015370, 0);
                dataTable.Rows.Add(x + 0.593750, -0.014547, 0);
                dataTable.Rows.Add(x + 0.597656, -0.013726, 0);
                dataTable.Rows.Add(x + 0.601562, -0.012906, 0);
                dataTable.Rows.Add(x + 0.605469, -0.012088, 0);
                dataTable.Rows.Add(x + 0.609375, -0.011272, 0);
                dataTable.Rows.Add(x + 0.613281, -0.010456, 0);
                dataTable.Rows.Add(x + 0.617188, -0.009642, 0);
                dataTable.Rows.Add(x + 0.621094, -0.008830, 0);
                dataTable.Rows.Add(x + 0.625000, -0.008019, 0);
                dataTable.Rows.Add(x + 0.628906, -0.007208, 0);
                dataTable.Rows.Add(x + 0.632812, -0.006399, 0);
                dataTable.Rows.Add(x + 0.636719, -0.005590, 0);
                dataTable.Rows.Add(x + 0.640625, -0.004781, 0);
                dataTable.Rows.Add(x + 0.644531, -0.003972, 0);
                dataTable.Rows.Add(x + 0.648438, -0.003160, 0);
                dataTable.Rows.Add(x + 0.652344, -0.002345, 0);
                dataTable.Rows.Add(x + 0.656250, -0.001523, 0);
                dataTable.Rows.Add(x + 0.660156, -0.000692, 0);
                dataTable.Rows.Add(x + 0.664062, 0.000152, 0);
                dataTable.Rows.Add(x + 0.667969, 0.001017, 0);
                dataTable.Rows.Add(x + 0.671875, 0.001910, 0);
                dataTable.Rows.Add(x + 0.675781, 0.002844, 0);
                dataTable.Rows.Add(x + 0.679688, 0.003831, 0);
                dataTable.Rows.Add(x + 0.683594, 0.004892, 0);
                dataTable.Rows.Add(x + 0.687500, 0.006051, 0);
                dataTable.Rows.Add(x + 0.691406, 0.007338, 0);
                dataTable.Rows.Add(x + 0.695312, 0.008790, 0);
                dataTable.Rows.Add(x + 0.699219, 0.010452, 0);
                dataTable.Rows.Add(x + 0.703125, 0.012380, 0);
                dataTable.Rows.Add(x + 0.707031, 0.014638, 0);
                dataTable.Rows.Add(x + 0.710938, 0.017298, 0);
                dataTable.Rows.Add(x + 0.714844, 0.020444, 0);

                dataTable.Rows.Add(x + 0.718750, 0.024168, 0);
                dataTable.Rows.Add(x + 0.722656, 0.028569, 0);
                dataTable.Rows.Add(x + 0.726562, 0.033750, 0);
                dataTable.Rows.Add(x + 0.730469, 0.039816, 0);
                dataTable.Rows.Add(x + 0.734375, 0.046866, 0);
                dataTable.Rows.Add(x + 0.738281, 0.054994, 0);
                dataTable.Rows.Add(x + 0.742188, 0.064276, 0);
                dataTable.Rows.Add(x + 0.746094, 0.074766, 0);
                dataTable.Rows.Add(x + 0.750000, 0.086492, 0);
                dataTable.Rows.Add(x + 0.753906, 0.099442, 0);
                dataTable.Rows.Add(x + 0.757812, 0.113563, 0);
                dataTable.Rows.Add(x + 0.761719, 0.128756, 0);
                dataTable.Rows.Add(x + 0.765625, 0.144869, 0);
                dataTable.Rows.Add(x + 0.769531, 0.161696, 0);
                dataTable.Rows.Add(x + 0.773438, 0.178984, 0);
                dataTable.Rows.Add(x + 0.777344, 0.196428, 0);
                dataTable.Rows.Add(x + 0.781250, 0.213687, 0);
                dataTable.Rows.Add(x + 0.785156, 0.230388, 0);
                dataTable.Rows.Add(x + 0.789062, 0.246143, 0);
                dataTable.Rows.Add(x + 0.792969, 0.260560, 0);
                dataTable.Rows.Add(x + 0.796875, 0.273262, 0);
                dataTable.Rows.Add(x + 0.800781, 0.283902, 0);
                dataTable.Rows.Add(x + 0.804688, 0.292179, 0);
                dataTable.Rows.Add(x + 0.808594, 0.297855, 0);
                dataTable.Rows.Add(x + 0.812500, 0.300762, 0);
                dataTable.Rows.Add(x + 0.816406, 0.300815, 1);


                dataTable.Rows.Add(x + 0.820312, 0.298012, 0);
                dataTable.Rows.Add(x + 0.824219, 0.292439, 0);
                dataTable.Rows.Add(x + 0.828125, 0.284262, 0);
                dataTable.Rows.Add(x + 0.832031, 0.273723, 0);
                dataTable.Rows.Add(x + 0.835938, 0.261124, 0);
                dataTable.Rows.Add(x + 0.839844, 0.246816, 0);
                dataTable.Rows.Add(x + 0.843750, 0.231181, 0);
                dataTable.Rows.Add(x + 0.847656, 0.214619, 0);
                dataTable.Rows.Add(x + 0.851562, 0.197525, 0);
                dataTable.Rows.Add(x + 0.855469, 0.180282, 0);
                dataTable.Rows.Add(x + 0.859375, 0.163241, 0);
                dataTable.Rows.Add(x + 0.863281, 0.146713, 0);
                dataTable.Rows.Add(x + 0.867188, 0.130961, 0);
                dataTable.Rows.Add(x + 0.871094, 0.116189, 0);
                dataTable.Rows.Add(x + 0.875000, 0.102539, 0);
                dataTable.Rows.Add(x + 0.878906, 0.090080, 0);
                dataTable.Rows.Add(x + 0.882812, 0.078784, 0);
                dataTable.Rows.Add(x + 0.886719, 0.068501, 0);

                dataTable.Rows.Add(x + 0.890625, 0.058909, 0);
                dataTable.Rows.Add(x + 0.894531, 0.049461, 0);
                dataTable.Rows.Add(x + 0.898438, 0.039347, 0);
                dataTable.Rows.Add(x + 0.902344, 0.027510, 0);
                dataTable.Rows.Add(x + 0.906250, 0.012773, 0);
                dataTable.Rows.Add(x + 0.910156, -0.005882, 0);
                dataTable.Rows.Add(x + 0.914062, -0.028893, 0);

                dataTable.Rows.Add(x + 0.917969, -0.055622, 0);
                dataTable.Rows.Add(x + 0.921875, -0.083951, 0);
                dataTable.Rows.Add(x + 0.925781, -0.110115, 0);
                dataTable.Rows.Add(x + 0.929688, -0.128898, 0);
                dataTable.Rows.Add(x + 0.933594, -0.134135, 2);

                dataTable.Rows.Add(x + 0.937500, -0.119383, 0);
                dataTable.Rows.Add(x + 0.941406, -0.078593, 0);
                dataTable.Rows.Add(x + 0.945312, -0.006757, 0);
                dataTable.Rows.Add(x + 0.949219, 0.099278, 0);
                dataTable.Rows.Add(x + 0.953125, 0.239600, 0);
                dataTable.Rows.Add(x + 0.957031, 0.409627, 0);
                dataTable.Rows.Add(x + 0.960938, 0.598585, 0);
                dataTable.Rows.Add(x + 0.964844, 0.789027, 0);
                dataTable.Rows.Add(x + 0.968750, 0.958345, 0);
                x = x + 0.968750;
            }
            /*
            for (int i = 0; i < 1000; i++)
            {
                double j = Math.Sin(Cntr + i / Rar);
                dataTable.Rows.Add(i, j);
            }
             * */
            chart1.Series[0].Points.DataBind(dataTable.DefaultView, "TimePoint", "Speed", null);
            foreach (DataPoint pt in chart1.Series[0].Points)
            {
                //pt.YValues[1] // this will be your value depending upon which you could set the color
                if (Cntr < freezedCntr)
                {
                    pt.Color = Color.Red;
                }
                else
                {
                    pt.Color = Color.Gray;
                }
            }
        }

        private void SimulationBand_Load()
        {
            dataTable.Columns.Add("TimePoint", typeof(double));
            dataTable.Columns.Add("Speed", typeof(double));
            dataTable.Columns.Add("Flag", typeof(double));
            /*
            for (int i = 0; i < 1000; i++)
            {
                double j = Math.Sin(Cntr + i);
                dataTable.Rows.Add(i, j);
            }
            */
            double x = 0;
dataTable.Rows.Add(x + 0.003906, 1.031029, 0);
dataTable.Rows.Add(x + 0.964844, 0.789027, 0);
dataTable.Rows.Add(x + 0.968750, 0.958345, 0);
            
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            this.chart1.ChartAreas [0].AxisX.Maximum = 5.1;

            this.chart1.ChartAreas [0].AxisY.Minimum = -2;
            this.chart1.ChartAreas [0].AxisY.Maximum = 2;

            //this.chart1.ChartAreas[0].AxisY.Interval = 0.01;
            //this.chart1.ChartAreas[0].AxisX.Interval = 0.01;

            this.chart1.ChartAreas[0].AxisY.Title = "Time";
            this.chart1.ChartAreas [0].AxisX.Title = "Speed";


            //this.AutoScaleDimensions = ;
            //this.AutoScaleFactor = 2;
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.chart1.Series[0].MarkerSize = 2;

            chart1.Series[0].Points.DataBind(dataTable.DefaultView, "TimePoint", "Speed", null);
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 4;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)(Convert.ToDecimal(domainUpDown1.SelectedItem.ToString()) * 1000);
            Evaluate(-1);
        }
        
        private void domainUpDown2_SelectedItemChanged_1(object sender, EventArgs e)
        {
            Rar = (int)(Convert.ToDecimal(domainUpDown2.SelectedItem.ToString()) * 1000);
            Evaluate(-1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            animate = checkBox1.Checked;
            if (checkBox1.Checked)
            {
                Cntr = freezedCntr;
                Rar = freezedRar;
                timer1.Interval = freezedInterval;
            }
            else
            {
                freezedCntr = Cntr;
                freezedRar = Rar;
                freezedInterval = timer1.Interval;
            }

            
        }
    }
}
