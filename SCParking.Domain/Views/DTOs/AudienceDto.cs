using System.Collections.Generic;
using Newtonsoft.Json;

namespace SCParking.Domain.Views.DTOs
{
    public class AudienceDto
    {
        [JsonIgnore]
        public string id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        [JsonIgnore]
        public string parent { get; set; }
        public List<AudienceDto> children { get; set; }

        public AudienceDto()
        {
            children = new List<AudienceDto>();
        }
	}

    
}
