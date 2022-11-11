using HtmlAgilityPack;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IWordsBuilder
    {
        IEnumerable<string> GetWordsFromHtml(HtmlDocument doc, Filter filter);
    }
}
