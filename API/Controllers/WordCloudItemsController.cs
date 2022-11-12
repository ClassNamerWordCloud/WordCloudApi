﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<ObjectResult> GetWordCloudItem()
        {
            string url = "https://www.classnamer.org/";
            var result =  await _wordCloudBuilder.GetWordCloud(100, url, new Filter("//p", "id", "classname", "<wbr>"));
            if (!result.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Fetching word-cloud from {url} Failed");
            }
            var transformed = from key in result.Keys
                select new { value = key, count = result[key] };
            return Ok(JsonConvert.SerializeObject(transformed));
        }
    }
}
