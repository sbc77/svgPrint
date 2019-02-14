using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SvgPrint.Model
{
    public class Page
    {
        public PageHeader Header { get; set; }

        public PageFooter Footer { get; set; }

        [XmlAttribute]
        public decimal LineSpacing { get; set; }

        [XmlAttribute]
        public decimal Height { get; set; }

        [XmlAttribute]
        public decimal Width { get; set; }

        [XmlAttribute]
        public decimal Margin { get; set; }

        [XmlAttribute]
        public string Format { get; set; }

        [XmlAttribute]
        public Orientation Orientation { get; set; }

        [XmlIgnore]
        public IEnumerable<IPageContent> Content { get; set; }
    }
}