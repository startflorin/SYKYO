using System.Data;
using System;
namespace WindowsFormsApplication1.BL {


    public partial class DataSet1
    {
        DataTable SYMBOLS;
        public DataTable AttachTable()
        {
            DataTable SYMBOLS = new DataTable("simbs");
            DataColumn a = SYMBOLS.Columns.Add("a", typeof(Int32));
            DataColumn b = SYMBOLS.Columns.Add("b", typeof(Int32));
            DataColumn c = SYMBOLS.Columns.Add("c", typeof(Int32));
            DataColumn d = SYMBOLS.Columns.Add("d", typeof(Int32));
            DataColumn e = SYMBOLS.Columns.Add("e", typeof(Int32));
            DataColumn f = SYMBOLS.Columns.Add("f", typeof(Int32));
            return SYMBOLS;
        }

        public DataSet1()
        {
            SYMBOLS = AttachTable();
            Tables.Add(SYMBOLS);
        }

    }
}
