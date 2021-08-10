using System;
using System.Data.SqlClient;

namespace WebShell.Data
{
    public class SqlServerContext : IDisposable
    {
        private SqlConnection _connection { get; set; }

        public SqlServerContext(string connectionString)
        {
            CreateDatabaseIfNotExists();
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            CreateInstructionsTable();
        }

        public SqlDataReader ExecuteReader(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, _connection);
            return sqlCommand.ExecuteReader();
        }

        public int ExecuteNonQuery(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, _connection);
            return sqlCommand.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        private void CreateDatabaseIfNotExists()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "IF NOT EXISTS (" +
                "SELECT * FROM master.dbo.sysdatabases " +
                "WHERE name = 'WebShellDB') " +
                "BEGIN CREATE DATABASE WebShellDB END;",
                sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void CreateInstructionsTable()
        {
            SqlCommand sqlCommand = new SqlCommand(
                "IF NOT EXISTS (" +
                "SELECT * FROM information_schema.tables WHERE table_name = 'Instructions') " +
                "BEGIN CREATE TABLE Instructions(" +
                "Id BIGINT IDENTITY(1,1) NOT NULL, " +
                "Content TEXT NOT NULL) END;",
                _connection);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
