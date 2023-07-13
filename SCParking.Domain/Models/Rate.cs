using System;
using System.Collections.Generic;

namespace SCParking.Domain.Models
{
    public class Rate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PlaceType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<RateDetails> RateDetails { get; set; }

        public Rate()
        {
            RateDetails = new HashSet<RateDetails>();
        }
    }
}
