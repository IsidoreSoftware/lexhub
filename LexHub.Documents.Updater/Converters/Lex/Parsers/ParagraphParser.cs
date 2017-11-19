using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;

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

        protected override async Task<ActUnit> ParseMetadata(StringReader source)
        {
            var header = await GetMetadataFromTheSingleLine(source, '.');

            var matches = _paragraphRegex.Match(header);

            if (matches.Success && matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Type = UnitType.Paragraph,
                    Title = String.Empty,
                    Number = matches.Groups[1].Value
                };
            }

            return null;
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
