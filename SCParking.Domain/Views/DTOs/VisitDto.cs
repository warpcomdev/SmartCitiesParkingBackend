using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class VisitDto
    {
        public string device { get; set; }
        public string datetime { get; set; }
        public string web { get; set; }
        public string browser { get; set; }

        public string so { get; set; }

        public string screenResolution { get; set; }

        public int duration { get; set; }

        public string utmCampaign { get; set; }

        public string utmMedium { get; set; }

        public string country { get; set; }

        public string utmSource { get; set; }

        public bool fillForm { get; set; }

        [JsonIgnore]
        public int actions { get; set; }

        [JsonIgnore]
        public int interactions { get; set; }


        public string dimension1 { get; set; }

        public string dimension2 { get; set; }

        public string dimension3 { get; set; }

        public string dimension4 { get; set; }
        public string dimension5 { get; set; }
        public string dimension6 { get; set; }
        public string dimension7 { get; set; }
        public string dimension8 { get; set; }
        public string dimension9 { get; set; }
        public string dimension10 { get; set; }
        public string dimension11 { get; set; }
        public string dimension12 { get; set; }
        public string dimension13 { get; set; }
        public string dimension14 { get; set; }
        public string dimension15 { get; set; }
        public string dimension16 { get; set; }
        public string dimension17 { get; set; }
        public string dimension18 { get; set; }
        public string dimension19 { get; set; }
        public string dimension20 { get; set; }


    }

}
