using System;

namespace SCParking.Domain.Views.DTOs
{
    public class AccountDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string logoUrl { get; set; }

        public string timeZone { get; set; }
    }
}
