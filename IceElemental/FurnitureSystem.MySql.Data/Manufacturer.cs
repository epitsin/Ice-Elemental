namespace FurnitureSystem.MySql.Data
{
    using System.Collections.Generic;
    using FurnitureSystem.MySql.Data;

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