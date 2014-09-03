namespace FurnitureSystem.Excel
{
    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

    public class ExcelWriter
    {
        public static void GenerateReports(List<Tuple<string, string, int, decimal>> customers)
        {
            var excelConnection = new OleDbConnection(Settings.Default.writerConnection);
            excelConnection.Open();

            DataTable excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

            var excelCommand = new OleDbCommand(@"INSERT INTO [" + sheetName + @"] VALUES (@customerName, @productName, @productQuantity, @productPrice)", excelConnection);

            foreach (var customer in customers)
            {
                excelCommand.Parameters.AddWithValue("@customerName", customer.Item1);
                excelCommand.Parameters.AddWithValue("@productName", customer.Item2);
                excelCommand.Parameters.AddWithValue("@productQuantity", customer.Item3);
                excelCommand.Parameters.AddWithValue("@productPrice", customer.Item4);
            }

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
