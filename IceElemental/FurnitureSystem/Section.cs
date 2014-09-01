namespace FurnitureSystem.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class Section
    {
        public Section()
        {
            this.FurniturePieces = new HashSet<FurniturePiece>();
        }

        public virtual ICollection<FurniturePiece> FurniturePieces { get; set; }

        public int Id { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public int ManufacturerId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}