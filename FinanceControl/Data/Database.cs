using Microsoft.Data.Sqlite;

namespace FinanceControl.Data
{
    public static class Database
    {
        private const string ConnectionString = "Data Source=finance.db";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Type TEXT,
                    Description TEXT,
                    Value REAL,
                    Date TEXT
                )";
            tableCmd.ExecuteNonQuery();
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
