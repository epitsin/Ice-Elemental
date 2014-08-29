namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Colour
    {
        private ICollection<FurniturePiece> furniturePieces;
        
        public Colour()
        {
            this.furniturePieces = new HashSet<FurniturePiece>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<FurniturePiece> FurniturePieces
        {
            get
            {
                return this.furniturePieces;
            }

            set
            {
                this.furniturePieces = value;
            }
        }
    }
}
