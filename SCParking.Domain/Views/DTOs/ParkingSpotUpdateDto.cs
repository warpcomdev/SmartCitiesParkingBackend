using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SCParking.Domain.Views.DTOs
{
    public class ParkingSpotUpdateDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("TimeInstant")]
        public TimeInstant TimeInstant { get; set; }

        [JsonProperty("address")]
        public TimeInstant Address { get; set; }

        [JsonProperty("category")]
        public TimeInstant Category { get; set; }

        [JsonProperty("description")]
        public TimeInstant Description { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("name")]
        public TimeInstant Name { get; set; }

        [JsonProperty("occupancyModified")]
        public TimeInstant OccupancyModified { get; set; }

        [JsonProperty("occupied")]
        public Occupied Occupied { get; set; }

        [JsonProperty("refDevice")]
        public TimeInstant RefDevice { get; set; }

        [JsonProperty("refOnStreetParking")]
        public TimeInstant RefOnStreetParking { get; set; }

        [JsonProperty("status")]
        public TimeInstant Status { get; set; }
    }
    public partial class TimeInstant
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class Metadata
    {
    }

    public partial class Location
    {
        

        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public partial class Occupied
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
