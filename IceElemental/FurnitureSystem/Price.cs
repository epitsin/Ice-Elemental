namespace FurnitureSystem.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class Price
    {
        public Price()
        {
            this.FurniturePieces = new HashSet<FurniturePiece>();
        }

        public virtual ICollection<FurniturePiece> FurniturePieces { get; set; }

        public int Id { get; set; }

        [DataMember]
        public decimal Money { get; set; }
    }
}