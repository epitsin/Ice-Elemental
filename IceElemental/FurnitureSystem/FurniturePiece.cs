namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class FurniturePiece
    {
        private ICollection<Colour> colours;

        public FurniturePiece()
        {
            this.colours = new HashSet<Colour>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public FurnitureType Type { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public int PriceId { get; set; }

        public Price Price { get; set; }

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
