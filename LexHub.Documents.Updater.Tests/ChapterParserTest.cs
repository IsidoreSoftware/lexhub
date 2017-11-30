using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class ChapterParserTest
    {
        private readonly ChapterParser _sectionParser;
        private const string SectionWithArticlesFile = "./TestFiles/ChapterWithSections.txt";

        public ChapterParserTest()
        {
            _sectionParser = new ChapterParser(new ParserFactory());
        }

        [Fact]
        public async Task parse_point_with_sub_units()
        {
            var source = new StringReader(File.ReadAllText(SectionWithArticlesFile));
            var line = await source.ReadLineAsync();

            var result = await _sectionParser.ParseUnit(source,line, new ActUnit());

            Assert.Equal("PIERWSZY", result.Number);
            Assert.Equal(UnitType.Chapter, result.Type);
            Assert.Equal("Przepisy ogólne", result.Title);

            Assert.Equal(1, result.SubUnits.Count);
        }
    }
}
