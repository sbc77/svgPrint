using System;
using System.IO;
using SvgPrint.Model;
using System.Text;
using System.Xml.Serialization;

namespace SvgPrint.Elements
{
    public class Image : IPageContent
    {
        [XmlAttribute]
        public string Src { get; set; }

        [XmlAttribute]
        public string Link { get; set; }

        [XmlAttribute]
        public decimal Width { get; set; }

        [XmlAttribute]
        public decimal Height { get; set; }

        public RenderingResult Render(PageInfo pageInfo)
        {
            string imageLink;
            if (Src != null)
            {
                var imageData = File.ReadAllText(Src);
                var ext = Path.GetExtension(Src).ToLower().Replace(".", string.Empty);

                if (ext == "svg")
                {
                    ext = "svg+xml";
                }

                var imageData64 = Base64Encode(imageData);
                imageLink = $"data:image/{ext};base64,{imageData64}";
            }
            else
            {
                imageLink = this.Link;
            }

            var content = $"<image xlink:href='{imageLink}' " +
                (this.Height == 0 ? string.Empty : $"height='{this.Height}' ") +
                (this.Width == 0 ? string.Empty : $"width='{this.Width}' ") +
                $"x='{pageInfo.CurrentX}' y='{pageInfo.CurrentY}' />" + Environment.NewLine;

            pageInfo.CurrentY += Height;

            return new RenderingResult
            {
                Content = content,
                PageInfo = pageInfo
            };
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}