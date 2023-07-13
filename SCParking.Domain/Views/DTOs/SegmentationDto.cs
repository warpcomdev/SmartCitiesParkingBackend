using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class SegmentationDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<SegmentationRuleDto> Rules { get; set; }
        [JsonIgnore]
        public string campaignId { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

    }
    public class SegmentationRequestPostDto
    {
       
        public string name { get; set; }
        public List<SegmentationRuleDto> rules { get; set; }
    }
    public class SegmentationResponsePostDto
    {
        public string id { get; set; }
        public string campaignId { get; set; }
        public string name { get; set; }
        public List<SegmentationRuleDto> rules { get; set; }
    }
    public class SegmentationRuleDto
    {
        public string id { get; set; }
        public string tagKey { get; set; }
        public string comparisonOperator { get; set; }
        public string value { get; set; }
        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }
    }
    public class SegmentationRuleResponseDto
    {
        public string id { get; set; }
        public string tagKey { get; set; }
        public string comparisonOperator { get; set; }
        public string value { get; set; }
    }

    public class SegmentationResponseDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string campaignId { get; set; }
        public List<SegmentationRuleResponseDto> rules { get; set; }
    }
}
