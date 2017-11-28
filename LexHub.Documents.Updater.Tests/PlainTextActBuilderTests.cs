using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Html;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Moq;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class PlainTextActBuilderTests
    {
        private const string TestfilesHeadingTxt = "./TestFiles/FullAct.txt";
        private readonly PlainTextActBuilder _plainTextActBuilder;
        private readonly Mock<IHtmlToText> _htmlReader;

        public PlainTextActBuilderTests()
        {
            _htmlReader = new Mock<IHtmlToText>();
            _plainTextActBuilder = new PlainTextActBuilder(_htmlReader.Object, new HeaderParser(), new ParserFactory());
        }

        [Fact]
        public async Task EmptyTest()
        {
            _htmlReader.Setup(x => x.ConvertHtml(It.IsAny<string>())).Returns("");

            var result = await _plainTextActBuilder.CreateLegislation("");
            Assert.Null(result);
        }

        [Fact]
        public async Task manage_to_parse_header_of_document()
        {
            _htmlReader.Setup(x => x.ConvertHtml("test_html")).Returns(File.ReadAllText(TestfilesHeadingTxt));

            var result = await _plainTextActBuilder.CreateLegislation("test_html");

            Assert.NotNull(result.Name);
            Assert.NotNull(result.CreationDate);
        }

        [Fact]
        public async Task manage_to_first_level_units()
        {
            _htmlReader.Setup(x => x.ConvertHtml("test_html")).Returns(File.ReadAllText(TestfilesHeadingTxt));

            var result = await _plainTextActBuilder.CreateLegislation("test_html");

            Assert.NotNull(result.Content);
            Assert.Equal(result.Content.Count, 1);
        }

        [Fact]
        public async Task double_nested_units()
        {
            _htmlReader.Setup(x => x.ConvertHtml("test_html")).Returns(File.ReadAllText(TestfilesHeadingTxt));

            var result = await _plainTextActBuilder.CreateLegislation("test_html");

            Assert.Equal(result.Content[0].SubUnits.Count, 1);
            Assert.Equal(result.Content[0].SubUnits[0].SubUnits.Count, 3);
        }
    }
}
