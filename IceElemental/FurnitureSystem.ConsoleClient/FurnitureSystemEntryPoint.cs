namespace FurnitureSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using FurnitureSystem.Data;
    using FurnitureSystem.Models;
    using FurnitureSystem.Models.Enums;
    using FurnitureSystem.Pdf;
    using FurnitureSystem.Xml;
    using FurnitureSystem.Excel;
    using FurnitureSystem.Zip;
    using FurnitureSystem.JsonReporter;
    using Telerik.OpenAccess;
    using FurnitureSystem.MySql;

    public class FurnitureSystemEntryPoint
    {
        
        public static void Main()
        {
            var mysql = new FurnitureSystemEntities();
            //var json = new JsonReporter();
            //json.GetJsonObjects();

            //var mysql = new FurnitureSystemEntities();
            //mysql.FurniturePieces.Select(x => x).FirstOrDefault().Price.Money = 5m;
            


            //string zipPath = @"../../../ExcelReports.zip";
            //string extractPath = @"../../../";
            //ZipExtractor.Extract(zipPath, extractPath);

            //var excel = new ExcelReader();
            //var items = excel.GetExtractedFilesInfo("../../../ExcelReports/");
            //foreach (var item in items)
            //{
            //    Console.WriteLine(item.Item1 + " -> " + item.Item2);
            //}

            //ExcelWriter.GenerateReports();


            //var database = new FurnitureSystemDbContext();
            //using (database)
            //{
            //    var ikea = new Manufacturer
            //    {
            //        Name = "IKEA"
            //    };

            //    var kitchen = new Section
            //    {
            //        Name = "Kitchen"
            //    };

            //    var alfredoLocation = new Location
            //    {
            //        Country = "BG",
            //        City = "Sofia",
            //        Street = "Malinov",
            //        Number = 50
            //    };

            //    var alfredo = new Shop
            //    {
            //        Name = "Alfredo",
            //        Location = alfredoLocation
            //    };

            //    database.Shops.Add(alfredo);
            //    database.SaveChanges();

            //    var chair = new FurniturePiece
            //    {
            //        Name = "Pesho",
            //        Type = FurnitureType.Chair,
            //        Material = Material.Wooden,
            //        Price = new Price
            //        {
            //            Money = 15.50m
            //        }
            //    };

            //    chair.Shops.Add(alfredo);

            //    kitchen.FurniturePieces.Add(chair);

            //    ikea.Sections.Add(kitchen);

            //    database.Manufacturers.Add(ikea);

            //    database.SaveChanges();

            //    var f = database.FurniturePieces.ToList();

            //    foreach (var furn in f)
            //    {
            //        Console.WriteLine(furn.Name);
            //    }

            //    var list = from p in database.Shops
            //               from t in database.FurniturePieces
            //               select new
            //               {
            //                   Shop = p.Name,
            //                   Furniture = t.Name
            //               };

            //    foreach (var l in list)
            //    {
            //        Console.WriteLine(string.Format("{0} {1}", l.Shop, l.Furniture));
            //    }

            //    XmlWriter.GenerateReports();

            //    var shopsWithFurniture = XmlReader.GetObjects("../../../XMLReports/ShopReport.xml");

            //    foreach (var shop in shopsWithFurniture)
            //    {
            //        Console.WriteLine(string.Format("{0} -> {1} -> {2} -> {3}", shop.Item1, shop.Item2, shop.Item3, shop.Item4));
            //    }

            //    PdfExporter.Export();
            //}

            //var database = new FurnitureSystemDbContext();
            //using (database)
            //{
            //    var furnitureWithDelivery = XmlReader.GetObjects("../../../XMLReports/Delivery.xml");

            //    foreach (var shop in furnitureWithDelivery)
            //    {
            //        var prices = database.FurniturePieces.Where(x => x.Name == shop.Item1).Select(x => x.Price);
            //        foreach (var price in prices)
            //        {
            //            Console.WriteLine(price.Money);
            //            price.Money -= shop.Item2;
            //        }
            //    }

            //    database.SaveChanges();
            //}
        }
    }
}