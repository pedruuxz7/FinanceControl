using FinanceControl.Data;
using FinanceControl.Models;
using Microsoft.Data.Sqlite;

namespace FinanceControl.Services
{
    public class TransactionService
    {
        public void AddTransaction(Transaction transaction)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO Transactions (Type, Description, Value, Date)
                VALUES ($type, $desc, $value, $date)";
            insertCmd.Parameters.AddWithValue("$type", transaction.Type);
            insertCmd.Parameters.AddWithValue("$desc", transaction.Description);
            insertCmd.Parameters.AddWithValue("$value", transaction.Value);
            insertCmd.Parameters.AddWithValue("$date", transaction.Date.ToString("yyyy-MM-dd"));
            insertCmd.ExecuteNonQuery();
        }

        public List<Transaction> GetAll()
        {
            var list = new List<Transaction>();
            using var connection = Database.GetConnection();
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Transactions";
            using var reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Transaction
                {
                    Id = reader.GetInt32(0),
                    Type = reader.GetString(1),
                    Description = reader.GetString(2),
                    Value = reader.GetDouble(3),
                    Date = DateTime.Parse(reader.GetString(4))
                });
            }
            return list;
        }

        public double GetBalance()
        {
            var transactions = GetAll();
            return transactions.Sum(t => t.Type == "Receita" ? t.Value : -t.Value);
        }
    }
}
