namespace FurnitureSystem.MySQLReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FurnitureSystem.JsonReporter;
    using MySql.Data.MySqlClient;

    public static class SQLReader
    {
        private static readonly string connectionStr = "Server=localhost; Database=furnituresystemreports; Uid=root; Pwd=root; pooling=true";

        private static readonly MySqlConnection connection = new MySqlConnection(connectionStr);

        public static IList<JsonReport> ReadReportsData()
        {
            var furnitureReportData = new List<JsonReport>();

            connection.Open();
            using (connection)
            {
                var command = new MySqlCommand("SELECT * FROM jsonreports", connection);
                var reportsReader = command.ExecuteReader();

                Console.WriteLine("Listing all reports.");
                using (reportsReader)
                {
                    while (reportsReader.Read())
                    {
                        int furnitureId = (int)reportsReader["Id"];
                        string furnitureName = (string)reportsReader["Name"];
                        decimal furniturePrice = (decimal)reportsReader["Price"];

                        furnitureReportData.Add(new JsonReport
                        {
                            FurnitureId = furnitureId,
                            FurnitureName = furnitureName,
                            Price = furniturePrice
                        });
                    }
                }
            }

            return furnitureReportData;
        }
    }
}
