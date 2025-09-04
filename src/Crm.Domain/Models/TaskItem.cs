namespace Crm.Domain.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}