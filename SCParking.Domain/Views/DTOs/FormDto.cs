using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class FormDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string submitText { get; set; }
        public bool? IsDefault { get; set; }
        public string laia_token { get; set; }
        public CallTrackingDto dynamicNumber { get; set; }
        public dynamic formPolicy { get; set; }
        public FormUIDto ui { get; set; }
        public List<FormFieldDto> formFields { get; set; }
        public MessagesDto messages { get; set; }
    }
}
