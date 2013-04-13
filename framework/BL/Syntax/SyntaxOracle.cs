using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPersistency.BL.Syntax
{
    public class SyntaxOracle : SyntaxInterface
    {
        string defaultDatabaseName = "System";

        /// <summary>
        /// Generate method for table creation
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public string GenerateMethodDropTable(string TableName, string DatabaseName)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                DatabaseName = defaultDatabaseName;
            }
            StringBuilder code = new StringBuilder();
            code.AppendFormat(Environment.NewLine + "        public string DropTable(string TableName, string DatabaseName)");
            code.AppendFormat(Environment.NewLine + "        {0}", "{");
            code.AppendFormat(Environment.NewLine + "                string commandString = \"{0}\";", DropTable(TableName, DatabaseName));
            code.AppendFormat(Environment.NewLine + "                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);");
            code.AppendFormat(Environment.NewLine + "                OracleCommand.ExecuteNonQuery();");
            code.AppendFormat(Environment.NewLine + "        {0}", "}");
            return code.ToString();
        }
        /// <summary>
        /// Generate method for table creation
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public string GenerateMethodCreateTable(string TableName, string DatabaseName)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                DatabaseName = defaultDatabaseName;
            }
            StringBuilder code = new StringBuilder();
            code.AppendFormat(Environment.NewLine + "        public string CreateTable(string TableName, string DatabaseName)");
            code.AppendFormat(Environment.NewLine + "        {0}", "{");
            code.AppendFormat(Environment.NewLine + "                string commandString = \"{0}\";", CreateTable(TableName, DatabaseName));
            code.AppendFormat(Environment.NewLine + "                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);");
            code.AppendFormat(Environment.NewLine + "                OracleCommand.ExecuteNonQuery();");

            code.AppendFormat(Environment.NewLine + "                string commandString = \"{0}\";", CreateSequence(TableName, DatabaseName));
            code.AppendFormat(Environment.NewLine + "                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);");
            code.AppendFormat(Environment.NewLine + "                OracleCommand.ExecuteNonQuery();");

            code.AppendFormat(Environment.NewLine + "                string commandString = \"{0}\";", CreateTrigger(TableName, DatabaseName));
            code.AppendFormat(Environment.NewLine + "                OracleCommand OracleCommand = new OracleCommand(commandString, OracleConnection);");
            code.AppendFormat(Environment.NewLine + "                OracleCommand.ExecuteNonQuery();");
            code.AppendFormat(Environment.NewLine + "        {0}", "}");
            return code.ToString();
        }

        /// <summary>
        /// Create table
        /// </summary>
        /// <param name="TableName">Table Name</param>
        /// <returns>Create table Syntax</returns>
        public string CreateTable(string TableName, string DatabaseName)
        {
            StringBuilder syntax = new StringBuilder();
            syntax.AppendFormat(" CREATE TABLE \\\"{0}\\\".\\\"{1}\\\" (", DatabaseName, TableName);
            syntax.AppendFormat("  \\\"ID\\\" NUMBER, ) ", DatabaseName, TableName);
            syntax.AppendFormat(" ) ", DatabaseName, TableName);
            return syntax.ToString();
        }


        /// <summary>
        /// Create table
        /// </summary>
        /// <param name="TableName">Table Name</param>
        /// <returns>Create table Syntax</returns>
        public string CreateSequence(string TableName, string DatabaseName)
        {
            StringBuilder syntax = new StringBuilder();
            syntax.AppendFormat(" CREATE SEQUENCE  \\\"{0}\\\".\\\"{1}_SEQ_ID\\\"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  ", DatabaseName, TableName);
            return syntax.ToString();
        }

        /// <summary>
        /// Create table
        /// </summary>
        /// <param name="TableName">Table Name</param>
        /// <returns>Create table Syntax</returns>
        public string CreateTrigger(string TableName, string DatabaseName)
        {
            StringBuilder syntax = new StringBuilder();
            syntax.AppendFormat(" CREATE OR REPLACE TRIGGER {1}_AUTOINCREMENT ", DatabaseName, TableName);
            syntax.AppendFormat(" BEFORE INSERT ON {1} ", DatabaseName, TableName);
            syntax.AppendFormat(" REFERENCING OLD AS OLD NEW AS NEW  ");
            syntax.AppendFormat(" FOR EACH ROW  ");
            syntax.AppendFormat(" WHEN (NEW.ID IS NULL)  ");
            syntax.AppendFormat(" BEGIN ");
            syntax.AppendFormat(" select {1}_SEQ_ID.NEXTVAL ", DatabaseName, TableName);
            syntax.AppendFormat(" INTO :NEW.ID FROM dual; ");
            syntax.AppendFormat(" END ; ");
            return syntax.ToString();
        }

        /// <summary>
        /// Drop Table
        /// </summary>
        /// <param name="TableName">Table Name</param>
        /// <returns>Drop Table Syntax</returns>
        public string DropTable(string TableName, string DatabaseName)
        {
            StringBuilder syntax = new StringBuilder();
            syntax.AppendFormat("DROP TABLE {0} ", TableName);
            return syntax.ToString();
        }
    }
}
