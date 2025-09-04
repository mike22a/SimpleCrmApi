namespace Crm.Domain.Models;

public class Contact
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    public List<Interaction> Interactions { get; set; } = new();
    public List<TaskItem> Tasks { get; set; } = new();
}