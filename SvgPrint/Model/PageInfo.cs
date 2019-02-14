using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgPrint.Model
{
    public class PageInfo
    {
        private readonly IEnumerable<PageDimension> dimensions = new List<PageDimension>()
        {
            new PageDimension("A0", 841, 1189,Orientation.Vertical),
            new PageDimension("A1", 594, 841, Orientation.Vertical),
            new PageDimension("A2", 420, 594, Orientation.Vertical),
            new PageDimension("A3", 297, 420, Orientation.Vertical),
            new PageDimension("A4", 210, 297, Orientation.Vertical),
            new PageDimension("A5", 148, 210, Orientation.Vertical),
            new PageDimension("A6", 105, 148, Orientation.Vertical),
            new PageDimension("A7", 74,  105, Orientation.Vertical),
            new PageDimension("A8", 52,  74,  Orientation.Vertical),

            new PageDimension("A0", 1189, 841, Orientation.Horizontal),
            new PageDimension("A1", 841,  594, Orientation.Horizontal),
            new PageDimension("A2", 594,  420, Orientation.Horizontal),
            new PageDimension("A3", 420,  297, Orientation.Horizontal),
            new PageDimension("A4", 297,  210, Orientation.Horizontal),
            new PageDimension("A5", 210,  148, Orientation.Horizontal),
            new PageDimension("A6", 148,  105, Orientation.Horizontal),
            new PageDimension("A7", 105,  74,  Orientation.Horizontal),
            new PageDimension("A8", 74,   52,  Orientation.Horizontal)
        };

        public PageInfo(Page page)
        {
            this.Margin = page.Margin;
            this.CurrentX = this.Margin;
            this.CurrentY = this.Margin;
            this.LineSpacing = page.LineSpacing == 0 ? 1 : page.LineSpacing;

            this.IsDebug = false;

            if (page.Width > 0 && page.Height > 0 && page.Format == null)
            {
                this.Width = page.Width;
                this.Height = page.Height;
            }

            if (page.Width == 0 && page.Height == 0 && page.Format == null)
            {
                throw new Exception("Page dimensions or format must be set");
            }

            var d = this.dimensions.SingleOrDefault(x => x.Name == page.Format && x.Orientation == page.Orientation);

            if (d == null)
            {
                throw new Exception("Invalid or unspupported page format: " + page.Format);
            }

            this.Width = d.Width;
            this.Height = d.Height - 4;
        }

        public bool IsDebug { get; set; }
        public decimal Margin { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal CurrentX { get; set; }
        public decimal CurrentY { get; set; }
        public decimal LineSpacing { get; set; }

        public string Debug => IsDebug ? "stroke='red' stroke-width='.25' fill='silver'" : "fill='white'";
    }
}
