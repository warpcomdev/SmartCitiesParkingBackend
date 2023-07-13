using System;

#nullable disable

namespace SCParking.Domain.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordLastChange { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public int? Status { get; set; }
        /*public Guid? CustomerId { get; set; }
        public Guid RoleId { get; set; }*/
        public string ResetTokenPassword { get; set; }
        public DateTime? ResetTokenPasswordExpire { get; set; }

        public string Phone { get; set; }

        public string SecondPhone { get; set; }

        public string AvatarUrl { get; set; }

        public bool? UnConfirmedEmail { get; set; }

        public string ResetTokenEmail { get; set; }
        public DateTime? ResetTokenEmailExpire { get; set; }
        public string ResetEmail { get; set; }

       /* public Guid AccountId { get; set; }

        public bool?  AllowCreateCustomers { get; set; }

        public bool? AllowCreateCampaigns { get; set; }

        public bool? AllowManageWorkflows { get; set; }

        public string TargetId { get; set; }

        public string TargetType { get; set; }

        public string UserType { get; set; }

        public string TimeZone { get; set; }        
       */
        public virtual string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }
        
    }
}
