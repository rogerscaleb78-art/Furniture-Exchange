using System.Data;
using API.Models;

namespace API.Database
{
    /// <summary>
    /// Converts database rows into model objects. Keeping the column order in
    /// one place means the "get all" and "get by id" queries can't drift apart,
    /// and lets the mapping be unit tested without a live database.
    /// </summary>
    public static class RecordMappers
    {
        // furniture table: furniture_id, type, quality, city, sold, price, image, seller_id
        public static Furniture ToFurniture(IDataRecord record)
        {
            return new Furniture()
            {
                Id = record.GetInt32(0),
                Type = record.GetString(1),
                Quality = record.GetString(2),
                City = record.GetString(3),
                Sold = record.GetBoolean(4),
                Price = record.GetInt32(5),
                Image = record.GetString(6),
                SellerId = record.GetInt32(7)
            };
        }

        // orderform table: order_id, furniture_id, buyer_id, pickup_date, price,
        // order_fname, order_lname, order_location, order_phone
        public static OrderForm ToOrderForm(IDataRecord record)
        {
            return new OrderForm()
            {
                Id = record.GetInt32(0),
                FurnitureId = record.GetInt32(1),
                BuyerId = record.GetInt32(2),
                PickupDate = record.GetDateTime(3),
                Price = record.GetInt32(4),
                FName = record.GetString(5),
                LName = record.GetString(6),
                Location = record.GetString(7),
                Phone = record.GetString(8)
            };
        }

        // accounts table: account_id, username, email, password, admin
        public static Account ToAccount(IDataRecord record)
        {
            return new Account()
            {
                Id = record.GetInt32(0),
                Username = record.GetString(1),
                Email = record.GetString(2),
                Password = record.GetString(3),
                Admin = record.GetBoolean(4)
            };
        }
    }
}
