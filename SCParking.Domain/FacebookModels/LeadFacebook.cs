using System.Collections.Generic;

namespace SCParking.Domain.FacebookModels
{
    public class LeadFacebook
    {
        public string created_time { get; set; }
        public string id { get; set; }

        public List<LeadValuesFacebook> field_data { get; set; }

        public FormFacebook form { get; set; }
    }

    public class LeadValuesFacebook
    {
        public string name { get; set; }
        public List<string> values { get; set; }
    }
   
    }
