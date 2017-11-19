using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class PartParserTest
    {
        private readonly PartParser _partParser;
        private const string PointWithLetters = "./TestFiles/PartWithChapters.txt";

        public PartParserTest()
        {
            _partParser = new PartParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_point_with_sub_units()
        {
            string text = File.ReadAllText(PointWithLetters);

            var result = await _partParser.ParseUnit(text);

            Assert.Equal("OGÓLNA", result.Title);
            Assert.Equal(UnitType.Part, result.Type);
            Assert.Null(result.Number);

            Assert.Equal(1, result.SubUnits.Count);
        }
    }
}
