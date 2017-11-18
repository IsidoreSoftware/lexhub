using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexHub.Documents.Contract
{
    public interface IDocumentsService
    {
        IEnumerable<Legislation> GetAllLegislations();
        Legislation GetLegislation(string id);

        Task<IEnumerable<Legislation>> GetAllLegislationsAsync();
        Task<Legislation> GetLegislationAsync(string id);
    }
}
