namespace Crm.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IContactRepository Contacts { get; }
    ICompanyRepository Companies { get; }
    IDealRepository Deals { get; }
    // IInteractionRepository Interactions { get; }
    // ITaskRepository Tasks { get; }

    Task<int> CompleteAsync();
}