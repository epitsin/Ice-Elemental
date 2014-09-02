namespace FurnitureSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using FurnitureSystem.Data.Migrations;
    using FurnitureSystem.Models;

    public class FurnitureSystemDbContext : DbContext, IFurnitureSystemDbContext
    {
        public FurnitureSystemDbContext()
            : base("FurnitureSystem")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FurnitureSystemDbContext, Configuration>());
        }


        public IDbSet<FurniturePiece> FurniturePieces { get; set; }

        public IDbSet<Location> Locations { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Price> Prices { get; set; }

        public IDbSet<Section> Sections { get; set; }

        public IDbSet<Shop> Shops { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}