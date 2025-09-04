using Crm.Application.Features.Contacts.DTOs;
using Crm.Application.Features.Dashboards.DTOs;
using Crm.Domain.Models;
using Crm.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetStats()
    {
        var deals = await _context.Deals.ToListAsync();
        var contacts = await _context.Contacts.ToListAsync();

        var stats = new DashboardStatsDto
        {
            ContactCount = deals.Count(),
            CompanyCount = deals.Count(),

            OpenDealsCount = deals
                .Count(d => d.Stage.ToString() != "ClosedWon" && d.Stage.ToString() != "ClosedLost"),

            OpenDealsValue = deals
                .Where(d => d.Stage.ToString() != "ClosedWon" && d.Stage.ToString() != "ClosedLost")
                .Sum(d => d.Value),

            RecentContacts = contacts
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .Select(c => ContactDto.From(c))
                .ToList(),

            DealsByStage = deals
                .GroupBy(d => d.Stage)
                .Select(g => new ChartDataDto { Name = g.Key.GetDisplayName(), Value = g.Count() })
                .ToList()
        };

        return Ok(stats);
    }
}