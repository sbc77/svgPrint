using SvgPrint.Model;

namespace SvgPrint
{
    class Program
    {
        static void Main(string[] args)
        {
            SvgTemplateProcessor
                .UseTemplate("Template/GS1.xml")
                        .UseStyle("Template/style.css")
                        .Process(MyGs1Data.GenData())
                        .SaveTo("result");

            /*
            SvgTemplateProcessor
                .UseTemplate("Template/invoice.xml")
                        .UseStyle("Template/style.css")
                        .Process(MyExampleInvoice.GenData())
                        .SaveTo("result");
            */
        }
    }
}
