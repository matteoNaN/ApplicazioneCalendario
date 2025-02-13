using SharedLibrary.DataAccess;
using SharedLibrary.DTOs;
using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models;
using SharedLibrary.Models.IdentityOverrides;
using System.Reflection.Metadata;

namespace API_ApplicazioneCalendario.Services
{
    public interface IGruppiService
    {
        public Task<Result<List<Gruppi>>> GetGruppiUtente(ApplicationUser user);

        public Task<Result> CreaGruppo(string user, CreazioneGruppoDTO gruppi);

        public Task<Result> EsciDaGruppo(ApplicationUser applicationUser, Gruppi gruppi);

        public Task<Result> EntraInGruppo(ApplicationUser appUser, Gruppi gruppi);



    }
    public class GruppiService : IGruppiService
    {
        private readonly ApplicationDbContext _context;

        public GruppiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> CreaGruppo(string user, CreazioneGruppoDTO gruppoDTO)
        {
            if(string.IsNullOrEmpty(gruppoDTO.Name))
            {
                return new Result(isSuccess: false, "Errore nella creazione del gruppo, Nome Mancante");
            }
            var gruppo = new Gruppi()
            {
                Id = Guid.NewGuid(),
                Name = gruppoDTO.Name,
                Description = gruppoDTO.Description,
                CreatorUserId = user

            };

            await _context.Gruppi.AddAsync(gruppo);
            await _context.SaveChangesAsync();
          
          
            return Result.Success();
            
        }

        public Task<Result> EntraInGruppo(ApplicationUser appUser, Gruppi gruppi)
        {
            throw new NotImplementedException();
        }

        public Task<Result> EsciDaGruppo(ApplicationUser applicationUser, Gruppi gruppi)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Gruppi>>> GetGruppiUtente(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
