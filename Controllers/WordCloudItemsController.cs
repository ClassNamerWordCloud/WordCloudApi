using Microsoft.AspNetCore.Mvc;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace WordCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordCloudItemsController : ControllerBase
    {
        private readonly IWordCloudBuilder _wordCloudBuilder;

        public WordCloudItemsController(IWordCloudBuilder wordCloudBuilder)
        {
            _wordCloudBuilder = wordCloudBuilder;
        }

        // GET: api/WordCloudItems
        [HttpGet]
        public async Task<string> GetWordCloudItem()
        {
            return await _wordCloudBuilder.GetWordCloud(100, "https://www.classnamer.org/", new Filter("//p", "id", "classname", "<wbr>"));
        }
    }
}
