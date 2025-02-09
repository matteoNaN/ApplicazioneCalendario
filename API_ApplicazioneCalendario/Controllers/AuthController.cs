using API_ApplicazioneCalendario.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DTOs.Auth;
using SharedLibrary.Models.IdentityOverrides;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static CalendarioFrontEnd.Pages.Auth.Login;

namespace API_ApplicazioneCalendario.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtManager _jwtManager;
        

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtManager jwtManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtManager = jwtManager;
          
        }

        [HttpPost]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new LoginResponse(false);

            var token = _jwtManager.GenerateJwtToken(user);
            HelperAuth.AppendRefreshTokenCookie(user, HttpContext.Response.Cookies);

            return new LoginResponse(true, token);
        }


        [HttpPost]
        public async Task<RegisterResponse> Register([FromBody] RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return  new RegisterResponse(result.Succeeded,errors.ToList());
            }

            return new RegisterResponse(result.Succeeded);
        }

        [HttpPost]
        public LoginResponse RefreshToken()
        {
            var cookie = HttpContext.Request.Cookies["refresh-token"];
            if (cookie != null)
            {
                var user = this._userManager.Users.FirstOrDefault(user => user.SecurityStamp == cookie);
                if (user != null)
                {
                    var jwtToken = _jwtManager.GenerateJwtToken(user);
                    return new LoginResponse(true, jwtToken);
                }
                else
                {
                    return new LoginResponse(false);
                }
            }
            else
            {
                return new LoginResponse(false);
            }
        }

    }
}
