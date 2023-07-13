using System;

namespace SCParking.Domain.Views.DTOs
{
    public class SiteDto
    {
        public Guid? id { get; set; }
        public string url { get; set; }

        public string description { get; set; }
        public string customerId { get; set; }

        public string pixelControl { get; set; }
    }
   

    public class SiteResponseDto
    {
        public Guid id { get; set; }
        public string url { get; set; }

        public string description { get; set; }

        public string name { get; set; }

        public Guid customerId { get; set; }
        public string pixelControl { get; set; }
        public string[] campaignIds { get; set; }
    }
}
