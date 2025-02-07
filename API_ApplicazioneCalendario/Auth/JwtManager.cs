using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models.IdentityOverrides;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_ApplicazioneCalendario.Auth
{
    public class JwtManager
    {
        private readonly JwtSettings _jwtSettings;

        public JwtManager(JwtSettings jwtSettings)
        {
            this._jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings)); ;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName), // NOTE: this will be the "User.Identity.Name" value
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                };

             var token = new JwtSecurityToken(
             issuer: _jwtSettings.ValidIssuer,
             audience: _jwtSettings.ValidAudience,
             claims: claims,
             expires: DateTime.Now.AddHours(2),
             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JwtSettings
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}

