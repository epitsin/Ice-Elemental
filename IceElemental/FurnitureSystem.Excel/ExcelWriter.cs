namespace FurnitureSystem.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;

    public static class ExcelWriter
    {
        public static void GenerateReports(List<Tuple<string, string, int, decimal>> customers)
        {
            var excelConnection = new OleDbConnection(Settings.Default.writerConnection);
            excelConnection.Open();

            var excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            var sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

            using (excelConnection)
            {
                foreach (var customer in customers)
                {
                    var excelCommand = new OleDbCommand(@"INSERT INTO [" + sheetName + @"] VALUES (@customerName, @productName, @productQuantity, @productPrice, @totalIncome)", excelConnection);

                    excelCommand.Parameters.AddWithValue("@customerName", customer.Item1);
                    excelCommand.Parameters.AddWithValue("@productName", customer.Item2);
                    excelCommand.Parameters.AddWithValue("@productQuantity", customer.Item3);
                    excelCommand.Parameters.AddWithValue("@productPrice", customer.Item4);
                    excelCommand.Parameters.AddWithValue("@totalIncome", customer.Item3 * customer.Item4);
                    excelCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
