﻿namespace FurnitureSystem.MongoDb.Data
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ShopLocation
    {
        [BsonConstructor]
        public ShopLocation(string name, string country, string city, string street, int streetNumber)
        {
            this.Name = name;
            this.Country = country;
            this.City = city;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }

        [BsonId]
        public ObjectId ShopId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }
    }
}
