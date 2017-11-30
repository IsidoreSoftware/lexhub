using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class SectionParser : BaseUnitStringParser
    {
        private readonly Regex _sectionRegex;

        public SectionParser(IParserFactory parserFactory) : base(parserFactory)
        {
            PossibleSubUnits = new HashSet<UnitType>
            {
                UnitType.Section,
                UnitType.Article
            };

            _sectionRegex = new Regex(HeadingRegexPatterns.Section, HeadingRegexPatterns.Options);
        }

        protected override async Task<ActUnit> ParseMetadata(StringReader source, string firstLine)
        {
            var heading = firstLine;
            var matches = _sectionRegex.Match(heading);
            var name = await source.ReadLineAsync();
            if (matches.Groups.Count == 2)
            {
                return new ActUnit
                {
                    Title =  name,
                    Number = matches.Groups[1].Value,
                    Type = UnitType.Section
                };
            }

            return null;
        }

        protected override HashSet<UnitType> PossibleSubUnits { get; }
    }
}
