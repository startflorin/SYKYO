using System;
using System.Collections.Generic;
using System.Data;

namespace GeneratedWF
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
        int InsertItem(List<object> values);
        void UpdateItem(List<object> values);
        List<DataRow> GetObjectByIDs(List<decimal> rowIDs);
    }
}