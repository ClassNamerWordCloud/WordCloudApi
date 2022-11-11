using HtmlAgilityPack;
using Newtonsoft.Json;

namespace WordCloudApi.Services
{
    public class HtmlHandler : IHtmlHandler
    {
        private readonly HtmlWeb _web;

        public HtmlHandler()
        {
            _web = new HtmlWeb();
        }
        public async Task<HtmlDocument> GetHtmlFromUrl(string url)
        {
            return await _web.LoadFromWebAsync(url);
        }
    }
}
