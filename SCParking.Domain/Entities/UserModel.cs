using System;

namespace SCParking.Domain.Entities
{
    public class UserModel:Base
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_hash { get; set; }
        public DateTime pwd_last_changed { get; set; }
        public int? user_type { get; set; }
        public Guid? customer_id { get; set; }
    }
}
