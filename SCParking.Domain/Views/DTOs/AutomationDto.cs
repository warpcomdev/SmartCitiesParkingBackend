using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class AutomationDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string segmentationId { get; set; }

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

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
        public WorkflowDto workflow { get; set; }
        public string status { get; set; }
     
    }

    public class AutomationRequestPostDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string segmentationId { get; set; }

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

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
        public string status { get; set; }
        public WorkflowDto workflow { get; set; }
    }


    public class AutomationRequestPutDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string segmentationId { get; set; }

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

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
        public string status { get; set; }
        public WorkflowDto workflow { get; set; }
    }

    public partial class AutomationResponseDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string segmentationId { get; set; }

        public Guid campaignId { get; set; }
        public string status { get; set; }
        public WorkflowDto workflow { get; set; }
    }

}
  
