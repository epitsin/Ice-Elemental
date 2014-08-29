namespace FurnitureSystem.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
