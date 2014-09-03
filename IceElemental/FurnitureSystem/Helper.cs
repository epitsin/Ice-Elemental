namespace FurnitureSystem.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Helper
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }
    }
}
