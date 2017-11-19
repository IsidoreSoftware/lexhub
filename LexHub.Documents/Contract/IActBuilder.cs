using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Contract
{
    public interface IActBuilder<in T>
    {
        Task<Act> CreateLegislation(T source);
    }
}
