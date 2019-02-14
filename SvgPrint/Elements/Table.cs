using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SvgPrint.Model;

namespace SvgPrint.Elements
{
    public class Table : IPageDataContent
    {
        private const decimal colWidth = 30;
        private const decimal fontSize = 4;

        [XmlAttribute]
        public string DataField { get; set; }

        [XmlElement("Column")]
        public Column[] Columns { get; set; }

        public RenderingResult Render(object data, PageInfo pageInfo)
        {
            var content = string.Empty;

            content += this.DrawHeader(pageInfo, data);
            content += this.DrawTable(pageInfo, data);

            return new RenderingResult
            {
                Content = content,
                PageInfo = pageInfo
            };
        }

        private string DrawHeader(PageInfo pageInfo, object data)
        {
            var content = string.Empty;

            var propertyType = data.GetType().GetProperty(this.DataField);
            var collectionRow = ((IEnumerable<object>)propertyType.GetValue(data)).First();

            foreach (var column in Columns)
            {
                var prop = collectionRow.GetType().GetProperty(column.DataField);
                var label = prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? column.DataField;
                var width = column.Width == 0 ? colWidth : column.Width;
                content += this.DrawText(pageInfo, label, "tableHeaderCell", width);
                pageInfo.CurrentX += width;
            }
            pageInfo.CurrentY += fontSize;
            pageInfo.CurrentX = pageInfo.Margin;
            content += this.DrawLine(pageInfo);

            return content;
        }

        private string DrawTable(PageInfo pageInfo, object data)
        {
            var propertyType = data.GetType().GetProperty(this.DataField);
            var collection = (IEnumerable<object>)propertyType.GetValue(data);
            var content = string.Empty;

            foreach (var row in collection)
            {
                foreach (var column in Columns)
                {
                    var property = row.GetType().GetProperty(column.DataField);
                    var propertyVal = property.GetValue(row);
                    var width = column.Width == 0 ? colWidth : column.Width;
                    content += this.DrawText(pageInfo, propertyVal.ToString(), "tableCell", width, propertyVal is double);
                    pageInfo.CurrentX += width;
                }

                pageInfo.CurrentX = pageInfo.Margin;
                pageInfo.CurrentY += fontSize;
            }


            content += this.DrawLine(pageInfo);

            return content;
        }

        private string DrawLine(PageInfo pageInfo)
        {
            pageInfo.CurrentY += 1.5m;
            var result = $"<line x1='{pageInfo.Margin}' y1='{pageInfo.CurrentY}' " +
                $"x2='{pageInfo.Width - pageInfo.Margin * 2}' y2='{pageInfo.CurrentY}' stroke-width='.25' stroke='black' />" + Environment.NewLine;
            pageInfo.CurrentY += 1;

            return result;
        }

        private string DrawText(PageInfo pageInfo, string text, string className, decimal width, bool alignRight = false)
        {
            var alignTxt = alignRight ? "text-anchor='end'" : string.Empty;
            return $"<text class='{className}' x='{pageInfo.CurrentX }' y='{pageInfo.CurrentY + fontSize}' " +
                    $"font-size='{fontSize}' {alignTxt}>{text}</text>" + Environment.NewLine;
        }
    }
}