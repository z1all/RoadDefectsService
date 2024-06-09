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

        public DbSet<Photo> Photos { get; set; }

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
                .WithOne()
                .HasForeignKey<TaskFixationWork>(task => task.PrevTaskId)
                .IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .UseTphMappingStrategy();

            // FixationDefect
            modelBuilder.Entity<FixationDefect>()
                .HasOne(fixationDefect => fixationDefect.Task)
                .WithOne(task => task.FixationDefect)
                .HasForeignKey<TaskEntity>(task => task.FixationDefectId);

            // FixationWork
            modelBuilder.Entity<FixationWork>()
                .HasOne(fixationWork => fixationWork.TaskFixationWork)
                .WithOne(task => task.FixationWork)
                .HasForeignKey<TaskFixationWork>(task => task.FixationWorkId);

            // Photo
            modelBuilder.Entity<Photo>()
                .HasOne(photo => photo.FixationWork)
                .WithMany(fixationWork => fixationWork.Photos)
                .HasForeignKey(photo =>  photo.FixationWorkId);

            modelBuilder.Entity<Photo>()
                .HasOne(photo => photo.FixationDefect)
                .WithMany(fixationDefect => fixationDefect.Photos)
                .HasForeignKey(photo => photo.FixationDefectId);
        }
    }
}
