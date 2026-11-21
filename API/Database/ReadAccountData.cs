using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Reads user accounts from the database.
    /// </summary>
    public class ReadAccountData : IGetAllAccounts, IGetAccount
    {
        public List<Account> GetAllAccounts()
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from accounts", connection);
                using MySqlDataReader reader = command.ExecuteReader();

                var allAccounts = new List<Account>();
                while (reader.Read())
                {
                    allAccounts.Add(RecordMappers.ToAccount(reader));
                }

                return allAccounts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read accounts: {ex.Message}");
                return null;
            }
        }

        public Account GetAccount(int id)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from accounts where account_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Prepare();

                using MySqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                return RecordMappers.ToAccount(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read account {id}: {ex.Message}");
                return null;
            }
        }
    }
}
