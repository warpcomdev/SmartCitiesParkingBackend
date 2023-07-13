using Newtonsoft.Json;


namespace SCParking.Domain.Tracking
{
    public class ResultSite
    {
        [JsonProperty("idsite")]
        public string IdSite { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("main_url")]
        public string MainUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
