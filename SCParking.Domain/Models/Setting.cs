using System;

namespace SCParking.Domain.Models
{
    public class Setting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SettingCode { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }        
      

        public Setting()
        {            
        }
    }
}
