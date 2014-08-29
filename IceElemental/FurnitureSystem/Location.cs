namespace FurnitureSystem.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
