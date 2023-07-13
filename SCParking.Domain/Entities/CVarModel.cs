using System;

namespace SCParking.Domain.Entities
{
    public class CVarModel
    {
        public Guid id { get; set; }
        public Guid site_id { get; set; }
        public int matomo_site_id { get; set; }
        public string name { get; set; }
        public int index { get; set; }
        public string scope { get; set; }
        public string laia_token { get; set; }
        public SourceModel source { get; set; }
    }
}
