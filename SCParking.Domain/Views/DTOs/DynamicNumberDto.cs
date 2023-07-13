namespace SCParking.Domain.Views.DTOs
{
    public class DynamicNumberDto
    {
        public string ctmID { get; set; }
        public string maskPhone { get; set; }
        public string phone { get; set; }
        public string textTop { get; set; }
        public string textBottom { get; set; }
    }

    public class CallTrackingDto
    {
        public dynamic ctmId { get; set; }
        public dynamic maskPhone { get; set; }
        public dynamic phoneNumber { get; set; }
        public dynamic textTop { get; set; }
        public dynamic textBottom { get; set; }
    }
}
