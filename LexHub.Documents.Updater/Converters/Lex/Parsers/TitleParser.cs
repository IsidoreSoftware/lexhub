using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class TitleParser : BaseUnitStringParser
    {
        public TitleParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Section
            };
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source, string firstLine)
        {
            var nameLine = firstLine;
            var titleLine = await source.ReadLineAsync();

            var result = nameLine.Split(' ',StringSplitOptions.RemoveEmptyEntries);

            return new ActUnit
            {
                Type = UnitType.Title,
                Title = titleLine,
                Number = result[1]
            };
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
