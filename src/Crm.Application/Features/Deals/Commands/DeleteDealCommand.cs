using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Deals.Commands;

public record DeleteDealCommand(Guid Id) : IRequest;

public class DeleteDealCommandHandler : IRequestHandler<DeleteDealCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDealCommand request, CancellationToken cancellationToken)
    {
        var deal = await _unitOfWork.Deals.GetByIdAsync(request.Id);
        if (deal is not null)
        {
            await _unitOfWork.Deals.DeleteAsync(deal);
            await _unitOfWork.CompleteAsync();
        }
    }
}