// Copyright (C) 2009 Ken Jones
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as published by
// the Free Software Foundation
//
// There are special exceptions to the terms and conditions of the GPL 
// as it is applied to this software. View the full text of the 
// exception in file EXCEPTIONS in the directory of this software 
// distribution.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using MySql.Data;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1.SP_SQL
{
    /// <summary>
    /// Class to assist in executing and accessing
    /// MySql Stored procedures.
    /// This class is a helper for a single stored procedure.
    /// There should be one of these built for each procedure to be accessed.
    /// </summary>
    public class Procedure
    {
        #region Data
        private string _connstr;
        private string _procName;
        private string  _drop;

	
        private MySqlConnection _conn;
        private MySqlCommand _cmd;

	
        #endregion
        #region Properties
        /// <summary>Sql to drop the stored procedure</summary>
	    public string  DropString
	    {
		    get { return _drop;}
		    set { _drop = value;}
	    }
        /// <summary>The MySql command structure</summary>
        public MySqlCommand cmd
        {
            get { return _cmd; }
            set { _cmd = value; }
        }
        /// <summary>Name of the stored procedure to execute</summary>
        public string ProcName
        {
            get { return _procName; }
            set { _procName = value; }
        }
        /// <summary>The one and only connection for this procedure</summary>
        public MySqlConnection conn
        {
            get { return _conn; }
            set { _conn = value; }
        }
        /// <summary>MySql connection string</summary>
        public string ConnectString
        {
            get { return _connstr; }
            set { _connstr = value; }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor - One for each stored procedure.
        /// </summary>
        /// <param name="procedurename">The name of the stored procedure</param>
        /// <param name="connectstring">The connection string</param>
        public Procedure(string procedurename,string connectstring)
        {
            ProcName = procedurename;
            ConnectString = connectstring;
            conn = new MySqlConnection(ConnectString);
            cmd = new MySqlCommand(procedurename, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            DropString = "DROP PROCEDURE IF EXISTS `" + procedurename + "`;";
        }
        /// <summary>
        /// Constructor - Made private to disallow. Must have parameters
        /// </summary>
        private Procedure()
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add a parameter to the command parameter array.
        /// This adds an IN parameter. The value must not be null.
        /// </summary>
        /// <param name="name">The parameter name such as "?ID"</param>
        /// <param name="val">The value to set can be any data type</param>
        public void Add(string name, Object val)
        {
            cmd.Parameters.AddWithValue(name, val);
            cmd.Parameters[name].Direction = ParameterDirection.Input;
        }
        /// <summary>
        /// Add an OUT parameter to the command parameter array.
        /// The value passed in can be null but it MUST be of the same data type
        /// as the expected output. Use the name of the field that will be
        /// assigned after the procedure executes.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="typ">The database data type</param>
        public void AddOut(string name, MySqlDbType typ)
        {
            //Type ty = val.GetType();
            cmd.Parameters.Add(new MySqlParameter(name, typ));
            cmd.Parameters[name].Direction = ParameterDirection.Output;
        }
        /// <summary>
        /// Add an INOUT parameter. The value must not be null.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="val">The value to be sent and the field to be returned</param>
        public void AddInOut(string name, Object val)
        {
            cmd.Parameters.AddWithValue(name, val);
            cmd.Parameters[name].Direction = ParameterDirection.InputOutput;
        }
        /// <summary>
        /// Create the stored procedure
        /// </summary>
        /// <param name="sql">SQL to create the procedure</param>
        /// <returns>"OK" or an error message</returns>
        public string Create(string sql)
        {
            return ExecSql(sql);
        }
        /// <summary>
        /// Drop the stored procedure
        /// </summary>
        /// <returns>"OK" or an error message</returns>
        public string Drop()
        {
            return ExecSql(DropString);
        }
        /// <summary>
        /// Execute an SQL string - not a procedure
        /// Calls to this function use SQL that requires no parameters
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
        /// <summary>
        /// Execute the stored procedure
        /// </summary>
        /// <returns>"OK" or an error message</returns>
        public string Execute()
        {
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return "OK";
        }
        /// <summary>
        /// Get an OUT or INOUT parameter from the parameter array
        /// after execution.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <returns>The requested object or null for failure</returns>
        public Object Get(string name)
        {
            if (cmd.Parameters[name].Value != null)
                return cmd.Parameters[name].Value;
            return null;
        }
        #endregion
    }   // end class
}
