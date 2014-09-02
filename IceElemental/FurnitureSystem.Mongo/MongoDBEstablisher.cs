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


namespace FurnitureSystem.Mongo
{
    public class MongoDBEstablisher
    {
        private static MongoDatabase mongoDB = ConnectToMongoDB("FurnitureSystem");

        public static void EstablishDatabase() {
            //---Locations populater---
            if (!mongoDB.CollectionExists("Locations"))
            {
                mongoDB.CreateCollection("Locations");
            }

            var locations = mongoDB.GetCollection("Locations");
            //locations.RemoveAll();
            var locationToAdd = MongoCreateLocation(1, "Bulgaria", "Sofia", "Kaspichan Street", 10);
            locations.Insert(locationToAdd);

            //---Shops populater---
            if (!mongoDB.CollectionExists("Shops"))
            {
                mongoDB.CreateCollection("Shops");
            }
            var shops = mongoDB.GetCollection("Shops");
            //shops.RemoveAll();
            var shopToAdd = MongoCreateShop(1, "Kaspichan Shop", new int[] {1,2,3});
            shops.Insert(shopToAdd);

            //---Furniture Pieces populater---
            if (!mongoDB.CollectionExists("FurniturePieces"))
            {
                mongoDB.CreateCollection("FurniturePieces");
            }
            var pieces = mongoDB.GetCollection("FurniturePieces");
            //pieces.RemoveAll();
            var pieceToAdd = MongoCreateFurniturePiece(1, "Divan", "Mebel", 1, 1, 1, new int[] { 1, 2 }, new int[] { 1, 2 });
            pieces.Insert(pieceToAdd);

            //---Prices populater---
            if (!mongoDB.CollectionExists("Prices"))
            {
                mongoDB.CreateCollection("Prices");
            }
            var prices = mongoDB.GetCollection("Prices");
            //prices.RemoveAll();
            var priceToAdd = MongoCreatePrice(1, 199.99);
            prices.Insert(priceToAdd);

            //---Colours populater---
            if (!mongoDB.CollectionExists("Colours"))
            {
                mongoDB.CreateCollection("Colours");
            }
            var colours = mongoDB.GetCollection("Colours");
            //colours.RemoveAll();
            var colourToAdd = MongoCreateColour(1, "White", new int[] { 1 });
            colours.Insert(colourToAdd);

            //---Manufacturers populater---
            if (!mongoDB.CollectionExists("Manufacturers"))
            {
                mongoDB.CreateCollection("Manufacturers");
            }
            var manufacturers = mongoDB.GetCollection("Manufacturers");
            //manufacturers.RemoveAll();
            var manufacturerToAdd = MongoCreateManufacturer(1, "SkurcatNiMebeliteOOD");
            manufacturers.Insert(manufacturerToAdd);

            //---Sections populater---
            if (!mongoDB.CollectionExists("Sections"))
            {
                mongoDB.CreateCollection("Sections");
            }
            var sections = mongoDB.GetCollection("Sections");
            //sections.RemoveAll();
            var sectionToAdd = MongoCreateSection(1, "TestSection", 1);
            sections.Insert(sectionToAdd);
        }

        public static BsonDocument MongoCreateLocation(int shopId, string country, string city, string street, int number)
        {
            BsonDocument location = new BsonDocument();
            location.Add("shopId", shopId);
            location.Add("country", country);
            location.Add("city", city);
            location.Add("street", street);
            location.Add("number", number);

            return location;
        }

        public static BsonDocument MongoCreateShop(int id, string name, int[] furnitureIds)
        {
            BsonDocument shop = new BsonDocument();
            shop.Add("id", id);
            shop.Add("name", name);
            var furnitureIdsArray = new BsonArray();
            foreach (var furnitureId in furnitureIds)
            {
                furnitureIdsArray.Add(furnitureId);
            }
            shop.Add("furnitureIds", furnitureIdsArray);

            return shop;
        }


        public static BsonDocument MongoCreateFurniturePiece(int id, string name, string type, int materialId,
            int sectionId, int priceId, int[] shopsIds, int[] coloursIds)
        {
            //check - is type an enumeration ?!
            BsonDocument piece = new BsonDocument();
            piece.Add("id", id);
            piece.Add("name", name);
            piece.Add("type", type);
            piece.Add("materialId", materialId);
            piece.Add("sectionId", sectionId);
            piece.Add("priceId", priceId);
            var shopsIdsArray = new BsonArray();
            foreach (var shopId in shopsIds)
            {
                shopsIdsArray.Add(shopId);
            }
            piece.Add("shopsIds", shopsIdsArray);

            var coloursIdsArray = new BsonArray();
            foreach (var colourId in coloursIdsArray)
            {
                coloursIdsArray.Add(colourId);
            }
            piece.Add("coloursIds", coloursIdsArray);

            return piece;
        }

        public static BsonDocument MongoCreatePrice(int id, double money)
        {
            BsonDocument price = new BsonDocument();
            price.Add("id", id);
            price.Add("money", money);

            return price;
        }

        public static BsonDocument MongoCreateColour(int id, string name, int[] furniturePiecesIds)
        {
            BsonDocument price = new BsonDocument();
            price.Add("id", id);
            price.Add("name", name);

            var furniturePiecesIdsArray = new BsonArray();
            foreach (var furniturePieceId in furniturePiecesIdsArray)
            {
                furniturePiecesIdsArray.Add(furniturePieceId);
            }
            price.Add("furniturePieceIds", furniturePiecesIdsArray);

            return price;
        }

        public static BsonDocument MongoCreateSection(int id, string name, int manufacturerId)
        {
            BsonDocument section = new BsonDocument();
            section.Add("id", id);
            section.Add("name", name);
            section.Add("manufacturerId", manufacturerId);

            return section;
        }

        public static BsonDocument MongoCreateManufacturer(int id, string name)
        {
            BsonDocument manufacturer = new BsonDocument();
            manufacturer.Add("id", id);
            manufacturer.Add("name", name);

            return manufacturer;
        }


        private static MongoDatabase ConnectToMongoDB(string dbName)
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost/");
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase mongoDb = mongoServer.GetDatabase(dbName);
            return mongoDb;
        }
    }
}
