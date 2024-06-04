using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Models;

namespace RoadDefectsService.Infrastructure.Identity.Contexts
{
    public class AppDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {
        public DbSet<Contractor> Contractors { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Contractors
            //modelBuilder.Entity<Contractor>()
            //    .HasAlternateKey(contractor => contractor.Email);
        }
    }
}
