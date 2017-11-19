using System;
using System.Threading.Tasks;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Html;
using Moq;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class PlainTextActBuilderTests
    {
        private readonly PlainTextActBuilder _plainTextActBuilder;
        private readonly Mock<IHtmlToText> _parser;

        public PlainTextActBuilderTests()
        {
            _parser = new Mock<IHtmlToText>();
            _plainTextActBuilder = new PlainTextActBuilder(_parser.Object);
        }

        [Fact]
        public async Task EmptyTest()
        {
            _parser.Setup(x => x.ConvertHtml(It.IsAny<string>())).Returns("");

            var result  = await _plainTextActBuilder.CreateLegislation("");
            Assert.Null(result);
        }
    }
}
