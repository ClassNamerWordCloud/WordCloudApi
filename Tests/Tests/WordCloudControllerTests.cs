using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using WordCloudApi.Controllers;
using WordCloudApi.Models;
using WordCloudApi.Services;

namespace Tests
{
    public class WordCloudControllerTests
    {
        private readonly WordCloudController _wordCloudController;
        private readonly IWordCloudBuilder _wordCloudBuilder;

        public WordCloudControllerTests()
        {
            _wordCloudBuilder = A.Fake<IWordCloudBuilder>();
            _wordCloudController = new WordCloudController(_wordCloudBuilder);
        }

        [Fact]
        public void GetWordCloud_Exception_Return500()
        {
            //arrange
            A.CallTo(() => _wordCloudBuilder.GetWordCloud(A<int>._, A<string>._, A<Filter>._))
                .Returns(new Dictionary<string, int>());
            //act
            var result = _wordCloudController.GetWordCloud();

            //assert
            Assert.Equal(500, result.Result.StatusCode);
        }

        [Fact]
        public void GetWordCloud_Success_Return200()
        {
            //arrange
            A.CallTo(() => _wordCloudBuilder.GetWordCloud(A<int>._, A<string>._, A<Filter>._))
                .Returns(new Dictionary<string, int>()
                    { { "Multiple", 5 }, { "Byte", 2 }, { "Adapter", 3 } });
            //act
            var result = _wordCloudController.GetWordCloud();

            //assert
            Assert.Equal(200, result.Result.StatusCode);
        }
    }
}
