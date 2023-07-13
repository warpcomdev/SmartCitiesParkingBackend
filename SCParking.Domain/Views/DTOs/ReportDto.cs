using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class ReportDto
    {
       
    }

    public partial class ReportLeadsReportResponseDto
    {
        public int visitsCount { get; set; }
        public int totalCount { get; set; }

        public int utilsCount { get; set; }

        public int deduplicatedCount { get; set; }

        public int noContactedCount { get; set; }
               
        public int contactedCount { get; set; }

        public int convertedCount { get; set; }

        public int registratedCount { get; set; }

        public decimal investment { get; set; }
        public int leadsCount { get; set; }

        public int sentCount { get; set; }
        
        public Guid campaignId { get; set; }
    }

    public partial class ReportLeadsReportByProductResponseDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportLeadsReportByCallCenterResponseDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }
    public partial class ReportDayLeadsReportResponseDto
    {
        public string date { get; set; }

        public int totalCount { get; set; }
        
        public int contactedCount { get; set; }

        public int convertedCount { get; set; }       
    }

    public partial class ReportDayLeadsReportByCallCenterResponseDto
    {
        public string date { get; set; }

        public List<ReportLeadsReportByCallCenterResponseDto> callCenters { get; set; }

    }

    public partial class ReportSitesReportResponseDto
    {
        public string id { get; set; }

        public string url { get; set; }

        public int visitsCount { get; set; }
    }

    public partial class ReportLeadsReportByChannelResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportLeadsReportByCtaResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportLeadsReportByLandingResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportLeadsReportByCampaignResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }



    public partial class ReportLeadsReportByProviderResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportDaySiteReportResponseDto
    {
        public string date { get; set; }
      
        public int visitCount { get; set; }

        public int fillFormCount { get; set; }
    }

    public partial class ReportCounter
    {
        public int productCount { get; set; }
        public int campaignCount { get; set; }
        public int customerCount { get; set; }
        public int callCenterCount { get; set; }
        public int conversionCount { get; set; }

        public int contactedCount { get; set; }
        public int totalCount { get; set; }

    }


    public partial class ReportLeadsDetailReportResponseDto
    {
        public string phone { get; set; }

        public DateTime dateLead { get; set; }

        public string infoExtra { get; set; }

        public string utmSource { get; set; }

        public string utmCampaign { get; set; }

        public string utmMedium { get; set; }

        public string landingUrl { get; set; }

        public string cta { get; set; }

        public string campaignName { get; set; }

        public string callCenterName { get; set; }

        public string siteName { get; set; }

        public string formName { get; set; }

        public string resultCallLead { get; set; }

        public string resultUtil { get; set; }

        public string resultCall { get; set; }

        public int productCount { get; set; }
        public string product { get; set; }
        public string productDetail { get; set; }

        public string productCampaign { get; set; }

        public string utmId { get; set; }

        public string leadId { get; set; }

        public string sourceCustomer { get; set; }

        public string sourceSector { get; set; }

        public string sourceProduct { get; set; }

        public string sourceDescription { get; set; }

        public string sourceTopic { get; set; }
        public string sourceStrategy { get; set; }
        public string sourceCampaignNameLaia { get; set; }
        public string sourceUtmCampaign { get; set; }
        public string sourceUtmSource { get; set; }
        public string sourceUtmMedium { get; set; }
        public string sourceLanding { get; set; }
        public string sourceLandingWithUtm { get; set; }
        public string sourceStaticPhone { get; set; }

        public string sourceDDI { get; set; }

        public string agent { get; set; }

        public string isContacted { get; set; }

        public string isSale { get; set; }

        public string productSale { get; set; }

        public string service { get; set; }


        public string date { get; set; }
        public string hour { get; set; }

    }

    public partial class ReportSaleReportResponseDto
    {       

        public DateTime dateSale { get; set; }

        public string phone { get; set; }

        public int productCount { get; set; }

        public string resultCall { get; set; }

        public string utmSource { get; set; }

        public string utmCampaign { get; set; }

        public string utmMedium { get; set; }

        public string leadId { get; set; }

        public string campaignName { get; set; }

        public string callCenterName { get; set; }

        public string landingUrl { get; set; }

        public string cta { get; set; }

    }

    public partial class ReportExport
    {
        public string file { get; set; }
    }

    public partial class ReportLeadsReportByAgentResponseDto
    {
        public string key { get; set; }

        public string name { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }
    }

    public partial class ReportValueSettingDto
    {
        public string nameField { get; set; }
        public string nameExport { get; set; }
        public int order { get; set; }
    }

    public partial class ReportReportGlobalResponseDto
    {

        public string nameItem { get; set; }

        public int visitsCount { get; set; }

        public int totalCount { get; set; }

        public int convertedCount { get; set; }

        public int registratedCount { get; set; }

        public int leadsCount { get; set; }
    }

    public partial class ReportLeadsCustomerResponseDto
    {

        public string id { get; set; }

        public string campaignId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string hour { get; set; }

        public string sourceUTM { get; set; }

        public string ctaType { get; set; }

        public string date { get; set; }


    }
    public partial class ReportLeadReportResponseDto
    {

        public DateTime dateLead { get; set; }

        public string phone { get; set; }

        public string utmSource { get; set; }

        public string utmCampaign { get; set; }

        public string utmMedium { get; set; }

        public string landingUrl { get; set; }

        public string cta { get; set; }
        public string campaignName { get; set; }

        public string siteName { get; set; }

        public string productCampaign { get; set; }

        public string campaignId { get; set; }

        public string id { get; set; }


    }

    public partial class ReportLeadsReportByAccountResponseDto
    {

        public string id { get; set; }
        public string campaignId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string hour { get; set; }
        public string sourceUTM { get; set; }
        public string ctaType { get; set; }
        public string date { get; set; }
        public string customerName { get; set; }
        public string fullname { get; set; }

    }

}
