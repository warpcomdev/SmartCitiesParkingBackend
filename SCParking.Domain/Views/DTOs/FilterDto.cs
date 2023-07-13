using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace SCParking.Domain.Views.DTOs
{
    public class FilterDto
    {
        /// <summary>
        /// Filter fields
        /// </summary>
        public Dictionary<string, string> filter { get; set; }

        /// <summary>
        /// id fields
        /// </summary>
        public Dictionary<string, string> id { get; set; }
        /// <summary>
        /// Paging fields   
        /// </summary>
        //public PageDto page { get; set; }
        public Dictionary<string, string> page { get; set; }

        /// <summary>
        /// Sort fields
        /// </summary>
        public Dictionary<string, string> order { get; set; }


        [BindNever]
        public Guid currentUserId { get; set; }

        [BindNever]
        public Guid currentAccountId { get; set; }

        [BindNever]
        public Guid currentRoleId { get; set; }


    }   

   

   

}
