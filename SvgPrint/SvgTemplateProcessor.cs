using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SvgPrint.Elements;
using SvgPrint.Model;

namespace SvgPrint
{
    public class SvgTemplateProcessor
    {
        private readonly XElement xmltemplate;
        private string style;
        private readonly bool isDebug;

        private SvgTemplateProcessor(string fullPath)
        {
            this.xmltemplate = XElement.Load(fullPath);

            if (this.xmltemplate.Name.LocalName.ToLower() != "template")
            {
                throw new Exception("Missing root node 'template");
            }

            var dbg = this.xmltemplate.Attribute("Debug");
            if (dbg != null && dbg.Value.ToLower() == "true")
            {
                this.isDebug = true;
            }
        }

        public static SvgTemplateProcessor UseTemplate(string fullPath)
        {
            return new SvgTemplateProcessor(fullPath);
        }

        public SvgTemplateProcessor UseStyle(string styleFile)
        {
            this.style = File.ReadAllText(styleFile);
            return this;
        }

        public ProcessingResult Process(object data)
        {
            var result = new List<string>();

            result.Add(string.Empty);
            var currentPage = 0;

            // todo: paging

            foreach (var page in this.xmltemplate.Elements().Where(x => x.Name.LocalName.ToLower() == "page"))
            {
                result[currentPage] += this.ProcessPage(page, data);
            }

            return new ProcessingResult(this, result);
        }

        private string ProcessPage(XElement pageElement, object data)
        {
            var pageSer = new XmlSerializer(typeof(Page));
            var page = (Page)pageSer.Deserialize(pageElement.CreateReader());

            var pageInfo = new PageInfo(page);


            var debug = pageInfo.IsDebug ? "stroke='red' stroke-width='0.5'" : string.Empty;

            var content = string.Empty;

            foreach (var elementXml in pageElement.Element("Content").Elements())
            {
                var elementTypeStr = "SvgPrint.Elements." + elementXml.Name.LocalName;
                var elementType = Type.GetType(elementTypeStr);

                var elementSer = new XmlSerializer(elementType);
                var elementObj = elementSer.Deserialize(elementXml.CreateReader());

                var renderingResult = ProcessElement(elementXml, elementObj, data, pageInfo);
                content += renderingResult.Content;

                if (!(elementObj as Field)?.PrintAfter == true)
                {
                    pageInfo.CurrentY += pageInfo.LineSpacing;
                }

                pageInfo = renderingResult.PageInfo;
            }

            return $@"<svg xmlns='http://www.w3.org/2000/svg' 
                    width='{pageInfo.Width}mm' 
                    height='{pageInfo.Height}mm' 
                    xmlns:xlink='http://www.w3.org/1999/xlink' 
                    viewBox='0 0 {pageInfo.Width} {pageInfo.Height}' >
                    <style>{this.style}</style>
                    <rect width='100%' height='100%' fill='white' />
                    {content}
                    </svg>";
        }

        private static RenderingResult ProcessElement(XElement elementXml, object elementObj, object data, PageInfo currentPosition)
        {
            if (elementObj is IPageDataContent pdc)
            {
                return pdc.Render(data, currentPosition);
            }

            if (elementObj is IPageContent pc)
            {
                return pc.Render(currentPosition);
            }

            return new RenderingResult { Content = string.Empty };
        }
    }
}
