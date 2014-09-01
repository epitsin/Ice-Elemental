namespace FurnitureSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FurnitureSystem.Data;
    using FurnitureSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<FurnitureSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(FurnitureSystemDbContext context)
        {
            this.SeedManufacturers(context);
        }

        private void SeedManufacturers(FurnitureSystemDbContext context)
        {
            if (context.Manufacturers.Any())
            {
                return;
            }

            context.Manufacturers.Add(new Manufacturer
            {
                Name = "IKEA"
            });

            context.Manufacturers.Add(new Manufacturer
            {
                Name = "Aron"
            });

            context.Manufacturers.Add(new Manufacturer
            {
                Name = "Videnov"
            });
        }
    }
}