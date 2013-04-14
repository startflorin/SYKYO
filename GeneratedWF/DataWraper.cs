using System;
using System.Data;
using System.Windows.Forms;

namespace GeneratedWF
{
    public partial class DataWraper : UserControl
    {
        private readonly Type type;
        private readonly DataTable dataTable;
        private readonly ITableAdaptor dataTableAdaptor;
        readonly BindingSource bindingSourceMain;
        
        public DataWraper(Type tableName, BindingSource componentId, ErrorProvider errorProvider)
        {
            errorProvider1 = errorProvider;
            
            bindingSourceMain = componentId;
            bindingSourceMain.PositionChanged += bindingSourceMain_PositionChanged;
            InitializeComponent();
            WrapperBindingSource.PositionChanged += WrapperBindingSource_PositionChanged;
            WrapperBindingSource.DataSource = tableName.GetType();
            //Type yy = tableName.GetType();
            Text = tableName.Name + "s";
            dataTableAdaptor = new MySqlTableAdaptor(tableName);
            dataTable = dataTableAdaptor.GetTable();
            WrapperBindingSource.DataSource = dataTable;
            errorProvider1.DataSource = WrapperBindingSource;
            if (componentId.Current != null)
            {
                WrapperBindingSource.Position = (int)componentId.Current;
            }
            WrapperID.DataBindings.Add("Text", WrapperBindingSource, "ID");
            WrapperComboBox.DataSource = dataTable;
            WrapperComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WrapperComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            WrapperComboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            WrapperComboBox.ValueMember = dataTable.Columns[0].ColumnName;
            //WrapperComboBox.DataSource = WrapperBindingSource;
            WrapperComboBox.SelectedValueChanged += WrapperComboBox_SelectedValueChanged;
            WrapperComponentSelection.Click+=WrapperComponentSelection_Click;
            WrapperComboBox.Validated += WrapperComboBox_Validated;
            WrapperLabel.Text = tableName.Name;
            WrapperID.TextChanged += WrapperID_TextChanged;
            //WrapperComboBox.DataBin
            type = tableName;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        void WrapperID_TextChanged(object sender, EventArgs e)
        {
            if (WrapperID.Text.Trim() == "0")
            {
                WrapperID.Text = string.Empty;
            }
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

        void WrapperBindingSource_PositionChanged(object sender, EventArgs e)
        {
            //decimal selectedId = Convert.ToDecimal(((DataRowView)WrapperBindingSource.Current).Row[0]);
            if (bindingSourceMain.Current != null && WrapperBindingSource.Current != null)
            {
                ((DataRowView)bindingSourceMain.Current).Row[type.Name + "P"] = ((DataRowView)WrapperBindingSource.Current).Row[0];
            }
        }

        void bindingSourceMain_PositionChanged(object sender, EventArgs e)
        {
            if (((BindingSource)sender).Position<0 || ((BindingSource)sender).Current == null)
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

        private void WrapperComponentSelection_Click(object sender, EventArgs e)
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
