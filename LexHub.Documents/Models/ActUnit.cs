using System.Collections.Generic;

namespace LexHub.Documents.Models
{
    public class ActUnit
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public string Content { get; set; }
        public UnitType Type { get; set; }
        public IList<ActUnit> SubUnits { get; set; }
    }
}
