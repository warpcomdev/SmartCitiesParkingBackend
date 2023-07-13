using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class FacebookDto
    {
        public Guid id { get; set; }
        public string customerId { get; set; }
        public FacebookPageDto facebookPage { get; set; }
        public FacebookFormDto facebookForm { get; set; }
        public string pageAccessToken { get; set; }
        public Guid? campaignSiteId { get; set; }
    }

    public class FacebookPostRequestDto
    {
        [JsonIgnore]
        public Guid? id { get; set; }
        public Guid customerId { get; set; }
        public FacebookPageDto facebookPage { get; set; }
        public FacebookFormDto facebookForm { get; set; }
        public string pageAccessToken { get; set; }
        public Guid? campaignSiteId { get; set; }
    }
    public class FacebookPageDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class FacebookFormDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class FacebookSettingDto
    {
        public FacebookPageDto facebookPage { get; set; }
        public FacebookFormDto facebookForm { get; set; }
        public string pageAccessToken { get; set; }
    }

    public class FacebookLaia
    {
        public string laiaToken { get; set; }
        public string pageAccessToken { get; set; }
    }
}
