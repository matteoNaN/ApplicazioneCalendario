using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataAccess;
using SharedLibrary.DTOs;
using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models;
using SharedLibrary.Models.IdentityOverrides;
using System.Globalization;
using System.Reflection.Metadata;

namespace API_ApplicazioneCalendario.Services
{
    public interface IGruppiService
    {
        public Task<Result<List<GruppoDTO>>> GetGruppiUtente(string userId);

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

            var GruppoUser = new GruppoUser()
            {
                UserId = user,
                GruppoId = gruppo.Id,
                CreationDate = DateTime.Now
            };

            await _context.GruppoUsers.AddAsync(GruppoUser);
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

        public async Task<Result<List<GruppoDTO>>> GetGruppiUtente(string userId)
        {
            var gruppiList = await _context.Gruppi
                .Include(gr => gr.CreatorUser)
                .Select(gr => new GruppoDTO
                {
                    Id = gr.Id.ToString(),
                    Name = gr.Name,
                    Description = gr.Description,
                    CreatorUser = new ApplicationUserDTO
                    {
                        Id = gr.CreatorUserId,
                        Email = gr.CreatorUser.Email,
                        Name = gr.CreatorUser.UserName
                    },
                    JoinedUser = _context.GruppoUsers
                        .Where(gu => gu.GruppoId == gr.Id)
                        .Select(gu => new ApplicationUserDTO
                        {
                            Id = gu.User.Id,
                            Email = gu.User.Email,
                            Name = gu.User.UserName
                        }).ToList()
                }).ToListAsync();





            if (gruppiList is null)
            {
                return Result.Failure<List<GruppoDTO>>(new Error("errore nella ricerca dei gruppi utente"));
            }

            return Result.Success(gruppiList);
        }
    }
}
