namespace SCParking.API.Helpers
{
    public class AppSettings
    {
        public string Jwt_Secret_key { get; set; }
        public string Jwt_Audience_Token { get; set; }
        public string Jwt_Issuer_Token { get; set; }
        public string Jwt_Expire_Minutes { get; set; }
        public string Jwt_RefreshTokenExpiration { get; set; }
        

    }
}
