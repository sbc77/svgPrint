using System.ComponentModel.DataAnnotations;

namespace SvgPrint.Model
{
    public class MyGs1Data
    {
        [Display(Name = "")]
        public string CustomerName { get; set; }

        [Display(Name = "")]
        public string CustomerStreet { get; set; }

        [Display(Name = "")]
        public string CustomerZip { get; set; }

        [Display(Name = "")]
        public string CustomerCity { get; set; }

        [Display(Name = "SSCC-Nr:")]
        public string SsccNo { get; set; }

        [Display(Name = "")]
        public string ArticleId { get; set; }

        [Display(Name = "Inhalt:")]
        public string ArticleEan { get; set; }

        [Display(Name = "")]
        public string ArticleDescription { get; set; }

        [Display(Name = "Menge:")]
        public decimal Quantity { get; set; }

        [Display(Name = "Bestell-Nr::")]
        public string OrderNo { get; set; }

        public string Barcode1 => $"(02){this.ArticleEan}(37){this.Quantity}(400){this.OrderNo}";

        public string Barcode2 => $"(00){this.SsccNo}";

        public static MyGs1Data GenData()
        {
            return new MyGs1Data
            {
                ArticleDescription = "Some amazing article XL #2, A4, 4-Ring Ø 45 mm, schwarz",
                ArticleId = "0443445.02",
                ArticleEan = "07611365331192",
                SsccNo = "376113650002691585",
                CustomerCity = "Bern",
                CustomerZip = "3000",
                CustomerName = "Migros",
                CustomerStreet = "Industriestrasse 1",
                OrderNo = "20216916",
                Quantity = 12,
            };
        }
    }
}
