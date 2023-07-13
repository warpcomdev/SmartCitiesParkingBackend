using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class ProductDto
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        [JsonIgnore]
        public Guid? EditedBy { get; set; }


        //[Required]
        public string name { get; set; }
        public string customerId { get; set; }

        public string description { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }

    }


    public class ProductResponseDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public string customerId { get; set; }

        public string customerName { get; set; }

        public DateTime? createdAt { get; set; }

        public string description { get; set; }

        public int campaignsCount { get; set; }

        public int contactedCounts { get; set; }
        public int conversionsCount { get; set; }

        public string customerLogoUrl { get; set; }

        public List<string> campaignIds { get; set; }
        public List<string> callCenterIds { get; set; }

    }
}
