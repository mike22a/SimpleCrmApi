using Crm.Domain.Interfaces;
using Crm.Domain.Models;
using MediatR;

namespace Crm.Application.Features.Companies.Commands;

public record CreateCompanyCommand(string Name, string? Industry, string? Website) : IRequest<Guid>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Industry = request.Industry,
            Website = request.Website
        };

        await _unitOfWork.Companies.AddAsync(company);
        await _unitOfWork.CompleteAsync();

        return company.Id;
    }
}