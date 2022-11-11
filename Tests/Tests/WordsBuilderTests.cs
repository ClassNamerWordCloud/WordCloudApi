using HtmlAgilityPack;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace Tests
{
    public class WordsBuilderTests
    {
        private IWordsBuilder _wordsBuilder;
        public WordsBuilderTests()
        {
            _wordsBuilder = new WordsBuilder();
        }
        [Fact]
        public void GetWordsFromHtml_ValidDocAndFilter_ReturnWordList()
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
    }
}