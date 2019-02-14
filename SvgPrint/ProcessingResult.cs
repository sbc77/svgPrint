using System.Collections.Generic;
using System.IO;

namespace SvgPrint
{
    public class ProcessingResult
    {
        private readonly SvgTemplateProcessor processor;

        private const string FileName = "result";

        private const string Extension = ".svg";

        public ProcessingResult(SvgTemplateProcessor processor, IEnumerable<string> pages)
        {
            this.processor = processor;
            this.Pages = pages;
        }

        public IEnumerable<string> Pages { get; }

        public SvgTemplateProcessor SaveTo(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                foreach (var file in Directory.GetFiles(path, $"{FileName}*{Extension}"))
                {
                    File.Delete(file);
                }
            }

            var pageNo = 1;

            foreach (var page in this.Pages)
            {
                var fullPath = Path.Combine(path, $"{FileName}{pageNo:D3}{Extension}");
                pageNo++;

                File.WriteAllText(fullPath, page);
            }

            return this.processor;
        }
    }
}
