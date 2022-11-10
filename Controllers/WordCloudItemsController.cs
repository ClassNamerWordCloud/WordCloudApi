using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace WordCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordCloudItemsController : ControllerBase
    {
        private readonly WordCloudContext _context;
        private readonly IHtmlFetcher _htmlFetcher;

        public WordCloudItemsController(WordCloudContext context, IHtmlFetcher htmlFetcher)
        {
            _context = context;
            _htmlFetcher = htmlFetcher;
        }

        // GET: api/WordCloudItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordCloudItem>>> GetWordCloudItem()
        {
            return _htmlFetcher.Fetch();
        }
    }
}
