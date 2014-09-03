namespace FurnitureSystem.MySQLReader
{
    using System;
    using System.Linq;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using FurnitureSystem.JsonReporter;

    public static class SQLReader
    {
        static string connectionStr = "Server=localhost; Database=furnituresystemreports; Uid=root; Pwd=root; pooling=true";
        static MySqlConnection connection = new MySqlConnection(connectionStr);

        public static IList<JsonReport> ReadReportsData()
        {
            List<JsonReport> furnitureReportData = new List<JsonReport>();

            connection.Open();
            using (connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM jsonreports", connection);
                MySqlDataReader reportsReader = command.ExecuteReader();

                Console.WriteLine("Listing all reports.\n");
                using (reportsReader)
                {
                    while (reportsReader.Read())
                    {
                        int furnitureId = (int)reportsReader["Id"];
                        string furnitureName = (string)reportsReader["Name"];
                        decimal furniturePrice = (decimal)reportsReader["Price"];

                        furnitureReportData.Add(new JsonReport
                        {
                            FurnitureId=furnitureId,
                            FurnitureName=furnitureName,
                            Price=furniturePrice
                        });
                    }
                }
            }

            return furnitureReportData;
        }
    }
}
