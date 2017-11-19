using System;
using System.Collections.Generic;

namespace LexHub.Documents.Models
{
    public class Act
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public List<ActUnit> Content { get; set; }
    }
}
