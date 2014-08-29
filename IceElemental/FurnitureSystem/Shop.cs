namespace FurnitureSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        private ICollection<FurniturePiece> furniturePieces;
        
        public Shop()
        {
            this.furniturePieces = new HashSet<FurniturePiece>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public virtual Location Location { get; set; }

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
