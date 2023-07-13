using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class MessagesDto
    {
        [JsonIgnore]
        public Guid? id { get; set; }
        public string success { get; set; }
        public string error { get; set; }
    }
}
