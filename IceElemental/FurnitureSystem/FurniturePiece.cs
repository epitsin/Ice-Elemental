namespace FurnitureSystem.Models
{
    public class FurniturePiece
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }
    }
}
