using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Companies.Commands;

public record UpdateCompanyCommand(Guid Id, string Name, string? Industry, string? Website) : IRequest;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(request.Id);
        if (company is not null)
        {
            company.Name = request.Name;
            company.Industry = request.Industry;
            company.Website = request.Website;

            await _unitOfWork.Companies.UpdateAsync(company);
            await _unitOfWork.CompleteAsync();
        }
    }
}