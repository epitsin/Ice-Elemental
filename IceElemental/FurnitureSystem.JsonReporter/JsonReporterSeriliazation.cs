namespace FurnitureSystem.JsonReporter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;

    using FurnitureSystem.Data;
    using FurnitureSystem.Models;

    public class JsonReporterSeriliazation
    {
        public void GetJsonObjects()
        {
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                var jr = database.FurniturePieces.Select(x =>
                    new Helper
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price.Money
                    });

                foreach (var item in jr)
                {
                    var stream1 = new MemoryStream();
                    var ser = new DataContractJsonSerializer(typeof(Helper));
                    ser.WriteObject(stream1, item);

                    stream1.Position = 0;
                    var sr = new StreamReader(stream1);
                    var sth = sr.ReadToEnd();

                    Console.WriteLine(sth);

                    var we = new StreamWriter(string.Format("../../../JSONReports/{0}.json", item.Id));
                    using (we)
                    {
                        we.Write(sth);
                    }
                }
            }
        }
    }
}
