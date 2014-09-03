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
    using FurnitureSystem.MongoDb.Data;
    using MongoDB.Driver;

    public class FurnitureSystemEntryPoint
    {

        public static void Main()
        {
            //var mysql = new FurnitureSystemEntities();

            //var json = new JsonReporter();
            //json.Report();


            //var mysql = new FurnitureSystemEntities();
            //mysql.FurniturePieces.Select(x => x).FirstOrDefault().Price.Money = 5m;



            //string zipPath = @"../../../ExcelReports.zip";
            //string extractPath = @"../../../";
            //ZipExtractor.Extract(zipPath, extractPath);

            var excel = new ExcelReader();
            var items = excel.GetExtractedFilesInfo("../../../ExcelReports/Read/");
            var db = new FurnitureSystemDbContext();
            using (db)
            {
                foreach (var item in items)
                {
                    var manufacturer = new Manufacturer
                    {
                        Name = item.Item1
                    };
                    var section = new Section
                    {
                        Name = item.Item2
                    };
                    var product = new FurniturePiece
                    {
                        Name = item.Item3,
                        Material = (Material)item.Item4,
                        Type = (FurnitureType)item.Item5,
                        Price = new Price
                        {
                            Money = item.Item6
                        }
                    };

                    db.Manufacturers.Add(manufacturer);
                    db.Sections.Add(section);
                    db.FurniturePieces.Add(product);
                    db.SaveChanges();
                }

                foreach (var item in db.FurniturePieces)
                {
                    Console.WriteLine(item.Name + " -> " + item.Section.Name);
                }


                var connectionStr = "mongodb://localhost/";
                var mongoClient = new MongoClient(connectionStr);
                var mongoServer = mongoClient.GetServer();
                var shopSystemDb = mongoServer.GetDatabase("ShopSystem");
                var seeder = new Seeder(shopSystemDb);
                seeder.SeedShops();
                var retriever = new DataRetriever(shopSystemDb);
                var shops = retriever.GetShopsLocal();
                foreach (var item in shops)
                {
                    var shop = new Shop
                    {
                        Name = item.Name
                    };
                    var location = new Location
                    {
                        Shop = shop,
                        Country = item.Country,
                        City = item.City,
                        Street = item.Street,
                        Number = item.StreetNumber
                    };
                    db.Shops.Add(shop);
                    db.Locations.Add(location);
                    db.SaveChanges();
                }

                foreach (var item in db.Shops)
                {
                    Console.WriteLine(item.Name + " -> " + item.Location.Street);
                }
            }

            //ExcelWriter.GenerateReports();

            //WORKS FROM HERE

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

            //    var shopsWithFurniture = XmlReader.GetObjects("../../../XMLReports/DiscountReport.xml");

            //    foreach (var shop in shopsWithFurniture)
            //    {
            //        Console.WriteLine(string.Format("{0} -> {1}", shop.Item1, shop.Item2));
            //    }

            //    PdfExporter.Export();
            //}

            //WORKS TIL HERE

            //UPDATE THE PRICE OF THE PRDUCTS AFTER THE DISCOUNT

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