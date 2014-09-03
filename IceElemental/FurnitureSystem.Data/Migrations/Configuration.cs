namespace FurnitureSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FurnitureSystem.Data;

    public sealed class Configuration : DbMigrationsConfiguration<FurnitureSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }
    }
}