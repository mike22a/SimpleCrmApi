using Crm.Application.Features.Deals.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Deals.Queries;

public record GetAllDealsQuery : IRequest<IEnumerable<DealDto>>;

public class GetAllDealsQueryHandler : IRequestHandler<GetAllDealsQuery, IEnumerable<DealDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDealsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DealDto>> Handle(GetAllDealsQuery request, CancellationToken cancellationToken)
    {
        var deals = await _unitOfWork.Deals.GetAllAsync();
        return deals.Select(d => new DealDto
        {
            Id = d.Id,
            Title = d.Title,
            Value = d.Value,
            Stage = d.Stage,
            CloseDate = d.CloseDate,
            ContactId = d.ContactId
        });
    }
}