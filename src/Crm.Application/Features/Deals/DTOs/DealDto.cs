namespace Crm.Application.Features.Deals.DTOs;

public class DealDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public Stage Stage { get; set; }
    public DateTime CloseDate { get; set; }
    public Guid ContactId { get; set; }
}