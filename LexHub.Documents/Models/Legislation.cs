using System;

namespace LexHub.Documents
{
    public class Legislation
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
