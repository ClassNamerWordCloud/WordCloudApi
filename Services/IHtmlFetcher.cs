using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IHtmlFetcher
    {
        public Task<IEnumerable<WordCloudItem>> Fetch();
    }
}
