using Microsoft.EntityFrameworkCore;
using TaskListAPI.API.Middlewares;
using TaskListAPI.Application.Services;
using TaskListAPI.Domain.Interfaces.Repositories;
using TaskListAPI.Domain.Interfaces.Services;
using TaskListAPI.Infrastructure;
using TaskListAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "API"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
// Database
builder.Services.AddDbContext<TaskListDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddHttpClient();
// Dependency Injection
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
