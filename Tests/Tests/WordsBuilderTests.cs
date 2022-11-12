using FakeItEasy;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace Tests
{
    public class WordsBuilderTests
    {
        private IWordsBuilder _wordsBuilder;
        public WordsBuilderTests()
        {
            var logger = A.Fake<ILogger<WordsBuilder>>();
            _wordsBuilder = new WordsBuilder(logger);
        }
        [Fact]
        public void GetWordsFromHtml_MatchFound_ReturnWordList()
        {
            //arrange
            string htmlFile = "testHtml.html";
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlFile);
            IEnumerable<string> expectedResult = new List<string>() { "Multiple", "Byte", "Adapter" };
            //act
            var result = _wordsBuilder.GetWordsFromHtml(doc, new Filter("//p", "id", "classname", "<wbr>"));
            
            //assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetWordsFromHtml_MatchNotFound_ReturnEmptyWordList()
        {
            //arrange
            string htmlFile = "testHtml.html";
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlFile);
            IEnumerable<string> expectedResult = new List<string>() { "Multiple", "Byte", "Adapter" };
            //act
            var result = _wordsBuilder.GetWordsFromHtml(doc, new Filter("//doesntexist", "id", "classname", "<wbr>"));

            //assert
            Assert.Empty(result);
        }
    }
}