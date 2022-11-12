using HtmlAgilityPack;
using Newtonsoft.Json;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class WordCloudBuilder : IWordCloudBuilder
    {
        private readonly IHtmlHandler _htmlHandler;
        private readonly IWordsBuilder _wordsBuilder;
        private readonly ILogger<WordCloudBuilder> _logger;

        public WordCloudBuilder(IHtmlHandler htmlHandler, IWordsBuilder wordsBuilder, ILogger<WordCloudBuilder> logger)
        {
            _htmlHandler = htmlHandler;
            _wordsBuilder = wordsBuilder;
            _logger = logger;
        }
        public async Task<IDictionary<string, int>> GetWordCloud(int numberOfRepetitions,string url, Filter filter)
        {
            IDictionary<string, int> wordCloud = new Dictionary<string, int>();

            List<Task<HtmlDocument>> tasks = new List<Task<HtmlDocument>>();
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                tasks.Add(_htmlHandler.GetHtmlFromUrl(url));
            }

            try
            {
                await Task.WhenAll(tasks);

            }
            catch (Exception e)
            {
                _logger.LogError($"Get Html From Url {url} Failed with: {e}");
                return wordCloud;
            }

            foreach (var task in tasks)
            {
                var wordsList = _wordsBuilder.GetWordsFromHtml(task.Result, filter);
                foreach (var item in wordsList)
                {
                    if (!wordCloud.TryAdd(item, 1))
                    {
                        wordCloud[item]++;
                    }
                }
            }
            return wordCloud;
        }
    }
}
