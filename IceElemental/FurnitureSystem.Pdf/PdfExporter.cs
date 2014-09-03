namespace FurnitureSystem.Pdf
{
    using System;
    using System.IO;
    using System.Linq;
    using FurnitureSystem.Data;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public static class PdfExporter
    {
        public static void Write(FurnitureSystemDbContext database)
        {
            Document pdfReport = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfReport, new FileStream(@"../../../PDFReports/ShopPdfReport.pdf", FileMode.Create, FileAccess.Write));

            var shops = database.Shops;

            pdfReport.Open();

            PdfPTable header = new PdfPTable(1);
            header.AddCell("Shops");
            pdfReport.Add(header);

            float[] widths = new float[] { 45f, 50f, 55f };

            foreach (var shop in shops)
            {
                PdfPTable shopName = new PdfPTable(1);
                shopName.AddCell(shop.Name);
                pdfReport.Add(shopName);

                PdfPTable body = new PdfPTable(3);
                body.SetWidths(widths);

                var furniturePieces = shop.FurniturePieces;

                foreach (var furniture in furniturePieces)
                {
                    body.AddCell(string.Format("{0}", furniture.Name));
                    body.AddCell(string.Format("{0}", furniture.Section.Name));
                    body.AddCell(string.Format("{0:F2}", furniture.Price.Money));
                }

                pdfReport.Add(body);
            }

            pdfReport.Close();
        }
    }
}