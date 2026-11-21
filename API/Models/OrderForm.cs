namespace API.Models
{
    /// <summary>
    /// A buyer's pickup order for a furniture listing (orderform table).
    /// </summary>
    public class OrderForm
    {
        public int Id { get; set; }

        public int FurnitureId { get; set; }

        /// <summary>Account ID of the buyer.</summary>
        public int BuyerId { get; set; }

        public DateTime PickupDate { get; set; }

        public int Price { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        /// <summary>Pickup location.</summary>
        public string Location { get; set; }

        public string Phone { get; set; }
    }
}
