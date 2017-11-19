using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class SectionParserTest
    {
        private readonly SectionParser _sectionParser;
        private const string SectionWithArticlesFile = "./TestFiles/SectionWithArticles.txt";

        public SectionParserTest()
        {
            _sectionParser = new SectionParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_point_with_sub_units()
        {
            string text = File.ReadAllText(SectionWithArticlesFile);

            var result = await _sectionParser.ParseUnit(text);

            Assert.Equal("I", result.Number);
            Assert.Equal(UnitType.Section, result.Type);
            Assert.Equal("Zakres obowiązywania", result.Title);

            Assert.Equal(1, result.SubUnits.Count);
        }
    }
}
