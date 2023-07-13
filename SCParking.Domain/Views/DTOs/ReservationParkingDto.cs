namespace SCParking.Domain.Views.DTOs
{
    public class ReservationParkingDto
    {
        public string reservationStartAt { get; set; }
        public int duration { get; set; }
        public string tokenUser { get; set; }
    }
}
