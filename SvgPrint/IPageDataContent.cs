using SvgPrint.Model;

namespace SvgPrint
{
    public interface IPageDataContent
    {
        string DataField { get; set; }

        RenderingResult Render(object data, PageInfo currentPosition);
    }
}