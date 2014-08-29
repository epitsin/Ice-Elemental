namespace FurnitureSystem.Models
{
    using System.Collections.Generic;

    public class Manufacturer
    {
        private ICollection<Section> sections;
        public Manufacturer()
        {
            this.sections = new HashSet<Section>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Section> Sections
        {
            get
            {
                return this.sections;
            }

            set
            {
                this.sections = value;
            }
        }
    }
}
