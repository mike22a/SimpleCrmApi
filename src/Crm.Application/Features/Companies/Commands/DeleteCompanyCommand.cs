using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Companies.Commands;

public record DeleteCompanyCommand(Guid Id) : IRequest;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(request.Id);
        if (company is not null)
        {
            await _unitOfWork.Companies.DeleteAsync(company);
            await _unitOfWork.CompleteAsync();
        }
    }
}