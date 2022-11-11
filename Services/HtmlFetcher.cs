using HtmlAgilityPack;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class HtmlFetcher : IHtmlFetcher
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly HtmlWeb _web;

        public HtmlFetcher(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["HtmlProviderUrl"];
            _web = new HtmlWeb();
        }

        public async Task<IEnumerable<WordCloudItem>> Fetch()
        {
            List<WordCloudItem> words = new List<WordCloudItem>();

            List<Task<IEnumerable<WordCloudItem>>> tasks = new List<Task<IEnumerable<WordCloudItem>>>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(LoadDataAsync());
            }

            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                words.AddRange(task.Result);
            }

            return words;
        }

        private async Task<IEnumerable<WordCloudItem>> LoadDataAsync()
        {
            List<WordCloudItem> words = new List<WordCloudItem>();
            var doc = await _web.LoadFromWebAsync(_url);
            foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes("//p").Where(node => node.Attributes["id"].Value == "classname"))
            {
                var inner = wbr.InnerHtml;
                words.AddRange(inner.Split("<wbr>").Select(word => new WordCloudItem() { Value = word }));
            }

            return words;
        }
    }
}
