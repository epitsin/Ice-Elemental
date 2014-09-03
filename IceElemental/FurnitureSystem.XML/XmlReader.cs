namespace FurnitureSystem.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlReader
    {
        public static List<Tuple<string, string, string, string, int, decimal>> GetObjects(string fileName)
        {
            var shopsWithFurniture = new List<Tuple<string, string, string, string, int, decimal>>();

            var xmlDoc = XDocument.Load(fileName);

            foreach (var furniture in xmlDoc.Descendants("shop"))
            {
                var shopName = furniture.Attribute("name").Value;
                var country = furniture.Element("country").Value;
                var city = furniture.Element("city").Value;
                var street = furniture.Element("street").Value;
                var number = int.Parse(furniture.Element("number").Value);
                var profit = decimal.Parse(furniture.Element("profit").Value);

                shopsWithFurniture.Add(new Tuple<string, string, string, string, int, decimal>(shopName, country, city, street, number, profit));

            }

            return shopsWithFurniture;
        }
    }
}