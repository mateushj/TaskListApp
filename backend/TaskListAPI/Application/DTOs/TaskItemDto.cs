using TaskListAPI.Domain.Enums;

namespace TaskListAPI.Application.DTOs;
public class TaskItemDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemStatus Status { get; set; }
}