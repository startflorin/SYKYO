using System;
using System.Collections.Generic;
using System.Data;

namespace GeneratedWPF
{
    public interface ITableAdaptor
    {
        DataTable GetTable();
        Type GetItem();
        DataTable CheckTableExistence();
        DataTable CreateTable(string TableName);
        bool DropTable(string TableName);
        bool DeleteItem(decimal itemId);
        bool FillTable(decimal limit);
        bool InsertItem(List<object> values);
        void UpdateItem(List<object> values);
        List<DataRow> GetObjectByIDs(List<decimal> rowIDs);
    }
}