using Microsoft.EntityFrameworkCore;
using TaskListAPI.Application.DTOs;
using TaskListAPI.Application.Mapper;
using TaskListAPI.Domain.Entities;
using TaskListAPI.Domain.Interfaces.Repositories;

namespace TaskListAPI.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskListDbContext _context;

    public TaskItemRepository(TaskListDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        => await _context.Tasks.AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(TaskItemMapper.AsDto())
            .ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(Guid id)
        => await _context.Tasks.FindAsync(id);

    public async Task AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        task.ModifiedAt = DateTime.UtcNow;
        _context.Tasks.Update(task);

        _context.Entry(task).Property(t => t.CreatedAt).IsModified = false;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}