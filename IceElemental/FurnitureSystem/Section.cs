namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Section
    {
        private ICollection<FurniturePiece> furniturePieces;

        public int Id { get; set; }

        public string Name { get; set; }

        public Section()
        {
            this.furniturePieces = new HashSet<FurniturePiece>();
        }

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

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
