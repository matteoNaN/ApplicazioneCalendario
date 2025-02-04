using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

        public string GenerateJwtToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.Email) },
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

