using HtmlAgilityPack;

namespace WordCloudApi.Services
{
    public class HtmlHandler : IHtmlHandler
    {
        private readonly ILogger<HtmlHandler> _logger;
        private readonly HtmlWeb _web;

        public HtmlHandler(ILogger<HtmlHandler> logger)
        {
            _logger = logger;
            _web = new HtmlWeb();
        }
        public async Task<HtmlDocument> GetHtmlFromUrl(string url)
        {
            try
            {
                return await _web.LoadFromWebAsync(url);

            }
            catch (Exception e)
            {
                _logger.LogError($"Get Html From Url {url} Failed with: {e}");
                throw;
            }
        }
    }
}
