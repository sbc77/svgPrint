using SvgPrint.Model;

namespace SvgPrint
{
    public interface IPageContent
    {
        RenderingResult Render(PageInfo currentPosition);
    }
}