using System.IO;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters
{
    internal interface IUnitParser
    {
        Task<ActUnit> ParseUnit(StringReader source, string firstLine, ActUnit parrent);
    }
}
