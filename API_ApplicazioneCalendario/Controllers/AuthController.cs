using API_ApplicazioneCalendario.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models.IdentityOverrides;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_ApplicazioneCalendario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized();

            var token = _jwtManager.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok(new { Message = "Registrazione completata con successo" });
        }

    }
}
