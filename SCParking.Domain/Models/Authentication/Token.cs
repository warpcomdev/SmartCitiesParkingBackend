using System.Text.Json.Serialization;

namespace SCParking.Domain.Models.Authentication
{
    public class Token
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public RefreshToken RefreshToken { get; set; }
    }
  
}
