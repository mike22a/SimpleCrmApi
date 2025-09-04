namespace Crm.Domain.Models;

public class Deal
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public Stage Stage { get; set; } = Stage.Prospecting; 
    public DateTime CloseDate { get; set; }

    // Foreign Key
    public Guid ContactId { get; set; }
}