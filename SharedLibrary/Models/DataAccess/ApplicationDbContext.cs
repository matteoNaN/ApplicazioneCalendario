using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.Models.IdentityOverrides;
using System.Reflection.Emit;

namespace SharedLibrary.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        

        public DbSet<Gruppi> Gruppi { get; set; }
        public DbSet<GruppoUser> GruppoUsers { get; set; }
        public DbSet<Impegno> Impegni { get; set; }
        public DbSet<Calendario> Calendari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Relazione tra Gruppi e ApplicationUser (Creator)
            builder.Entity<Gruppi>()
                .HasOne(g => g.CreatorUser)
                .WithMany()
                .HasForeignKey(g => g.CreatorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relazione molti-a-molti tra Gruppi e ApplicationUser tramite GruppoUser
            builder.Entity<GruppoUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.JoinedGroups)
                .HasForeignKey(gu => gu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GruppoUser>()
                .HasOne(gu => gu.Gruppo)
                .WithMany(g => g.JoinedUsers)
                .HasForeignKey(gu => gu.GruppoId)
                .OnDelete(DeleteBehavior.NoAction);



            // Relazione 1-N tra Gruppi e Impegni
            builder.Entity<Impegno>()
                .HasOne(i => i.Gruppo)  // Un Impegno appartiene a un Gruppo
                .WithMany(g => g.Impegni)  // Un Gruppo può avere molti Impegni
                .HasForeignKey(i => i.GruppoId) // Chiave esterna in Impegno
                .OnDelete(DeleteBehavior.Cascade); // Se elimini un Gruppo, elimini i suoi Impegni

            // Relazione 1-N tra ApplicationUser e Impegni
            builder.Entity<Impegno>()
                .HasOne(i => i.CreationUser)  // Un Impegno ha un solo Creatore
                .WithMany()  // L'utente non ha una collezione di Impegni
                .HasForeignKey(i => i.UserId) // Chiave esterna
                .OnDelete(DeleteBehavior.Restrict); // Non vogliamo eliminare tutti gli Impegni se un utente viene rimosso


            builder.Entity<Gruppi>()
                .HasOne(g => g.Calendario)
                .WithOne(c => c.Gruppo)
                .HasForeignKey<Calendario>(c => c.GruppoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
