using Crm.Application.Features.Deals.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Deals.Queries;

public record GetDealByIdQuery(Guid Id) : IRequest<DealDto?>;

public class GetDealByIdQueryHandler : IRequestHandler<GetDealByIdQuery, DealDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDealByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DealDto?> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
    {
        var deal = await _unitOfWork.Deals.GetByIdAsync(request.Id);
        if (deal is null) return null;

        return new DealDto
        {
            Id = deal.Id,
            Title = deal.Title,
            Value = deal.Value,
            Stage = deal.Stage,
            CloseDate = deal.CloseDate,
            ContactId = deal.ContactId
        };
    }
}