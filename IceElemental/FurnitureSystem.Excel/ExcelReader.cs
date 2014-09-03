namespace FurnitureSystem.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    public class ExcelReader
    {
        public IList<Tuple<string, string, string, int, int, decimal>> GetExtractedFilesInfo(string extractionPath)
        {
            var connectionStrings = this.CreateConnectionStrings(extractionPath);
            var listOfItems = new List<Tuple<string, string, string, int, int, decimal>>();

            foreach (var connString in connectionStrings)
            {
                var excelConnection = new OleDbConnection(connString);
                excelConnection.Open();

                var excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

                var excelCommand = new OleDbCommand(@"SELECT * FROM [" + sheetName + "]", excelConnection);

                using (excelConnection)
                {
                    using (var oleDbDataAdapter = new OleDbDataAdapter(excelCommand))
                    {
                        var dataSet = new DataSet();
                        oleDbDataAdapter.Fill(dataSet);

                        using (DataTableReader reader = dataSet.CreateDataReader())
                        {
                            while (reader.Read())
                            {
                                var manufacturer = reader["ManufacturerName"].ToString();
                                var section = reader["SectionName"].ToString();
                                var furniture = reader["FurnitureName"].ToString();
                                var material = int.Parse(reader["Material"].ToString());
                                var type = int.Parse(reader["Type"].ToString());
                                var price = decimal.Parse(reader["Price"].ToString());

                                listOfItems.Add(new Tuple<string, string, string, int, int, decimal>(manufacturer, section, furniture, material, type, price));
                            }
                        }
                    }
                }
            }

            return listOfItems;
        }

        private IList<string> CreateConnectionStrings(string directoryPath)
        {
            var connectionStrings = new List<string>();
            var filePaths = this.GetFilePathsWithDfs(directoryPath);

            foreach (var path in filePaths)
            {
                string provider = Settings.Default.provider;
                string dataSource = @"Data Source=" + path;
                string extendedProperties = Settings.Default.extendedSettings;

                string connectionString = provider + dataSource + extendedProperties;
                connectionStrings.Add(connectionString);
            }

            return connectionStrings;
        }

        private IList<string> GetFilePathsWithDfs(string sourceDirectory)
        {
            var filePaths = new List<string>();

            var stack = new Stack<string>();
            stack.Push(sourceDirectory);

            while (stack.Count > 0)
            {
                string currentDirectory = stack.Pop();
                var files = Directory.GetFiles(currentDirectory);

                foreach (var file in files)
                {
                    string filePath = Path.GetFullPath(file);
                    filePaths.Add(filePath);
                }

                var subDirectories = Directory.GetDirectories(currentDirectory);

                for (int i = 0; i < subDirectories.Length; i++)
                {
                    string subDirectory = subDirectories[i].ToString();
                    stack.Push(subDirectory);
                }
            }

            return filePaths;
        }
    }
}
