using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<ActUnit> ParseUnit(StringReader source, string firstLine, ActUnit parrentUnit)
        {
            var currentUnit = await ParseMetadata(source, firstLine);
            var contentBuilder = new StringBuilder(currentUnit.Content);
            while (source.Peek()>=0)
            {
                var nextLine  = await source.ReadLineAsync();
                if (nextLine.Trim() == String.Empty)
                {
                    continue;
                }

                var lineType = nextLine.GetTypeOfHeader();

                if (lineType == null)
                {
                    contentBuilder.AppendLine(nextLine);
                    continue;
                }

                var parser = _parserFactory.GetParser(lineType.Value);
                if (IsSubUnit(lineType.Value))
                {
                    currentUnit.SubUnits.Add(await parser.ParseUnit(source, nextLine, currentUnit));
                }
                else if(IsSibling(lineType.Value))
                {
                    parrentUnit.SubUnits.Add(await parser.ParseUnit(source, nextLine, parrentUnit));
                }
            }
            currentUnit.Content = contentBuilder.ToString();
            currentUnit.SubUnits = currentUnit.SubUnits.Reverse().ToList();
            return currentUnit;
        }

        private bool IsSibling(UnitType lineTypeValue)
        {
            return this._parserFactory.GetParser(lineTypeValue).GetType() == this.GetType();
        }

        protected abstract Task<ActUnit> ParseMetadata(StringReader source, string firstLine);

        protected abstract HashSet<UnitType> PossibleSubUnits { get; }

        private bool IsSubUnit(UnitType type)
        {
            return PossibleSubUnits.Contains(type);
        }

        private bool IsNextUnit(UnitType? type)
        {
            return type.HasValue && !IsSubUnit(type.Value);
        }

        protected static async Task<string> GetMetadataFromTheSingleLine(string line, char endOfMetadataChar)
        {
            var metadataStringBuilder = new StringBuilder();
            var buffer = new char[1];
            var lineReader = new StringReader(line);
            do
            {
                await lineReader.ReadAsync(buffer, 0, 1);
                metadataStringBuilder.Append(buffer[0]);
            } while (buffer[0] != endOfMetadataChar && buffer[0] != '\0');

            var header = metadataStringBuilder.ToString();
            return header;
        }
    }
}
