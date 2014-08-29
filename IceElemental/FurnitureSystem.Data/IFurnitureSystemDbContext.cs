namespace FurnitureSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using FurnitureSystem.Models;

    public interface IFurnitureSystemDbContext
    {
        IDbSet<Manufacturer> Manufacturers { get; set; }

        IDbSet<FurniturePiece> FurniturePieces { get; set; }

        IDbSet<Section> Sections { get; set; }

        IDbSet<Colour> Colours { get; set; }

        IDbSet<Price> Prices { get; set; }

        IDbSet<Shop> Shops { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
