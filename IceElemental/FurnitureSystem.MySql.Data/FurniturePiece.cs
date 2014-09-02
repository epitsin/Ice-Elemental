namespace FurnitureSystem.MySql.Data
{
    using System.Collections.Generic;

    public class FurniturePiece
    {
        public FurniturePiece()
        {
            this.Colours = new HashSet<Colour>();
            this.Shops = new HashSet<Shop>();
        }

        public virtual ICollection<Colour> Colours { get; set; }

        public int Id { get; set; }


        public string Name { get; set; }

        public virtual Price Price { get; set; }

        public int PriceId { get; set; }

        public virtual Section Section { get; set; }

        public int SectionId { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }

    }
}