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
    using MongoDB.Bson;
    using Telerik.OpenAccess;
    using FurnitureSystem.MySql;
    using FurnitureSystem.MongoDb.Data;
    using MongoDB.Driver;
    using FurnitureSystem.MySQLReader;
    using FurnitureSystem.SQLite.Model;
    using FurnitureSystem.SQLite.Data;
    using MongoDB.Driver.Builders;
    using System.Collections.Generic;

    public class FurnitureSystemEntryPoint
    {
        public static void Main()
        {
            string zipPath = @"../../../ExcelReports.zip";
            string extractPath = @"../../../ExcelReports/";
            ZipExtractor.Extract(zipPath, extractPath);

            //SQL SERVER DATABASE
            var sqlServerDatabase = new FurnitureSystemDbContext();

            //MONGODB DATABASE
            var connectionStr = "mongodb://localhost/";
            var mongoClient = new MongoClient(connectionStr);
            var mongoServer = mongoClient.GetServer();
            var mongoDatabase = mongoServer.GetDatabase("ShopSystem");

            ReadAndSaveDataFromMongoToSqlServer(sqlServerDatabase, mongoDatabase);

            ReadAndSaveDataFromExcelToSqlServer(sqlServerDatabase);

            MakeConnectionsBetweenShopsAndFurniture(sqlServerDatabase);

            PdfExporter.Write(sqlServerDatabase);

            XmlWriter.GenerateReports(sqlServerDatabase);

            //MYSQL DATABASE
            var mySqlDatabase = new FurnitureSystemEntities();

            var json = new JsonReporter();
            json.Report();

            ReadXmlUpdateSqlAndMongoBases(sqlServerDatabase, mongoDatabase);

            var dataFromMySql = mySqlDatabase.Jsonreports;

            //SQLITE DATABASE
            var customerData = new CustomerContext();

            ReadFromMySqlAndSqLiteAndWriteInExcel(dataFromMySql, customerData);
        }

        private static void MakeConnectionsBetweenShopsAndFurniture(FurnitureSystemDbContext sqlServerDatabase)
        {
            var shops = sqlServerDatabase.Shops;
            var furniture = sqlServerDatabase.FurniturePieces;
            var index = 1;

            int startIndex = 1;
            int endIndex = 3;
            foreach (var shop in shops)
            {
                var start = startIndex;
                foreach (var furniturePiece in furniture)
                {
                    if (start < endIndex)
                    {
                        shop.FurniturePieces.Add(furniturePiece);
                        furniturePiece.Price.Money *= (1 + shop.ProfitPercentage);
                    }

                    start += 1;
                }
                //var firstFurniture = furniture.FirstOrDefault(x => x.Id == index);
                //shop.FurniturePieces.Add(firstFurniture);
                //firstFurniture.Price.Money *= (1 + shop.ProfitPercentage);
                //index++;
                startIndex++;
                endIndex++;
            }
        }

        private static void ReadAndSaveDataFromMongoToSqlServer(FurnitureSystemDbContext sqlServerDatabase, MongoDatabase shopSystemDb)
        {
            if (!shopSystemDb.CollectionExists("ShopSystem"))
            {
                var seeder = new Seeder(shopSystemDb);
                seeder.SeedShops();
            }
            if (sqlServerDatabase.Shops.Count() == 0)
            {
                var retriever = new DataRetriever(shopSystemDb);
                var shops = retriever.GetShopsLocal();
                foreach (var item in shops)
                {
                    var shop = new Shop
                    {
                        Name = item.Name,
                        ProfitPercentage = item.ProfitPercentage
                    };

                    var location = new Location
                    {
                        Shop = shop,
                        Country = item.Country,
                        City = item.City,
                        Street = item.Street,
                        Number = item.StreetNumber
                    };

                    sqlServerDatabase.Shops.Add(shop);
                    sqlServerDatabase.Locations.Add(location);
                    sqlServerDatabase.SaveChanges();
                }
            }
        }

        private static void ReadAndSaveDataFromExcelToSqlServer(FurnitureSystemDbContext sqlServerDatabase)
        {
            var excel = new ExcelReader();
            var items = excel.GetExtractedFilesInfo("../../../ExcelReports/Read/");
            if (sqlServerDatabase.Manufacturers.Count() == 0)
            {
                foreach (var item in items)
                {
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

                    var section = new Section
                    {
                        Name = item.Item2
                    };

                    var manufacturer = new Manufacturer
                    {
                        Name = item.Item1
                    };


                    var existingManufacturer = sqlServerDatabase.Manufacturers.FirstOrDefault(x => x.Name == item.Item1);

                    if (existingManufacturer == null)
                    {
                        section.FurniturePieces.Add(product);
                        manufacturer.Sections.Add(section);
                        sqlServerDatabase.Manufacturers.Add(manufacturer);
                    }
                    else
                    {
                        var existingSection = existingManufacturer.Sections.FirstOrDefault(x => x.Name == item.Item2);
                        if (existingSection == null)
                        {
                            section.FurniturePieces.Add(product);
                            existingManufacturer.Sections.Add(section);
                        }
                        else
                        {
                            var existingProduct = existingSection.FurniturePieces.FirstOrDefault(x => x.Name == item.Item3);
                            if (existingProduct == null)
                            {
                                existingSection.FurniturePieces.Add(product);
                            }
                        }
                    }

                    sqlServerDatabase.SaveChanges();
                }
            }
        }
        private static void ReadXmlUpdateSqlAndMongoBases(FurnitureSystemDbContext sqlServerDatabase, MongoDatabase mongoDatabase)
        {
            var shopsWithFurniture = XmlReader.GetObjects("../../../XMLReports/ShopReport.xml");

            foreach (var shop in shopsWithFurniture)
            {
                var newShop = new Shop
                {
                    Name = shop.Item1,
                    ProfitPercentage = shop.Item6
                };

                sqlServerDatabase.Shops.Add(newShop);

                sqlServerDatabase.Locations.Add(new Location
                {
                    Shop = newShop,
                    Country = shop.Item2,
                    City = shop.Item3,
                    Street = shop.Item4,
                    Number = shop.Item5
                });

                var newMongoShop = new ShopLocation(shop.Item1, shop.Item2, shop.Item3, shop.Item4, shop.Item5, shop.Item6);
                var shops = mongoDatabase.GetCollection("ShopSystem");

                shops.Insert(newMongoShop);
            }

            sqlServerDatabase.SaveChanges();
        }

        private static void ReadFromMySqlAndSqLiteAndWriteInExcel(IQueryable<Jsonreport> dataFromMySql, CustomerContext sqLiteDatabase)
        {
            var dataFromSqLite = sqLiteDatabase.Customers.ToList();
            var customers = new List<Tuple<string, string, int, decimal>>();

            foreach (var customer in dataFromSqLite)
            {
                var id1 = customer.ProductId;
                var product = dataFromMySql.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).FirstOrDefault(x => x.Id == customer.ProductId);
                var cName = customer.Name;
                var pName = product.Name;
                var quantity = customer.ProductQuantity;
                var price = decimal.Parse(product.Price);
                var newCustomer = new Tuple<string, string, int, decimal>(cName, pName, quantity, price);

                customers.Add(newCustomer);
            }

            ExcelWriter.GenerateReports(customers);
        }
    }
}