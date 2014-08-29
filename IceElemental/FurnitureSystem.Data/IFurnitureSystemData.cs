namespace FurnitureSystem.Data
{
    using FurnitureSystem.Data.Repositories;
    using FurnitureSystem.Models;

    public interface IFurnitureSystemData
    {
        IGenericRepository<FurniturePiece> FurniturePieces { get; } // Manufacturers, Shops??????????
    }
}
