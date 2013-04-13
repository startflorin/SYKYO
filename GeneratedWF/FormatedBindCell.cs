using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace GeneratedWF
{
    public class FormatedBindCell : DataGridViewTextBoxCell
    {
        public FormatedBindCell()
        {
            //this.Value
        }

        ITableAdaptor tableAdaptor = null;
        string stringValue = null;

        protected override object GetValue(int rowIndex)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                if (OwningRow.Index < 0)
                {
                    return string.Empty;
                }

                tableAdaptor = (MySqlTableAdaptor)OwningColumn.Tag;

                List<DataRow> poinedRows = new List<DataRow>();
                if (((DataRowView)OwningRow.DataBoundItem).Row[ColumnIndex + 1] != DBNull.Value)
                {
                    poinedRows = tableAdaptor.GetObjectByIDs(new List<decimal>() { (decimal)((DataRowView)OwningRow.DataBoundItem).Row[ColumnIndex + 1] });
                }
                string result = string.Empty;

                foreach (DataRow dr in poinedRows)
                {
                    result += ", " + dr["NAME"];
                }
                if (result.Length > 2)
                {
                    stringValue = result.Substring(2);
                }
            }
            return stringValue;
        }

        private List<DataRow> GetObjectByIDs(List<decimal> rowIDs)
        {
            return tableAdaptor.GetObjectByIDs(rowIDs);
        }
    }
}
