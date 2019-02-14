using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;
using SvgPrint.Model;

namespace SvgPrint.Elements
{
    public class Field : IPageDataContent
    {
        [XmlAttribute]
        public string DataField { get; set; }

        [XmlAttribute]
        public bool PrintAfter { get; set; }

        [XmlAttribute]
        public decimal Width { get; set; }

        public RenderingResult Render(object data, PageInfo pageInfo)
        {
            var labelWidth = 50;
            var topPadding = -.5m;
            var leftPadding = .5m;

            var fontSize = 4m;
            var height = fontSize + .5m;

            var property = data.GetType().GetProperty(DataField);

            var label = property.GetCustomAttribute<DisplayAttribute>()?.Name ?? DataField;

            var value = property.GetValue(data);

            var content = string.Empty;

            if (label == string.Empty)
            {
                labelWidth = 0;
            }
            else
            {
                /* LABEL */
                content += $"<text class='fieldLabel' x='{pageInfo.CurrentX + leftPadding}' y='{pageInfo.CurrentY + topPadding + fontSize}' font-size='{fontSize}'>{label}</text>" + Environment.NewLine;
            }

            //$"<rect x='{pageInfo.CurrentX}' y='{pageInfo.CurrentY}' width='{pageInfo.Width - pageInfo.Margin * 2}' height='{height}' {pageInfo.Debug} />" + Environment.NewLine +

            /* DATA */
            content += $"<text class='fieldData' x='{pageInfo.CurrentX + labelWidth}' y='{pageInfo.CurrentY + topPadding + fontSize}' font-size='{fontSize}'>{value}</text>" + Environment.NewLine;

            if (this.PrintAfter)
            {
                pageInfo.CurrentX = this.Width + labelWidth;
            }
            else
            {
                pageInfo.CurrentX = pageInfo.Margin;
                pageInfo.CurrentY += height;
            }


            return new RenderingResult
            {
                Content = content,
                PageInfo = pageInfo
            };
        }
    }
}
