namespace Crm.Domain.Models;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Industry { get; set; }
    public string? Website { get; set; }

    // Navigation Property
    public List<Contact> Contacts { get; set; } = new();
}