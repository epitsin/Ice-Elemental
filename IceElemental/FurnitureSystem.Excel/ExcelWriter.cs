namespace FurnitureSystem.Excel
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class ExcelWriter
    {
        public static void GenerateReports()
        {
            var excelConnection = new OleDbConnection(Settings.Default.writerConnection);
            excelConnection.Open();

            DataTable excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

            string name = "Videnov";
            string location = "Sofia, Tsarigradsko, 15";

            var excelCommand = new OleDbCommand(@"INSERT INTO [" + sheetName + @"] VALUES (@name, @location)", excelConnection);

            excelCommand.Parameters.AddWithValue("@name", name);
            excelCommand.Parameters.AddWithValue("@location", location);

            using (excelConnection)
            {
                for (int i = 0; i < 1; i++)
                {
                    var queryResult = excelCommand.ExecuteNonQuery();

                    Console.WriteLine("({0} row(s) affected)", queryResult);
                }
            }
        }
    }
}
