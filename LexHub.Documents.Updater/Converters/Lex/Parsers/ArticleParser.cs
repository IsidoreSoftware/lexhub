using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Exceptions;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class ArticleParser : BaseUnitStringParser
    {
        private readonly Regex _headingRegex;

        public ArticleParser(IParserFactory parserFactory):base(parserFactory)
        {
            _headingRegex = new Regex(HeadingRegexPatterns.Article, HeadingRegexPatterns.Options);
            PossibleSubUnits = new HashSet<UnitType>(new []
            {
                UnitType.Indent,
                UnitType.Letter,
                UnitType.Paragraph,
                UnitType.Point
            });
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source, string firstLine)
        {
            var actUnit = new ActUnit();
            var heading = firstLine;
            ParseHeading(heading, actUnit);

            return actUnit;
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }

        private void ParseHeading(string heading, ActUnit actUnit)
        {
            var match = _headingRegex.Match(heading);
            if (!(match.Success && match.Groups.Count == 3))
            {
                throw new HeaderParsingException();
            }
            actUnit.Type = UnitType.Article;
            actUnit.Title = match.Groups[2].Value;
            actUnit.Number = match.Groups[1].Value;
        }
    }
}
