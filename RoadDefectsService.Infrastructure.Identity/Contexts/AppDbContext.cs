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

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskFixationDefect> FixationDefectTasks { get; set; }
        public DbSet<TaskFixationWork> FixationWorkTasks { get; set; }

        public DbSet<FixationDefect> FixationDefects { get; set; }
        public DbSet<DefectType> DefectTypes { get; set; }
        public DbSet<FixationWork> FixationWorks { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

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

            // Tasks
            modelBuilder.Entity<TaskEntity>()
                .HasOne(task => task.RoadInspector)
                .WithMany(roadInspector => roadInspector.AppointedTasks)
                .HasForeignKey(task => task.RoadInspectorId);

            modelBuilder.Entity<TaskFixationWork>()
                .HasOne(task => task.PrevTask)
                .WithOne(prevTask => prevTask.NextTask)
                .HasForeignKey<TaskFixationWork>(task => task.PrevTaskId)
                .IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .UseTphMappingStrategy();

            // Fixation
            modelBuilder.Entity<FixationWork>()
                .HasMany(fixation => fixation.Photos)
                .WithOne()
                .HasForeignKey(photo => photo.FixationWorkId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixationDefect>()
               .HasMany(fixation => fixation.Photos)
               .WithOne()
               .HasForeignKey(photo => photo.FixationDefectId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixationDefect>()
                .HasOne(fixation => fixation.DefectType)
                .WithMany()
                .HasForeignKey(fixation => fixation.DefectTypeId);

            modelBuilder.Entity<Photo>()
                .HasIndex(c => new { c.FixationWorkId, c.FixationDefectId });

            modelBuilder.Entity<Photo>()
                .ToTable(table => table.HasCheckConstraint("CK_ModelC_SingleReference", "(\"FixationWorkId\" IS NULL     AND \"FixationDefectId\" IS NOT NULL OR " +
                                                                                         "\"FixationWorkId\" IS NOT NULL AND \"FixationDefectId\" IS NULL)"));
            // Assignment
            modelBuilder.Entity<Assignment>()
                .HasOne(assignment => assignment.FixationDefect)
                .WithOne()
                .HasForeignKey<Assignment>(assignment => assignment.FixationDefectId);

            modelBuilder.Entity<Assignment>()
               .HasOne(assignment => assignment.Contractor)
               .WithMany()
               .HasForeignKey(assignment => assignment.ContractorId);
        }
    }
}
