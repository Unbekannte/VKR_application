using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace VKR_FlangeCoupling
{
    class GOST_Provider
    {
        MySqlConnection connection = new MySqlConnection();


        string DB_login = Properties.Settings.Default.DB_login;
        string DB_password = Properties.Settings.Default.DB_password;
        string DB_database_name = Properties.Settings.Default.DB_database_name;
        string DB_server = Properties.Settings.Default.DB_server;
        string DB_port = Properties.Settings.Default.DB_port;

        public GOST_Provider(/*string server = "localhost", string port = "3306", string user = "root", string pass = "root", string database = "gosts"*/)
        {
            try
            {
                
                string connStr = "server=" +DB_server +
                             ";user=" + DB_login +
                             ";database=" + DB_database_name +
                             ";port=" + DB_port +
                             ";password=" + DB_password + ";";
                
                connection.ConnectionString = connStr;
                connection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Не удалось подключиться к СУБД");
                //throw;
            }
            
        }

        private string check_connect(string server = "localhost", string port = "3306", string user = "root", string pass = "root", string database = "gosts")
        {
            string connStr = "server=" + server +
                             ";user=" + user +
                             ";database=" + database +
                             ";port=" + port +
                             ";password=" + pass + ";";
            if (connStr == connection.ConnectionString)
            {
                
            }
            return "";
        }

        public DataTable get_rows(string gost_number, string table_number, string columns = "*", string parameters = "")
        {

            string table = "gosts.gost_" + gost_number.Replace('-', '_') + "_table_" + table_number.ToLower();
            
            parameters = parameters.Replace(", ", " AND ");

            string query = "SELECT " + columns + " FROM " + table;

            if (!string.IsNullOrEmpty(parameters))
            {
                query += " WHERE " + parameters;
            }
            query += ";";
            

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            return dt;

        }

        public List<string> get_one_column_as_List(string gost_number, string table_number, string column = "id", string parameters = "")
        {
            string table = "gosts.gost_" + gost_number.Replace('-', '_') + "_table_" + table_number.ToLower();

            parameters = parameters.Replace(", ", " AND ");

            string query = "SELECT " + column + " FROM " + table;

            if (!string.IsNullOrEmpty(parameters))
            {
                query += " WHERE " + parameters;
            }
            query += ";";


            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            List<DataRow> dataRowsList = dt.AsEnumerable().ToList();
            List<string> strList = new List<string>();

            foreach (var row in dataRowsList)
            {
               strList.Add(row[column].ToString());
            }

            return strList;
        }

        //взаимствовано с stackowerflow
        /// <summary>
        /// Привести лист к таблице
        /// </summary>
        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
