using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [NotMapped]
        public string Password { get; set; }
        public int? Status { get; set; }

        public string Phone { get; set; }

        public string SecondPhone { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }


    public class UserRequestPostDto
    {       
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string roleId { get; set; }

        public string customerId { get; set; }

        public bool allowCreateCustomers { get; set; }
        public bool allowCreateCampaigns { get; set; }
        public bool allowManageWorkflows { get; set; }

        public object targetId { get; set; }

        public string targetType { get; set; }


        [JsonIgnore]
        public Guid createdBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }       

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
    }

    public class UserRequestPutDto
    {
        [JsonIgnore]
        public Guid id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string roleId { get; set; }

        public string customerId { get; set; }

        public bool allowCreateCustomers { get; set; }
        public bool allowCreateCampaigns { get; set; }
        public bool allowManageWorkflows { get; set; }

        public object targetId { get; set; }

        public string targetType { get; set; }


        [JsonIgnore]
        public Guid modifiedBy { get; set; }

        [JsonIgnore]
        public Guid currentUserId { get; set; }

        [JsonIgnore]
        public Guid currentAccountId { get; set; }

        [JsonIgnore]
        public Guid currentRoleId { get; set; }
    }


    public class UserResponsePutDto
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string roleId { get; set; }

        public string customerId { get; set; }
    }

    public class UserResponsePostDto
    {
        public string id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string roleId { get; set; }

        public string customerId { get; set; }
    }


    public class UserResponseGetDto
    {
        public Guid id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        [JsonIgnore]
        public virtual string fullName
        {
            get {
                return  string.Concat(firstName, " ", lastName);
            }
        }

        public string email { get; set; }

        public string phone { get; set; }

        public int status { get; set; }

        public Guid roleId { get; set; }

        public Guid? customerId { get; set; }

        public string avatarUrl { get; set; }

        public Guid accountId { get; set; }

        public bool allowCreateCustomers { get; set; }
        public bool allowCreateCampaigns { get; set; }
        public bool allowManageWorkflows { get; set; }

        public dynamic targetId { get; set; }

        public string targetType { get; set; }

    }

    public class UserResponseToAssignGetDto
    {
        public Guid id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public Guid roleId { get; set; }

        public bool allowCreateCustomers { get; set; }
        public bool allowCreateCampaigns { get; set; }
        public bool allowManageWorkflows { get; set; }

        public dynamic targetId { get; set; }

        public string targetType { get; set; }

        [JsonIgnore]
        public virtual string fullName
        {
            get
            {
                return string.Concat(firstName, " ", lastName);
            }
        }

    }
}
