using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IHtmlFetcher
    {
        public Task<string> Fetch();
    }
}
