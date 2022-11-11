using HtmlAgilityPack;

namespace WordCloudApi.Services
{
    public interface IHtmlHandler
    {
        public Task<HtmlDocument> GetHtmlFromUrl(string url);
    }
}
