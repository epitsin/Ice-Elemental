using System.Runtime.Serialization;
namespace FurnitureSystem.Models
{
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
