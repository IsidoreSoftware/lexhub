using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class ParagraphParserTest
    {
        private readonly ParagraphParser _articleParser;
        private const string ParagraphFile = "./TestFiles/Paragraph.txt";
        private const string ParagraphWithPointsFile = "./TestFiles/ParagraphWithPoints.txt";

        public ParagraphParserTest()
        {
            _articleParser = new ParagraphParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_paragrath()
        {
            string text = File.ReadAllText(ParagraphFile);

            var result = await _articleParser.ParseUnit(text);

            Assert.Equal(String.Empty, result.Title);
            Assert.Equal(UnitType.Paragraph, result.Type);
            Assert.Equal("1a", result.Number);
            Assert.Equal(
                "Tej samej karze podlega...",
                result.Content);
        }

        [Fact]
        public async Task parse_paragrath_with_sub_units()
        {
            string text = File.ReadAllText(ParagraphWithPointsFile);

            var result = await _articleParser.ParseUnit(text);

            Assert.Equal(String.Empty, result.Title);
            Assert.Equal(UnitType.Paragraph, result.Type);
            Assert.Equal("4", result.Number);
            Assert.Equal("Kto:", result.Content);
        }
    }
}
