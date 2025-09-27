using Microsoft.EntityFrameworkCore;
using TaskListAPI.Domain.Entities;
using TaskListAPI.Infrastructure.Configurations;

namespace TaskListAPI.Infrastructure;

public class TaskListDbContext : DbContext
{
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
