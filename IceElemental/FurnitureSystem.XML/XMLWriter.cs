﻿namespace FurnitureSystem.Xml
{
    using System.Linq;
    using System.Xml.Linq;

    using FurnitureSystem.Data;

    public static class XmlWriter
    {
        public static string Path = "../../../XMLReports/ManufacturersReport.xml";

        public static void GenerateReports(FurnitureSystemData database)
        {
            var root = new XElement("manufacturers-and-their-products");

            var manufacturers = database.Manufacturers;

            foreach (var manufacturer in manufacturers.All())
            {
                var currentManufacturer = new XElement("manufacturer");
                currentManufacturer.SetAttributeValue("name", manufacturer.Name);

                var sections = manufacturer.Sections;
                foreach (var section in sections)
                {
                    var currentSection = new XElement("section");
                    currentSection.SetAttributeValue("name", section.Name);

                    var products = section.FurniturePieces;
                    foreach (var product in products)
                    {
                        var currentProduct = new XElement("product");
                        currentProduct.Add(new XElement("name", product.Name));
                        currentProduct.Add(new XElement("price", product.Price.Money));

                        currentSection.Add(currentProduct);
                    }

                    currentManufacturer.Add(currentSection);
                }

                root.Add(currentManufacturer);
            }

            root.Save(Path);
        }
    }
}