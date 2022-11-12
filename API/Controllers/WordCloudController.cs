using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace WordCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordCloudController : ControllerBase
    {
        private readonly IWordCloudBuilder _wordCloudBuilder;

        public WordCloudController(IWordCloudBuilder wordCloudBuilder)
        {
            _wordCloudBuilder = wordCloudBuilder;
        }

        // GET: api/GetWordCloud
        [HttpGet]
        public async Task<ObjectResult> GetWordCloud()
        {
            string url = "https://www.classnamer.org/";
            var result =  await _wordCloudBuilder.GetWordCloud(5, url, new Filter("//p", "id", "classname", "<wbr>"));
            if (!result.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Fetching word-cloud from {url} Failed");
            }
            var transformed = from key in result.Keys
                select new { text = key, value = result[key] };
            return Ok(JsonConvert.SerializeObject(transformed));
        }
    }
}
