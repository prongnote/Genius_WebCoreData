using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Genius_WebCoreData.App_Class {
    public class ClassDB {

        private MySqlConnection sqlConnection;
        private bool isConnection = false ;

        public ClassDB() {
            sqlConnection = getConnection(); 
        }
        public MySqlConnection getConnection() {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "genius_core_data";
            string username = "genius";
            string password = "123456";

            return getDBConnection(host, port, database, username, password);
        }

        public bool IsConnect() {
            if (isConnection == false) {
                return false;                
            }
            else {
                return true;
            }
           
        }


        public MySqlConnection getDBConnection(string host, int port, string database, string username, string password) {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            try {                
                conn.Open();
                isConnection = true;
            }
            catch(MySqlException ex) {
                Debug.WriteLine(ex);
                isConnection = false ;
            }

            return conn;
        }

        public string selectOnceData(string table , string field , string where) {
            string sql = "SELECT "+field+" FROM "+table+" WHERE "+where ; 
            DataTable dataTable = this.returnDataTable(sql);

            if (dataTable.Rows.Count == 1) {
                return dataTable.Rows[0][0].ToString(); 
            }
            else {
                return ""; 
            }

        }
        public void insertData(Dictionary<string, string> fields, string tableName) {
            this.insertUpdateData(fields ,tableName , "INSERT"); 
        }

        public void updateData(Dictionary<string, string> fields, string tableName , string where) {
            this.insertUpdateData(fields, tableName, "UPDATE" , where);
        }

        public String nextNumber(string fieldName, string tableName, string whereSql) {

            string sql = "SELECT MAX(" + fieldName + ") + 1 as nextNumber FROM " + tableName + "  ";
            if (!whereSql.Equals("")) {
                sql += " WHERE " + whereSql;
            }
            DataRow dataRow = this.returnDataRow(sql);
           
            if (string.IsNullOrEmpty(dataRow["nextNumber"].ToString()) || dataRow["nextNumber"].Equals("")) {
                return "1";
            }
            else { return dataRow["nextNumber"].ToString(); }
        }

        private void insertUpdateData(Dictionary<string, string> fields, string tableName , string type , string whereSql = "" ) {
            StringBuilder sql = new StringBuilder();         

            if (type.Equals("INSERT") || type.Equals("UPDATE")) {
                sql.Append(type + "  " + tableName + " SET ");
                int countField = 0;
                foreach (KeyValuePair<string, string> pairKV in fields) {

                    if (countField > 0) sql.Append(" , ");
                    if (!string.IsNullOrEmpty(pairKV.Value) && pairKV.Value.Trim().ToUpper().IndexOf("to_date".ToUpper()) > -1) {
                        sql.Append(pairKV.Key + "=" + pairKV.Value.ToString() + "");
                    }
                    else {
                        sql.Append(pairKV.Key + "='" + pairKV.Value + "'");
                    }
                    countField++;
                }
                if (!whereSql.Equals("")) {
                    sql.Append(" WHERE " + whereSql);
                }
                Debug.WriteLine("----->" + sql);
                this.executeCommand(sql.ToString());
                sql.Clear();
            }
            

        }

        public void executeCommand(string sql) {

            using (MySqlCommand mySqlCommand = new MySqlCommand(sql, sqlConnection)) {
                using (var msdr = mySqlCommand.ExecuteReader()) {                 
                } 

            }  
            /*
            using (MySqlCommand mySqlCommand = new MySqlCommand(sql, sqlConnection))
            using (reader = mySqlCommand.ExecuteReader()) {

                //do stuff.....
            }
            */
            //mySqlCommand.ExecuteReader();


        }

        private DataRow returnDataRow(string sql) {
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, sqlConnection);
            adapter.SelectCommand.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count == 1) {
                return dataTable.Rows[0];
            }
            else {
                return null; 
            }
        }

        private DataTable returnDataTable(string sql) {
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, sqlConnection);

            adapter.SelectCommand.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;

        }

        public DataTable selectDataTable(string sql) {
            return this.returnDataTable(sql);            
        }

        public DataTable getDataTable(string sql) {
            return this.returnDataTable(sql);
        }


    }
}