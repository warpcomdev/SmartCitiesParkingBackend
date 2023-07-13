using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SCParking.Domain.Views.DTOs
{
    public  class ParkingSpotDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public List<string> Category { get; set; }

        [JsonProperty("refParkingSite")]
        public string RefParkingSite { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public partial class ParkingSpotsDto
    {
        public static ParkingSpotDto FromJson(string json) => JsonConvert.DeserializeObject<ParkingSpotDto>(json, DTOs.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ParkingSpotDto self) => JsonConvert.SerializeObject(self, DTOs.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class ParkingSpotResponseDto : ParkingSpotDto
    {
        public string tokenUsuario { get; set; }
        public string reservationStartAt { get; set; }
        public string reservationEndAt { get; set; }
    }
}
