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

        public Task<Result> EsciDaGruppo(string applicationUser, Guid gruppoId);

        public Task<Result> EntraInGruppo(ApplicationUser appUser, Gruppi gruppi);

        public Task<Result> AggiungiImpegno(AggiungiImpegnoDTO impegnoDTO);

        public Task<Result<CalendarioDTO>> GetCalendarioGruppo(string userId, Guid gruppoId);



    }
    public class GruppiService : IGruppiService
    {
        private readonly ApplicationDbContext _context;

        public GruppiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AggiungiImpegno(AggiungiImpegnoDTO impegnoDTO)
        {
            var impegno = new Impegno()
            {
                Id = Guid.NewGuid(),
                Name = impegnoDTO.Name,
                Description = impegnoDTO.Description,
                StartDate = impegnoDTO.Start,
                EndDate = impegnoDTO.End.HasValue? impegnoDTO.End.Value : DateTime.Now,
                CreationDate = DateTime.Now,
                UserId = impegnoDTO.UserId,
                GruppoId = impegnoDTO.GruppoId
            };

            _context.Impegni.Add(impegno);
            await _context.SaveChangesAsync();

            return Result.Success();
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

            var Calendario = new Calendario()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Description = "",
                GruppoId = gruppo.Id
            };

            await _context.GruppoUsers.AddAsync(GruppoUser);
            await _context.Gruppi.AddAsync(gruppo);
            await _context.Calendari.AddAsync(Calendario);
            await _context.SaveChangesAsync();
          
          
            return Result.Success();
            
        }

        public Task<Result> EntraInGruppo(ApplicationUser appUser, Gruppi gruppi)
        {
            throw new NotImplementedException();
        }

        public Task<Result> EsciDaGruppo(string applicationUser, Guid gruppoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<CalendarioDTO>> GetCalendarioGruppo(string userId, Guid gruppoId)
        {
            var gruppoDTO = await _context.Calendari
                .Include(c => c.Gruppo)
                .Include(c => c.Gruppo.Impegni)
                .Where(c => c.GruppoId == gruppoId &&
                       c.Gruppo.JoinedUsers.Any(ju=> ju.UserId == userId))
                .Select(c => new CalendarioDTO
                {
                    Id = c.Id,
                    Impegni = c.Gruppo.Impegni.Select(i => new ImpegnoDTO
                    {
                        APP_Id = i.Id.ToString(),
                        Name = i.Name,
                        Start = i.StartDate,
                        End = i.EndDate,
                        Description = i.Description,
                        CreationDate = i.CreationDate,
                        CreationUser = new ApplicationUserDTO
                        {
                            Id = i.UserId,
                            Email = i.CreationUser.Email,
                            Name = i.CreationUser.UserName
                        }
                    }).ToList()
                }).FirstOrDefaultAsync();

            if(gruppoDTO is null)
            {
                return Result.Failure<CalendarioDTO>(new Error("Errore nella ricerca del calendario del gruppo"));  
            }

            return Result.Success(gruppoDTO);
        }

        public async Task<Result<List<GruppoDTO>>> GetGruppiUtente(string userId)
        {
            var gruppiList = await _context.Gruppi
                .Include(gr => gr.CreatorUser)
                .Where(gr => gr.JoinedUsers.Any(ju => ju.UserId == userId))
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
