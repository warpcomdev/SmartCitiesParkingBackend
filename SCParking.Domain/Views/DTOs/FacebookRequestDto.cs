using Microsoft.AspNetCore.Mvc;

namespace SCParking.Domain.Views.DTOs
{
    public class FacebookRequestDto
    {

        [FromQuery(Name = "hub.mode")]
        public string hubMode { get; set; }

        [FromQuery(Name = "hub.challenge")]
        public int hubChallenge { get; set; }

        [FromQuery(Name = "hub.verify_token")]
        public string hubVerifyToken { get; set; }

    }

    public class FacebookField{
    
         public string field { get; set; }
    }

    public class FacebookLeadGenDto : FacebookField
    {
        public LeadGenDto value { get; set; }
    }

    public class LeadGenDto
    {
        public string ad_id { get; set; }
        public string form_id { get; set; }
        public string leadgen_id { get; set; }
        public int created_time { get; set; }
        public string page_id { get; set; }
        public string adgroup_id { get; set; }
    }
}
