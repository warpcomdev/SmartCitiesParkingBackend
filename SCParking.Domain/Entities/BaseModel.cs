using System;

namespace SCParking.Domain.Entities
{
    public class Base
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DateTime date_entered { get; set; }
        public DateTime? date_modified { get; set; }
        public Guid? modified_by { get; set; }
        public Guid created_by { get; set; }

        public string description { get; set; }

        public int status { get; set; }

        public string status_description { get; set; }

        public UserModel user_created { get; set; }

        public UserModel user_modified { get; set; }
    }
}
