namespace FurnitureSystem.SQLite.Model
{
    using System;
    using System.Linq;

    public class Customer
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}
