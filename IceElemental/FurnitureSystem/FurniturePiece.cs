namespace FurnitureSystem.Models
{
    using System.Collections.Generic;
    using FurnitureSystem.Models.Enums;

    public class FurniturePiece
    {
        public FurniturePiece()
        {
            this.Colours = new HashSet<Colour>();
            this.Shops = new HashSet<Shop>();
        }

        public virtual ICollection<Colour> Colours { get; set; }

        public int Id { get; set; }

        public Material Material { get; set; }

        public int MaterialId { get; set; }

        public string Name { get; set; }

        public virtual Price Price { get; set; }

        public int PriceId { get; set; }

        public virtual Section Section { get; set; }

        public int SectionId { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }

        public FurnitureType Type { get; set; }
    }
}