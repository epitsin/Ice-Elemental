namespace FurnitureSystem.MySql.Data
{
    using System.Collections.Generic;

    public class Shop
    {
        public Shop()
        {
            this.FurniturePieces = new HashSet<FurnitureSystem.MySql.Data.FurniturePiece>();
        }

        public virtual ICollection<FurniturePiece> FurniturePieces { get; set; }

        public int Id { get; set; }

        public virtual Location Location { get; set; }

        public string Name { get; set; }
    }
}