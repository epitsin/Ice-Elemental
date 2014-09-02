using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureSystem.MongoDb.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStr = "mongodb://localhost/";
            var mongoClient = new MongoClient(connectionStr);
            var mongoServer = mongoClient.GetServer();
            var shopSystemDb = mongoServer.GetDatabase("ShopSystem");
            var seeder = new Seeder(shopSystemDb);
            seeder.SeedShops();
            var retriever = new DataRetriever(shopSystemDb);
            var shops = retriever.GetShopsLocal();
            foreach (var item in shops)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
