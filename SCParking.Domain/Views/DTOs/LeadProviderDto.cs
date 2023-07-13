using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class LeadProviderDto
    {        
        public Guid? Id { get; set; }
        public string name { get; set; }
        public int? status { get; set; }
        public Guid? campaignId { get; set; }

        [JsonIgnore]
        public Guid? createdBy { get; set; }

        [JsonIgnore]
        public Guid? currentUserId { get; set; }

        [JsonIgnore]
        public Guid? currentAccountId { get; set; }

        [JsonIgnore]
        public Guid? currentRoleId { get; set; }
    }

    public class leadProviderResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public int? status { get; set; }
        //public List<string> campaignId { get; set; }
        //public Guid campaignId { get; set; }
    }
}
