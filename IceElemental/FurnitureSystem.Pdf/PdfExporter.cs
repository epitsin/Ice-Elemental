namespace FurnitureSystem.Pdf
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using FurnitureSystem.Data;
    using iTextSharp.text;

    public static class PdfExporter
    {
        public static void Export()
        {
            var database = new FurnitureSystemDbContext();
            var strBuilder = new StringBuilder();

            var shops = database.Shops.Select(x => x.Name).ToArray();

            strBuilder.Append("<table border='1'>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<th style=\"font-size:16px; text-align:center;\" colspan='4'>Shops Report</th>");
            strBuilder.Append("</tr>");
            strBuilder.Append("</table>");

            for (int i = 0; i < shops.Count(); i++)
            {
                strBuilder.Append("<table border='1'>");
                strBuilder.Append("<tr bgcolor='#BBBBBB'>");
                strBuilder.AppendFormat("<th colspan='4'>Shop: {0}</th>", shops[i]);
                strBuilder.Append("</tr>");
                strBuilder.Append("<tr bgcolor='#BBBBBB'>");
                strBuilder.Append("<th class=\"th\"><b>Furniture</b></th>");
                strBuilder.Append("<th><b>Section</b></th>");
                strBuilder.Append("<th><b>Price</b></th>");
                strBuilder.Append("<th><b>Colour</b></th>");
                strBuilder.Append("</tr>");

                var shopName = shops[i];
                var furnitures =
                                from s in database.Shops
                                from f in database.FurniturePieces
                                where s.Name == shopName
                                select new
                                {
                                    Name = f.Name,
                                    Section = f.Section.Name,
                                    Price = f.Price,
                                    Colour = f.Colours.FirstOrDefault().Name
                                };

                foreach (var furniture in furnitures)
                {
                    strBuilder.Append("<tr>");
                    strBuilder.AppendFormat("<td>{0}</td>", furniture.Name);
                    strBuilder.AppendFormat("<td>{0}</td>", furniture.Section);
                    strBuilder.AppendFormat("<td>{0}</td>", furniture.Price.Money);
                    strBuilder.AppendFormat("<td>{0}</td>", furniture.Colour);
                    strBuilder.Append("</tr>");
                }

                strBuilder.Append("</table>");

                if (i != 2)
                {
                    strBuilder.Append("<br />");
                }
            }

            var builder = new PdfBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            var page = builder.AddPage();
            page.AppendHtml(strBuilder.ToString());

            var file = builder.RenderPdf();
            var tempFolder = "../../../PDFReports/";
            var tempFileName = string.Format("{0}-{1}.pdf", DateTime.Now.ToString("yyyy-MM-dd"), Guid.NewGuid());
            if (Helpers.DirectoryExist(tempFolder))
            {
                if (!File.Exists(string.Format("{0}{1}", tempFolder, tempFileName)))
                {
                    File.WriteAllBytes(string.Format("{0}{1}", tempFolder, tempFileName), file);
                }
            }
        }
    }
}