using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CurrentSessionDto
    {
        public Guid id { get; set; }

        public string email { get; set; }

        public string unconfirmedEmail { get; set; }

        public string role { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phone { get; set; }

        public string secondPhone { get; set; }

        public string avatarUrl { get; set; }

        public Guid? customerId { get; set; }

        public Guid accountId { get; set; }

        public bool allowCreateCustomers { get; set; }
        public bool allowCreateCampaigns { get; set; }
        public bool allowManageWorkflows { get; set; }

        public dynamic targetId { get; set; }

        public string targetType { get; set; }
    }

    public class CurrentSessionPutDto
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string firstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string lastName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string phone { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string secondPhone { get; set; }

        [JsonIgnore]
        public string avatarUrl { get; set; }

        public IFormFile avatar { get; set; }
    }

    public class CurrentSessionEmailPutDto
    {
        public string email { get; set; }

        public string password { get; set; }

    }
    
}
