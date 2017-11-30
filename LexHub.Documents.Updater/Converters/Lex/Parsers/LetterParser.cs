using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class LetterParser : BaseUnitStringParser
    {
        private readonly Regex _letter;

        public LetterParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Indent
            };

            _letter = new Regex(HeadingRegexPatterns.Letter, HeadingRegexPatterns.Options);
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source, string firstLine)
        {
            var header = await GetMetadataFromTheSingleLine(firstLine, ')');

            var matches = _letter.Match(header);

            if (matches.Success && matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Type = UnitType.Letter,
                    Title = String.Empty,
                    Number = matches.Groups[1].Value,
                    Content = header
                };
            }

            return null;
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
