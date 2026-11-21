namespace API.Models
{
    /// <summary>
    /// A piece of furniture listed for sale (furniture table).
    /// </summary>
    public class Furniture
    {
        public int Id { get; set; }

        /// <summary>Kind of item, e.g. couch, table, dresser.</summary>
        public string Type { get; set; }

        /// <summary>Condition, e.g. new, like new, used.</summary>
        public string Quality { get; set; }

        /// <summary>City where the item can be picked up.</summary>
        public string City { get; set; }

        public bool Sold { get; set; }

        public int Price { get; set; }

        /// <summary>URL of the item's photo.</summary>
        public string Image { get; set; }

        /// <summary>Account ID of the seller.</summary>
        public int SellerId { get; set; }
    }
}
