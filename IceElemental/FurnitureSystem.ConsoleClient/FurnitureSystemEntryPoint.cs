namespace FurnitureSystem.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FurnitureSystem.Data;
    using FurnitureSystem.Excel;
    using FurnitureSystem.JsonReporter;
    using FurnitureSystem.Models;
    using FurnitureSystem.Models.Enums;
    using FurnitureSystem.MongoDb.Data;
    using FurnitureSystem.MySql;
    using FurnitureSystem.Pdf;
    using FurnitureSystem.SQLite.Data;
    using FurnitureSystem.Xml;
    using FurnitureSystem.Zip;
    using MongoDB.Driver;

    public class FurnitureSystemEntryPoint
    {
        private static readonly string zipPath = @"../../../ExcelReports.zip";

        private static readonly string extractPath = @"../../../ExcelReports/";

        private static readonly string mongoDbConnectionString = "mongodb://localhost/";

        private static readonly string excelFilesLocation = "../../../ExcelReports/Read/";

        private static readonly string xmlReportFile = "../../../XMLReports/ShopReport.xml";

        public static void Main()
        {
            Console.WriteLine("Started extracting the zip file...");

            ZipExtractor.Extract(zipPath, extractPath);

            Console.WriteLine("Zip file extracted.");
            Console.WriteLine("Connecting to the SQL Server...");

            ////SQL SERVER DATABASE
            var sqlServerDatabase = new FurnitureSystemDbContext();

            Console.WriteLine("Successfully connected to the SQL Server.");
            Console.WriteLine("Connecting to the MongoDb Server...");

            ////MONGODB DATABASE
            var mongoClient = new MongoClient(mongoDbConnectionString);
            var mongoServer = mongoClient.GetServer();

            Console.WriteLine("Successfully connected to the MongoDb Server.");

            var mongoDatabase = mongoServer.GetDatabase("ShopSystem");

            ReadAndSaveDataFromMongoToSqlServer(sqlServerDatabase, mongoDatabase);

            ReadAndSaveDataFromExcelToSqlServer(sqlServerDatabase);

            MakeConnectionsBetweenShopsAndFurniture(sqlServerDatabase);

            Console.WriteLine("Exporting data to a PDF file...");

            PdfExporter.Write(sqlServerDatabase);

            Console.WriteLine("PDF file is created/updated.");

            XmlWriter.GenerateReports(sqlServerDatabase);

            Console.WriteLine("Connecting to the MySql Server...");

            ////MYSQL DATABASE
            var mySqlDatabase = new FurnitureSystemEntities();

            Console.WriteLine("Successfully connected to the MySql Server.");

            var json = new JsonReporter();
            json.Report();

            ReadXmlUpdateSqlAndMongoBases(sqlServerDatabase, mongoDatabase);

            var dataFromMySql = mySqlDatabase.Jsonreports;

            Console.WriteLine("Connecting to the SQLite database...");

            ////SQLITE DATABASE
            var customerData = new CustomerContext();

            Console.WriteLine("Successfully connected to the SQLite database.");

            ReadFromMySqlAndSqLiteAndWriteInExcel(dataFromMySql, customerData);
        }

        private static void MakeConnectionsBetweenShopsAndFurniture(FurnitureSystemDbContext sqlServerDatabase)
        {
            var shops = sqlServerDatabase.Shops;
            var furniture = sqlServerDatabase.FurniturePieces;
            var randomGenerator = new Random();

            foreach (var shop in shops)
            {
                for (int i = 0; i < 3; i++)
                {
                    var index = randomGenerator.Next(1, 16);
                    var furniturePiece = furniture.FirstOrDefault(x => x.Id == index);
                    shop.FurniturePieces.Add(furniturePiece);
                    furniturePiece.Price.Money *= 1 + shop.ProfitPercentage;
                }
            }
        }

        private static void ReadAndSaveDataFromMongoToSqlServer(FurnitureSystemDbContext sqlServerDatabase, MongoDatabase shopSystemDb)
        {
            if (!shopSystemDb.CollectionExists("ShopSystem"))
            {
                Console.WriteLine("Seeding initial data in the MongoDb database...");

                var seeder = new Seeder(shopSystemDb);
                seeder.SeedShops();
            }

            if (sqlServerDatabase.Shops.Count() == 0)
            {
                Console.WriteLine("Saving the data from MongoDb to the Sql Server database...");

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
            Console.WriteLine("Extracting data from the excel files...");

            var items = ExcelReader.GetExtractedFilesInfo(excelFilesLocation);
            if (sqlServerDatabase.Manufacturers.Count() == 0)
            {
                Console.WriteLine("Reading and saving the data to the Sql Server...");
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
            Console.WriteLine("Getting new data from the xml file...");

            var shopsWithFurniture = XmlReader.GetObjects(xmlReportFile);

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

            Console.WriteLine("Data updated in the MongoDb and Sql Server databases.");
        }

        private static void ReadFromMySqlAndSqLiteAndWriteInExcel(IQueryable<Jsonreport> dataFromMySql, CustomerContext sqLiteDatabase)
        {
            Console.WriteLine("Extracting data from MySql and SQLite databases...");

            var dataFromSqLite = sqLiteDatabase.Customers.ToList();
            var customers = new List<Tuple<string, string, int, decimal>>();

            foreach (var customer in dataFromSqLite)
            {
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

            Console.WriteLine("Generating excel report...");

            ExcelWriter.GenerateReports(customers);

            Console.WriteLine("Excel report generated.");
        }
    }
}