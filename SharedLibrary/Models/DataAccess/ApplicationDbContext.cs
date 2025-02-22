using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.Models.IdentityOverrides;

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

            // Relazione tra Impegno e ApplicationUser
            builder.Entity<Impegno>()
                .HasOne(i => i.CreationUser)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relazione tra Impegno e Gruppi
            builder.Entity<Impegno>()
                .HasOne(i => i.Gruppo)
                .WithMany()
                .HasForeignKey(i => i.GruppoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
