using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Infrastructure.Identity.Contexts
{
    public class AppDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {
        public DbSet<ContractorEntity> Contractors { get; set; }
        public DbSet<RoadInspectorEntity> RoadInspectors { get; set; }
        public DbSet<OperatorEntity> Operators { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskFixationDefectEntity> FixationDefectTasks { get; set; }
        public DbSet<TaskFixationWorkEntity> FixationWorkTasks { get; set; }

        public DbSet<FixationDefectEntity> FixationDefects { get; set; }
        public DbSet<DefectTypeEntity> DefectTypes { get; set; }
        public DbSet<FixationWorkEntity> FixationWorks { get; set; }

        public DbSet<PhotoEntity> Photos { get; set; }

        public DbSet<AssignmentEntity> Assignments { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Operator
            modelBuilder.Entity<OperatorEntity>()
                .HasOne(_operator => _operator.User)
                .WithOne()
                .HasForeignKey<OperatorEntity>(_operator => _operator.Id)
                .IsRequired();

            // RoadInspector
            modelBuilder.Entity<RoadInspectorEntity>()
                .HasOne(roadInspector => roadInspector.User)
                .WithOne()
                .HasForeignKey<RoadInspectorEntity>(roadInspector => roadInspector.Id)
                .IsRequired();

            // Tasks
            modelBuilder.Entity<TaskEntity>()
                .HasOne(task => task.RoadInspector)
                .WithMany(roadInspector => roadInspector.AppointedTasks)
                .HasForeignKey(task => task.RoadInspectorId);

            modelBuilder.Entity<TaskFixationWorkEntity>()
                .HasOne(task => task.PrevTask)
                .WithOne(prevTask => prevTask.NextTask)
                .HasForeignKey<TaskFixationWorkEntity>(task => task.PrevTaskId)
                .IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .UseTphMappingStrategy();

            // Fixation
            modelBuilder.Entity<FixationWorkEntity>()
                .HasMany(fixation => fixation.Photos)
                .WithOne()
                .HasForeignKey(photo => photo.FixationWorkId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixationDefectEntity>()
               .HasMany(fixation => fixation.Photos)
               .WithOne()
               .HasForeignKey(photo => photo.FixationDefectId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixationDefectEntity>()
                .HasOne(fixation => fixation.DefectType)
                .WithMany()
                .HasForeignKey(fixation => fixation.DefectTypeId);

            modelBuilder.Entity<PhotoEntity>()
                .HasIndex(c => new { c.FixationWorkId, c.FixationDefectId });

            modelBuilder.Entity<PhotoEntity>()
                .ToTable(table => table.HasCheckConstraint("CK_ModelC_SingleReference", "(\"FixationWorkId\" IS NULL     AND \"FixationDefectId\" IS NOT NULL OR " +
                                                                                         "\"FixationWorkId\" IS NOT NULL AND \"FixationDefectId\" IS NULL)"));
            // Assignment
            modelBuilder.Entity<AssignmentEntity>()
                .HasOne(assignment => assignment.FixationDefect)
                .WithOne(fixationDefect => fixationDefect.Assignment)
                .HasForeignKey<AssignmentEntity>(assignment => assignment.FixationDefectId);

            modelBuilder.Entity<AssignmentEntity>()
               .HasOne(assignment => assignment.Contractor)
               .WithMany()
               .HasForeignKey(assignment => assignment.ContractorId);
        }
    }
}
