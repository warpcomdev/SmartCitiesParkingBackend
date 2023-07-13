using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SCParking.Domain.Views.DTOs
{
    public class CustomerDto
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? EditedBy { get; set; }

        //[JsonIgnore]
        //public string cti { get; set; }

        //[JsonIgnore]
        public string city { get; set; }

        //[Required]
        //public string Name { get; set; }
        public string name { get; set; }
        

        //public string legalName { get; set; }
        public string address { get; set; }

        public string country { get; set; }

        public string province { get; set; }

        public string postalCode { get; set; }

        //[Required]
        public string email { get; set; }

        public string phone { get; set; }      

        [System.Text.Json.Serialization.JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }



        [System.Text.Json.Serialization.JsonIgnore]
        public Guid createdBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string login { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        public Guid currentUserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid currentAccountId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid currentRoleId { get; set; }

        public string type { get; set; }

        public string activityTypeId { get; set; }

        public string legalIdentifier { get; set; }

        public string socialReason { get; set; }

        public IList<SiteDto> sites { get; set; }

        public bool? googleAdwords { get; set; }
        public bool? googleCampaignManager { get; set; }
        public bool? RTB { get; set; }
        public bool? DMP { get; set; }
        public bool? CRM { get; set; }
        public bool? ERP { get; set; }

        public bool? facebookLeadAds { get; set; }
    }


    public class CustomerRequestPostDto
    {
       
        public string name { get; set; }
        public string address { get; set; }

        public string country { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }

        public string email { get; set; }

        public string phone { get; set; }
       
        [System.Text.Json.Serialization.JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }

        public string type { get; set; }

        public string activityTypeId { get; set; }

        public string legalIdentifier { get; set; }

        public CustomerIntegrationsDto integrations { get; set; }

        public string socialReason { get; set; }


        public IList<SiteDto> sites { get; set; }

        //public dynamic sites { get; set; }


    }


    public class CustomerRequestPutDto
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string country { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string postalCode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string city { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string province { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string phone { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string type { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string activityTypeId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string legalIdentifier { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string socialReason { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public CustomerIntegrationsDto integrations { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public IList<SiteDto> sites { get; set; }
    }



    public class CustomerResponseDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string country { get; set; }

        public string email { get; set; }

        public string phone { get; set; }   

        public int status { get; set; }

        public string type { get; set; }
        public string legalIdentifier { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string socialReason { get; set; }

        public string activityTypeId { get; set; }

        public IList<SiteDto> sites { get; set; }

        public string accountId { get; set; }

        public string postalCode { get; set; }

        public CustomerIntegrationsDto integrations { get; set; }

        public string logoUrl { get; set; }
        public int productsCount { get; set; }
        public int conversionsCount { get; set; }

        public int campaignsCount { get; set; }

        public int callCentersCount { get; set; }
        public int totalCount { get; set; }

    }

    public class CustomerIntegrationsDto
    {
       
        public bool? googleAdwords { get; set; }
        public bool? googleCampaignManager { get; set; }
        [JsonProperty("RTB")]
        public bool? RTB { get; set; }
        [JsonProperty("DMP")]
        public bool? DMP { get; set; }
        [JsonProperty("CRM")]
        public bool? CRM { get; set; }
        [JsonProperty("ERP")]
        public bool? ERP { get; set; }

        [JsonProperty("facebookLeadAds")]
        public bool? FacebookLeadAds { get; set; }
    }

    public class CustomerCampaingSiteDto
    {
        [JsonProperty("campaignSiteId")]
        public string campaignSiteId { get; set; }

        [JsonProperty("campaignName")]
        public string campaignName { get; set; }

        [JsonProperty("siteName")]
        public string siteName { get; set; }

        [JsonProperty("laiaToken")]
        public string laiaToken { get; set; }


    }
}
