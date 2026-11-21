using System.Data;
using API.Database;

namespace API.Tests
{
    /// <summary>
    /// The mappers take an IDataRecord, so a DataTable can stand in for a
    /// MySQL result set and the tests need no database.
    /// </summary>
    public class RecordMappersTests
    {
        private static DataTableReader FurnitureRow()
        {
            var table = new DataTable();
            table.Columns.Add("furniture_id", typeof(int));
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("quality", typeof(string));
            table.Columns.Add("city", typeof(string));
            table.Columns.Add("sold", typeof(bool));
            table.Columns.Add("price", typeof(int));
            table.Columns.Add("image", typeof(string));
            table.Columns.Add("seller_id", typeof(int));
            table.Rows.Add(7, "Couch", "Like New", "Tuscaloosa", false, 250, "couch.jpg", 3);

            var reader = table.CreateDataReader();
            reader.Read();
            return reader;
        }

        [Fact]
        public void ToFurniture_MapsEveryColumn()
        {
            using var row = FurnitureRow();

            var furniture = RecordMappers.ToFurniture(row);

            Assert.Equal(7, furniture.Id);
            Assert.Equal("Couch", furniture.Type);
            Assert.Equal("Like New", furniture.Quality);
            Assert.Equal("Tuscaloosa", furniture.City);
            Assert.False(furniture.Sold);
            Assert.Equal(250, furniture.Price);
            Assert.Equal("couch.jpg", furniture.Image);
            Assert.Equal(3, furniture.SellerId);
        }

        [Fact]
        public void ToFurniture_DoesNotSwapQualityAndCity()
        {
            // Regression test: the "get by id" query used to read city and
            // quality from each other's columns.
            using var row = FurnitureRow();

            var furniture = RecordMappers.ToFurniture(row);

            Assert.NotEqual("Tuscaloosa", furniture.Quality);
            Assert.NotEqual("Like New", furniture.City);
        }

        [Fact]
        public void ToOrderForm_MapsEveryColumn()
        {
            var table = new DataTable();
            table.Columns.Add("order_id", typeof(int));
            table.Columns.Add("furniture_id", typeof(int));
            table.Columns.Add("buyer_id", typeof(int));
            table.Columns.Add("pickup_date", typeof(DateTime));
            table.Columns.Add("price", typeof(int));
            table.Columns.Add("order_fname", typeof(string));
            table.Columns.Add("order_lname", typeof(string));
            table.Columns.Add("order_location", typeof(string));
            table.Columns.Add("order_phone", typeof(string));
            var pickup = new DateTime(2024, 4, 20, 14, 30, 0);
            table.Rows.Add(12, 7, 5, pickup, 250, "Jane", "Doe", "Tuscaloosa", "2055550100");

            using var row = table.CreateDataReader();
            row.Read();

            var order = RecordMappers.ToOrderForm(row);

            Assert.Equal(12, order.Id);
            Assert.Equal(7, order.FurnitureId);
            Assert.Equal(5, order.BuyerId);
            Assert.Equal(pickup, order.PickupDate);
            Assert.Equal(250, order.Price);
            Assert.Equal("Jane", order.FName);
            Assert.Equal("Doe", order.LName);
            Assert.Equal("Tuscaloosa", order.Location);
            Assert.Equal("2055550100", order.Phone);
        }

        [Fact]
        public void ToAccount_MapsEveryColumn()
        {
            var table = new DataTable();
            table.Columns.Add("account_id", typeof(int));
            table.Columns.Add("username", typeof(string));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("password", typeof(string));
            table.Columns.Add("admin", typeof(bool));
            table.Rows.Add(5, "jdoe", "jdoe@example.com", "hashed-value", true);

            using var row = table.CreateDataReader();
            row.Read();

            var account = RecordMappers.ToAccount(row);

            Assert.Equal(5, account.Id);
            Assert.Equal("jdoe", account.Username);
            Assert.Equal("jdoe@example.com", account.Email);
            Assert.Equal("hashed-value", account.Password);
            Assert.True(account.Admin);
        }
    }
}
