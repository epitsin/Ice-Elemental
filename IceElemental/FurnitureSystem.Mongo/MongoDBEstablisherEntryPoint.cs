using FurnitureSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureSystem.Mongo
{
    public class MongoDBEstablisherEntryPoint
    {
        public static void Main()
        {
            MongoDBEstablisher.EstablishDatabase();

            var mongoDB = MongoDBEstablisher.ConnectToMongoDB("FurnitureSystem");
            var locations = mongoDB.GetCollection("Locations");
            for (int i = 1; i < 10; i++)
            {
                var locationToAdd = MongoDBUtility.MongoCreateLocation(i, "Bulgaria", "Sofia", "Kaspichan Street", i);
                locations.Insert(locationToAdd);
            }

            var shops = mongoDB.GetCollection("Shops");
            for (int i = 1; i < 10; i++)
            {
                var shopToAdd = MongoDBUtility.MongoCreateShop(i, "Kaspichan Shop" + i, new int[] { i, i+1, i+2 });
                shops.Insert(shopToAdd);
            }


            var pieces = mongoDB.GetCollection("FurniturePieces");
            for (int i = 1; i < 10; i++)
            {
                var pieceToAdd = MongoDBUtility.MongoCreateFurniturePiece(i, "Leglo" + i, FurnitureType.Bed, Material.Wooden, 1, 1, new int[] { i, i+1, i+2 }, new int[] { 1, 2 });
                pieces.Insert(pieceToAdd);
            }


            var prices = mongoDB.GetCollection("Prices");
            for (int i = 1; i < 10; i++)
            {
                var priceToAdd = MongoDBUtility.MongoCreatePrice(i, i*5);
                prices.Insert(priceToAdd);
            }


            var colours = mongoDB.GetCollection("Colours");
            for (int i = 1; i < 10; i++)
            {
                var colourToAdd = MongoDBUtility.MongoCreateColour(i, "White", new int[] { i });
                colours.Insert(colourToAdd);
            }


            var manufacturers = mongoDB.GetCollection("Manufacturers");
            for (int i = 1; i < 10; i++)
            {
                var manufacturerToAdd = MongoDBUtility.MongoCreateManufacturer(i, "SkurcatNiMebeliteOOD" + i);
                manufacturers.Insert(manufacturerToAdd);
            }


            var sections = mongoDB.GetCollection("Sections");
            for (int i = 1; i < 10; i++)
            {
                var sectionToAdd = MongoDBUtility.MongoCreateSection(i, "TestSection", i);
                sections.Insert(sectionToAdd);
            }

            Console.WriteLine(manufacturers.Count());
        }
    }
}
