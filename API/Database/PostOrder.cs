using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    /// <summary>
    /// Inserts a new pickup order. The furniture and buyer IDs are checked
    /// against their tables so an order can't reference missing records.
    /// </summary>
    public class PostOrder : IPostOrder
    {
        void IPostOrder.PostOrder(OrderForm value)
        {
            try
            {
                using var connection = new MySqlConnection(new ConnectionString().cs);
                connection.Open();

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText =
                    @"insert into orderform(order_id, furniture_id, buyer_id, pickup_date, price,
                                            order_fname, order_lname, order_location, order_phone)
                      values(@orderid,
                             (select furniture_id from furniture where furniture_id = @furnid limit 1),
                             (select account_id from accounts where account_id = @buyerid limit 1),
                             @date, @price, @fname, @lname, @location, @phone)";
                command.Parameters.AddWithValue("@orderid", value.Id);
                command.Parameters.AddWithValue("@furnid", value.FurnitureId);
                command.Parameters.AddWithValue("@buyerid", value.BuyerId);
                command.Parameters.AddWithValue("@date", value.PickupDate);
                command.Parameters.AddWithValue("@price", value.Price);
                command.Parameters.AddWithValue("@fname", value.FName);
                command.Parameters.AddWithValue("@lname", value.LName);
                command.Parameters.AddWithValue("@location", value.Location);
                command.Parameters.AddWithValue("@phone", value.Phone);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add order: {ex.Message}");
            }
        }
    }
}
