using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Updates whether a furniture listing has been sold.
    /// </summary>
    public class SellFurniture : ISellFurniture
    {
        public void ChangeSoldStatus(Furniture value)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "update furniture set furniture_sold = @sold where furniture_id = @id";
                command.Parameters.AddWithValue("@sold", value.Sold);
                command.Parameters.AddWithValue("@id", value.Id);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update sold status: {ex.Message}");
            }
        }
    }
}
