using System;
using SCParking.Domain.Enums;

namespace SCParking.Domain.Entities
{
    public class Result
    {
        public ECodeResult code_result { get; set; }
        public Guid? id { get; set; }
        public bool error { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
      
    }
}
