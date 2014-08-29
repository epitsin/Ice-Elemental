namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class FurniturePiece
    {
        private ICollection<Manufacturer> manufacturers;
        private ICollection<Price> prices;

        public FurniturePiece()
        {
            this.manufacturers = new HashSet<Manufacturer>();
            this.prices = new HashSet<Price>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers
        {
            get
            {
                return this.manufacturers;
            }

            set
            {
                this.manufacturers = value;
            }
        }

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
    }
}
