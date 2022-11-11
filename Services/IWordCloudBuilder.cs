using WordCloudApi.Models;

namespace WordCloudApi.Services
{
    public interface IWordCloudBuilder
    {
        Task<string> GetWordCloud(int numberOfDocs, string url, Filter filter);
    }
}
