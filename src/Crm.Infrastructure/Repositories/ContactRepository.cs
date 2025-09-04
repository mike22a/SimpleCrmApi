using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using Crm.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crm.Infrastructure.Persistence.Repositories;

public class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Example of a specific method that the generic repository doesn't have.
    public async Task<Contact?> GetContactWithDetailsAsync(Guid id)
    {
        return await _context.Contacts
            .Include(c => c.Interactions)
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}