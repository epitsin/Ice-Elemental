namespace FurnitureSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FurnitureSystemDbContext : DbContext
    {
        public FurnitureSystemDbContext()
            : base("FurnitureSystem")
        {
        }

    }
}