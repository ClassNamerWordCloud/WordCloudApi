using HtmlAgilityPack;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public class WordsBuilder : IWordsBuilder
    {
        public IEnumerable<string> GetWordsFromHtml(HtmlDocument doc, Filter filter)
        {
            List<string> words = new List<string>();
            foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes(filter.Tag).Where(node => node.Attributes[filter.Attribute].Value == filter.Value))
            {
                var inner = wbr.InnerHtml;
                words.AddRange(inner.Split(filter.SplitTag));
            }
            return words;
        }
    }
}
