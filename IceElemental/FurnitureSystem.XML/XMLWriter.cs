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
                var root = new XElement("shops with furniture");

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
                    var currentShop = new XElement("shop");
                    currentShop.SetAttributeValue("name", shop.Shop);
                    currentShop.SetAttributeValue("location", shop.Location);

                    var shopInfo = new XElement("details");
                    shopInfo.Add(new XElement("furniture", shop.Furniture));
                    shopInfo.Add(new XElement("price", shop.Price));

                    currentShop.Add(shopInfo);
                    root.Add(currentShop);
                }
                root.Save(path);
            }
        }
    }
}
