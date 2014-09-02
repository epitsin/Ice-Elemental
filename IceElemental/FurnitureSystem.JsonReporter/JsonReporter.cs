namespace FurnitureSystem.JsonReporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FurnitureSystem.Data;

    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    public class JsonReporter
    {
        private const string ConnectionString = @"Server=localhost;Port=3306;Database=furnituresystemreports;Uid=root;Pwd=root;";
        private const string ReportsPath = "../../../JSONReports/";
        private const string ReportsFileExtension = ".json";

        public void Report()
        {
            Console.WriteLine("Started creating JSON reports...");

            if (!Directory.Exists(ReportsPath))
            {
                Console.WriteLine("Creating Sales reports folder...");
                Directory.CreateDirectory(ReportsPath);
            }

            var dbContext = new FurnitureSystemDbContext();

            var reports = JsonReporter.GetJsonReports(dbContext);

            JsonReporter.SaveReportsToFiles(reports);

            JsonReporter.SendReportToDatabase(reports);

            Console.WriteLine("JSON reports created.");
        }

        private static IQueryable<JsonReport> GetJsonReports(FurnitureSystemDbContext dbContext)
        {
            Console.WriteLine("Generating reports from the SQL Server database data...");

            var furniture = dbContext.FurniturePieces;

            var reports = furniture.Select(x => new JsonReport
                                       {
                                           FurnitureId = x.Id,
                                           FurnitureName = x.Name,
                                           Price = x.Price.Money
                                       });

            return reports;
        }

        private static void SaveReportsToFiles(IQueryable<JsonReport> reports)
        {
            Console.WriteLine("Saving reports to files...");

            var reportsFilePaths = new List<string>();

            foreach (var report in reports)
            {
                string reportFilePath = string.Format("{0}{1}{2}", ReportsPath, report.FurnitureId, ReportsFileExtension);

                var writer = new StreamWriter(reportFilePath);

                using (writer)
                {
                    string serialized = JsonConvert.SerializeObject(report, Formatting.Indented);

                    writer.WriteLine(serialized);

                    reportsFilePaths.Add(reportFilePath);
                }
            }
        }

        private static void SendReportToDatabase(IQueryable<JsonReport> reportsQuery)
        {
            Console.WriteLine("Sending report data to MySQL database...");

            var reports = reportsQuery.ToList<JsonReport>();

            for (int i = 0, length = reports.Count; i < length; i++)
            {
                var report = reports[i];

                var connection = new MySqlConnection(ConnectionString);

                using (connection)
                {
                    connection.Open();

                    string commandText = "INSERT INTO jsonreports(Id, Name, Price) " +
                                  "VALUES (@furnitureId, @furnitureName, @price);";

                    var command = new MySqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@furnitureId", report.FurnitureId);
                    command.Parameters.AddWithValue("@furnitureName", report.FurnitureName);
                    command.Parameters.AddWithValue("@price", report.Price);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
