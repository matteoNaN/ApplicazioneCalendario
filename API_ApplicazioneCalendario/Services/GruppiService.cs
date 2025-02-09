using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models;
using SharedLibrary.Models.IdentityOverrides;
using System.Reflection.Metadata;

namespace API_ApplicazioneCalendario.Services
{
    public interface IGruppiService
    {
        public Task<Result<List<Gruppi>>> GetGruppiUtente(ApplicationUser user);

        public Task<Result> CreaGruppo(ApplicationUser user, Gruppi gruppi);

        public Task<Result> EsciDaGruppo(ApplicationUser applicationUser, Gruppi gruppi);

        public Task<Result> EntraInGruppo(ApplicationUser appUser, Gruppi gruppi);



    }
    public class GruppiService : IGruppiService
    {
        public Task<Result> CreaGruppo(ApplicationUser user, Gruppi gruppi)
        {
            throw new NotImplementedException();
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
