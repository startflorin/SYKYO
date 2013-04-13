using System.Data;
using System;

namespace ObjectCollision.BL {
    
    
    public partial class CurrentStateDataSet 
    {
        //MYSYMBOLSDataTable.
        //DataRelation MySymbolsMyUser = new DataRelation("MySymbolsMyUser",
        //    MYUSERDataTable.
        //    );
        //MYSYMBOLSDataTable

        //public Data

	//
	// Get the DataTable.
	//
	DataTable table = GetTable();


    /// <summary>
    /// This example method generates a DataTable.
    /// </summary>
    static DataTable GetTable()
    {
	//
	// Here we create a DataTable with four columns.
	//
	DataTable table = new DataTable();
	table.Columns.Add("Dosage", typeof(int));
	table.Columns.Add("Drug", typeof(string));
	table.Columns.Add("Patient", typeof(string));
	table.Columns.Add("Date", typeof(DateTime));

	//
	// Here we add five DataRows.
	//
	table.Rows.Add(25, "Indocin", "David", DateTime.Now);
	table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
	table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
	table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
	table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
	return table;
    }

    }
}
