using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex;
using LexHub.Documents.Updater.Converters.Lex.Parsers;
using Xunit;

namespace LexHub.Documents.Updater.Tests
{
    public class ArticleParserTest
    {
        private readonly ArticleParser _articleParser;
        private const string TestfilesHeadingTxt = "./TestFiles/Article.txt";
        private const string ArticleWithParagrathFile = "./TestFiles/ArticleWithParagraphs.txt";

        public ArticleParserTest()
        {
            _articleParser = new ArticleParser(new ParserFactory());
        }

        [Fact]
        public async Task not_string_source_throws_exception()
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _articleParser.ParseUnit(File.OpenRead(TestfilesHeadingTxt)));
        }

        [Fact]
        public async Task parsed_correctly_simple_example()
        {
            string text = File.ReadAllText(TestfilesHeadingTxt);

            var result = await _articleParser.ParseUnit(text);
            Assert.Equal("Kara aresztu", result.Title);
            Assert.Equal(UnitType.Article, result.Type);
            Assert.Equal("19", result.Number);
            Assert.Equal("Kara aresztu trwa najkrócej 5, najdłużej 30 dni; wymierza się ją w dniach.", result.Content);
        }

        [Fact]
        public async Task parse_article_with_paragraphs()
        {
            string text = File.ReadAllText(ArticleWithParagrathFile);

            var result = await _articleParser.ParseUnit(text);
            Assert.Equal("Przedawnienie karalności i wykonania kary", result.Title);
            Assert.Equal(UnitType.Article, result.Type);
            Assert.Equal("45", result.Number);

            Assert.Equal(4, result.SubUnits.Count);
        }
    }
}
