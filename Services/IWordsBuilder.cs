using HtmlAgilityPack;

namespace WordCloudApi.Services
{
    public interface IWordsBuilder
    {
        IEnumerable<string> GetWordsFromHtml(HtmlDocument doc);
    }
}
