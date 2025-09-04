using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Contacts.Commands;

public record UpdateContactCommand(Guid Id, string FirstName, string LastName, string Email, string? PhoneNumber) : IRequest;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateContactCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _unitOfWork.Contacts.GetByIdAsync(request.Id);

        if (contact is not null)
        {
            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.Email = request.Email;
            contact.PhoneNumber = request.PhoneNumber;

            await _unitOfWork.Contacts.UpdateAsync(contact);
            await _unitOfWork.CompleteAsync();
        }
    }
}