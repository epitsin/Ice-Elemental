namespace FurnitureSystem.ConsoleClient
{
    using System;
    using System.Linq;

    using FurnitureSystem.Data;
    using FurnitureSystem.Models;

    public class FurnitureSystemEntryPoint
    {
        public static void Main()
        {
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                database.Manufacturers.Add(new Manufacturer
                {
                    Name = "IKEA"
                });
                database.SaveChanges();

                var manufacturers = database.Manufacturers.ToList();

                foreach (var man in manufacturers)
                {
                    Console.WriteLine(man.Name);
                }
            }
        }
    }
}
