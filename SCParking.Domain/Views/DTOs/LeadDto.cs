using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class LeadDto
    {
        public string id { get; set; }
        public int cliente { get; set; }
        public int proveedor { get; set; }
        public int categoriaOrigen { get; set; }
        public string servicio { get; set; }
        public string centro { get; set; }
        public string telefono { get; set; }
        public string producto { get; set; }
        public string categoriaLead1 { get; set; }
        public string categoriaLead2 { get; set; }
        public string nombreCompleto { get; set; }
        public string email { get; set; }
        public string familiaCliente { get; set; }
        public string fecha { get; set; }
        public object informacionExtra { get; set; }

        [JsonIgnore]
        public Guid campaignId { get; set; }
        public string idLead { get; set; }

        public string utmCampaign { get; set; }

        public string utmMedium { get; set; }

        public string utmSource { get; set; }

        public string campaignName { get; set; }

        public string idSource { get; set; }

        public string cta { get; set; }
    }

    public class LeadResponseDto
    {
        public string id { get; set; }

        public string campaignId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string avatarUrl { get; set; }

        public string hour { get; set; }

        public string sourceUTM { get; set; }

        public string ctaType { get; set; }

        public string date { get; set; }

        [JsonIgnore]
        public DateTime fullDate { get; set; }

        [JsonIgnore]
        public string fullName { get; set; }

    }


    public class LeadResponseDetailDto
    {
        public string id { get; set; }

        public string campaignId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string avatarUrl { get; set; }

        public string adress { get; set; }

        public string country { get; set; }

        public int visitTime { get; set; }

        public int visitCount { get; set; }

        public int visitPagesCount { get; set; }

        public int avgPageLoadTime { get; set; }

        public Guid? providerId { get; set; }
        public string status { get; set; }

        [JsonIgnore]
        public string fullName { get; set; }

        public List<VisitDto> visits { get; set; }

    }

    public class LeadResponseDetailCallCenterDto
    {
        public string id { get; set; }

        public DateTime date { get; set; }

        public string campaignName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public string country { get; set; }

        public string utmCampaignLast { get; set; }

        public string utmMediumLast { get; set; }

        public string utmSourceLast { get; set; }

        public int visitTime { get; set; }

        public int visitCount { get; set; }

        public int visitPagesCount { get; set; }

        public int avgPageLoadTime { get; set; }

        public Guid? providerId { get; set; }

        [JsonIgnore]
        public string fullName { get; set; }

        public List<VisitDto> visitTracking { get; set; }

        //public List<CustomDimensionDto> customDimention { get; set; }

    }

    #region LeadApiOnline
    public class LeadRequestApiDto
    {

        public LeadRequestApiDto()
        {
            formFields = new ExpandoObject();
        }
        public string formId { get; set; }

        public string visitorId { get; set; }
        public string visitorInfo { get; set; }

        public string laia_token_ce { get; set; }

        public ExpandoObject formFields { get; set; }

        public ExpandoObject serviceFields { get; set; }

        public ExpandoObject trackingFields { get; set; }
    }

    public class LeadResponseApiDto
    {
        public string id { get; set; }

        public string phone { get; set; }

        public string dateLead { get; set; }

        public string campaignId { get; set; }
        public LeadResponseApiDto()
        {
            formFields = new ExpandoObject();
            serviceFields = new ExpandoObject();
        }
        public string formId { get; set; }

        public string visitorId { get; set; }

        public ExpandoObject formFields { get; set; }
        public ExpandoObject serviceFields { get; set; }
    }

    public class LeadVisitor
    {
        public string matomo_id { get; set; }
        public string laia_token { get; set; }
    }

    #endregion LeadApiOnline

    #region LeadApiCallTrackingMetrics
    public class LeadRequestApiCallTrackingDto
    {
        public LeadRequestApiCallTrackingDto()
        {
            formFields = new ExpandoObject();
        }

        public string visitorInfo { get; set; }
        public string laia_token_ce { get; set; }

        public string visitorId { get; set; }
        public ExpandoObject formFields { get; set; }
        public ExpandoObject serviceFields { get; set; }
        public ExpandoObject trackingFields { get; set; }
    }

    #endregion


    #region Infinity
    public class InfinityRequestDto
    {
        public string laia_token { get; set; }
        public string service__call_provider { get; set; }
        public string service__infinity_id { get; set; }
        public string service__call_type { get; set; }
        public string service__source { get; set; }
        public string form__phone { get; set; }
        public string service__tracking_phone { get; set; }
        public string service__destination_phone { get; set; }
        public string service__infinity_source { get; set; }
        public string service__call_date { get; set; }
        public string matomo_id { get; set; }
        public string tracking__utm_term { get; set; }
        public string tracking__utm_campaign { get; set; }
        public string tracking__utm_source { get; set; }
    }

    #endregion


    #region LeadCTI
    public class ListDisponible
    {
        public string disponibles { get; set; }
    }
    public class StatusUserCtiDto
    {
        public string id { get; set; }
        public List<ListDisponible> lista { get; set; }
    }
    public class ListHistory
    {
        public string his_id { get; set; }
        public string his_ts { get; set; }
        public string cat_description { get; set; }
        public string sub_description { get; set; }
    }
    public class LeadHistoryDto
    {
        public string id { get; set; }
        public List<ListHistory> lista { get; set; }
    }

    public class SendLeadDto
    {
        public string id { get; set; }
        public string lead { get; set; }
    }
    #endregion

    #region ApiLead
    public class InsertLeadDto
    {
        public int contact_id { get; set; }
        public bool success { get; set; }
    }

    public class InfoContacto
    {
        public int contact_id { get; set; }
        public data data { get; set; }
        public bool success { get; set; }
    }
    public class data
    {
        public string id { get; set; }
        public string phone { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string office { get; set; }
        public string vatin { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string ssc { get; set; }
        public string year_of_birth { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string postal { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string other_info_ft { get; set; }
        public string other_info_sec { get; set; }
        public string other_info_thir { get; set; }
        public string other_info_4 { get; set; }
        public string other_info_5 { get; set; }
        public string other_info_6 { get; set; }
        public string comment { get; set; }
        public string client_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string batch_name { get; set; }
        public string sec_phone { get; set; }
        public string thr_phone { get; set; }
        public string fourth_phone { get; set; }
        public string fax { get; set; }
        public string www { get; set; }
        public string other_i_name_1 { get; set; }
        public string other_i_name_2 { get; set; }
        public string other_i_name_3 { get; set; }
        public string other_i_name_4 { get; set; }
        public string other_i_name_5 { get; set; }
        public string other_i_name_6 { get; set; }
        public string other_info_7 { get; set; }
        public string other_i_name_7 { get; set; }
        public string other_info_8 { get; set; }
        public string other_i_name_8 { get; set; }
        public string other_info_9 { get; set; }
        public string other_i_name_9 { get; set; }
        public string other_info_10 { get; set; }
        public string other_i_name_10 { get; set; }
        public string other_info_11 { get; set; }
        public string other_i_name_11 { get; set; }
        public string other_info_12 { get; set; }
        public string other_i_name_12 { get; set; }
        public string other_info_13 { get; set; }
        public string other_i_name_13 { get; set; }
        public string other_info_14 { get; set; }
        public string other_i_name_14 { get; set; }
        public string other_info_15 { get; set; }
        public string other_i_name_15 { get; set; }
        public string other_info_16 { get; set; }
        public string other_i_name_16 { get; set; }
        public string other_info_17 { get; set; }
        public string other_i_name_17 { get; set; }
        public string other_info_18 { get; set; }
        public string other_i_name_18 { get; set; }
        public string other_info_19 { get; set; }
        public string other_i_name_19 { get; set; }
        public string other_info_20 { get; set; }
        public string other_i_name_20 { get; set; }
        public string other_info_21 { get; set; }
        public string other_i_name_21 { get; set; }
        public string other_info_22 { get; set; }
        public string other_i_name_22 { get; set; }
        public string other_info_23 { get; set; }
        public string other_i_name_23 { get; set; }
        public string other_info_24 { get; set; }
        public string other_i_name_24 { get; set; }
        public string other_info_25 { get; set; }
        public string other_i_name_25 { get; set; }
        public string other_info_26 { get; set; }
        public string other_i_name_26 { get; set; }
        public string other_info_27 { get; set; }
        public string other_i_name_27 { get; set; }
        public string other_info_28 { get; set; }
        public string other_i_name_28 { get; set; }
        public string other_info_29 { get; set; }
        public string other_i_name_29 { get; set; }
        public string other_info_30 { get; set; }
        public string other_i_name_30 { get; set; }
        public string other_info_31 { get; set; }
        public string other_i_name_31 { get; set; }
        public string other_info_32 { get; set; }
        public string other_i_name_32 { get; set; }
        public string other_info_33 { get; set; }
        public string other_i_name_33 { get; set; }
        public string other_info_34 { get; set; }
        public string other_i_name_34 { get; set; }
        public string other_info_35 { get; set; }
        public string other_i_name_35 { get; set; }
        public string assigned_agent_id { get; set; }
        public string company_id { get; set; }
        public string last_reason { get; set; }
        public string calling_list_id { get; set; }
        public string last_call_id { get; set; }
        public List<string> phonenumbers { get; set; }
    }
    //public class TelefonoNumeros
    //{
    //    public string numero { get; set; }
    //}

    public class InfoCall
    {
        public string id { get; set; }
        public string agent_id { get; set; }
        public string agent_username { get; set; }
        public string talk_time { get; set; }
        public string talk_start { get; set; }
        public string talk_end { get; set; }
        public string number { get; set; }
        public string campaign { get; set; }
        public string campaign_name { get; set; }
        public string record_file { get; set; }
        public string created_at { get; set; }
        public string customer_id { get; set; }
        public object comment { get; set; }
        public string agent_group_id { get; set; }
        public string agent_group_name { get; set; }
        public string call_ending_reason { get; set; }
        public string call_ending_reason_name { get; set; }
        public string handling_stop { get; set; }
        public string direction { get; set; }
        public string call_type { get; set; }
        public string contact_id { get; set; }
        public string call_type_name { get; set; }

        public bool success { get; set; }
    }
    #endregion

    #region CTI PRESENCE

    public class AuthPresenceDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class TokenPresenceDto
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public DatosToken Data { get; set; }
    }
    public class DatosToken
    {
        public string Token { get; set; }
    }
    public class ServicesPresenceDto
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public DatosServices[] Data { get; set; }
    }
    public class DatosServices
    {
        public int LoadId { get; set; }
        public int ServiceId { get; set; }
        public int SourceId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RequestInsertPresenceDto
    {
        public int SourceId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneDescription { get; set; }
        public string PhoneTimeZoneId { get; set; }
        public string PhoneNumber2 { get; set; }
        public int PhoneDescription2 { get; set; }
        public string PhoneTimeZoneId2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public int PhoneDescription3 { get; set; }
        public string PhoneTimeZoneId3 { get; set; }
        public string PhoneNumber4 { get; set; }
        public int PhoneDescription4 { get; set; }
        public string PhoneTimeZoneId4 { get; set; }
        public string PhoneNumber5 { get; set; }
        public int PhoneDescription5 { get; set; }
        public string PhoneTimeZoneId5 { get; set; }
        public string PhoneNumber6 { get; set; }
        public int PhoneDescription6 { get; set; }
        public string PhoneTimeZoneId6 { get; set; }
        public string PhoneNumber7 { get; set; }
        public int PhoneDescription7 { get; set; }
        public string PhoneTimeZoneId7 { get; set; }
        public string PhoneNumber8 { get; set; }
        public int PhoneDescription8 { get; set; }
        public string PhoneTimeZoneId8 { get; set; }
        public string PhoneNumber9 { get; set; }
        public int PhoneDescription9 { get; set; }
        public string PhoneTimeZoneId9 { get; set; }
        public string PhoneNumber10 { get; set; }
        public int PhoneDescription10 { get; set; }
        public string PhoneTimeZoneId10 { get; set; }
        public int Priority { get; set; }
        public string Comments { get; set; }
        public bool Scheduled { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public long CapturingAgent { get; set; }
        public string CustomData1 { get; set; }
        public string CustomData2 { get; set; }
        public string CustomData3 { get; set; }
        public string CallerId { get; set; }
        public string CallerName { get; set; }
        public string CustomerId { get; set; }
    }

    public class ResponseInsertPresenceDto
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Data { get; set; }

        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public string Content { get; set; }

        public string InternalCode { get; set; }
    }

   

    public class RecordPresenceDto
    {
        public int Status { get; set; }
        public int LastQCode { get; set; }
        public string LasQCodeDescription { get; set; }
        public string NumberOperation { get; set; }
        public List<string> TypeOperation { get; set; }
    }
    #endregion

    public class ImportFileDto
    {
        public string campaignId { get; set; }
        public List<ImportMappignDto> mapping { get; set; }

    }
    public class ImportMappignDto
    {
        public string column { get; set; }
        public  string leadField { get; set; }
    }

    public class ImportLead
    {
        public string phone { get; set; }
        public string email { get; set; }
        public string  firstName { get; set; }
        public string lastName { get; set; }
        public string  avatarUrl { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string  device { get; set; }
        public string browser { get; set; }
        public string screenResolution { get; set; }
    }

    public class DetailUploadFileDto
    {
        public string id { get; set; }
        public List<string> headers { get; set; }
        public List<List<string>> rows { get; set; }

    }
}