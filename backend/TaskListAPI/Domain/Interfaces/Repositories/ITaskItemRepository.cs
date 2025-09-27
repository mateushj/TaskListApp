using TaskListAPI.Application.DTOs;
using TaskListAPI.Domain.Entities;

namespace TaskListAPI.Domain.Interfaces.Repositories;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItemDto>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}