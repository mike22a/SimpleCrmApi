using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using Crm.Infrastructure.Persistence.Repositories;

namespace Crm.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IContactRepository Contacts { get; }
    public ICompanyRepository Companies { get; }
    public IDealRepository Deals { get; }


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Contacts = new ContactRepository(_context);
        Companies = new CompanyRepository(_context);
        Deals = new DealRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}