using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class LetterParserTest
    {
        private readonly LetterParser _letterParser;
        private const string LettersWithIndents = "./TestFiles/LetterWithIndents.txt";

        public LetterParserTest()
        {
            _letterParser = new LetterParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_point_with_sub_units()
        {
            var stream= new StringReader(File.ReadAllText(LettersWithIndents));
            var lineLookup = await stream.ReadLineAsync();

            var result = await _letterParser.ParseUnit(stream, lineLookup, new ActUnit());

            Assert.Equal(String.Empty, result.Title);
            Assert.Equal(UnitType.Letter, result.Type);
            Assert.Equal("b", result.Number);
            Assert.Equal("następujące terytoria poszczególnych państw członkowskich traktuje się jako wyłączone z terytorium Unii Europejskiej:", result.Content);

            Assert.Equal(7, result.SubUnits.Count);
        }
    }
}
