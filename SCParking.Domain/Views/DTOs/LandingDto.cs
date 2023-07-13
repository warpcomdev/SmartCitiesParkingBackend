using System;
namespace SCParking.Domain.Views.DTOs
{
    public class LandingDto
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class LandingMatomoDto
    {
        public string name { get; set; }
        public string nameWithQuery { get; set; }
        public DateTime? dateAction { get; set; }
    }
}
