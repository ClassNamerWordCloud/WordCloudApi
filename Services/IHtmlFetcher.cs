using Microsoft.AspNetCore.Mvc;
using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IHtmlFetcher
    {
        public ActionResult<IEnumerable<WordCloudItem>> Fetch();
    }
}
