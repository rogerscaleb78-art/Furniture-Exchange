using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Reads pickup orders from the database.
    /// </summary>
    public class ReadOrderData : IGetAllOrders, IGetOrder
    {
        public List<OrderForm> GetAllOrders()
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from orderform", connection);
                using MySqlDataReader reader = command.ExecuteReader();

                var allOrders = new List<OrderForm>();
                while (reader.Read())
                {
                    allOrders.Add(RecordMappers.ToOrderForm(reader));
                }

                return allOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read orders: {ex.Message}");
                return null;
            }
        }

        public OrderForm GetOrder(int id)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand("select * from orderform where order_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Prepare();

                using MySqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                return RecordMappers.ToOrderForm(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read order {id}: {ex.Message}");
                return null;
            }
        }
    }
}
