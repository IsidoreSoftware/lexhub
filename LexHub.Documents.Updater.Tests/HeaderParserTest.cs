using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Updater.Converters;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class HeaderParserTest
    {
        private const string TestfilesHeadingTxt = "./TestFiles/Heading.txt";
        private readonly IMetaDataParser _headerParser;

        public HeaderParserTest()
        {
            this._headerParser = new HeaderParser();
        }

        [Fact]
        public async Task when_parameter_is_not_null_we_throw_exception()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _headerParser.GetMetaData(File.OpenRead(TestfilesHeadingTxt)));
        }

        [Fact]
        public async Task EmptyTest()
        {
            var result = await _headerParser.GetMetaData("");
            Assert.Null(result);
        }

        [Fact]
        public async Task parse_id_correctly()
        {
            string text = File.ReadAllText(TestfilesHeadingTxt);

            var result = await _headerParser.GetMetaData(text);
            Assert.Equal("Dz.U.17.1257", result.Id);
            Assert.Equal("Dz.U.17.1257", result.Version);
        }

        [Fact]
        public async Task parse_name_correctly()
        {
            string text = File.ReadAllText(TestfilesHeadingTxt);

            var result = await _headerParser.GetMetaData(text);
            Assert.Equal("Kodeks postępowania administracyjnego", result.Name);
        }

        [Fact]
        public async Task parse_date_correctly()
        {
            string text = File.ReadAllText(TestfilesHeadingTxt);

            var result = await _headerParser.GetMetaData(text);
            Assert.Equal(new DateTime(1960,6,14), result.CreationDate);
        }


    }
}
