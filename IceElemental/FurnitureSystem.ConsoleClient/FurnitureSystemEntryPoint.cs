namespace FurnitureSystem.ConsoleClient
{
    using System;
    using System.Linq;

    using FurnitureSystem.Data;
    using FurnitureSystem.Models;
    using FurnitureSystem.Models.Enums;

    public class FurnitureSystemEntryPoint
    {
        public static void Main()
        {
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                var ikea =new Manufacturer
                {
                    Name = "IKEA"
                };

                var kitchen = new Section
                    {
                        Name = "Kitchen"
                    };

                kitchen.FurniturePieces.Add(new FurniturePiece
                    {
                        Name = "Pesho",
                        Type = FurnitureType.Chair,
                        Material = Material.Wooden,
                        Price = new Price
                        {
                            Money = 15.50m
                        }
                    });

                ikea.Sections.Add(kitchen);

                    
                database.Manufacturers.Add(ikea);
                database.SaveChanges();

                var f = database.FurniturePieces.ToList();

                foreach (var furn in f)
                {
                    Console.WriteLine(furn.Name);
                }
            }
        }
    }
}
