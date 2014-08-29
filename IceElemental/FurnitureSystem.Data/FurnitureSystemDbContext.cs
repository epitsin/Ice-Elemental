namespace FurnitureSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FurnitureSystemDbContext : DbContext, IFurnitureSystemDbContext
    {
        public FurnitureSystemDbContext()
            : base("FurnitureSystem")
        {
        }

        public IDbSet<Models.Manufacturer> Manufacturers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Models.FurniturePiece> FurniturePieces
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Models.Shop> Shops
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Models.Price> Prices
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public new void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}