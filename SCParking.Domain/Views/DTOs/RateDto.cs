using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class RateDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDateTime { get; set; }
        public string placeType { get; set; }
        public decimal price { get; set; }
        public List<RateDetailsPostDto> rateDetails { get; set; }
    }

    public class RatePostResponseDto
    {

    }
    
    public class RatePostDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string placeType { get; set; }//Valores PMR VE
        public decimal price { get; set; }
        public List<RateDetailsPostDto> rateDetails { get; set; }
    }

    public class RateDetailsPostDto
    {
        public string id { get; set; }
        public int hour { get; set; }
        public string day { get; set; }
        public decimal price { get; set; }

        
    }
}
