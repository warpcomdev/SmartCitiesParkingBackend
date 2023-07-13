using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class TagAnalyticDto
    {
        public Guid id { get; set; }
        public string value { get; set; }
        public decimal efficiency { get; set; }
        public Guid? tagId { get; set; }
       // public virtual Tag tag { get; set; }
    }

    public class TagAnalyticEfficientDto
    {
        public string key { get; set; }
        public decimal? efficiency { get; set; }
        public List<TagEfficientDto> values { get; set; }

        public TagAnalyticEfficientDto()
        {
            values = new List<TagEfficientDto>();
        }
    }

    public class TagEfficientDto
    {
        public string value { get; set; }
        public decimal efficiency { get; set; }
    }
}
