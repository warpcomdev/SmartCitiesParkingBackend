using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CampaignDto
    {
        [JsonIgnore]
        public Guid? id { get; set; }
        public string name { get; set; }
        public string userId { get; set; }

        public decimal investment { get; set; }

        public string defaultFormId { get; set; }

        public List<string> productIds { get; set; }

        public List<string> siteIds { get; set; }             
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

         public List<CampaignCallCenterDto> callCenters { get; set; }


    }

    public class CampaignRequestPostDto
    {        
        public string name { get; set; }
        public string userId { get; set; }
        public decimal investment { get; set; }

        public string defaultFormId { get; set; }
        public List<string> productIds { get; set; }     
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }

        public List<CampaignCallCenterDto> callCenters { get; set; }

        public List<string> siteIds { get; set; }
    }

    public class CampaignResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }

        public decimal investment { get; set; }
        public int? status { get; set; }
        public List<string> siteIds { get; set; }   

        public List<CampaignSiteDto> sites { get; set; }

        public List<string> productIds { get; set; }
        
        public string description { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string pixelControl { get; set; }
        public int leadsCount { get; set; }
        public int contactedCount { get; set; }
        public int conversionsCount { get; set; }
        public int visitCount { get; set; }

        public string defaultFormId { get; set; }

        public string customerId { get; set; }

        public string customerName { get; set; }

        public string customerLogoUrl { get; set; }

        public string userId { get; set; }

        public string campaignDraftId { get; set; }

        public string campaignDraftUserId { get; set; }

        public List<CampaignCallCenterDto> callCenters { get; set; }

        public List<CampaignAssignedUserDto> assignedUsers { get; set; }
    }

    public class CampaignCallCenterDto
    {
        public string id { get; set; }
        public string ctiId { get; set; }
    }

    public class CampaignSiteDto
    {
        public string id { get; set; }
        public string url { get; set; }
        public string pixelControl { get; set; }

        public string description { get; set; }
    }
    public class CampaignAssignedUserDto
    {
        public string id { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string roleId { get; set; }
    }

    public class CampaignResponseApiDto
    {
        public string id { get; set; }
        public string name { get; set; }
      
        public List<CampaignResponseSiteApiDto> sites { get; set; }      
    }

    public class CampaignResponseSiteApiDto
    {
        public string id { get; set; }
        public string url { get; set; }

        public string description { get; set; }

        public string laiaToken { get; set; }
    }
}
