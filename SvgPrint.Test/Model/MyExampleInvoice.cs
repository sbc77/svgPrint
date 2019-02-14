using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SvgPrint.Model
{
    public class MyExampleInvoice
    {
        [Display(Name = "Invoice No")]
        public string InvoiceId { get; set; }

        public string Barcode { get; set; }

        [Display(Name = "Date Created")]
        public DateTime Date { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        public IList<MyInvoiceLine> Lines { get; set; }

        public double TotalValue => Lines.Sum(l => l.Amount);

        public static MyExampleInvoice GenData()
        {
            var data = new MyExampleInvoice
            {
                InvoiceId = "INV00001/2019",
                CustomerAddress = "Maria Schürrerstrasse 44, Brügg",
                CustomerName = "SCHWEIZ AG",
                Barcode = "(00)34123412342314",
                Date = DateTime.Now,
                Lines = new List<MyInvoiceLine>()

            };

            for (var i = 1; i < 15; i++)
            {
                data.Lines.Add(
                new MyInvoiceLine
                {
                    PositionNo = i,
                    ArticleId = $"ART00{i:D2}",
                    ArticleDescription = $"Some amazing article {i:D2}",
                    Quantity = 10 * i,
                    Price = 2.99 + i
                });

            }

            return data;
        }
    }
}
