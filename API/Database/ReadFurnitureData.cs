using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Reads furniture listings from the database.
    /// </summary>
    public class ReadFurnitureData : IGetAllFurniture, IGetFurniture
    {
        public List<Furniture> GetAllFurniture()
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from furniture", connection);
                using MySqlDataReader reader = command.ExecuteReader();

                var allFurniture = new List<Furniture>();
                while (reader.Read())
                {
                    allFurniture.Add(RecordMappers.ToFurniture(reader));
                }

                return allFurniture;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read furniture: {ex.Message}");
                return null;
            }
        }

        public Furniture GetFurniture(int id)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from furniture where furniture_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Prepare();

                using MySqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                return RecordMappers.ToFurniture(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read furniture {id}: {ex.Message}");
                return null;
            }
        }
    }
}
