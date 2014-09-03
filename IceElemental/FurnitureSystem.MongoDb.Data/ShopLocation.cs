namespace FurnitureSystem.MongoDb.Data
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ShopLocation
    {
        [BsonConstructor]
        public ShopLocation(string name, string country, string city, string street, int streetNumber, decimal profitPercentage)
        {
            this.Name = name;
            this.Country = country;
            this.City = city;
            this.Street = street;
            this.StreetNumber = streetNumber;
            this.ProfitPercentage = profitPercentage;
        }

        [BsonId]
        public ObjectId ShopId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public decimal ProfitPercentage { get; set; }
    }
}
