using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class PointParser : BaseUnitStringParser
    {
        private readonly Regex _pointRegex;

        public PointParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Letter,
                UnitType.Indent
            };
            _pointRegex = new Regex(HeadingRegexPatterns.Point, HeadingRegexPatterns.Options);
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source)
        {
            var header = await GetMetadataFromTheSingleLine(source, ')');

            var matches = _pointRegex.Match(header);

            if (matches.Success && matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Type = UnitType.Point,
                    Title = String.Empty,
                    Number = matches.Groups[1].Value
                };
            }

            return null;
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
