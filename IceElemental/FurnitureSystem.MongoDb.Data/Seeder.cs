namespace FurnitureSystem.MongoDb.Data
{
    using FurnitureSystem.Models.Enums;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;

    public class Seeder
    {
        private MongoDatabase dbContext;

        public Seeder(MongoDatabase dbContext)
        {
            this.dbContext = dbContext;
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

            if (this.dbContext.GetCollection("ShopSystem") == null)
            {
                this.dbContext.CreateCollection("ShopSystem");
            }

            var shopsCollection = this.dbContext.GetCollection("ShopSystem");

            foreach (var shop in innitialShops)
            {
                shopsCollection.Insert(shop);
            }
        }
    }
}