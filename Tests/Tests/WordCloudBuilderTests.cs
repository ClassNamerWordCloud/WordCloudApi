using FakeItEasy;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace Tests
{
    public class WordCloudBuilderTests
    {
        private readonly IWordsBuilder _wordsBuilder;
        private readonly IHtmlHandler _htmlHandler;
        private ILogger<WordCloudBuilder> _logger;
        private readonly WordCloudBuilder _wordCloudBuilder;


        public WordCloudBuilderTests()
        {
            _wordsBuilder = A.Fake<IWordsBuilder>();
            _htmlHandler = A.Fake<IHtmlHandler>();
            _logger = A.Fake<ILogger<WordCloudBuilder>>();
            _wordCloudBuilder = new WordCloudBuilder(_htmlHandler, _wordsBuilder, _logger);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(20)]
        public async void GetWordCloud_MatchFound_ReturnDictionaryWordCloud(int numberOfRepetitions)
        {
            //arrange
            string htmlFile = "testHtml.html";
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlFile);
            Filter filter = new Filter("//p", "id", "classname", "<wbr>");
            IEnumerable<string> getWordsFromHtmlResult = new List<string>() { "Multiple", "Byte", "Adapter" };

            A.CallTo(() => _htmlHandler.GetHtmlFromUrl(A<string>._)).Returns(doc);
            A.CallTo(() => _wordsBuilder.GetWordsFromHtml(doc, filter)).Returns(getWordsFromHtmlResult);

            IDictionary<string, int> expectedResult = new Dictionary<string, int>()
                { { "Multiple", numberOfRepetitions }, { "Byte", numberOfRepetitions }, { "Adapter", numberOfRepetitions } };
            //act
            var result = await _wordCloudBuilder.GetWordCloud(numberOfRepetitions, "https://www.classnamer.org/", filter);
            
            //assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async void GetWordCloud_MatchNotFound_ReturnEmptyDictionary()
        {
            string htmlFile = "testHtml.html";
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlFile);
            Filter filter = new Filter("//doesntExist", "id", "classname", "<wbr>");

            A.CallTo(() => _htmlHandler.GetHtmlFromUrl(A<string>._)).Returns(doc);
            A.CallTo(() => _wordsBuilder.GetWordsFromHtml(doc, filter)).Returns(new List<string>());
            //act
            var result = await _wordCloudBuilder.GetWordCloud(1, "https://www.classnamer.org/", filter);

            //assert
            Assert.Empty(result);
        }
    }
}