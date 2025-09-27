using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskListAPI.Infrastructure;
public class TaskListDbContextFactory : IDesignTimeDbContextFactory<TaskListDbContext>
{
    public TaskListDbContext CreateDbContext(string[] args)
    {
         var basePath = Path.Combine(Directory.GetCurrentDirectory(), "API");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskListDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new TaskListDbContext(optionsBuilder.Options);
    }
}
