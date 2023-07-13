namespace SCParking.Domain.Views.DTOs
{
    public class ProviderDto
    {
        public string key { get; set; }
        public string name { get; set; }
        public int? status { get; set; }
        public string valueKey { get; set; }
    }

    public class LeadProviderResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string valueKey { get; set; }
        public int? status { get; set; }
    }
}
