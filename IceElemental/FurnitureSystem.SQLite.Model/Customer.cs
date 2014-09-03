namespace FurnitureSystem.SQLite.Model
{
    using System;
    using System.Linq;

    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}
