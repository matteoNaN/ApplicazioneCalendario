using System.Security.Claims;

namespace CalendarioFrontEnd.Auth
{
    public class LoginUser
    {
        public string DisplayName { get; set; }
        public string Jwt { get; set; }
        public ClaimsPrincipal? Claims { get; set; }

        public LoginUser(string displayName, string jwt, ClaimsPrincipal? claims)
        {
            this.DisplayName = displayName;
            this.Jwt = jwt;
            this.Claims = claims;
        }

    }
}
