using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class SyncCallCenterParameterDto
    {
        public string campaignId { get; set; }
        public string siteId { get; set; }

        public string startDate { get; set; }

        public string endDate { get; set; }

        public List<string> servicesC2C { get; set; } 

        public string serviceCampaignId { get; set; }

        public List<string> tablesName { get; set; }
    }
}
