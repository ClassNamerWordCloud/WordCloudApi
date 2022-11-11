using Microsoft.AspNetCore.Mvc;
using WordCloudApi.Services;

namespace WordCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordCloudItemsController : ControllerBase
    {
        private readonly IHtmlHandler _htmlFetcher;

        public WordCloudItemsController(IHtmlHandler htmlFetcher)
        {
            _htmlFetcher = htmlFetcher;
        }

        // GET: api/WordCloudItems
        [HttpGet]
        public async Task<string> GetWordCloudItem()
        {
            return await _htmlFetcher.Fetch();
        }
    }
}
