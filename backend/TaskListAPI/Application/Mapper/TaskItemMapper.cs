using System.Linq.Expressions;
using TaskListAPI.Application.DTOs;
using TaskListAPI.Domain.Entities;

namespace TaskListAPI.Application.Mapper;

public static class TaskItemMapper
{
     public static TaskItemDto ToDto(this TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status
        };
    }
    
    public static Expression<Func<TaskItem, TaskItemDto>> AsDto()
    {
        return t => new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Status = t.Status
        };
    }
}
