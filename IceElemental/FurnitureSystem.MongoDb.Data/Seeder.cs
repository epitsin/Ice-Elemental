namespace FurnitureSystem.MongoDb.Data
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    public class Seeder
    {
        private readonly MongoDatabase mongoDatabase;

        public Seeder(MongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public void SeedShops()
        {
            var innitialShops = new List<ShopLocation> 
            {
                new ShopLocation("Castle Shop", "Erathia", "Middleheim", "Alexandretta", 50, 0.1m),
                new ShopLocation("Rampart Shop", "Erathia", "Gladeroot", "Wise Oak", 34, 0.15m),
                new ShopLocation("Tower Shop", "Erathia", "Cloudfire", "Mystos", 23, 0.3m),
                new ShopLocation("Inferno Shop", "Erathia", "Brimstone", "Daemon Gate", 12, 0.23m),
                new ShopLocation("Necropolis Shop", "Erathia", "Sanctum", "Dark Cloud", 5, 0.2m),
                new ShopLocation("Dungeon Shop", "Erathia", "Darkburrow", "Lost Hold", 45, 0.4m),
                new ShopLocation("Stronghold Shop", "Erathia", "Rockwarren", "Drago Breach", 30, 0.27m),
                new ShopLocation("Fortress Shop", "Erathia", "Mosswood", "Hermit Cove", 47, 0.23m)
            };

            if (this.mongoDatabase.GetCollection("ShopSystem") == null)
            {
                this.mongoDatabase.CreateCollection("ShopSystem");
            }

            var shopsCollection = this.mongoDatabase.GetCollection("ShopSystem");

            foreach (var shop in innitialShops)
            {
                shopsCollection.Insert(shop);
            }
        }
    }
}