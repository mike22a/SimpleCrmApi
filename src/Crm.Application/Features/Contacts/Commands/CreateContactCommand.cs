using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using MediatR;

namespace Crm.Application.Features.Contacts.Commands;

public record CreateContactCommand(string FirstName, string LastName, string Email, string? PhoneNumber) : IRequest<Guid>;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateContactCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Contacts.AddAsync(contact);
        await _unitOfWork.CompleteAsync();

        return contact.Id;
    }
}