using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class IndentParser : BaseUnitStringParser
    {
        public IndentParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>();
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source)
        {
            await GetMetadataFromTheSingleLine(source, '-');
            return new ActUnit
            {
                Type = UnitType.Letter,
                Title = String.Empty,
                Number = ""
            };
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
