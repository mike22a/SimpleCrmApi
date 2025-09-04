using Crm.Application.Features.Companies.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Companies.Queries;

public record GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>>;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCompaniesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _unitOfWork.Companies.GetAllAsync();
        return companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            Name = c.Name,
            Industry = c.Industry,
            Website = c.Website
        });
    }
}