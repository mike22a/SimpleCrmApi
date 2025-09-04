using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Contacts.Commands;

public record DeleteContactCommand(Guid Id) : IRequest;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteContactCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _unitOfWork.Contacts.GetByIdAsync(request.Id);

        if (contact is not null)
        {
            await _unitOfWork.Contacts.DeleteAsync(contact);
            await _unitOfWork.CompleteAsync();
        }
    }
}