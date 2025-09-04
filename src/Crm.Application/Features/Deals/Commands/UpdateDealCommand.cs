using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Deals.Commands;

public record UpdateDealCommand(
    Guid Id,
    string Title,
    decimal Value,
    Stage Stage,
    DateTime CloseDate,
    Guid ContactId) : IRequest;

public class UpdateDealCommandHandler : IRequestHandler<UpdateDealCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDealCommand request, CancellationToken cancellationToken)
    {
        var deal = await _unitOfWork.Deals.GetByIdAsync(request.Id);
        if (deal is not null)
        {
            deal.Title = request.Title;
            deal.Value = request.Value;
            deal.Stage = request.Stage;
            deal.CloseDate = DateTime.SpecifyKind(request.CloseDate, DateTimeKind.Utc);
            deal.ContactId = request.ContactId;

            await _unitOfWork.Deals.UpdateAsync(deal);
            await _unitOfWork.CompleteAsync();
        }
    }
}