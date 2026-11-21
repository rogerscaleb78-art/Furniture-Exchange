using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Inserts a new furniture listing. The seller ID is checked against the
    /// accounts table so a listing can't reference a missing seller.
    /// </summary>
    public class PostFurniture : IPostFurniture
    {
        void IPostFurniture.PostFurniture(Furniture value)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText =
                    @"insert into furniture(furniture_id, furniture_type, furniture_quality, furniture_city,
                                            furniture_sold, furniture_price, furniture_image, furniture_seller_id)
                      values(@id, @type, @quality, @city, @sold, @price, @image,
                             (select account_id from accounts where account_id = @sellid limit 1))";
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@type", value.Type);
                command.Parameters.AddWithValue("@quality", value.Quality);
                command.Parameters.AddWithValue("@city", value.City);
                command.Parameters.AddWithValue("@sold", value.Sold);
                command.Parameters.AddWithValue("@price", value.Price);
                command.Parameters.AddWithValue("@image", value.Image);
                command.Parameters.AddWithValue("@sellid", value.SellerId);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add furniture: {ex.Message}");
            }
        }
    }
}
