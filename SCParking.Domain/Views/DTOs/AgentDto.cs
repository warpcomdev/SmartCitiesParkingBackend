using System;

namespace SCParking.Domain.Views.DTOs
{
    public class AgentDto
    {
        public Guid? id { get; set; }
        public string name { get; set; }
        public string valueKey { get; set; }
        public int status { get; set; }
    }

    public class AgentsResponseDto
    {
        public string key { get; set; }
        public string name { get; set; }
    }
}
