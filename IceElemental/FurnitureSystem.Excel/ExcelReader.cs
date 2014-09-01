namespace FurnitureSystem.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    public class ExcelReader
    {
        // private const string Provider = "Provider=Microsoft.ACE.OLEDB.12.0;";
        // private const string ExtendedProperties = @";Extended Properties='Excel 12.0 xml;HDR=Yes';";
        // TODO: Extract data as List<Tuple<type, type,...>>
        public void GetExtractedFilesInfo(string extractionPath)
        {
            var connectionStrings = this.CreateConnectionStrings(extractionPath);

            foreach (var connString in connectionStrings)
            {

                var excelConnection = new OleDbConnection(connString);
                excelConnection.Open();

                DataTable excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
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
                                var firstName = reader["Name"];
                                var lastName = reader["Location"];
                                
                                Console.WriteLine(firstName);
                                Console.WriteLine(lastName);
                            }
                        }
                    }
                }
            }
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

            Stack<string> stack = new Stack<string>();
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

                string[] subDirectories = Directory.GetDirectories(currentDirectory);

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
