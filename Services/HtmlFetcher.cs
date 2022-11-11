using HtmlAgilityPack;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class HtmlFetcher : IHtmlFetcher
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly int _numberOfFetches;

        public HtmlFetcher(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["HtmlProviderUrl"];
            _numberOfFetches = int.Parse(_configuration["NumberOfFetches"]);
            _web = new HtmlWeb();
        }

        public async Task<string> Fetch()
        {
            IDictionary<string, int> words = new Dictionary<string, int>();

            List<Task<IEnumerable<string>>> tasks = new List<Task<IEnumerable<string>>>();
            for (int i = 0; i < _numberOfFetches; i++)
            {
                tasks.Add(LoadDataAsync());
            }

            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                foreach (var item in task.Result)
                {
                    if (!words.TryAdd(item, 1))
                    {
                        words[item]++;
                    } 
                }
            }
            var transformed = from key in words.Keys
                select new { value = key, count = words[key] };
            return JsonConvert.SerializeObject(transformed);
        }

        private async Task<IEnumerable<string>> LoadDataAsync()
        {
            List<string> words = new List<string>();
            var doc = await _web.LoadFromWebAsync(_url);
            foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes("//p").Where(node => node.Attributes["id"].Value == "classname"))
            {
                var inner = wbr.InnerHtml;
                words.AddRange(inner.Split("<wbr>"));
            }

            return words;
        }
    }
}
