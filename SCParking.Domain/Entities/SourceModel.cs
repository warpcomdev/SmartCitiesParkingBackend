using System;

namespace SCParking.Domain.Entities
{
    public class SourceModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string key { get; set; }
    }
}
