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
                new ShopLocation("Trendy Shop", "Bulgaria", "Sofia", "ul. Tsvetna gradina 2", 50, 0.1m),
                new ShopLocation("Mediocre Shop", "Bulgaria", "Plovdiv", "kv. Kiochuk Parizh", 34, 0.15m),
                new ShopLocation("Shop Za Naroda", "Bulgaria", "Pernik", "mestnost Do Zavoda", 23, 0.3m),
                new ShopLocation("Very Expensive Furniture", "Bulgaria", "Sofia", "ul. Fritiof Nansen 6", 12, 0.23m),
                new ShopLocation("Effecta", "Bulgaria", "Sofia", "bul. Evlogi Georgiev 34", 5, 0.2m),
                new ShopLocation("Kitaiski Mebeli", "Bulgaria", "Burgas", "ul. Kitaiska Stena 4", 45, 0.4m),
                new ShopLocation("Furrniture Shop", "Bulgaria", "Stara Zagora", "bul. Simeon Veliki 6", 30, 0.27m),
                new ShopLocation("Dolce Casa", "Bulgaria", "Sofia", "bul. Bulgaria 23", 47, 0.23m),
                new ShopLocation("Casa Viva", "Bulgaria", "Varna", "ul.3ti mart 20", 41, 0.17m),
                new ShopLocation("Platan", "Bulgaria", "Sofia", "ul. Saborna 14", 36, 0.3m)
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