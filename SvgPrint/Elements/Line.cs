using System;
using SvgPrint.Model;

namespace SvgPrint.Elements
{
    public class Line : IPageContent
    {
        public RenderingResult Render(PageInfo pageInfo)
        {
            var margin = 1;
            var content = $"<rect x='{pageInfo.Margin}' y='{pageInfo.CurrentY + margin}' width='{pageInfo.Width - pageInfo.Margin * 2}' height='.25' " +
                "fill='black' />" + Environment.NewLine;

            pageInfo.CurrentY += margin * 2;

            return new RenderingResult
            {
                Content = content,
                PageInfo = pageInfo
            };
        }
    }
}