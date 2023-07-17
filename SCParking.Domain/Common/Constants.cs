using System;

namespace SCParking.Domain.Common
{
    public static class Constants
    {
        public static Guid Operator = Guid.Parse("65A35546-3A91-4B67-8669-A6483606BB0D");
        public static Guid Administrator = Guid.Parse("FE745357-6C7D-4CEC-88B2-88A68399D7B0");
        public static Guid SuperAdmin = Guid.Parse("B2099B6B-7FE7-452B-969B-A106E61D25F2");
        public static Guid Agent = Guid.Parse("945E8525-4DDC-4F4B-9EF0-CC50E26FE26D");


        public const string SitePermission = "site";
        public const string CustomerPermission = "customer";
        public const string CallCenterPermission = "call-center";

        public static string[] Scopes = { "visit", "page", "action" };
        public static string[] SourceLocations = { "url", "localStorage", "sessionStorage", "cookie", "dataLayer" };
        public static string[] FieldTypes = { "text", "number" };
        public static string[] LeadFields = { "phone", "email","firstName","lastName","avatarUrl","address","country","device","browser","screenResolution" };
        public static string[] ComparisonOperators = { "equal", "distinct", "greaterThan", "lessThan","contains" };

        public const string LeadFilePhone = "phone";
        public const string LeadFileEmail = "email";
        public const string LeadFileFirstName = "firstName";
        public const string LeadFileLastName = "lastName";
        public const string LeadFileAvatarUrl = "avatarUrl";
        public const string LeadFileAddress = "address";
        public const string LeadFileCountry = "country";
        public const string LeadFileDevice = "device";
        public const string LeadFileBrowser = "browser";
        public const string LeadFileScreenResolution = "screenResolution";

        public const string CTIPresence = "Presence CTI";
        public const string CTILeonTel = "LeonTel";
        public const string CTIEvolution = "Evolution CTI";
        public const string CTILeadDesk = "LeadDesk";

        public const string CTIPresenceId = "070FDC19-418F-4549-B92F-9131B984FDDB";
        public const string CTILeonTelId = "519B4270-C415-43F5-A706-8C88C44EB706";
        public const string CTIEvolutionId = "34ED5D03-40E0-4D64-98AE-BE6CBFB160CD";
        public const string CTILeadDeskId = "153DF2DE-6D8D-4206-A1DD-D329DC84A4E2";

        public const string CTATypeForm = "Form";
        public const string CTATypeInbound = "Inbound";

        public const int AutomationStatusStopped = 0;
        public const int AutomationStatusRunning = 1;

        public const string WorflowStatusStopped = "stopped";
        public const string WorflowStatusRunning = "running";

        public const int LeadSendingToProvider = 2;
        public const int LeadSentToProviderSuccess = 1;
        public const int LeadSentToProviderError = 0;
        public const string LeadSentToProviderAgency = "Calima";
        public const string LeadSentOKToProvider = "Success";
        public const string LeadSentCodificationOk = "Codification Saved";


        public const string ValidationNoPhoneCode = "1001";
        public const string ValidationNoPhone = "No Phone number";

        public const string ValidationDuplicatedCode = "1002";
        public const string ValidationDuplicated = "Duplicated";
        
        public const string ValidationRobinsonCode = "1003";
        public const string ValidationRobinson = "Robinson";

        public const string ValidationNoVisitorCode = "1004";
        public const string ValidationNoVisitor = "No Visitor Id";

        public const string ValidationNoFormCode = "1005";
        public const string ValidationNoForm= "No Form Id";

        public const string PlaceTypePMR = "PMR";
        public const string PlaceTypePMRDescription = "Plaza de movilidad reducida";
        public const string PlaceTypeVE = "VE";

        public const string Setting_URLSCAUTH = "URL_SC_AUTH";
        public const string Setting_URLSCENTITY = "URL_SC_ENTITY";
        public const string Setting_USRSCSERVICEPARKING = "USR_SC_SERVICE_PARKING";
        public const string Setting_PASSSCSERVICEPARKING = "PASS_SC_SERVICE_PARKING";
        public const string Setting_URL_MUYBICI_ENTITY = "URL_MUYBICI_ENTITY";

        public const string PARKING_STATUS_FREE = "free";
        public const string PARKING_STATUS_OCCUPIED = "occupied";
        public const string PARKING_STATUS_RESERVED = "reserved";

    }
}
