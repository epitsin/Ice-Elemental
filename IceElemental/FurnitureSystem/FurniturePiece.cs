namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class FurniturePiece
    {
        private ICollection<Price> prices;
        private ICollection<Colour> colours;

        public FurniturePiece()
        {
            this.prices = new HashSet<Price>();
            this.colours = new HashSet<Colour>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public virtual ICollection<Price> Prices
        {
            get
            {
                return this.prices;
            }

            set
            {
                this.prices = value;
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
