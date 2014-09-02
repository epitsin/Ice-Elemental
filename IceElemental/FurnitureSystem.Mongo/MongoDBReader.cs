namespace FurnitureSystem.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class MongoDBReader
    {

        private static MongoDatabase mongoDB = MongoDBEstablisher.ConnectToMongoDB("FurnitureSystem");

        public static MongoCollection<BsonDocument> GetLocations()
        {
            var locations = mongoDB.GetCollection("Locations");

            return locations;
        }

        public static MongoCollection<BsonDocument> GetShops()
        {
            var shops = mongoDB.GetCollection("Shops");

            return shops;
        }


        public static MongoCollection<BsonDocument> GetFurniturePieces()
        {
            var pieces = mongoDB.GetCollection("FurniturePieces");

            return pieces;
        }

        public static MongoCollection<BsonDocument> GetPrices()
        {
            var prices = mongoDB.GetCollection("Prices");

            return prices;
        }

        public static MongoCollection<BsonDocument> GetColours()
        {
            var colours = mongoDB.GetCollection("Colours");

            return colours;
        }

        public static MongoCollection<BsonDocument> GetSections()
        {
            var sections = mongoDB.GetCollection("Sections");

            return sections;
        }

        public static MongoCollection<BsonDocument> GetManufacturers()
        {
            var manufacturers = mongoDB.GetCollection("Manufacturers");

            return manufacturers;
        }
    }
}
