using Crm.Application.Features.Contacts.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Contacts.Queries;

public record GetAllContactsQuery : IRequest<IEnumerable<ContactDto>>;

public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllContactsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _unitOfWork.Contacts.GetAllAsync();

        // Map domain objects to DTOs
        return ContactDto.From(contacts);
    }
}