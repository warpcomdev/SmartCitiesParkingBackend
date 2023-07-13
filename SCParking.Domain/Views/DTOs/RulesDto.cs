using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class RulesDto
    {
        public List<ConditionFormResponseDto> conditions { get; set; }
        public Guid formId { get; set; }
    }
}
