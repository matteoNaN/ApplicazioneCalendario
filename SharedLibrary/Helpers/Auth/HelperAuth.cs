using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.DTOs.Auth;
using SharedLibrary.Models.IdentityOverrides;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SharedLibrary.Helpers.Auth
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

