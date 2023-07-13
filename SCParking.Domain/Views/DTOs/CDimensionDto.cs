using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class CDimensionDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string scope { get; set; }

        public string fieldType { get; set; }

        public string leadField { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

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

        public int? index { get; set; }
        public string laia_token { get; set; }
        public SourceDto source { get; set; }
    }

    public class CDimensionRequestPostDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string fieldType { get; set; }

        public string leadField { get; set; }

        public string scope { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

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


    public class CDimensionRequestPutDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public string fieldType { get; set; }

        public string leadField { get; set; }

        public string scope { get; set; }

        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

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

    public partial class CdimensionResponseDto
    {
        public Guid id { get; set; }       
        public string name { get; set; }
        public string scope { get; set; }
        public string sourceLocation { get; set; }

        public string sourceKey { get; set; }

        public string fieldType { get; set; }

        public string leadField { get; set; }
        public Guid campaignId { get; set; }
    }

    #region Dtos API Online
    public class CDimensionDtoOnline
    {
        public string id { get; set; }
        public string name { get; set; }
        public string scope { get; set; }
        public string fieldType { get; set; }

        public string leadField { get; set; }

        public int? index { get; set; }
        public string laia_token { get; set; }
        public SourceDto source { get; set; }
    }

    public class CDimensionRequestPostDtoOnline
    {      

        public string name { get; set; }

        public string scope { get; set; }

        public int index { get; set; }

        public string laia_token { get; set; }

        [JsonIgnore]
        public string fieldType { get; set; }

        [JsonIgnore]
        public string leadField { get; set; }

        [JsonIgnore]
        public string sourceLocation { get; set; }

        [JsonIgnore]
        public string sourceKey { get; set; }

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

        public SourceDto source { get; set; }

    }
    #endregion Dtos API Online
}
