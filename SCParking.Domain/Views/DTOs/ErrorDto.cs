namespace SCParking.Domain.Views.DTOs
{

    public class ErrorDto
    {      
        public int status { get; set; }
        public dynamic errors { get; set; } 
    }
    public class ErrorDetailDto
    {
        public string error { get; set; }
        public int? count { get; set; }
        public string authorized_types { get; set; }

        public string message { get; set; }


    }

    public class DataErrorDto
    {
        public dynamic errors { get; set; }
    }




}
