using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1.SP_SQL
{
    class ProcedureCatalogue
    {
    static string db = "rpro0001_cabinet";
        /// <summary>
        /// Drop procedure if it exists
        /// </summary>
    public string dropCreateRelationAsNew = "DROP PROCEDURE IF EXISTS `" + db + "`.`CreateRelationAsNew`;";
        /// <summary>
        /// Create insertion procedure to demonstrate IN parameters
        /// </summary>
    public string procCreateRelationAsNew = "CREATE PROCEDURE `" + db + "`.`CreateRelationAsNew`" +
            "(IN usr varchar(60), IN dsply varchar(250)) " +
        "BEGIN " +
            "insert into `test`.`kentest` (`ID`,`login`,`name`,`latest_acc`) " +
            "values (NULL, usr, dsply, NULL); " +
        "END;";
        /// <summary>
        /// Class to handle the spInsert stored procedure
        /// </summary>
        public Procedure CreateRelationAsNew;

        /// <summary>
        /// Drop procedure if it exists
        /// </summary>
        public string dropInitializeTable = "DROP PROCEDURE IF EXISTS `" + db + "`.`CreateTable`;";
        /// <summary>
        /// Create insertion procedure to demonstrate IN parameters
        /// </summary>
        public string createInitializeTable = "CREATE PROCEDURE `" + db + "`.`CreateTable`" +
            "(IN tableName varchar(60)) " +
        "BEGIN " +
            "CREATE TABLE IF NOT EXISTS `"+db+"`.tableName ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , PRIMARY KEY (`ID`) ); " +
            "INSERT INTO `"+db+"`.tableName (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (0,0,0,0,0,0) ;" +
        "values (NULL, usr, dsply, NULL); " +
        "END;";
        /// <summary>
        /// Class to handle the spInsert stored procedure
        /// </summary>
        public Procedure CreateTable;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string MyCreateTable(string tableName)
        {
            // Add the parameters
            CreateTable.Add("?usr", "path\\to\\ken\'s\\data");
            return CreateTable.Execute();  // execute the stored procedure
        }

        #region Methods
        /// <summary>
        /// Execute an SQL string - not a procedure
        /// Calls to this function use SQL that requires no parameters
        /// This is used for dropping and creating tables
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <returns>"OK" or an error message</returns>
        public string ExecSql(string sql)
        {
            MySqlConnection conn = new MySqlConnection(ConnectString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                conn.Open();
                // Return value is meaningless with this procudure so ignore it
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                // return the mysql error message
                // the caller can put it in a messagebox
                return ex.Message;
            }
            finally
            {
                conn.Close();  // always close the connection
            }
            return "OK";
        }
        #endregion
        #region
        /// <summary>
        /// Variables from the stored procedures
        /// </summary>
        private string _connect;   // save the MySql connect string
        public Int64 lastinsertid;
        public string newval = "";
        #endregion
        #region Properties
        /// <summary>ConnectString getter and setter property</summary>
        public string ConnectString
        {
            get { return _connect; }
            set { _connect = value; }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor with connection string
        /// Builds 5 classes for the stored procedures.
        /// </summary>
        /// <param name="connectString">MySql connection string</param>
        public ProcedureCatalogue(string connectString)
	    {
            ConnectString = connectString; // save it
            // Make one object per stored procedure
            //CreateTable(TableName);
            //InitializeTable = new Procedure("CreateTable", ConnectString);
        }
        #endregion

/*CREATE TABLE IF NOT EXISTS `rpro0001_cabinet`.`s" + symbolPosition[0].ToString("00000000") + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `a` INT NULL , `b` INT NULL , `c` INT NULL , `d` INT NULL , `e` INT NULL , `f` INT NULL , PRIMARY KEY (`ID`) );"
        INSERT INTO `rpro0001_cabinet`.`s" + symbolPosition[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (0,0,0,0,0,0) ;"
        
        INSERT INTO `rpro0001_cabinet`.`s" + b[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + b[0] + ", " + b[1] + "," + r[2] + ", " + r[3] + "," + a[0] + ", " + a[1] + " ) ;
        INSERT INTO `rpro0001_cabinet`.`s" + a[0].ToString("00000000") + "` (`a`, `b`, `c`, `d`, `e`, `f`) VALUES (" + a[0] + ", " + a[1] + ", " + r[0] + ", " + r[1] + ", " + b[0] + ", " + b[1] + " ) ;
*/    }
}
