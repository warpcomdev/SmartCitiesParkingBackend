using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class WorkflowDto
    {
        public string Id { get; set; }
        public List<StepDto> Steps { get; set; }
    }
    public class EvaluateDto
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }

    public class HeaderDto
    {
        [System.Text.Json.Serialization.JsonPropertyName("@Authorization")]
        public string Authorization { get; set; }
    }

    public class QueryParamDto
    {
        public string Country { get; set; }
    }

    public class BodyDto
    {
        public string Phone { get; set; }
    }

    public class ResponseMappingDto
    {
        public string PhoneCompany { get; set; }
    }

    public class FieldDto
    {
        [System.Text.Json.Serialization.JsonPropertyName("commercial.commercial1")]
        public string CommercialCommercial1 { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("workflow.phoneCompany")]
        public string WorkflowPhoneCompany { get; set; }
    }

    public class ConfigDto
    {
        public EvaluateDto Evaluate { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public HeaderDto Headers { get; set; }
        public QueryParamDto QueryParams { get; set; }
        public BodyDto Body { get; set; }
        public ResponseMappingDto ResponseMapping { get; set; }
        public FieldDto Fields { get; set; }
        public string CallCenterId { get; set; }
        public string CtiId { get; set; }
    }

    public class BranchDto
    {
        public object Value { get; set; }
        public string NextStepId { get; set; }
    }

    public class StepDto
    {
        public string Id { get; set; }
        public string StepType { get; set; }
        public string NextStepId { get; set; }
        public string Name { get; set; }
        public ConfigDto Config { get; set; }
        public List<BranchDto> Branchs { get; set; }
    }
}
