using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CampaignDraftDto
    {
        [JsonIgnore]
        public Guid? id { get; set; }
        public string name { get; set; }

        public decimal investment { get; set; }
        public string userId { get; set; }
        public string siteId { get; set; }
        public List<string> productIds { get; set; }
        public List<string> callCenterIds { get; set; }
        public string cti { get; set; }
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentRoleId { get; set; }


    }

    public class CampaignDraftRequestDto
    {
        [JsonIgnore]
        public string id { get; set; }

        public string name { get; set; }
        public string userId { get; set; }
        public string customerId { get; set; }
        public string description { get; set; }

        public decimal investment { get; set; }


        public string startAt { get; set; }
        public string endAt { get; set; }
        public bool finished { get; set; }

        public List<string> siteIds { get; set; }

        public List<string> productIds { get; set; }

        public List<CampaignDraftCallCenter> callCenters { get; set; }
        
        public CampaignDraftDefaultForm defaultForm { get; set; }


        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentRoleId { get; set; }

    }

    public class CampaignDraftCallCenter
    {
        public string id { get; set; }
        public string ctiId { get; set; }
    }

    public class CampaignDraftDefaultForm
    {       
        public string title { get; set; }
        public string color { get; set; }

        public string launcher { get; set; }        

        public string campaignId { get; set; }

        public string submit { get; set; }

        public DynamicNumberDto callTracking { get; set; }

        public List<FormFieldDto> fields { get; set; }
        public MessagesDto messages { get; set; }

        public CampaignDraftFormPolicyResponseDto formPolicy { get; set; }

    }


    public class CampaignDraftFormPolicyResponseDto
    {
        public string Label { get; set; }
        public string Link { get; set; }
    }
       
    public class CampaignDraftResponseDto
    {
       
        public string id { get; set; }

        public string name { get; set; }
        public string userId { get; set; }
        public string customerId { get; set; }
        public string description { get; set; }

        public decimal investment { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public bool finished { get; set; }

        public List<string> siteIds { get; set; }

        public List<string> productIds { get; set; }

        public List<CampaignDraftCallCenter> callCenters { get; set; }

        public CampaignDraftDefaultForm defaultForm { get; set; }
    }
}
