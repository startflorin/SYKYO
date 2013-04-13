using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneratedWF
{
    public partial class DataWraper : UserControl
    {
        private Type type;
        public DataTable dataTable;
        public ITableAdaptor dataTableAdaptor;
        BindingSource bindingSourceMain = null;
        
        public DataWraper(Type tableName, BindingSource componentId, ErrorProvider errorProvider)
        {
            errorProvider1 = errorProvider;
            
            bindingSourceMain = componentId;
            bindingSourceMain.PositionChanged += new EventHandler(bindingSourceMain_PositionChanged);
            InitializeComponent();
            WrapperBindingSource.PositionChanged += new EventHandler(WrapperBindingSource_PositionChanged);
            this.WrapperBindingSource.DataSource = tableName.GetType();
            System.Type yy = tableName.GetType();
            this.Text = tableName.Name + "s";
            dataTableAdaptor = new MySqlTableAdaptor(tableName);
            dataTable = dataTableAdaptor.GetTable();
            WrapperBindingSource.DataSource = dataTable;
            this.errorProvider1.DataSource = this.WrapperBindingSource;
            int defaultSelection = -1;
            if (componentId.Current != null)
            {
                WrapperBindingSource.Position = (int)componentId.Current;
            }
            // TODO: Complete member initialization
            WrapperID.DataBindings.Add("Text", WrapperBindingSource, "ID");
            WrapperComboBox.DataSource = dataTable;
            WrapperComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WrapperComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            WrapperComboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            WrapperComboBox.ValueMember = dataTable.Columns[0].ColumnName;
            //WrapperComboBox.DataSource = WrapperBindingSource;
            WrapperComboBox.SelectedValueChanged += new EventHandler(WrapperComboBox_SelectedValueChanged);
            WrapperComponentSelection.Click+=new EventHandler(WrapperComponentSelection_Click);
            WrapperComboBox.Validated += new EventHandler(WrapperComboBox_Validated);
            WrapperLabel.Text = tableName.Name;
            WrapperID.TextChanged += new EventHandler(WrapperID_TextChanged);
            //WrapperComboBox.DataBin
            this.type = tableName;
        }

        void WrapperID_TextChanged(object sender, EventArgs e)
        {
            if (WrapperID.Text.Trim() == "0")
            {
                WrapperID.Text = string.Empty;
            }
        }

        

        bool SetNullContent()
        {
            //((DataRowView)bindingSourceMain.Current).Row[type.Name + "P"] = 0;
            //WrapperComboBox.SelectedValue = -1;
            //WrapperComboBox.Text = "";
            //WrapperBindingSource.MoveLast();
            //WrapperID.Text = string.Empty;
            return true;
        }

        void WrapperComboBox_Validated(object sender, EventArgs e)
        {
            //((ComboBox)sender).SelectedIndex;
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                ((DataRowView)bindingSourceMain.Current).Row[type.Name + "P"] = ((DataRowView)WrapperBindingSource.Current).Row[0];
            }
            else
            {
                if (bindingSourceMain.Current != null)
                {
                    ((DataRowView) bindingSourceMain.Current).Row[type.Name + "P"] = -1;
                }
            }
            if (dataTable.Rows.Count > 0)
            {
                dataTable.Rows[0].SetColumnError(0, "dddfff");
            }
            if (WrapperBindingSource.Current != null)
            {
                ((DataRowView) WrapperBindingSource.Current).Row.SetColumnError(0, "0dddfff");
                ((DataRowView) WrapperBindingSource.Current).Row.SetColumnError(1, "1dddfff");
                ((DataRowView) WrapperBindingSource.Current).Row.SetColumnError(2, "2dddfff");
                ((DataRowView) WrapperBindingSource.Current).Row.SetColumnError(3, "3dddfff");
            }
        }

        void WrapperComboBox_Validated(object sender, CancelEventArgs e)
        {
            
        }

        void WrapperBindingSource_PositionChanged(object sender, EventArgs e)
        {

            decimal selectedId = Convert.ToDecimal(((DataRowView)WrapperBindingSource.Current).Row[0]);
            if (bindingSourceMain.Current != null && WrapperBindingSource.Current != null)
            {
                ((DataRowView)bindingSourceMain.Current).Row[type.Name + "P"] = ((DataRowView)WrapperBindingSource.Current).Row[0];
            }
        }

        void bindingSourceMain_PositionChanged(object sender, EventArgs e)
        {
            if (((BindingSource) sender).Current == null)
            {
                return;
            }
            decimal defaultSelection = -1;
            if (((DataRowView)((BindingSource)sender).Current).Row[type.Name + "P"] != DBNull.Value)
            {
                defaultSelection = Convert.ToDecimal(((DataRowView)((BindingSource)sender).Current).Row[type.Name + "P"]);
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if ((decimal)dataRow[0] == defaultSelection)
                {
                    WrapperBindingSource.Position = dataTable.Rows.IndexOf(dataRow);
                    WrapperComboBox.SelectedValue = dataRow[0];
                } 
            }
        }

        public void WrapperComponentSelection_Click(object sender, System.EventArgs e)
        {
            Program.CreateForm(type);
        }

        void WrapperComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (((ComboBox)sender).SelectedItem != null)
            WrapperBindingSource.Position = dataTable.Rows.IndexOf(((DataRowView)((ComboBox)sender).SelectedItem).Row);
        }
    }
}
