using TaskListAPI.Domain.Interfaces.Repositories;
using TaskListAPI.Domain.Interfaces.Services;
using TaskListAPI.Application.DTOs;
using TaskListAPI.Domain.Entities;
using TaskListAPI.Domain.Enums;
using TaskListAPI.Application.Mapper;

namespace TaskListAPI.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _repository;

    public TaskItemService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<TaskItemDto?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Task with id {id} not found.");

        return task.ToDto();
    }

    public async Task<TaskItemDto> CreateAsync(TaskCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("Title is required.");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = TaskItemStatus.Todo
        };
        await _repository.AddAsync(task);

        return task.ToDto();
    }

    public async Task UpdateAsync(TaskUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("Title is required.");

        var task = await _repository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Task with id {dto.Id} not found.");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        await _repository.UpdateAsync(task);
        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Task with id {id} not found.");

        await _repository.DeleteAsync(task);
    }
}