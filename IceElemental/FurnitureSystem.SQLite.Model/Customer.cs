namespace FurnitureSystem.SQLite.Model
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}
