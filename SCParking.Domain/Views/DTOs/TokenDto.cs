using System.Text.Json.Serialization;

namespace SCParking.Domain.Views.DTOs
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public string token { get; set; }

       // [JsonPropertyName("refreshToken")]
       // public string RefreshToken { get; set; }
    }
  
}
