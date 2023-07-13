namespace SCParking.Domain.Views.DTOs
{
    public class ParkingSlotStatusDto
    {
        public string id { get; set; }
        public StatusValue status { get; set; }
    }

    public class StatusValue
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}
