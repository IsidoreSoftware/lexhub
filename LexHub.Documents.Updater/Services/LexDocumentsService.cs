using System.Collections.Generic;
using System.Threading.Tasks;
using LexHub.Documents.Contract;
using System.Net.Http;
using LexHub.Documents.Updater.Options;
using Microsoft.Extensions.Options;

namespace LexHub.Documents.Updater.Services
{
    class LexDocumentsService : IDocumentsService
    {
        public LexDocumentsService(IOptions<LexDocuments> options)
        {
            httpClient = new HttpClient();
            Options = options.Value;
        }

        private readonly HttpClient httpClient;

        public LexDocuments Options { get; }

        public IEnumerable<Legislation> GetAllLegislations()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Legislation>> GetAllLegislationsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Legislation GetLegislation(string id)
        {
            return GetLegislationAsync(id).Result;
        }

        public async Task<Legislation> GetLegislationAsync(string id)
        {
            var idParts = id.Split('.');
            var result = await httpClient.GetAsync(string.Format(Options.UrlTemplate,$"20{idParts[2]}",idParts[3]));
            return new Legislation
            {
                Version = id,
                Content = await result.Content.ReadAsStringAsync(),
            };
        }
    }
}
