using TaskListAPI.Domain.Enums;

namespace TaskListAPI.Application.DTOs;

public class TaskUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}