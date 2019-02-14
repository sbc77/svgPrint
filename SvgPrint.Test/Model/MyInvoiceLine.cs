using System.ComponentModel.DataAnnotations;

namespace SvgPrint.Model
{
    public class MyInvoiceLine
    {
        [Display(Name = "No")]
        public int PositionNo { get; set; }

        [Display(Name = "Article ID")]
        public string ArticleId { get; set; }

        [Display(Name = "Description")]
        public string ArticleDescription { get; set; }

        public double Price { get; set; }

        public double Quantity { get; set; }

        public double Amount => Price * Quantity;
    }
}
