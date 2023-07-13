using System;

namespace SCParking.Domain.Views.DTOs
{
    public class V3ClickDto
    {
        public string Provider { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class SendCodificationDto
    {
        public int V3LeadID { get; set; }
        public string codificationID { get; set; }
        public string codificationDescription { get; set; }
        public string codificationDate { get; set; }
        public string agency { get; set; }
        public string agencyIP { get; set; }

        public Guid id { get; set; }
    }

    public class SendCodificationResponseDto
    {
        public int V3LeadID { get; set; }
        public string response { get; set; }
    }
    public class SendCallLeadDto
    {
        public int phoneNumber { get; set; }
        public string freePhone { get; set; }
        public string agency { get; set; }
        public string agencyLeadId { get; set; }
        public string agencyIp { get; set; }
    }

    public class SendCallLeadResponseDto
    {
        public string agencyLeadID { get; set; }
        public string response { get; set; }
        public int V3LeadID { get; set; }
    }
}
