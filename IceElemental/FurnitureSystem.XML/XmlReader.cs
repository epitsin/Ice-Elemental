
namespace FurnitureSystem.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlReader
    {
        public static List<Tuple<string, string, string>> GetObjects(string fileName)
        {
            List<Tuple<string, string, string>>
            shopsWithFurniture = new List<Tuple<string, string, string>>();

            XDocument xmlDoc = XDocument.Load(fileName);

            //foreach (var sale in xmlDoc.Descendants("shop"))
            //{
            //    string vendorName = sale.Attribute("vendor").Value;

            //    foreach (var expense in sale.Descendants("expenses"))
            //    {
            //        string month = expense.Attribute("month").Value;
            //        string value = expense.Value;

            //        shopsWithFurniture.Add(new Tuple<string, string, string>(vendorName, month, value));
            //    }
            //}

            return shopsWithFurniture;
        }
    }
}
