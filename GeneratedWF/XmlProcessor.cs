using System.Data;
using System.Reflection;
using System.Text;

namespace GeneratedWF
{
    class XmlProcessor
    {
        /*
        public void XmlWriter(object source)
        {
            var x = new System.Xml.Serialization.XmlSerializer(source.GetType());
            x.Serialize(new XmlTextWriter("mainXML", Encoding.UTF8),  source);
        }

        public void XmlReader(object source)
        {
            var x = new System.Xml.Serialization.XmlSerializer(source.GetType());
            x.Serialize(new XmlTextWriter("mainXML", Encoding.UTF8), source);
        }
        */

        internal string XmlSyncronixe(ITableAdaptor dataTableAdaptor)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\n";
            xml += "<Data>\n";
            xml += ToXML(dataTableAdaptor, -1, "\t");
            xml += "</Data>";
            const string xmlPath = "..\\";
            var file = new System.IO.StreamWriter(xmlPath + "XML.XML") {AutoFlush = true};
            file.WriteLine(xml);
            file.Close();
            return xml;
        }


        private string ToXML(ITableAdaptor dataTableAdaptor, int id,string tab)
        {
            var sb = new StringBuilder();
            DataTable dataTable =  dataTableAdaptor.GetTable();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (id > -1)
                {
                    int currentId;
                    int.TryParse(dataTable.Rows[i].ItemArray[0].ToString(), out currentId);
                    if (currentId != id)
                    {
                        continue;
                    }
                }
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (j>0 && dataTable.Columns[j].DataType == typeof (decimal))
                    {
                        sb.Append(tab+"<" + dataTable.Columns[j].ColumnName + ">\n");
                
                        object objo =
                            dataTableAdaptor.GetItem().UnderlyingSystemType.GetProperty(
                                dataTable.Columns[j].ColumnName);
                        if (objo != null)
                        {
                            ITableAdaptor dataTableAdaptorChild =
                                new MySqlTableAdaptor(((PropertyInfo) objo).PropertyType);
                            int idToFind;
                            int.TryParse(dataTable.Rows[i].ItemArray[j].ToString(), out idToFind);
                            string childXml = ToXML(dataTableAdaptorChild, idToFind,tab+"\t");
                            sb.Append(childXml);

                        }
                        sb.Append(tab+"</" + dataTable.Columns[j].ColumnName + ">\n");
                    }
                    else
                    {
                        sb.Append(tab+"<" + dataTable.Columns[j].ColumnName + ">" + dataTable.Rows[i].ItemArray[j] + "</" + dataTable.Columns[j].ColumnName + ">\n");
                
                    }
                }
            }

            string result = sb.ToString();
            return result;
        }
    }
}
