namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class FurniturePiece
    {
        private ICollection<Colour> colours;
        private ICollection<Shop> shops;

        public FurniturePiece()
        {
            this.colours = new HashSet<Colour>();
            this.shops = new HashSet<Shop>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public FurnitureType Type { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public int SectionId { get; set; }

        public virtual Section Section { get; set; }

        public int PriceId { get; set; }

        public virtual Price Price { get; set; }

        public virtual ICollection<Shop> Shops
        {
            get
            {
                return this.shops;
            }

            set
            {
                this.shops = value;
            }
        }

        public virtual ICollection<Colour> Colours
        {
            get
            {
                return this.colours;
            }

            set
            {
                this.colours = value;
            }
        }
    }
}
