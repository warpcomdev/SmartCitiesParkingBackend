using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CompleteFormDto
    {      
        public string id { get; set; }
        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? EditedBy { get; set; }
        public string title { get; set; }
        public string launcher { get; set; } = "phone";
        public string color { get; set; }
        [JsonIgnore]
        public string formSegmentationId { get; set; }
        public string campaignId { get; set; }
        public string submit { get; set; }
        [JsonIgnore]
        public bool? isDefault { get; set; }
        [JsonIgnore]
        public string laiaToken { get; set; }
        public List<FormFieldDto> fields { get; set; }
        public DynamicNumberDto callTracking { get; set; }
        public MessagesDto messages { get; set; }
        [JsonIgnore]
        public Guid currentUserId { get; set; }
        [JsonIgnore]
        public Guid currentAccountId { get; set; }
        [JsonIgnore]
        public Guid currentRoleId { get; set; }

        public FormPolicyRequestDto formPolicy { get; set; }
    }


    public class FormRequestPutDto
    {
        public string id { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }
        public string title { get; set; }
        public string launcher { get; set; }
        public string color { get; set; }
        public string campaignId { get; set; }
        public string submit { get; set; }
        [JsonIgnore]
        public bool? isDefault { get; set; }
        public List<FormFieldDto> fields { get; set; }
        public DynamicNumberDto callTracking { get; set; }
        public MessagesDto messages { get; set; }
        public FormPolicyRequestDto formPolicy { get; set; }
    }

    public class FormPolicyRequestDto {     
         public string label { get; set; }
        public string link { get; set; }
    }

    public class FormPolicyResponseDto
    {
        public string label { get; set; }
        public string link { get; set; }
    }

    public class FormResponseDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string launcher { get; set; }
        public string color { get; set; }
        public string submit { get; set; }
        //public string formSegmentationId { get; set; }
        public string campaignId { get; set; }
        public IList<FormFieldDto> fields { get; set; }
        public DynamicNumberDto callTracking { get; set; }
        public MessagesDto messages { get; set; }

        public FormPolicyResponseDto formPolicy { get; set; }
        public IList<ConditionFormResponseDto> conditions { get; set; }
    }
}
