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
    using FurnitureSystem.Models.Enums;
    using FurnitureSystem.Data;

    public class MongoDBUtility
    {
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


        public static BsonDocument MongoCreateFurniturePiece(int id, string name, FurnitureType type, Material material,
            int sectionId, int priceId, int[] shopsIds, int[] coloursIds)
        {
            BsonDocument piece = new BsonDocument();
            piece.Add("id", id);
            piece.Add("name", name);
            piece.Add("type", type.ToString());
            piece.Add("materialId", material.ToString());
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


       
    }
}
