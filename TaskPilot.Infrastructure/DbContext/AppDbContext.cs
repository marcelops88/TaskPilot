using Microsoft.EntityFrameworkCore;
using TaskPilot.Domain.Entities;

namespace TaskPilot.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskHistory>()
                .HasKey(th => th.Id);

            modelBuilder.Entity<TaskComment>()
                .HasOne(tc => tc.ProjectTask)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.ProjectTaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
