using HtmlAgilityPack;

namespace WordCloudApi.Services
{
    public class WordsBuilder : IWordsBuilder
    {
        public IEnumerable<string> GetWordsFromHtml(HtmlDocument doc)
        {
            List<string> words = new List<string>();
            foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes("//p").Where(node => node.Attributes["id"].Value == "classname"))
            {
                var inner = wbr.InnerHtml;
                words.AddRange(inner.Split("<wbr>"));
            }
            return words;
        }
    }
}
