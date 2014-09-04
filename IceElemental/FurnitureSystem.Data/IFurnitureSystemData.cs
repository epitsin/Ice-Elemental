namespace FurnitureSystem.Data
{
    using FurnitureSystem.Data.Repositories;
    using FurnitureSystem.Models;

    public interface IFurnitureSystemData
    {
        IGenericRepository<FurniturePiece> FurniturePieces { get; }

        IGenericRepository<Location> Locations { get; }

        IGenericRepository<Manufacturer> Manufacturers { get; }

        IGenericRepository<Price> Prices { get; }

        IGenericRepository<Section> Sections { get; }

        IGenericRepository<Shop> Shops { get; }

        void SaveChanges();
    }
}