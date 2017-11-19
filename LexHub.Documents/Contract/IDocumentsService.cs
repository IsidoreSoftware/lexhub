using System.Collections.Generic;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Contract
{
    public interface IDocumentsService
    {
        IEnumerable<Act> GetAllLegislations();
        Act GetLegislation(string id);

        Task<IEnumerable<Act>> GetAllLegislationsAsync();
        Task<Act> GetLegislationAsync(string id);
    }
}
