using System.Threading.Tasks;
using LexHub.Documents.Contract;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Html;

namespace LexHub.Documents.Updater.Converters.Lex
{
    internal class PlainTextActBuilder : IActBuilder<string>
    {
        private readonly IHtmlToText _htmlToText;

        public PlainTextActBuilder(IHtmlToText htmlToText)
        {
            _htmlToText = htmlToText;
        }

        public Task<Act> CreateLegislation(string source)
        {
            var plainText = _htmlToText.ConvertHtml(source);
            return null;
        }
    }
}
