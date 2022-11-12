using HtmlAgilityPack;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class WordsBuilder : IWordsBuilder
    {
        private readonly ILogger<WordsBuilder> _logger;

        public WordsBuilder(ILogger<WordsBuilder> logger)
        {
            _logger = logger;
        }
        public IEnumerable<string> GetWordsFromHtml(HtmlDocument doc, Filter filter)
        {
            List<string> words = new List<string>();
            try
            {
                foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes(filter.Tag).Where(node => node.Attributes[filter.Attribute].Value == filter.Value))
                {
                    var inner = wbr.InnerHtml;
                    words.AddRange(inner.Split(filter.SplitTag));
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Getting words from HTML \n {doc.Text} \n Failed with filter: {filter}");
            }
            return words;
        }
    }
}
