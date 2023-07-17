using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;


namespace SCParking.Domain.Views.DTOs
{
    public class BiciEntityDto
    {
        public BiciEntityDto()
        {
            

        }
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("TimeInstant")]
        public TimeInstantBici TimeInstant { get; set; }

        [JsonProperty("availableSpot")]
        public Spot availableSpot { get; set; }

        [JsonProperty("description")]
        public TimeInstantBici description { get; set; }

        [JsonProperty("location")]
        public LocationBici location { get; set; }

        [JsonProperty("municipality")]
        public TimeInstantBici municipality { get; set; }

        [JsonProperty("occupiedSpot")]
        public Spot occupiedSpot { get; set; }

        [JsonProperty("shortName")]
        public TimeInstantBici shortName { get; set; }

        [JsonProperty("status")]
        public TimeInstantBici status { get; set; }

        [JsonProperty("totalSpot")]
        public Spot totalSpot { get; set; }
    }
    public partial class LocationBici
    {
        [JsonProperty("type")]
        public string type { get; set; }

        public ValueLocationBici value { get; set; }
    }
    public partial class ValueLocationBici
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> coordinates { get; set; }
    }
    public partial class TimeInstantBici
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }
        
    }
    public partial class Spot
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public long value { get; set; }
    }

    public class BiciEntityRequestDto
    {
        
        public string actionType { get; set; }

        public List<BiciEntityDto> entities { get; set; }
    }

}
