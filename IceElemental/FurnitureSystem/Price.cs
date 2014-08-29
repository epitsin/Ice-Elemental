namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Price
    {
        private ICollection<FurniturePiece> furniturePieces;

        public Price()
        {
            this.furniturePieces = new HashSet<FurniturePiece>();
        }

        public int Id { get; set; }

        public decimal Money { get; set; }

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
