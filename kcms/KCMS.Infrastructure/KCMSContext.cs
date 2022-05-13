using KCMS.Domain.Advertising;
using KCMS.Domain.Article;
using KCMS.Domain.League;
using KCMS.Domain.Match;
using KCMS.Domain.Team;
using KCMS.Domain.User;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KCMS.Infrastructure
{
    public class KCMSContext : DbContext
    {
        public KCMSContext(DbContextOptions<KCMSContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertising>()
                .HasIndex(a => a.Url);
            modelBuilder.Entity<Advertising>()
                .HasIndex(a => a.Position);
            modelBuilder.Entity<User>(entity => { entity.HasIndex(u => u.Username).IsUnique(); });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
