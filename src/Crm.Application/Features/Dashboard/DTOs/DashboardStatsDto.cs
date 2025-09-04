using Crm.Application.Features.Contacts.DTOs;
namespace Crm.Application.Features.Dashboards.DTOs;
public class DashboardStatsDto
{
    public int ContactCount { get; set; }
    public int CompanyCount { get; set; }
    public int OpenDealsCount { get; set; }
    public decimal OpenDealsValue { get; set; }
    public List<ContactDto> RecentContacts { get; set; } = new(); // Reusing ContactDto
    public List<ChartDataDto> DealsByStage { get; set; } = new();
}