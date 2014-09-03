namespace FurnitureSystem.SQLite.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FurnitureSystem.SQLite.Model;

    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
