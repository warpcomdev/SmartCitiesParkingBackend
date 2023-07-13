using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class ConditionDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string comparisonOperator { get; set; }

        public string fieldType { get; set; }

        public string comparisonValue { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string formId { get; set; }

        [JsonIgnore]
        public string campaignId { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }

    }

    public class ConditionRequestPostDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string fieldType { get; set; }

        public string comparisonValue { get; set; }

        public string comparisonOperator { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string formId { get; set; }

        [JsonIgnore]
        public string campaignId { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]       
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
    }


    public class ConditionRequestPutDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string fieldType { get; set; }

        public string comparisonValue { get; set; }

        public string comparisonOperator { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string formId { get; set; }

        [JsonIgnore]
        public string campaignId { get; set; }

        [JsonIgnore]
        public Guid createdBy { get; set; }
        [JsonIgnore]
        public Guid? editedBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
    }

    public partial class ConditionResponseDto
    {
        public Guid id { get; set; }       
        public string name { get; set; }
        public string comparisonValue { get; set; }
        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string fieldType { get; set; }

        public string comparisonOperator { get; set; }
        public Guid campaignId { get; set; }

        public Guid formId { get; set; }
    }

    public class ConditionFormResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string comparisonOperator { get; set; }

        public string fieldType { get; set; }

        public string comparisonValue { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string formId { get; set; }

        [JsonIgnore]
        public string campaignId { get; set; }

     

    }

}
