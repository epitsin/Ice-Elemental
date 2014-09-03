namespace FurnitureSystem.MongoDb.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    public class DataRetriever
    {
        private readonly MongoDatabase dbContext;

        public DataRetriever(MongoDatabase dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<ShopLocation> GetShopsLocal()
        {
            var shopsCollection = this.dbContext.GetCollection("ShopSystem");
            var shopsAsList = shopsCollection.FindAllAs<ShopLocation>().ToList();

            return shopsAsList;
        }
    }
}