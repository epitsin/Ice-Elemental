namespace FurnitureSystem.MongoDb.Data
{
    using System.Collections.Generic;
    using MongoDB.Driver;
    using System.Linq;

    public class DataRetriever
    {
        private MongoDatabase dbContext;

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