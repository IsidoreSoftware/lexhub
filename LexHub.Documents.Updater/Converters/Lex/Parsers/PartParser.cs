using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Exceptions;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class PartParser : BaseUnitStringParser
    {
        private readonly Regex _partRegex;

        public PartParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Tome,
                UnitType.Chapter,
                UnitType.Section,
                UnitType.Article
            };

            _partRegex = new Regex(HeadingRegexPatterns.Part, HeadingRegexPatterns.Options);
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source)
        {
            var heading = await source.ReadLineAsync();
            var matches = _partRegex.Match(heading);
            if (matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Title =  matches.Groups[1].Value,
                    Type = UnitType.Part
                };
            }

            throw new ParserNotFitActualContentException(heading, this);
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
