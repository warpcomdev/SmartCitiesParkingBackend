namespace SCParking.Domain.Views.DTOs
{
    public class AuthenticationDto
    {        
        public string email { get; set; }
               
        public string password { get; set; }

      
    }

    public class AuthenticationDtoOnline
    {
        public string username { get; set; }

        public string password { get; set; }


    }
}
