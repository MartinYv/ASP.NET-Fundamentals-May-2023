using Homies.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventParticipant> EvensParticimants { get; set; } = null!;
        public DbSet<Homies.Data.Models.Type> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder
                .Entity<Event>()
                .HasOne(e => e.Type)
                .WithMany(t => t.Events)
                .HasForeignKey(t => t.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventParticipant>().HasKey(k => new { k.EventId, k.HelperId });

            modelBuilder.Entity<EventParticipant>().HasOne(k => k.Event).WithMany().HasForeignKey(k => k.EventId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EventParticipant>().HasOne(k => k.Helper).WithMany().HasForeignKey(k => k.HelperId).OnDelete(DeleteBehavior.NoAction);

			modelBuilder
				.Entity<Homies.Data.Models.Type>()
                .HasData(new Homies.Data.Models.Type()
                {
                    Id = 1,
                    Name = "animals"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 2,
                    Name = "fun"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 3,
                    Name = "discussion"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 4,
                    Name = "work"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}