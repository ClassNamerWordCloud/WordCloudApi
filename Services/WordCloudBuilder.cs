using HtmlAgilityPack;
using Newtonsoft.Json;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class WordCloudBuilder : IWordCloudBuilder
    {
        private readonly IHtmlHandler _htmlHandler;
        private readonly IWordsBuilder _wordsBuilder;

        public WordCloudBuilder(IHtmlHandler htmlHandler, IWordsBuilder wordsBuilder)
        {
            _htmlHandler = htmlHandler;
            _wordsBuilder = wordsBuilder;
        }
        public async Task<string> GetWordCloud(int numberOfDocs,string url, Filter filter)
        {
            IDictionary<string, int> words = new Dictionary<string, int>();

            List<Task<HtmlDocument>> tasks = new List<Task<HtmlDocument>>();
            for (int i = 0; i < numberOfDocs; i++)
            {
                tasks.Add(_htmlHandler.GetHtmlFromUrl(url));
            }
            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                var wordsList = _wordsBuilder.GetWordsFromHtml(task.Result, filter);
                foreach (var item in wordsList)
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
    }
}
