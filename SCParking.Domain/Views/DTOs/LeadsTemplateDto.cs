using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class LeadsTemplateDto
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        //public Guid leadTemplateId { get; set; }
        //[JsonIgnore]
        //public Guid LeadTemplateId { get; set; }
        public string name { get; set; }
        public int? status { get; set; }   
        public string campaignId { get; set; }
        public List<LeadsTemplateDto> mapping { get; set; }
        public string leadField { get; set; }
        public int? column { get; set; }

        [JsonIgnore]
        public Guid? createdBy { get; set; }

        [JsonIgnore]
        public Guid? currentUserId { get; set; }

        [JsonIgnore]
        public Guid? currentAccountId { get; set; }

        [JsonIgnore]
        public Guid? currentRoleId { get; set; }
    }

    public class leadTemplateMappingsDto
    {
        public string leadFields { get; set; }
        public int? columns { get; set; }
    }

    public class leadTemplateResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }        
        public int? status { get; set; }
        public List<LeadsTemplateDto> mapping { get; set; }
        public string leadFields { get; set; }
        public int? columns { get; set; }
        public string leadTemplateId { get; set; }
        public leadTemplateResponseDto()
        {
            mapping = new List<LeadsTemplateDto>();
        }
    }
}
