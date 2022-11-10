using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public ActionResult<IEnumerable<WordCloudItem>> Fetch()
        {
            List<WordCloudItem> words = new List<WordCloudItem>();

            for (int i = 0; i < 100; i++)
            {
                var doc = _web.Load(_url);
                foreach (HtmlNode wbr in doc.DocumentNode.SelectNodes("//p").Where(node => node.Attributes["id"].Value == "classname"))
                {
                    var inner = wbr.InnerHtml;
                    words.AddRange(inner.Split("<wbr>").Select(word => new WordCloudItem() { Name = word }));
                }
            }
            return new ActionResult<IEnumerable<WordCloudItem>>(words);
        }
    }
}
