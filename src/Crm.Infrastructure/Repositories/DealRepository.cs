using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using Crm.Infrastructure.Persistence;
using Crm.Infrastructure.Repositories;

namespace Crm.Infrastructure.Persistence.Repositories;

public class DealRepository : GenericRepository<Deal>, IDealRepository
{
    public DealRepository(ApplicationDbContext context) : base(context)
    {
    }
}