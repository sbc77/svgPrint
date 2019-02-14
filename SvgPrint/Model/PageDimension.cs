using System;
using System.Collections.Generic;

namespace SvgPrint.Model
{
    public class PageDimension
    {
        public PageDimension(string name, decimal width, decimal height, Orientation orientation)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Orientation = orientation;
        }

        public string Name { get; }

        public decimal Width { get; }

        public decimal Height { get; }

        public Orientation Orientation { get; }
    }
}