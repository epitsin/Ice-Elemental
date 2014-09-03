namespace FurnitureSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        public Shop()
        {
            this.FurniturePieces = new HashSet<FurniturePiece>();
        }

        public virtual ICollection<FurniturePiece> FurniturePieces { get; set; }

        public int Id { get; set; }

        [Required]
        public virtual Location Location { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal ProfitPercentage { get; set; }
    }
}