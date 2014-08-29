namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Manufacturer
    {
        private ICollection<FurniturePiece> furniturePieces;

        public Manufacturer()
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
