using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SCParking.Domain.SmartCitiesModels
{
    public class ParkingSpot
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("TimeInstant")]
        public TimeInstant TimeInstant { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant Address { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant Category { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant Description { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("name")]
        public TimeInstant Name { get; set; }

        [JsonProperty("occupancyModified", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant OccupancyModified { get; set; }

        [JsonProperty("occupied", NullValueHandling = NullValueHandling.Ignore)]
        public Occupied Occupied { get; set; }

        [JsonProperty("refDevice", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant RefDevice { get; set; }

        [JsonProperty("refOnStreetParking", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant RefOnStreetParking { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInstant Status { get; set; }

        [JsonProperty("polygon", NullValueHandling = NullValueHandling.Ignore)]
        public Polygon Polygon { get; set; }

        [JsonProperty("totalSpotNumber", NullValueHandling = NullValueHandling.Ignore)]
        public Occupied TotalSpotNumber { get; set; }
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
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public LocationValue Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class LocationValue
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

    public partial class Polygon
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public PolygonValue Value { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class PolygonValue
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<List<List<double>>> Coordinates { get; set; }
    }

    public enum TimeInstantType { DateTime, Text };

    public enum LocationType { GeoJson };

    public enum ValueType { Point };

    public enum OccupiedType { Number };

    public enum ParkingSpotType { OnStreetParking, ParkingSpot };

    public  class ParkingSpots
    {
        public static List<ParkingSpot> FromJson(string json) => JsonConvert.DeserializeObject<List<ParkingSpot>>(json, SmartCitiesModels.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<ParkingSpot> self) => JsonConvert.SerializeObject(self, SmartCitiesModels.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TimeInstantTypeConverter.Singleton,
                LocationTypeConverter.Singleton,
                ValueTypeConverter.Singleton,
                OccupiedTypeConverter.Singleton,
                ParkingSpotTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TimeInstantTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TimeInstantType) || t == typeof(TimeInstantType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "DateTime":
                    return TimeInstantType.DateTime;
                case "Text":
                    return TimeInstantType.Text;
            }
            throw new Exception("Cannot unmarshal type TimeInstantType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TimeInstantType)untypedValue;
            switch (value)
            {
                case TimeInstantType.DateTime:
                    serializer.Serialize(writer, "DateTime");
                    return;
                case TimeInstantType.Text:
                    serializer.Serialize(writer, "Text");
                    return;
            }
            throw new Exception("Cannot marshal type TimeInstantType");
        }

        public static readonly TimeInstantTypeConverter Singleton = new TimeInstantTypeConverter();
    }

    internal class LocationTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LocationType) || t == typeof(LocationType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "geo:json")
            {
                return LocationType.GeoJson;
            }
            throw new Exception("Cannot unmarshal type LocationType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (LocationType)untypedValue;
            if (value == LocationType.GeoJson)
            {
                serializer.Serialize(writer, "geo:json");
                return;
            }
            throw new Exception("Cannot marshal type LocationType");
        }

        public static readonly LocationTypeConverter Singleton = new LocationTypeConverter();
    }

    internal class ValueTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ValueType) || t == typeof(ValueType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Point")
            {
                return ValueType.Point;
            }
            throw new Exception("Cannot unmarshal type ValueType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ValueType)untypedValue;
            if (value == ValueType.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }
            throw new Exception("Cannot marshal type ValueType");
        }

        public static readonly ValueTypeConverter Singleton = new ValueTypeConverter();
    }

    internal class OccupiedTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OccupiedType) || t == typeof(OccupiedType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Number")
            {
                return OccupiedType.Number;
            }
            throw new Exception("Cannot unmarshal type OccupiedType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OccupiedType)untypedValue;
            if (value == OccupiedType.Number)
            {
                serializer.Serialize(writer, "Number");
                return;
            }
            throw new Exception("Cannot marshal type OccupiedType");
        }

        public static readonly OccupiedTypeConverter Singleton = new OccupiedTypeConverter();
    }

    internal class ParkingSpotTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ParkingSpotType) || t == typeof(ParkingSpotType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "OnStreetParking":
                    return ParkingSpotType.OnStreetParking;
                case "ParkingSpot":
                    return ParkingSpotType.ParkingSpot;
            }
            throw new Exception("Cannot unmarshal type ParkingSpotType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ParkingSpotType)untypedValue;
            switch (value)
            {
                case ParkingSpotType.OnStreetParking:
                    serializer.Serialize(writer, "OnStreetParking");
                    return;
                case ParkingSpotType.ParkingSpot:
                    serializer.Serialize(writer, "ParkingSpot");
                    return;
            }
            throw new Exception("Cannot marshal type ParkingSpotType");
        }

        public static readonly ParkingSpotTypeConverter Singleton = new ParkingSpotTypeConverter();
    }

}
