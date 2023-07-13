using System;

namespace SCParking.Domain.Models
{
    public class RateDetails
    {
        public Guid Id { get; set; }
        public int Hour { get; set; }
        public string Day { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid RateId { get; set; }
        public virtual Rate Rate { get; set; }
    }
}
