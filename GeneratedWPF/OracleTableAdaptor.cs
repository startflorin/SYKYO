using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Data;
using System;

namespace GeneratedWPF
{
    public class OracleTableAdaptor : ITableAdaptor
    {
        public DataTable Table = null;
        public Type Item = null;

        List<string> automaticColumns = new List<string>() { "ID", "NAME", "DESCRIPTION", "URL" };

        public DataTable GetTable()
        {
            return Table;
        }
        public Type GetItem()
        {
            return Item;
        }
        public OracleTableAdaptor()
        {
            oracleConnection = new OracleConnection("DATA SOURCE=;PERSIST SECURITY INFO=True;USER ID=SYSTEM; PASSWORD=Start1312");
            try
            {
                oracleConnection.Open();
            }catch(Exception e1){};
        }
        public OracleTableAdaptor(Type recordItemReference)
        {
            Item = recordItemReference;
            oracleConnection = new OracleConnection("DATA SOURCE=;PERSIST SECURITY INFO=True;USER ID=SYSTEM; PASSWORD=Start1312");
            try
            {
                oracleConnection.Open();
            Table = CheckTableExistence();
            FillTable(100);
            }
            catch(Exception e2){};
            
        }

        OracleConnection oracleConnection = null;

        public DataTable CheckTableExistence()
        {
            DataTable dataTable = new DataTable(((Type)Item).UnderlyingSystemType.Name);
            Type metainformation = Type.GetType("Object.Meta." + dataTable.TableName+ "Metainformation");

            string SqlCommand = string.Empty;
            SqlCommand = "SELECT * FROM USER_TAB_COLUMNS WHERE TABLE_NAME='" + dataTable.TableName.ToUpper() + "' ";
            OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
            OracleDataReader dr = OracleTriggerCommand.ExecuteReader();
            if (!dr.HasRows)
            {
                dataTable = CreateTable(((Type)Item).UnderlyingSystemType.Name);
                dr = OracleTriggerCommand.ExecuteReader();
            }
            else
            {
                dataTable.Columns.Clear();
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                    {
                        Type columnType = typeof(string);
                        string type = dr[2].ToString();
                        switch (type)
                        {
                            case "NUMBER": columnType = typeof(decimal); break;
                        }
                        DataColumn newColumn = new DataColumn(dr[1].ToString(), columnType);
                        dataTable.Columns.Add(newColumn);
                        newColumn.AllowDBNull = true;
                    }
                }
            }


            System.Reflection.FieldInfo[] mi = metainformation.GetFields();

            dataTable.Columns["NAME"].AllowDBNull = false;
                foreach (System.Reflection.FieldInfo fi in mi)
                {
                    if (fi.Name.StartsWith("isNullable"))
                    {
                        dataTable.Columns[fi.Name.Substring(10)].AllowDBNull = false;
                    }
                }
            
            // Assign MetaInformations
            //foreach(MemberInfo in Cats)

            return dataTable;
        }

