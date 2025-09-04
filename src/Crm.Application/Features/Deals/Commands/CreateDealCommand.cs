using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using MediatR;

namespace Crm.Application.Features.Deals.Commands;

public record CreateDealCommand(
    string Title,
    decimal Value,
    Stage Stage,
    DateTime CloseDate,
    Guid ContactId) : IRequest<Guid>;

public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        var deal = new Deal
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Value = request.Value,
            Stage = request.Stage,
            CloseDate = DateTime.SpecifyKind(request.CloseDate, DateTimeKind.Utc),
            ContactId = request.ContactId
        };

        await _unitOfWork.Deals.AddAsync(deal);
        await _unitOfWork.CompleteAsync();

        return deal.Id;
    }
}