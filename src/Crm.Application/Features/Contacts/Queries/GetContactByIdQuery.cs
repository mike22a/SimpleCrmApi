using Crm.Application.Features.Contacts.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Contacts.Queries;

public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto?>;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetContactByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ContactDto?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _unitOfWork.Contacts.GetContactWithDetailsAsync(request.Id);

        if (contact is null)
        {
            return null;
        }

        // We can create a more detailed DTO later if we want to include tasks/interactions
        return ContactDto.From(contact);
    }
}