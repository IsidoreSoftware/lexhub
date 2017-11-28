using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters
{
    interface IParserFactory
    {
        IUnitParser GetParser(UnitType type);

        IUnitParser TopLevelParser { get; }
    }
}
