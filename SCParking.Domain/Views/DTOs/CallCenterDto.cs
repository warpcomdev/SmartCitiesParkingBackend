using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CallCenterDto
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        [JsonIgnore]
        public Guid? EditedBy { get; set; }

        //[Required]
        public string address { get; set; }

        public string name { get; set; }
        public string postalCode { get; set; }
        //public string legalName { get; set; }
        //public string cti { get; set; }
        public string[] ctiIds { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        //[Required]
        public string email { get; set; }

        public string phone { get; set; }

        [JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public string login { get; set; }
        //public Guid? customerParentId { get; set; }

        //[Required]
        //public Guid customerTypeId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid currentRoleId { get; set; }
    }

    public class CallCenterRequestPostDto
    {

        public string name { get; set; }
        public string address { get; set; }

        public string postalCode { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string[] ctiIds { get; set; }

        [JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }
    }

    public class CallCenterRequestPutDto
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string name { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string country { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string city { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string email { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string phone { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string postalCode { get; set; }

        public string[] ctiIds { get; set; }

        [JsonIgnore]
        public string logoUrl { get; set; }

        public IFormFile logo { get; set; }
    }


    public class CallCenterResponseDto
    {
        public string id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string postalCode { get; set; }

        public string client { get; set; }

        public string[] ctiIds { get; set; }

        public string country { get; set; }

        public string city { get; set; }


        public string email { get; set; }

        public string phone { get; set; }

        public int status { get; set; }

        public string logoUrl { get; set; }

        public int productsCount { get; set; }
        public int campaignsCount { get; set; }

        public int contactedCount { get; set; }

        public int conversionsCount { get; set; }

        public int customersCount { get; set; }
        public int totalCount { get; set; }
    }
}
