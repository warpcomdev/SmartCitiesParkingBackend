namespace SCParking.Domain.Views.DTOs
{
    public class ResetPasswordDto
    {
       
        public string token { get; set; }

        public string password { get; set; }
       
        public string confirmPassword { get; set; }
    }
}
