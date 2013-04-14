using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace GeneratedWF
{
    public class FormatedBindCell : DataGridViewTextBoxCell
    {
        ITableAdaptor tableAdaptor;
        string stringValue;

        protected override object GetValue(int rowIndex)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                if (OwningRow.Index < 0)
                {
                    return string.Empty;
                }

                tableAdaptor = (MySqlTableAdaptor)OwningColumn.Tag;

                var poinedRows = new List<DataRow>();
                if (((DataRowView)OwningRow.DataBoundItem).Row[ColumnIndex + 1] != DBNull.Value)
                {
                    poinedRows = tableAdaptor.GetObjectByIDs(new List<decimal> { (decimal)((DataRowView)OwningRow.DataBoundItem).Row[ColumnIndex + 1] });
                }

                // Advanced
                string result = poinedRows.Aggregate(string.Empty, (current, dr) => current + (", " + dr["NAME"]));

                if (result.Length > 2)
                {
                    stringValue = result.Substring(2);
                }
            }
            return stringValue;
        }
        /*
        private List<DataRow> GetObjectByIDs(List<decimal> rowIDs)
        {
            return tableAdaptor.GetObjectByIDs(rowIDs);
        }
        */
    }
}
