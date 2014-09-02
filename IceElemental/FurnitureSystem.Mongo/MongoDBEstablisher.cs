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
    using FurnitureSystem.Models;
    using FurnitureSystem.Data;
    using FurnitureSystem.Models.Enums;

    public class MongoDBEstablisher
    {
        private static MongoDatabase mongoDB = ConnectToMongoDB("FurnitureSystem");

        public static void EstablishDatabase()
        {
            //---Locations populater---
            if (!mongoDB.CollectionExists("Locations"))
            {
                mongoDB.CreateCollection("Locations");
                var locations = mongoDB.GetCollection("Locations");
                //locations.RemoveAll();
                var locationToAdd = MongoDBUtility.MongoCreateLocation(1, "Bulgaria", "Sofia", "Kaspichan Street", 10);
                locations.Insert(locationToAdd);
            }

            //---Shops populater---
            if (!mongoDB.CollectionExists("Shops"))
            {
                mongoDB.CreateCollection("Shops");
                var shops = mongoDB.GetCollection("Shops");
                //shops.RemoveAll();
                var shopToAdd = MongoDBUtility.MongoCreateShop(1, "Kaspichan Shop", new int[] { 1, 2, 3 });
                shops.Insert(shopToAdd);
            }

            //---Furniture Pieces populater---
            if (!mongoDB.CollectionExists("FurniturePieces"))
            {
                mongoDB.CreateCollection("FurniturePieces");
                var pieces = mongoDB.GetCollection("FurniturePieces");
                //pieces.RemoveAll();
                var pieceToAdd = MongoDBUtility.MongoCreateFurniturePiece(1, "Leglo", FurnitureType.Bed, Material.Wooden, 1, 1, new int[] { 1, 2, 3 }, new int[] { 1, 2 });
                pieces.Insert(pieceToAdd);
            }

            //---Prices populater---
            if (!mongoDB.CollectionExists("Prices"))
            {
                mongoDB.CreateCollection("Prices");
                var prices = mongoDB.GetCollection("Prices");
                //prices.RemoveAll();
                var priceToAdd = MongoDBUtility.MongoCreatePrice(1, 199.99);
                prices.Insert(priceToAdd);
            }

            //---Colours populater---
            if (!mongoDB.CollectionExists("Colours"))
            {
                mongoDB.CreateCollection("Colours");
                var colours = mongoDB.GetCollection("Colours");
                //colours.RemoveAll();
                var colourToAdd = MongoDBUtility.MongoCreateColour(1, "White", new int[] { 1 });
                colours.Insert(colourToAdd);
            }

            //---Manufacturers populater---
            if (!mongoDB.CollectionExists("Manufacturers"))
            {
                mongoDB.CreateCollection("Manufacturers");
                var manufacturers = mongoDB.GetCollection("Manufacturers");
                //manufacturers.RemoveAll();
                var manufacturerToAdd = MongoDBUtility.MongoCreateManufacturer(1, "SkurcatNiMebeliteOOD");
                manufacturers.Insert(manufacturerToAdd);
            }

            //---Sections populater---
            if (!mongoDB.CollectionExists("Sections"))
            {
                mongoDB.CreateCollection("Sections");
                var sections = mongoDB.GetCollection("Sections");
                //sections.RemoveAll();
                var sectionToAdd = MongoDBUtility.MongoCreateSection(1, "TestSection", 1);
                sections.Insert(sectionToAdd);
            }
        }

        internal static MongoDatabase ConnectToMongoDB(string dbName)
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase mongoDb = mongoServer.GetDatabase(dbName);
            return mongoDb;
        }

    }
}
