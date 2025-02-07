using SharedLibrary.Models.IdentityOverrides;

namespace API_ApplicazioneCalendario.Auth
{
    public static class HelperAuth
    {
        public static void AppendRefreshTokenCookie(ApplicationUser user, IResponseCookies cookies)
        {
            var options = new CookieOptions();
            options.HttpOnly = true;
            options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.Expires = DateTime.Now.AddMinutes(60);
            cookies.Append("refresh-token", user.SecurityStamp, options);
        }
    }
}
