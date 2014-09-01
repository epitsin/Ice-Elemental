namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Sections = new HashSet<Section>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}