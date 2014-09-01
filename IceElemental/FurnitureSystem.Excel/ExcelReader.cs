namespace FurnitureSystem.Excel
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class ExcelReader
    {
        public void GetData()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:../../../ExcelReports/Furniture.xls;Extended Properties=\'Excel 12.0 xml;HDR=Yes\';";

            var excelConnection = new OleDbConnection(connectionString);
            excelConnection.Open();

            var excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

            var excelCommand = new OleDbCommand(@"SELECT * FROM [" + sheetName + "]", excelConnection);

            using (excelConnection)
            {
                using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(excelCommand))
                {
                    var dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet);

                    using (DataTableReader reader = dataSet.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            var fullName = reader["Name"];
                            var location = reader["Location"];

                            Console.WriteLine(fullName + " -> " + location);
                        }
                    }
                }
            }
        }
    }
}
