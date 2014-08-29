
namespace FurnitureSystem.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlReader
    {
        public static List<Tuple<string, string, string, decimal>> GetObjects(string fileName)
        {
            List<Tuple<string, string, string, decimal>>
            shopsWithFurniture = new List<Tuple<string, string, string, decimal>>();

            XDocument xmlDoc = XDocument.Load(fileName);

            foreach (var shop in xmlDoc.Descendants("shop"))
            {
                var shopName = shop.Attribute("name").Value;
                var shopLocation = shop.Attribute("location").Value;

                foreach (var furniture in shop.Descendants("furnitures"))
                {
                    var furnitureName = furniture.Element("name").Value;
                    var furniturePrice = furniture.Element("price").Value;

                        shopsWithFurniture.Add(new Tuple<string, string, string, decimal>(shopName, shopLocation, furnitureName, decimal.Parse(furniturePrice)));
                }
            }

            return shopsWithFurniture;
        }
    }
}
