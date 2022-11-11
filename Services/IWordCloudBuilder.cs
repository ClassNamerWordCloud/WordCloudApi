namespace WordCloudApi.Services
{
    public interface IWordCloudBuilder
    {
        Task<string> GetWordCloud(int numberOfDocs, string url);
    }
}
