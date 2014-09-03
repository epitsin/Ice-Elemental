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
                new ShopLocation("Alfredo", "Bulgaria", "Sofia", "Malinov", 50),
                new ShopLocation("Kaspichan", "China", "Mimi", "Malinov", 34),
                new ShopLocation("Kifla", "Irak", "Pepi", "Malinov", 23),
                new ShopLocation("Something", "Nigeria", "Jiji", "Malinov", 12),
                new ShopLocation("Pesho", "Kaspichan", "Sofia", "Malinov", 5)
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