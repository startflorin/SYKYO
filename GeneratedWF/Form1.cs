using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GeneratedWF
{
    public partial class Form1 : Form
    {
        public DataTable dataTable;
        public ITableAdaptor dataTableAdaptor;
        private bool dataTableIgnoreChanges ;

        public void ReloadData()
        {
            bindingSource1.SuspendBinding();
            dataTableIgnoreChanges = true;
            dataTableAdaptor.FillTable(100);
            dataTable = dataTableAdaptor.GetTable();
            dataTableIgnoreChanges = false;
            bindingSource1.ResumeBinding();
            
            //bindingSource1.EndEdit();
        }

        public Form1(Type tableType)
        {
            InitializeComponent();
            this.bindingSource1.DataSource = tableType.GetType();
            bindingSource1.DataSourceChanged += bindingSource1_DataSourceChanged;
            System.Type yy = tableType.GetType();
            this.Text = tableType.Name+"s";
            dataTableAdaptor = new MySqlTableAdaptor(tableType);
            dataTable = dataTableAdaptor.GetTable();
            
            dataGridView1.Columns.Clear();
            int position = 3;

            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            
            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
                DataGridViewCell dataGridViewCell = null;
                if (dataTable.Columns[i].DataType == typeof(decimal))
                {
                    dataGridViewCell = new FormatedBindCell();
                    //((FormatedBindCell)dataGridViewCell).InitializecatTableAdaptor(dataTableAdaptor);
                }
                else
                {
                    dataGridViewCell = new DataGridViewTextBoxCell();
                }
                dataGridViewColumn.DataPropertyName = dataTable.Columns[i].ColumnName;
                dataGridViewColumn.HeaderText = dataTable.Columns[i].ColumnName;
                dataGridViewColumn.CellTemplate = dataGridViewCell;
                dataGridViewColumn.Name = dataTable.Columns[i].ColumnName;
                dataGridView1.Columns.Add(dataGridViewColumn);



                if (dataTable.Columns[i].DataType == typeof(decimal))
                {

                    DataWraper dataWrapper = null;
                    object objo = ((Type)dataTableAdaptor.GetItem()).UnderlyingSystemType.GetProperty(dataTable.Columns[i].ColumnName);
                    if (objo != null)
                    {
                        dataWrapper = new DataWraper(((PropertyInfo)objo).PropertyType, bindingSource1, errorProvider1);//.MemberType.GetType();
                        dataGridViewColumn.Tag = new MySqlTableAdaptor(((PropertyInfo)objo).PropertyType);
                    
                    dataWrapper.Location = new System.Drawing.Point(6, position);
                    tabPage2.Controls.Add(dataWrapper);
                    }
                }
                else
                {
                    Button button1 = new Button();
                    Button button2 = new Button();
                    TextBox textBox1 = new TextBox();
                    Label label1 = new Label();

                    label1.AutoSize = true;
                    label1.Location = new System.Drawing.Point(6, position + 6);
                    label1.Name = dataTable.Columns[i].ColumnName + "Label";
                    label1.Size = new System.Drawing.Size(46, 13);
                    label1.TabIndex = 0;
                    label1.Text = dataTable.Columns[i].ColumnName; ;
                    // 
                    // textBox1
                    // 
                    textBox1.Location = new System.Drawing.Point(80, position + 3);
                    textBox1.Name = dataTable.Columns[i].ColumnName + "TextBox";
                    textBox1.Size = new System.Drawing.Size(111, 20);
                    textBox1.TabIndex = 1;
                    textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", bindingSource1, dataTable.Columns[i].ColumnName));
                    this.tabPage2.Controls.Add(textBox1);
                    this.tabPage2.Controls.Add(label1);
                }

                SimulationBand dataSimulator = null;
                dataSimulator = new SimulationBand();

                dataSimulator.Location = new System.Drawing.Point(6, position * 5);
                dataSimulator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            
                tabPage3.Controls.Add(dataSimulator);

                position += 28;
            }


            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();

            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();

            bindingSource1.DataSource = dataTable;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = bindingSource1;

            
            this.tabControl1.DrawMode =
                    TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem +=
                new DrawItemEventHandler(PageTab_DrawItem);
            this.tabControl1.Selecting +=
                new TabControlCancelEventHandler(PageTab_Selecting);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DoubleClick += new System.EventHandler(dataGridView1_DoubleClick);
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            dataTable.ColumnChanged += new DataColumnChangeEventHandler(HELP_ColumnChanged);
            //dataGridView1.Columns[1]..Format += new ConvertEventHandler(Form1_Format);
        }

        private void RefreshTabEnability()
        {
            if (bindingSource1.Count > 0)
            {
                tabPage2.Enabled = true;
            }
            else
            {
                tabPage2.Enabled = false;
            }
        }

        void bindingSource1_DataSourceChanged(object sender, System.EventArgs e)
        {
            RefreshTabEnability();
        }


       public void button2_Click(object sender, System.EventArgs e)
        {
            string fildname = sender.ToString().Substring(sender.ToString().LastIndexOf(' ')+1);
            Type obj = null;
            object objo = ((Type)dataTableAdaptor.GetItem()).UnderlyingSystemType.GetProperty(fildname);
            if (objo != null)
            {
                //Program.CreateForm(((MemberInfo[])objo).FieldType.UnderlyingSystemType);
                Program.CreateForm(((PropertyInfo)objo).PropertyType);//.MemberType.GetType();
            }
            if (obj != null)
            {
                Program.CreateForm(obj);
            }
            else
            {
                objo = ((Type)dataTableAdaptor.GetItem()).UnderlyingSystemType.GetField(fildname);
            }
            if (objo != null)
            {
                //MemberInfo[] mi = ((Type)dataTableAdaptor.recordItem).UnderlyingSystemType.GetMember(fildname);
                //mi[0].;
                Program.CreateForm(((FieldInfo)objo).FieldType.UnderlyingSystemType);
            }
        }

        void dataGridView1_DoubleClick(object sender, System.EventArgs e)
        {
            SetNewModeOfForm();
        }

        private void ViewRecord()
        {
            throw new System.NotImplementedException();
        }

        void HELP_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (!dataTableIgnoreChanges)
            {
                SetNewModeOfForm();
            }
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                SetMultiselectModeOfForm();
            }
            else if (dataGridView1.SelectedRows.Count < 1)
            {
                SetNoSelectionModeOfForm();
            }
            else
            {
                SetInitialModeOfForm();
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void actionButtonDelete_Click(object sender, EventArgs e)
        {
            //dataGridView1.au = false;
            while (dataGridView1.SelectedRows.Count>0)
            {
                DataRow rowToDelete = dataTable.Rows[bindingSource1.Position];
                if (rowToDelete.RowState != DataRowState.Deleted)
                {
                    dataTableAdaptor.DeleteItem(Convert.ToDecimal(rowToDelete["ID"]));
                }
                dataTable.Rows[dataGridView1.SelectedRows[0].Index].Delete();
            }
            bindingSource1.RaiseListChangedEvents = true;
            dataTable.AcceptChanges();
            RefreshTabEnability();
            ReportChanges();
        }

        private void actionButtonCancel_Click(object sender, EventArgs e)
        {
            dataTable.Rows[bindingSource1.Position].RejectChanges();
            dataTable.AcceptChanges();
            SetInitialModeOfForm();
        }

        private void actionButtonSave_Click(object sender, EventArgs e)
        {
            //dataTable.HasErrors = false;
            DataRow rowToSave = dataTable.Rows[bindingSource1.Position];
            if (dataTable.HasErrors)
            {
                DataRow[] rowsWithErrors = dataTable.GetErrors();
                foreach (DataRow dataRow in rowsWithErrors)
                {
                    // dataRow.RowError = 
                    dataRow.ClearErrors();
                }
                //rowsWithErrors.
            }
            foreach (DataColumn dr in dataTable.Columns)
            {
                if (dr.AllowDBNull == false)
                {
                    if (dr.DataType == typeof(decimal))
                    {
                        if ((decimal)rowToSave[dr] < 0)
                        {
                            rowToSave.SetColumnError(dr, "Please select " + dr.ColumnName);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace((string)rowToSave[dr]))
                        {
                            rowToSave.SetColumnError(dr, "Please type a "+dr.ColumnName);
                        }
                    }
                    //rowToSave.SetColumnError(dr, "ppp");
                }
            }
            if (dataTable.HasErrors)
            {
                DataRow[] rowsWithErrors = dataTable.GetErrors();
                foreach (DataRow dataRow in rowsWithErrors)
                {
                   // dataRow.RowError = 
                   // dataRow.ClearErrors();
                }
                //rowsWithErrors.
            }

            if (Convert.ToDecimal(dataTable.Rows[bindingSource1.Position].ItemArray[0]) < 0)
            {
                int ID = dataTableAdaptor.InsertItem(rowToSave.ItemArray.ToList());
                rowToSave[0] = ID;
                dataTable.Rows[bindingSource1.Position].AcceptChanges();
            }
            else
            {
                dataTableAdaptor.UpdateItem(dataTable.Rows[bindingSource1.Position].ItemArray.ToList());
                dataTable.Rows[bindingSource1.Position].AcceptChanges();
            }
            dataTable.AcceptChanges();
            SetInitialModeOfForm();
            ReportChanges();
        }

        int seq = 8990;

        private void actionButtonNew_Click(object sender, EventArgs e)
        {
            DataRow newRow = dataTable.NewRow();
            foreach (DataColumn dr in dataTable.Columns)
            {
                if (dr.AllowDBNull == false || dr.ColumnName == "ID")
                {
                    if (dr.DataType == typeof(decimal))
                    {
                        newRow[dr] = -1;
                    }
                    else
                    {
                        newRow[dr] = " ";
                    }
                }
            }
            dataTable.Rows.Add(newRow);
            bindingSource1.MoveLast();
            RefreshTabEnability();
            SetNewModeOfForm();
        }


        private void actionButtonCopy_Click(object sender, EventArgs e)
        {
            DataRow newRow = dataTable.NewRow();
            newRow.ItemArray = dataTable.Rows[bindingSource1.Position].ItemArray;
            //newRow["SEQ"] = seq++;
            dataTable.Rows.Add(newRow);
            bindingSource1.MoveLast();
            SetNewModeOfForm();
        }

        #region INTERFACE STATES

        private void SetNoSelectionModeOfForm()
        {
            actionButtonNew.Visible = true;
            actionButtonCopy.Visible = false;
            actionButtonCancel.Visible = false;
            actionButtonSave.Visible = false;
            actionButtonDelete.Visible = false;
        }
        private void SetMultiselectModeOfForm()
        {
            actionButtonNew.Visible = true;
            actionButtonCopy.Visible = false;
            actionButtonCancel.Visible = false;
            actionButtonSave.Visible = false;
            actionButtonDelete.Visible = true;
        }
        private void SetNewModeOfForm()
        {
            actionButtonNew.Visible = false;
            actionButtonCopy.Visible = false;
            actionButtonCancel.Visible = true;
            actionButtonSave.Visible = true;
            actionButtonDelete.Visible = false;

            tabControl1.SelectedTab = tabPage2;
            tabPage1.Enabled = false;
            tabControl1.Refresh();
        }
        private void SetInitialModeOfForm()
        {
            actionButtonNew.Visible = true;
            actionButtonCopy.Visible = true;
            actionButtonCancel.Visible = false;
            actionButtonSave.Visible = false;
            actionButtonDelete.Visible = true;

            tabPage1.Enabled = true;
            tabControl1.SelectedTab = tabPage1;
            tabControl1.Refresh();
        }

        #endregion INTERFACE STATES

        /// <summary>
        /// Draw a tab page based on whether it is disabled or enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageTab_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage tabPage = tabControl.TabPages[e.Index];

            if (tabPage.Enabled == false)
            {
                using (SolidBrush brush =
                   new SolidBrush(SystemColors.GrayText))
                {
                    e.Graphics.DrawString(tabPage.Text, tabPage.Font, brush,
                       e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(tabPage.ForeColor))
                {
                    e.Graphics.DrawString(tabPage.Text, tabPage.Font, brush,
                       e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
        }

        /// <summary>
        /// Cancel the selecting event if the TabPage is disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        private void actionButtonExit_Click(object sender, EventArgs e)
        {
            if (true)
            {
                DialogResult option = MessageBox.Show(this, "Save Changes ?", "Closing Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch(option)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        try
                        {
                            MethodInfo methodInfo = dataTable.GetType().GetMethod("FlusgTable");
                            methodInfo.Invoke(dataTable, new object[] { dataTable });
                        }
                        catch (Exception f)
                        {
                        }
                        //dataSet1.AcceptChanges();
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                        break;
                }
                this.Dispose();
            }
        }

        private void ReportChanges()
        {
            XmlProcessor xmlProcessor = new XmlProcessor();
            string xml = xmlProcessor.XMLSyncronixe(dataTableAdaptor);
            Console.SendXml(xml);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ReportChanges();
        }
    }
}
