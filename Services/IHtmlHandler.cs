namespace WordCloudApi.Services
{
    public interface IHtmlHandler
    {
        public Task<string> Fetch();
    }
}
