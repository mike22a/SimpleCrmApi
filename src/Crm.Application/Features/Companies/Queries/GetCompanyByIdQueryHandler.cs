using Crm.Application.Features.Companies.DTOs;
using Crm.Domain.Interfaces;
using MediatR;

namespace Crm.Application.Features.Companies.Queries;

public record GetCompanyByIdQuery(Guid Id) : IRequest<CompanyDto?>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(request.Id);
        if (company is null) return null;

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Industry = company.Industry,
            Website = company.Website
        };
    }
}