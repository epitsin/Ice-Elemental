namespace FurnitureSystem.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlReader
    {
        public static List<Tuple<string, int>> GetObjects(string fileName)
        {
            var shopsWithFurniture = new List<Tuple<string, int>>();

            var xmlDoc = XDocument.Load(fileName);


            foreach (var furniture in xmlDoc.Descendants("furnitures"))
            {
                var furnitureName = furniture.Element("name").Value;
                var shopName = furniture.Element("discount").Value;

                shopsWithFurniture.Add(new Tuple<string, int>(furnitureName, int.Parse(shopName)));
            }

            return shopsWithFurniture;
        }
    }
}