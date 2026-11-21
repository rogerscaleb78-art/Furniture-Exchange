using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Deletes a furniture listing. Orders for that listing are deleted first
    /// so the foreign key on orderform doesn't block the delete.
    /// </summary>
    public class DeleteData : IDeleteData
    {
        void IDeleteData.DeleteData(int id)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);

                command.CommandText = "delete from orderform where furniture_id = @id";
                command.ExecuteNonQuery();

                command.CommandText = "delete from furniture where furniture_id = @id";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete furniture {id}: {ex.Message}");
            }
        }
    }
}
