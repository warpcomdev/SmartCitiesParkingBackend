using System;

namespace SCParking.Domain.Entities
{
    public class SiteModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int matomoSiteId { get; set; }
        public string laiaToken { get; set; }
    }
}
