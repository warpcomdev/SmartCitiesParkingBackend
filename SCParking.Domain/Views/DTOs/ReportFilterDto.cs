namespace SCParking.Domain.Views.DTOs
{
    public class ReportFilterDto: FilterDto
    {
        public string campaignId { get; set; } = "";

        public string from { get; set; } = "";

        public string to { get; set; } = "";

        public string productId { get; set; } = "";

        public string callCenterId { get; set; } = "";

        public string channel { get; set; } = "";

        public string utmCampaign { get; set; } = "";

        public string utmSource { get; set; } = "";

        public string utmMedium { get; set; } = "";

        public string landing { get; set; } = "";

        public string ctaType { get; set; } = "";

        public string agentId { get; set; } = "";

        public string typeItem { get; set; } = "";
        public string phone { get; set; } = "";
        public string customerId { get; set; } = "";
    }
}


