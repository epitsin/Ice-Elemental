namespace FurnitureSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using FurnitureSystem.Models;

    public interface IFurnitureSystemDbContext
    {

        IDbSet<FurniturePiece> FurniturePieces { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<Manufacturer> Manufacturers { get; set; }

        IDbSet<Price> Prices { get; set; }

        IDbSet<Section> Sections { get; set; }

        IDbSet<Shop> Shops { get; set; }

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();

        IDbSet<T> Set<T>() where T : class;
    }
}