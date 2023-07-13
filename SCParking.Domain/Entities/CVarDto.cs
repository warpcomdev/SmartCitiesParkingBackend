using SCParking.Domain.Entities;

namespace SCParking.Domain
{
    public class CVarDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string scope { get; set; }
        public int index { get; set; }
        public string location { get; set; }
        public string laia_token { get; set; }
        public SourceModel source { get; set; }

    }
}
