using System;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class FormFieldDto
    {
        [JsonIgnore]
        public Guid? id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string fieldType { get; set; }

        public string leadField { get; set; }
       
        public string fieldValidation { get; set; }
        public bool? required { get; set; }
    }
}
