using System.Data.SqlClient;

namespace layeredarchitecturedemo.Utility
{
    public static class SqlServerConnectionManager
    {
        public static SqlConnection OpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
