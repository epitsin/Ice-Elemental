namespace FurnitureSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Shop
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //public int LocationId { get; set; }

        [Required]
        public virtual Location Location { get; set; }
    }
}
