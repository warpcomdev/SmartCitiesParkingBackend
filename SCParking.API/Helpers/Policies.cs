using Microsoft.AspNetCore.Authorization;

namespace SCParking.API.Helpers
{
    public static class Policies
    {
        public const string Admin   = "Admin";
        public const string User    = "User";
        public const string UserApi = "UserApi";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }

        public static AuthorizationPolicy UserApiPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserApi).Build();
        }

    }
}
