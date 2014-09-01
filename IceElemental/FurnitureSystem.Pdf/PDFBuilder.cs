namespace FurnitureSystem.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using iTextSharp.text;
    using iTextSharp.text.html;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;

    public class PdfBuilder
    {
        /// <summary>
        /// Delegate for rendering events
        /// </summary>
        public delegate void RenderEvent(PdfWriter writer, Document document);

        /// <summary>
        /// A page to insert into a HtmlToPdfBuilder Class
        /// </summary>
        public class HtmlPdfPage
        {
            //parts for generating the page
            internal StringBuilder Html;

            /// <summary>
            /// The default information for this page
            /// </summary>
            public HtmlPdfPage()
            {
                this.Html = new StringBuilder();
            }

            /// <summary>
            /// Appends the formatted HTML onto a page
            /// </summary>
            public virtual void AppendHtml(string content, params object[] values)
            {
                this.Html.AppendFormat(content, values);
            }
        }

        /// <summary>
        /// Simplifies generating HTML into a PDF file -http://aspnettutorialonline.blogspot.com/
        /// </summary>
        public class HtmlToPdfBuilder
        {
            private const string DocumentHtmlEnd = "</body></html>";

            private const string DocumentHtmlStart = "<html><head></head><body>";

            //amazing regular expression magic
            private const string RegexGetStyles = @"(?<selector>[^\{\s]+\w+(\s\[^\{\s]+)?)\s?\{(?<style>[^\}]*)\}";

            private const string RegexGroupSelector = "selector";

            private const string RegexGroupStyle = "style";

            private const string StyleDefaultType = "style";

            private readonly List<HtmlPdfPage> pages;

            private readonly StyleSheet styles;

            /// <summary>
            /// Creates a new PDF document template. Use PageSizes.{DocumentSize}
            /// </summary>
            public HtmlToPdfBuilder(Rectangle size)
            {
                this.PageSize = size;
                this.pages = new List<HtmlPdfPage>();
                this.styles = new StyleSheet();
            }

            /// <summary>
            /// Method to override to have additional control over the document
            /// </summary>
            public event RenderEvent AfterRender;

            /// <summary>
            /// Method to override to have additional control over the document
            /// </summary>
            public event RenderEvent BeforeRender;

            /// <summary>
            /// Returns a list of the pages available
            /// </summary>
            public HtmlPdfPage[] Pages
            {
                get
                {
                    return this.pages.ToArray(); //http://aspnettutorialonline.blogspot.com/
                }
            }

            /// <summary>
            /// The page size to make this document
            /// </summary>
            public Rectangle PageSize { get; set; }

            /// <summary>
            /// Returns the page at the specified index
            /// </summary>
            public HtmlPdfPage this[int index]
            {
                get
                {
                    return this.pages[index];
                }
            }

            /// <summary>
            /// Appends and returns a new page for this document http://aspnettutorialonline.blogspot.com/
            /// </summary>
            public HtmlPdfPage AddPage()
            {
                HtmlPdfPage page = new HtmlPdfPage();
                this.pages.Add(page);
                return page;
            }

            /// <summary>
            /// Appends a style for this sheet http://aspnettutorialonline.blogspot.com/
            /// </summary>
            public void AddStyle(string selector, string styles)
            {
                this.styles.LoadTagStyle(selector, HtmlToPdfBuilder.StyleDefaultType, styles);
            }

            /// <summary>
            /// Imports a stylesheet into the document
            /// </summary>
            public void ImportStylesheet(string path)
            {
                //load the file
                string content = File.ReadAllText(path);

                //use a little regular expression magic
                foreach (Match match in Regex.Matches(content, HtmlToPdfBuilder.RegexGetStyles))
                {
                    string selector = match.Groups[HtmlToPdfBuilder.RegexGroupSelector].Value;
                    string style = match.Groups[HtmlToPdfBuilder.RegexGroupStyle].Value;
                    this.AddStyle(selector, style);
                }
            }

            /// <summary>
            /// Moves a page after another
            /// </summary>
            public void InsertAfter(HtmlPdfPage page, HtmlPdfPage after)
            {
                this.pages.Remove(page);
                this.pages.Insert(
                    Math.Min(this.pages.IndexOf(after) + 1, this.pages.Count),
                    page);
            }

            /// <summary>
            /// Moves a page before another
            /// </summary>
            public void InsertBefore(HtmlPdfPage page, HtmlPdfPage before)
            {
                this.pages.Remove(page);
                this.pages.Insert(
                    Math.Max(this.pages.IndexOf(before), 0),
                    page);
            }

            /// <summary>
            /// Removes the page from the document http://aspnettutorialonline.blogspot.com/
            /// </summary>
            public void RemovePage(HtmlPdfPage page)
            {
                this.pages.Remove(page);
            }

            /// <summary>
            /// Renders the PDF to an array of bytes
            /// </summary>
            public byte[] RenderPdf()
            {
                //Document is inbuilt class, available in iTextSharp
                MemoryStream file = new MemoryStream();
                Document document = new Document(this.PageSize);
                PdfWriter writer = PdfWriter.GetInstance(document, file);

                //allow modifications of the document
                if (this.BeforeRender is RenderEvent)
                {
                    this.BeforeRender(writer, document);
                }

                //header
                document.Add(new Header(Markup.HTML_ATTR_STYLESHEET, string.Empty));
                document.Open();

                //render each page that has been added
                foreach (HtmlPdfPage page in this.pages)
                {
                    document.NewPage();

                    //generate this page of text
                    MemoryStream output = new MemoryStream();
                    StreamWriter html = new StreamWriter(output, Encoding.UTF8);

                    //get the page output
                    html.Write(string.Concat(HtmlToPdfBuilder.DocumentHtmlStart, page.Html.ToString(), HtmlToPdfBuilder.DocumentHtmlEnd));
                    html.Close();
                    html.Dispose();

                    //read the created stream
                    MemoryStream generate = new MemoryStream(output.ToArray());
                    StreamReader reader = new StreamReader(generate);
                    foreach (object item in HTMLWorker.ParseToList(reader, this.styles))
                    {
                        document.Add((IElement)item);
                    }

                    //cleanup these streams
                    html.Dispose();
                    reader.Dispose();
                    output.Dispose();
                    generate.Dispose();
                }

                //after rendering
                if (this.AfterRender is RenderEvent)
                {
                    this.AfterRender(writer, document);
                }

                //return the rendered PDF
                document.Close();
                return file.ToArray();
            }
        }
    }
}