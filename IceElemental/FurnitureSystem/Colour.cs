namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Colour
    {
        public Colour()
        {
            this.FurniturePieces = new HashSet<FurniturePiece>();
        }

        public virtual ICollection<FurniturePiece> FurniturePieces { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}