using System.Xml.Serialization;
using SvgPrint.Model;

namespace SvgPrint.Elements
{
    public class Column : IPageDataContent
    {
        [XmlAttribute]
        public string DataField { get; set; }

        [XmlAttribute]
        public decimal Width { get; set; }


        public RenderingResult Render(object data, PageInfo pageInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}