using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect
{
    //[DefaultProperty("Connector")]
    public class DBConnector
    {
        private static DBConnector _Connector;
        public static DBConnector Connector
        {
            get
            {
                if (_Connector != null)
                    return _Connector;
                throw new Exception("Подключение к серверу базы данных не было инициализировано");
            }
            set { _Connector = value; }
        }

        public string ConnectionString { get; set; }
        private SqlConnection MSSQLConnection;
        public Dictionary<string, object> Parameters;
        public MSSQLQueries CommonQueries;

        public DBConnector()
        {
            CommonQueries = new MSSQLQueries();
        }
        public DBConnector(string connectionString)
        {
            CommonQueries = new MSSQLQueries();
            ConnectionString = connectionString;
        }

        public object this[string paramName]
        {
            get
            {
                if (Parameters.ContainsKey(paramName))
                    return Parameters[paramName];
                return null;
            }
            set
            {
                Parameters[paramName] = value;
            }
        }

        public void OpenConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("Не указана строка поединения с базой (ConnectionString)");

            MSSQLConnection = new SqlConnection(ConnectionString);
            MSSQLConnection.Open();

            Parameters = new Dictionary<string, object>();
        }

        public void CloseConnection()
        {
            MSSQLConnection.Close();
        }

        public void ClearParameters()
        {
            Parameters.Clear();
        }

        private void PrepareCommandParameters(SqlCommand command)
        {
            foreach (var pair in Parameters)
                command.Parameters.AddWithValue(pair.Key, pair.Value);
        }

        public void ExecuteQuery(string queryString)
        {
            SqlCommand command = new SqlCommand(queryString, MSSQLConnection);
            PrepareCommandParameters(command);
            command.ExecuteNonQuery();
            ClearParameters();
        }

        public void ExecuteStoredProcedure(string procedureName)
        {
            SqlCommand command = new SqlCommand(procedureName, MSSQLConnection);
            command.CommandType = CommandType.StoredProcedure;
            PrepareCommandParameters(command);
            command.ExecuteNonQuery();
            ClearParameters();
        }

        public DataTable OpenQuery(string queryString)
        {
            SqlCommand command = new SqlCommand(queryString, MSSQLConnection);
            PrepareCommandParameters(command);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataAdapter.Dispose();
            ClearParameters();
            return dataTable;
        }

        public DataTable OpenStoredProcedure(string procedureName)
        {
            SqlCommand command = new SqlCommand(procedureName, MSSQLConnection);
            command.CommandType = CommandType.StoredProcedure;
            PrepareCommandParameters(command);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataAdapter.Dispose();
            ClearParameters();
            return dataTable;
        }

        public DataTable OpenTable(string tableName)
        {
            SqlCommand command = new SqlCommand(string.Format("SELECT * FROM [{0}]", tableName), MSSQLConnection);
            PrepareCommandParameters(command);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataAdapter.Dispose();
            ClearParameters();
            return dataTable;
        }

    }
}
