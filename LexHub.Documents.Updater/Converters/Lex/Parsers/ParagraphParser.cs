using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Exceptions;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class ParagraphParser : BaseUnitStringParser
    {
        private readonly Regex _paragraphRegex;

        public ParagraphParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Indent,
                UnitType.Letter,
                UnitType.Point
            };
            _paragraphRegex = new Regex(HeadingRegexPatterns.Paragraph, HeadingRegexPatterns.Options);
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source, string firstLine)
        {
            var header = await GetMetadataFromTheSingleLine(firstLine, '.');

            var matches = _paragraphRegex.Match(header);

            if (matches.Success && matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Type = UnitType.Paragraph,
                    Title = String.Empty,
                    Number = matches.Groups[1].Value,
                    Content = header
                };
            }

            throw new ParserNotFitActualContentException(firstLine, this);
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
