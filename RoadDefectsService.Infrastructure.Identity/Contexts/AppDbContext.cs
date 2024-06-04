using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Infrastructure.Identity.Contexts
{
    public class AppDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<RoadInspector> RoadInspectors { get; set; }
        public DbSet<Operator> Operators { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Operator
            modelBuilder.Entity<Operator>()
                .HasOne(_operator => _operator.User)
                .WithOne()
                .HasForeignKey<Operator>(_operator => _operator.Id)
                .IsRequired();

            // RoadInspector
            modelBuilder.Entity<RoadInspector>()
                .HasOne(roadInspector => roadInspector.User)
                .WithOne()
                .HasForeignKey<RoadInspector>(roadInspector => roadInspector.Id)
                .IsRequired();

            // Contractors
            //modelBuilder.Entity<Contractor>()
            //    .HasAlternateKey(contractor => contractor.Email);
        }
    }
}
