using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Inserts a new user account.
    /// </summary>
    public class PostAccount : IPostAccount
    {
        void IPostAccount.PostAccount(Account value)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText =
                    @"insert into accounts(account_id, account_username, account_email, account_password, account_admin)
                      values(@id, @username, @email, @password, @admin)";
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@username", value.Username);
                command.Parameters.AddWithValue("@email", value.Email);
                command.Parameters.AddWithValue("@password", value.Password);
                command.Parameters.AddWithValue("@admin", value.Admin);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add account: {ex.Message}");
            }
        }
    }
}
