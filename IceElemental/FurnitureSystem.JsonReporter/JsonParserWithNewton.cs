namespace FurnitureSystem.JsonReporter
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    using FurnitureSystem.Data;

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

            var saveFilesPaths = JsonReporter.SaveReportsToFiles(reports);

            JsonReporter.SendReportToDatabase(reports, saveFilesPaths);

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

        private static List<string> SaveReportsToFiles(IQueryable<JsonReport> reports)
        {
            Console.WriteLine("Saving reports to files...");

            List<string> reportsFilePaths = new List<string>();

            foreach (var report in reports)
            {
                string reportFilePath = string.Format("{0}{1}{2}", ReportsPath, report.FurnitureId, ReportsFileExtension);

                StreamWriter writer = new StreamWriter(reportFilePath);

                using (writer)
                {
                    string serialized = JsonConvert.SerializeObject(report, Formatting.Indented);

                    writer.WriteLine(serialized);

                    reportsFilePaths.Add(reportFilePath);
                }
            }

            return reportsFilePaths;
        }

        private static void SendReportToDatabase(IQueryable<JsonReport> reportsQuery, List<string> saveFilesPaths)
        {
            Console.WriteLine("Sending report data to MySQL database...");

            var reports = reportsQuery.ToList<JsonReport>();

            for (int i = 0, length = reports.Count; i < length; i++)
            {
                JsonReport report = reports[i];
                string reportFilePath = saveFilesPaths[i];

                MySqlConnection connection = new MySqlConnection(ConnectionString);

                using (connection)
                {
                    connection.Open();

                    string commandText = JsonReporter.GetCommandText(connection, reportFilePath);

                    MySqlCommand command = new MySqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@furnitureId", report.FurnitureId);
                    command.Parameters.AddWithValue("@filePath", reportFilePath);
                    command.Parameters.AddWithValue("@furnitureName", report.FurnitureName);
                    command.Parameters.AddWithValue("@price", report.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static string GetCommandText(MySqlConnection connection, string reportFilePath)
        {
            string returnCommand = "INSERT INTO furnituresystemreports(FurnitureId, FurnitureReportFilePath, FurnitureName, Price) " +
                                   "VALUES (@furnitureId, @filePath, @furnitureName, @price);";

            MySqlCommand reportFileCheckCommand = new MySqlCommand("SELECT FurnitureReportFilePath FROM furnituresystemreports", connection);
            var reader = reportFileCheckCommand.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string rowValue = (string)reader["FurnitureReportFilePath"];

                    if (rowValue == reportFilePath)
                    {
                        returnCommand = "UPDATE furnituresystemreports " +
                                        "SET Price = @price " +
                                        "WHERE FurnitureReportFilePath = @filePath;";
                        break;
                    }
                }
            }

            return returnCommand;
        }
    }
}