        public DataTable CreateTable(string TableName)
        {
            DataTable dataTable = new DataTable(TableName);

            List<string> columns = new List<string>();
            DataTable CatTable = new DataTable(TableName);

            CatTable.Columns.Add(new DataColumn("ID", typeof(decimal)));
            CatTable.Columns.Add(new DataColumn("NAME", typeof(string)));
            System.Reflection.MemberInfo[] memberi = ((Type)Item).UnderlyingSystemType.GetMembers(); ;
            foreach (System.Reflection.MemberInfo member in memberi)
            {
                if (member.MemberType == System.Reflection.MemberTypes.Field || member.MemberType == System.Reflection.MemberTypes.Property)
                {
                    if (member.ReflectedType.Name == member.DeclaringType.Name)
                    {
                        if (!automaticColumns.Contains(member.Name.ToUpper()))
                        {
                            string name = member.Name;
                            if (name.Length > 29)
                            {
                                name = name.Substring(0, 29);
                            }
                            CatTable.Columns.Add(new DataColumn(name, typeof(decimal)));
                        }
                    }
                }
            }
            CatTable.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));
            CatTable.Columns.Add(new DataColumn("URL", typeof(string)));

            string commandCreateTable = " CREATE TABLE \"" + dataTable.TableName.ToUpper() + "\" ( \"ID\" NUMBER, \"NAME\" VARCHAR(30) ";
            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < CatTable.Columns.Count; i++)
			{
                if (!automaticColumns.Contains(CatTable.Columns[i].ColumnName.ToUpper()))
                SqlCommandColumns += ", \"" + CatTable.Columns[i].ColumnName + "\" INTEGER";
			}
            SqlCommandColumns +=", \"DESCRIPTION\" VARCHAR(300), \"URL\" VARCHAR(300) ";

            commandCreateTable += SqlCommandColumns;
            commandCreateTable += " ) "; //\"ID\" NUMBER, \"HELP\" NUMBER;
            OracleCommand OracleTableCommand = new OracleCommand(commandCreateTable, oracleConnection);
            OracleTableCommand.ExecuteNonQuery();
            string commandCreateSequence = " CREATE SEQUENCE  \"" + dataTable.TableName.ToUpper() + "_SEQ_ID\"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  ";
            OracleCommand OracleSequenceCommand = new OracleCommand(commandCreateSequence, oracleConnection);
            OracleSequenceCommand.ExecuteNonQuery();
            string commandCreateTrigger = " CREATE OR REPLACE TRIGGER " + dataTable.TableName.ToUpper() + "_AUTOINCREMENT  BEFORE INSERT ON " + dataTable.TableName + "  REFERENCING OLD AS OLD NEW AS NEW   FOR EACH ROW   WHEN (NEW.ID IS NULL)   BEGIN  select " + dataTable.TableName + "_SEQ_ID.NEXTVAL  INTO :NEW.ID FROM dual;  END ; ";
            OracleCommand OracleTriggerCommand = new OracleCommand(commandCreateTrigger, oracleConnection);
            OracleTriggerCommand.ExecuteNonQuery();
            
            return CatTable;
        }

        public bool DropTable(string TableName)
        {
            string dropTableQwery = "DROP TABLE \"" + TableName.ToUpper() + "\" ";
            string dropSequenceQwery = "DROP SEQUENCE \"" + TableName.ToUpper() + "_SEQ_ID\" ";
            string dropTriggerQwery = "DROP TRIGGER \"" + TableName.ToUpper() + "_AUTOINCREMENT\" ";
            OracleCommand dropTableCommand = new OracleCommand(dropTableQwery, oracleConnection);
            OracleCommand dropSequenceCommand = new OracleCommand(dropSequenceQwery, oracleConnection);
            OracleCommand dropTriggerCommand = new OracleCommand(dropTriggerQwery, oracleConnection);
            try
            {
                dropTableCommand.ExecuteNonQuery();
                dropSequenceCommand.ExecuteNonQuery();
                dropTriggerCommand.ExecuteNonQuery();
            }
            catch(Exception h){}
            return true;
        }

        public bool DeleteItem(decimal itemId)
        {
            string SqlCommand = string.Empty;
            SqlCommand = "DELETE FROM \"SYSTEM\".\"" + Table.TableName.ToUpper() + "\" WHERE \"ID\" = " + itemId;
            OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
            OracleTriggerCommand.ExecuteNonQuery();

            return true;
        }

        public bool FillTable(decimal limit)
        {
            string SqlCommand = string.Empty;
            SqlCommand = "SELECT * FROM \"SYSTEM\".\"" + Table.TableName.ToUpper() + "\" ";
            OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
            try
            {
                OracleDataReader dr = OracleTriggerCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr[2].ToString()))
                        {
                            DataRow dataRow = Table.NewRow();
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                dataRow[i] = dr[i];
                            }
                            Table.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch(Exception r){}
            

            return true;
        }

        public bool InsertItem(List<object> values)
        {
            
            string SqlCommand = string.Empty;
            SqlCommand = "INSERT INTO \"SYSTEM\".\"" + Table.TableName.ToUpper() + "\" (";

            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
			{
                if (Table.Columns[i].ColumnName.ToUpper() != "ID")
                {
                    SqlCommandColumns += ", \"" + Table.Columns[i].ColumnName + "\"";
                }
			}
            if (SqlCommandColumns.Length>2)
            {
                SqlCommandColumns = SqlCommandColumns.Substring(2);
            }
            else
            {
                return false;
            }
            SqlCommand += SqlCommandColumns;

            SqlCommand += ") VALUES (";

            String SqlCommandValues = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
			{
                if (Table.Columns[i].ColumnName.ToUpper()!="ID")
                {
                    SqlCommandValues += ", '" + values[i] + "'";
                }
			}
            if (SqlCommandValues.Length>2)
            {
                SqlCommandValues = SqlCommandValues.Substring(2);
            }

            SqlCommand += SqlCommandValues;

            SqlCommand += ")";

            OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
            OracleTriggerCommand.ExecuteNonQuery();

            return true;
        }

        public void UpdateItem(List<object> values)
        {
            string SqlCommand = string.Empty;
            SqlCommand = "UPDATE \"SYSTEM\".\"" + Table.TableName.ToUpper() + "\" SET ";

            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
            {
                if (Table.Columns[i].ColumnName.ToUpper() != "ID")
                {
                    SqlCommandColumns += ", \"" + Table.Columns[i].ColumnName + "\" = '" + values[i] + "' ";
                }
            }
            if (SqlCommandColumns.Length > 2)
            {
                SqlCommandColumns = SqlCommandColumns.Substring(2);
            }
            else
            {
                return;// false;
            }

            SqlCommand += SqlCommandColumns;

            SqlCommand += " WHERE \"ID\" = " + values[0];

            OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
            OracleTriggerCommand.ExecuteNonQuery();

           // return true;
        }

        public List<DataRow> GetObjectByIDs(List<decimal> rowIDs)
        {
            List<DataRow> wantedRoews = new List<DataRow>();
            foreach(int ID in rowIDs)
            {
                string SqlCommand = string.Empty;
                SqlCommand = "SELECT * FROM \"SYSTEM\".\"HEAD\" WHERE \"ID\" = " + ID + " ";
                OracleCommand OracleTriggerCommand = new OracleCommand(SqlCommand, oracleConnection);
                OracleDataReader dr = OracleTriggerCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataRow drr = Table.NewRow();
                        for (int i = 0; i < 5; i++)
                        {
                            if (!dr.IsDBNull(i))
                            {
                                if (Table.Columns[i].DataType == typeof(string))
                                {
                                    drr[i] = dr.GetString(i);
                                }
                                if (Table.Columns[i].DataType == typeof(decimal))
                                {
                                    drr[i] = dr.GetDecimal(i);
                                }
                            }
                        }
                        wantedRoews.Add(drr);
                    }
                }
            }
            return wantedRoews;
        }
    }
}
