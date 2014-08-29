namespace FurnitureSystem.Models
{
    public class Colour
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FurniturePieceId { get; set; }

        public FurniturePiece FurniturePiece { get; set; }
    }
}
