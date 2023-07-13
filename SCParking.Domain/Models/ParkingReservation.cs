using System;

namespace SCParking.Domain.Models
{
    public class ParkingReservation
    {
        public Guid Id { get; set; }
        public string UserToken { get; set; }
        public string ParkingSpotId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
    }
}
