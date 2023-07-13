namespace SCParking.Domain.Views.DTOs
{
    public class ChangePasswordDto
    {
        public string currentPassword { get; set; }
       
        public string password { get; set; }

        public string confirmPassword { get; set; }
    }
}
