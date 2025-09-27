using TaskListAPI.Application.DTOs;
using TaskListAPI.Domain.Entities;

namespace TaskListAPI.Domain.Interfaces.Services;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemDto>> GetAllAsync();
    Task<TaskItemDto?> GetByIdAsync(Guid id);
    Task<TaskItemDto> CreateAsync(TaskCreateDto task);
    Task UpdateAsync(TaskUpdateDto task);
    Task DeleteAsync(Guid id);
}