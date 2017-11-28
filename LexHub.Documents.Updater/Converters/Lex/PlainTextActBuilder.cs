using System;
using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Contract;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Html;

namespace LexHub.Documents.Updater.Converters.Lex
{
    internal class PlainTextActBuilder : IActBuilder<string>
    {
        private readonly IHtmlToText _htmlToText;
        private readonly IMetaDataParser _metaDataParser;
        private readonly IParserFactory _parserFactory;

        public PlainTextActBuilder(
            IHtmlToText htmlToText,
            IMetaDataParser metaDataParser,
            IParserFactory parserFactory)
        {
            _htmlToText = htmlToText;
            _metaDataParser = metaDataParser;
            _parserFactory = parserFactory;
        }

        public async Task<Act> CreateLegislation(string source)
        {
            var plainText = _htmlToText.ConvertHtml(source);
            var stream = new StringReader(plainText);
            if (String.IsNullOrWhiteSpace(plainText))
            {
                return null;
            }

            var act = await _metaDataParser.GetMetaData(stream);
            act.Content.Add(await _parserFactory.TopLevelParser.ParseUnit(await stream.ReadToEndAsync()));

            return act;
        }
    }
}
