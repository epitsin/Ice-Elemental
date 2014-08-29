namespace FurnitureSystem.Xml
{
    using System.Linq;
    using System.Xml.Linq;

    using FurnitureSystem.Data;

    public class XmlWriter
    {
        public static void GenerateReports()
        {
            var path = "../../../XMLReports/ShopReport.xml";
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                var root = new XElement("shops-with-furniture");

                var shops = database.Shops;

                foreach (var shop in shops)
                {
                    var furnitures = shop.FurniturePieces;
                    var currentShop = new XElement("shop");
                    currentShop.SetAttributeValue("name", shop.Name);
                    currentShop.SetAttributeValue("location", shop.Location.City + shop.Location.Street + shop.Location.Number);

                    foreach (var furniture in furnitures)
                    {
                        var shopInfo = new XElement("furnitures");
                        shopInfo.Add(new XElement("name", furniture.Name));
                        shopInfo.Add(new XElement("price", furniture.Price.Money));

                        currentShop.Add(shopInfo);
                    }

                    root.Add(currentShop);
                }

                root.Save(path);
            }
        }
    }
}
