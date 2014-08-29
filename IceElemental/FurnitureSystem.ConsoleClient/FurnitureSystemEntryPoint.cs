namespace FurnitureSystem.ConsoleClient
{
    using System;
    using System.Linq;

    using FurnitureSystem.Data;
    using FurnitureSystem.Models;
    using FurnitureSystem.Models.Enums;
    using FurnitureSystem.Xml;

    public class FurnitureSystemEntryPoint
    {
        public static void Main()
        {
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                var ikea = new Manufacturer
                {
                    Name = "IKEA"
                };

                var kitchen = new Section
                    {
                        Name = "Kitchen"
                    };

                var alfredoLocation = new Location
                    {
                        Country = "BG",
                        City = "Sofia",
                        Street = "Malinov",
                        Number = 50
                    };

                var alfredo = new Shop
                {
                    Name = "Alfredo",
                    Location = alfredoLocation
                };

                database.Shops.Add(alfredo);
                database.SaveChanges();

                var chair = new FurniturePiece
                    {
                        Name = "Pesho",
                        Type = FurnitureType.Chair,
                        Material = Material.Wooden,
                        Price = new Price
                        {
                            Money = 15.50m
                        }
                    };

                chair.Shops.Add(alfredo);

                kitchen.FurniturePieces.Add(chair);

                ikea.Sections.Add(kitchen);

                database.Manufacturers.Add(ikea);

                database.SaveChanges();

                var f = database.FurniturePieces.ToList();

                foreach (var furn in f)
                {
                    Console.WriteLine(furn.Name);
                }

                var list = from p in database.Shops
                           from t in database.FurniturePieces
                           select new
                           {
                               Shop = p.Name,
                               Furniture = t.Name
                           };

                foreach (var l in list)
                {
                    Console.WriteLine(l.Shop + " " + l.Furniture);
                }

                XmlWriter.GenerateReports();

                var shopsWithFurniture = XmlReader.GetObjects("../../../XMLReports/ShopReport.xml");

                foreach (var shop in shopsWithFurniture)
                {
                    Console.WriteLine(shop.Item1 + " -> " + shop.Item2 + " -> " + shop.Item3 + " -> " + shop.Item4);
                }
            }
        }
    }
}
