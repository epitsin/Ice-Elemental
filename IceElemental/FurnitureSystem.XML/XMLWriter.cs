namespace FurnitureSystem.Xml
{
    using System.Linq;
    using System.Xml.Linq;
    using System.Data.Entity;

    using FurnitureSystem.Data;

    public class XmlWriter
    {
        public static void GenerateReports()
        {
            var path = "../../../XMLReports/ShopReport.xml";
            var database = new FurnitureSystemDbContext();
            using (database)
            {
                var root = new XElement("Shops");

                var shopsWithFurniture = (
                            from s in database.Shops
                            from f in database.FurniturePieces
                            select new
                            {
                                Shop = s.Name,
                                Location = s.Location.City + s.Location.Street + s.Location.Number,
                                Furniture = f.Name,
                                Price = f.Price
                            });

                foreach (var shop in shopsWithFurniture)
                {
                    var currentShop = new XElement("Shop");
                    currentShop.SetAttributeValue("name", shop.Shop);
                    currentShop.SetAttributeValue("furniture", shop.Furniture);

                    var shopInfo = new XElement("Details");
                    shopInfo.Add(new XElement("Location", shop.Location));
                    shopInfo.Add(new XElement("Price", shop.Price));

                    currentShop.Add(shopInfo);
                    root.Add(currentShop);
                }
                root.Save(path);
            }
        }
    }
}
