using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IWordCloudBuilder
    {
        Task<IDictionary<string, int>> GetWordCloud(int numberOfDocs, string url, Filter filter);
    }
}
