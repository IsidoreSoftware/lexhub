using System.Collections.Generic;
using System.Threading.Tasks;
using LexHub.Documents.Contract;
using System.Net.Http;
using LexHub.Documents.Updater.Options;
using Microsoft.Extensions.Options;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Html;

namespace LexHub.Documents.Updater.Services
{
    class LexDocumentsService : IDocumentsService
    {
        private readonly IActBuilder<string> _actBuilder;
        private readonly HttpClient _httpClient;

        public LexDocumentsService(IOptions<LexDocuments> options, IActBuilder<string> actBuilder)
        {
            _actBuilder = actBuilder;
            _httpClient = new HttpClient();
            Options = options.Value;
        }

        public LexDocuments Options { get; }

        public IEnumerable<Act> GetAllLegislations()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Act>> GetAllLegislationsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Act GetLegislation(string id)
        {
            return GetLegislationAsync(id).Result;
        }

        public async Task<Act> GetLegislationAsync(string id)
        {
            var idParts = id.Split('.');
            var result = await _httpClient.GetAsync(string.Format(Options.UrlTemplate, $"20{idParts[2]}", idParts[3]));
            var doc = new HtmlToText(new []{ "Art.", "§", "[0-9][a-z]?\\)" });
            var plainText = doc.ConvertHtml(await result.Content.ReadAsStringAsync());
            return await _actBuilder.CreateLegislation(plainText);
        }
    }
}
