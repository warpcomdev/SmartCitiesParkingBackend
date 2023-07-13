using System;

namespace SCParking.Domain.Views.DTOs
{
    public class CustomerTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }


    public class CustomerTypeResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
