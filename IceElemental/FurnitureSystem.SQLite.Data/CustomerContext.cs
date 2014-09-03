namespace FurnitureSystem.SQLite.Data
{
    using System.Data.Entity;

    using FurnitureSystem.SQLite.Model;

    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
