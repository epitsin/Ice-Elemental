namespace FurnitureSystem.MySql.Data
{
    public class Location
    {
        public string City { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        public virtual Shop Shop { get; set; }


        public int ShopId { get; set; }

        public string Street { get; set; }
    }
}