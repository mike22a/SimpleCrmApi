using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using Crm.Infrastructure.Repositories;

namespace Crm.Infrastructure.Persistence.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
    }
}