namespace Crm.Domain.Models;

public class Interaction
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }
    public string InteractionType { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime InteractionDate { get; set; }
}