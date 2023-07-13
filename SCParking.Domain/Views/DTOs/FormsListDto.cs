using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class FormsListDto
    {
        public DefaultFormDto defaults { get; set; }
        public FormUIDto ui { get; set; }
        public List<FormDto> forms { get; set; }
        public List<RulesDto> rules { get; set; }
    }
}
