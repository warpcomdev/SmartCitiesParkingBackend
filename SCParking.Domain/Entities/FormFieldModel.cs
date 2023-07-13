using System;

namespace SCParking.Domain.Entities
{
    public class FormFieldModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public string validation { get; set; }
        public bool required { get; set; }
        public DateTime cerated_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public Guid site_id { get; set; }
    }
}
