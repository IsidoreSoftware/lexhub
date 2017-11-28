using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    abstract class BaseUnitStringParser : IUnitParser
    {
        private readonly IParserFactory _parserFactory;

        protected BaseUnitStringParser(IParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }

        public async Task<ActUnit> ParseUnit<T>(T source)
        {
            var toParse = source as string;
            if (toParse == null)
            {
                throw new ArgumentException("This parser supports only string as a parameter");
            }
            if (string.IsNullOrWhiteSpace(toParse))
            {
                return null;
            }

            var reader = new StringReader(toParse);
            var unit = await ParseMetadata(reader);
            var contentStringBuilder = new StringBuilder();
            UnitType? nextUnitType;

            if (unit.SubUnits == null)
            {
                unit.SubUnits = new List<ActUnit>();
            }

            do
            {
                var line = await reader.ReadLineAsync();
                if (line==null)
                {
                    break;
                }

                nextUnitType = line.GetTypeOfHeader();
                if (nextUnitType.HasValue && IsSubUnit(nextUnitType.Value))
                {
                    var parser = _parserFactory.GetParser(nextUnitType.Value);

                    unit.SubUnits.Add(await parser.ParseUnit(line));
                }
                else
                {
                    contentStringBuilder.AppendLine(line);
                }
            } while (!IsNextUnit(nextUnitType));

            unit.Content = contentStringBuilder.ToString().Trim();

            return unit;
        }

        protected static async Task<string> GetMetadataFromTheSingleLine(StringReader source, char endOfMetadataChar)
        {
            var metadataStringBuilder = new StringBuilder();
            var buffer = new char[1];

            do
            {
                await source.ReadAsync(buffer, 0, 1);
                metadataStringBuilder.Append(buffer[0]);
            } while (buffer[0] != endOfMetadataChar && buffer[0] != '\0');

            var header = metadataStringBuilder.ToString();
            return header;
        }

        protected abstract Task<ActUnit> ParseMetadata(StringReader source);

        protected abstract HashSet<UnitType> PossibleSubUnits { get; }

        private bool IsSubUnit(UnitType type)
        {
            return PossibleSubUnits.Contains(type);
        }

        private bool IsNextUnit(UnitType? type)
        {
            return type.HasValue && !IsSubUnit(type.Value);
        }
    }
}
