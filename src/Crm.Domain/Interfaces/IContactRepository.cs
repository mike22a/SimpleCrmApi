using Crm.Domain.Models;

namespace Crm.Domain.Interfaces;

public interface IContactRepository : IGenericRepository<Contact>
{
    Task<Contact?> GetContactWithDetailsAsync(Guid id);
}