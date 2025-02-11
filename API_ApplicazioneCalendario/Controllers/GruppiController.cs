using API_ApplicazioneCalendario.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;
using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models.IdentityOverrides;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace API_ApplicazioneCalendario.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GruppiController : ControllerBase
    {
        private readonly IGruppiService _gruppiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GruppiController(IGruppiService gruppiService, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._gruppiService = gruppiService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGruppiUtente()
        {
            // Estrai il "jti" (JWT ID) dal token
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrEmpty(jti))
            {
                return Unauthorized("errore di authentificazione");
            }

            var user = await _userManager.FindByIdAsync(jti);

            if (user == null)
            {
                return Unauthorized("User non trovato");
            }

            // Ottieni i gruppi dell'utente
            var res = await _gruppiService.GetGruppiUtente(user);

            return Ok(res);
        }


        [HttpPost]
        
        public async Task<IActionResult> AggiungiGruppo([FromBody] CreazioneGruppoDTO gruppoToAdd)
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrEmpty(jti))
            {
                return Unauthorized("errore di authentificazione");
            }


            var res = await _gruppiService.CreaGruppo(jti, gruppoToAdd);

            if(!res.IsSuccess)
            {
                return BadRequest(res.Error.Message);
            }

            return Ok();

        }



    }
}
