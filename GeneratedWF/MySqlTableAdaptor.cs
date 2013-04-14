using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace GeneratedWF
{
    public class MySqlTableAdaptor : ITableAdaptor
    {
        public DataTable Table = null;
        public Type Item = null;
        private MySqlDataReader dr;
        List<string> automaticColumns = new List<string>() { "ID", "NAME", "DESCRIPTION", "URL" };

        public DataTable GetTable()
        {
            return Table;
        }
        public Type GetItem()
        {
            return Item;
        }
        public void SetDataTable(DataTable table)
        {
            Table = table;
        }


        public MySqlTableAdaptor()
        {
            //mySqlConnection = new MySqlConnection("DATA SOURCE=;PERSIST SECURITY INFO=True;USER ID=SYSTEM; PASSWORD=Start1312");
            mySqlConnection = new MySqlConnection("Server=localhost;Port=3306;Uid=sykyo_test;Pwd=start1;");

            string sqlString = "CREATE DATABASE IF NOT EXISTS EraseMe";
            MySqlCommand MySqlCommand = new MySqlCommand(sqlString, mySqlConnection);
            mySqlConnection.Open();
            MySqlCommand.ExecuteNonQuery();

            mySqlConnection = new MySqlConnection("Server=localhost;Port=3306;Database=EraseMe;Uid=sykyo_test;Pwd=start1;");
            if (mySqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    mySqlConnection.Open();
                }
                catch (Exception e1)
                {
                    dr.Close();
                }
            }


        }
        public MySqlTableAdaptor(Type recordItemReference)
        {
            Item = recordItemReference;
            //mySqlConnection = new MySqlConnection("DATA SOURCE=;PERSIST SECURITY INFO=True;USER ID=SYSTEM; PASSWORD=Start1312");
            mySqlConnection = new MySqlConnection("Server=localhost;Port=3306;Uid=sykyo_test;Pwd=start1;");

            string sqlString = "CREATE DATABASE IF NOT EXISTS EraseMe";
            MySqlCommand MySqlCommand = new MySqlCommand(sqlString, mySqlConnection);
            mySqlConnection.Open();
            MySqlCommand.ExecuteNonQuery();

            mySqlConnection = new MySqlConnection("Server=localhost;Port=3306;Database=EraseMe;Uid=sykyo_test;Pwd=start1;");
            if (mySqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    mySqlConnection.Open();
                    Table = CheckTableExistence();
                    FillTable(100);
                }
                catch (Exception e1)
                {
                    if (dr !=null)
                    dr.Close();
                }
            }
        }

        MySqlConnection mySqlConnection = null;

        public DataTable CheckTableExistence()
        {

            if (dr != null) dr.Close();
            DataTable dataTable = new DataTable(((Type)Item).UnderlyingSystemType.Name);
            Type metainformation = Type.GetType("Object.Meta." + dataTable.TableName+ "Metainformation");

            string SqlCommand = string.Empty;
            SqlCommand = "SELECT * FROM information_schema.columns WHERE table_name='" + dataTable.TableName.ToUpper() + "' ";
            MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
            dr = MySqlTriggerCommand.ExecuteReader();
            if (!dr.HasRows)
            {
                dr.Close();
                dataTable = CreateTable(((Type)Item).UnderlyingSystemType.Name);
                dr = MySqlTriggerCommand.ExecuteReader();
            }
            else
            {
                dataTable.Columns.Clear();
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr.GetString("Column_Name").ToString()))
                    {
                        Type columnType = typeof(string);
                        string type = dr.GetString("Data_Type").ToString();
                        switch (type)
                        {
                            case "int": columnType = typeof(decimal); break;
                        }
                        DataColumn newColumn = new DataColumn(dr["Column_Name"].ToString(), columnType);
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
            dr.Close();
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
            string commandCreateTable = " CREATE TABLE IF NOT EXISTS `" + dataTable.TableName.ToUpper() + "` ( `ID` int(11) NOT NULL AUTO_INCREMENT, `NAME` varchar(30) NOT NULL ";
            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < CatTable.Columns.Count; i++)
			{
                if (!automaticColumns.Contains(CatTable.Columns[i].ColumnName.ToUpper()))
                SqlCommandColumns += ", `" + CatTable.Columns[i].ColumnName + "` INT(11) ";
			}
            SqlCommandColumns += ", `DESCRIPTION` varchar(300), `URL` varchar(300) ";

            commandCreateTable += SqlCommandColumns;
            commandCreateTable += ", `TIMESTAMP` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,  PRIMARY KEY (`ID`)) "; //\"ID\" NUMBER, \"HELP\" NUMBER;
            MySqlCommand MySqlTableCommand = new MySqlCommand(commandCreateTable, mySqlConnection);
            MySqlTableCommand.ExecuteNonQuery();
            
            return CatTable;
        }

        public bool DropTable(string TableName)
        {
            dr.Close();
            string dropTableQwery = "DROP TABLE `" + TableName.ToUpper() + "` ";
            //string dropSequenceQwery = "DROP SEQUENCE \"" + TableName.ToUpper() + "_SEQ_ID\" ";
            //string dropTriggerQwery = "DROP TRIGGER \"" + TableName.ToUpper() + "_AUTOINCREMENT\" ";
            MySqlCommand dropTableCommand = new MySqlCommand(dropTableQwery, mySqlConnection);
            //MySqlCommand dropSequenceCommand = new MySqlCommand(dropSequenceQwery, mySqlConnection);
            //MySqlCommand dropTriggerCommand = new MySqlCommand(dropTriggerQwery, mySqlConnection);
            try
            {
                dropTableCommand.ExecuteNonQuery();
                //dropSequenceCommand.ExecuteNonQuery();
                //dropTriggerCommand.ExecuteNonQuery();
            }
            catch(Exception h){}
            return true;
        }

        public bool DeleteItem(decimal itemId)
        {
            dr.Close();
            if (itemId < 0) return false;
            string SqlCommand = string.Empty;
            SqlCommand = "DELETE FROM `" + Table.TableName.ToUpper() + "` WHERE `ID` = " + itemId;
            MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
            MySqlTriggerCommand.ExecuteNonQuery();

            return true;
        }

        public bool FillTable(decimal limit)
        {
            dr.Close();
            string SqlCommand = string.Empty;
            SqlCommand = "SELECT * FROM `" + Table.TableName.ToUpper() + "` ";
            MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
            try
            {
                dr = MySqlTriggerCommand.ExecuteReader();
                Table.Rows.Clear();
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

        public int InsertItem(List<object> values)
        {
            dr.Close();
            string SqlCommand = string.Empty;
            SqlCommand = "INSERT INTO `" + Table.TableName.ToUpper() + "` (";

            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
			{
                if (Table.Columns[i].ColumnName.ToUpper() != "ID")
                    if (Table.Columns[i].ColumnName.ToUpper() != "TIMESTAMP")
                    {
                    SqlCommandColumns += ", `" + Table.Columns[i].ColumnName + "` ";
                }
			}
            if (SqlCommandColumns.Length>2)
            {
                SqlCommandColumns = SqlCommandColumns.Substring(2);
            }
            else
            {
                return -1;
            }
            SqlCommand += SqlCommandColumns;

            SqlCommand += ") VALUES (";

            String SqlCommandValues = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
			{
                if (Table.Columns[i].ColumnName.ToUpper() != "ID")
                    if (Table.Columns[i].ColumnName.ToUpper() != "TIMESTAMP")
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

            MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
            MySqlTriggerCommand.ExecuteNonQuery();
            int ID = (int)MySqlTriggerCommand.LastInsertedId;

            return ID;
        }

        public void UpdateItem(List<object> values)
        {
            dr.Close();
            string SqlCommand = string.Empty;
            SqlCommand = "UPDATE `" + Table.TableName.ToUpper() + "` SET ";

            String SqlCommandColumns = string.Empty;
            for (int i = 0; i < values.Count && i < Table.Columns.Count; i++)
            {
                if (Table.Columns[i].ColumnName.ToUpper() != "ID")
                    if (Table.Columns[i].ColumnName.ToUpper() != "TIMESTAMP")
                    {
                    SqlCommandColumns += ", `" + Table.Columns[i].ColumnName + "` = '" + values[i] + "' ";
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

            SqlCommand += " WHERE `ID` = " + values[0];

            MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
            MySqlTriggerCommand.ExecuteNonQuery();

           // return true;
        }

        public List<DataRow> GetObjectByIDs(List<decimal> rowIDs)
        {
            dr.Close();
            List<DataRow> wantedRoews = new List<DataRow>();
            foreach(int ID in rowIDs)
            {
                string SqlCommand = string.Empty;
                SqlCommand = "SELECT * FROM `HEAD` WHERE `ID` = " + ID + " ";
                MySqlCommand MySqlTriggerCommand = new MySqlCommand(SqlCommand, mySqlConnection);
                dr = MySqlTriggerCommand.ExecuteReader();
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
