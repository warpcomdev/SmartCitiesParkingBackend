using Newtonsoft.Json;
using SCParking.Domain.SmartCitiesModels;
using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class MuyBiciDto//Options values
    {
        [JsonProperty("entityID")]
        public string EntityId { get; set; }

        [JsonProperty("entityType")]
        public string EntityType { get; set; }

        [JsonProperty("TimeInstant")]
        public DateTimeOffset TimeInstant { get; set; }

        [JsonProperty("availableSpot")]
        public long AvailableSpot { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("occupiedSpot")]
        public long OccupiedSpot { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("totalSpot")]
        public long TotalSpot { get; set; }
    }
    

    public class MuyBiciResponseDto//Response muybici
    {
        public int status { get; set; }
        public string msg { get; set; }
        public List<MuyBici> data { get; set; }
    }

    public class MuyBiciPostDto//Normalizado
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("TimeInstant")]
        public TimeInstant TimeInstant { get; set; }

        [JsonProperty("availableSpot")]
        public AvailableSpot AvailableSpot { get; set; }

        [JsonProperty("description")]
        public TimeInstant Description { get; set; }

        [JsonProperty("entityID")]
        public TimeInstant EntityId { get; set; }

        [JsonProperty("entityType")]
        public TimeInstant EntityType { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("municipality")]
        public TimeInstant Municipality { get; set; }

        [JsonProperty("occupiedSpot")]
        public AvailableSpot OccupiedSpot { get; set; }

        [JsonProperty("shortName")]
        public TimeInstant ShortName { get; set; }

        [JsonProperty("status")]
        public AvailableSpot Status { get; set; }

        [JsonProperty("totalSpot")]
        public AvailableSpot TotalSpot { get; set; }
    }
    public partial class AvailableSpot
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
