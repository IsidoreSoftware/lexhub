using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters
{
    internal interface IMetaDataParser
    {
        Task<Act> GetMetaData<T>(T source);
    }
}
