using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class PointParserTest
    {
        private readonly PointParser _pointParser;
        private const string PointWithLetters = "./TestFiles/PointWithLetters.txt";

        public PointParserTest()
        {
            _pointParser = new PointParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_point_with_sub_units()
        {
            string text = File.ReadAllText(PointWithLetters);

            var result = await _pointParser.ParseUnit(text);

            Assert.Equal(String.Empty, result.Title);
            Assert.Equal(UnitType.Point, result.Type);
            Assert.Equal("27b", result.Number);
            Assert.Equal("wartości rynkowej - rozumie się przez to całkowitą kwotę,...", result.Content);

            Assert.Equal(2, result.SubUnits.Count);
        }
    }
}
