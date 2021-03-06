﻿namespace FurnitureSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Location
    {
        public string City { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        public virtual Shop Shop { get; set; }

        [Key, ForeignKey("Shop")]
        public int ShopId { get; set; }

        public string Street { get; set; }
    }
}