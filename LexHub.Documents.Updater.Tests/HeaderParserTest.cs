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
            _headerParser = new HeaderParser();
        }

        [Fact]
        public async Task EmptyTest()
        {
            var result = await _headerParser.GetMetaData(null);
            Assert.Null(result);
        }

        [Fact]
        public async Task parse_id_correctly()
        {
            var result = await _headerParser.GetMetaData(File.OpenText(TestfilesHeadingTxt));
            Assert.Equal("Dz.U.17.1257", result.Id);
            Assert.Equal("Dz.U.17.1257", result.Version);
        }

        [Fact]
        public async Task parse_name_correctly()
        {
            var result = await _headerParser.GetMetaData(File.OpenText(TestfilesHeadingTxt));
            Assert.Equal("Kodeks postępowania administracyjnego", result.Name);
        }

        [Fact]
        public async Task parse_date_correctly()
        {
            var result = await _headerParser.GetMetaData(File.OpenText(TestfilesHeadingTxt));
            Assert.Equal(new DateTime(1960,6,14), result.CreationDate);
        }


    }
}
