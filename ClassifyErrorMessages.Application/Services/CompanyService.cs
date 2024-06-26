using ClassifyErrorMessages.Core.Entities;
using ClassifyErrorMessages.Core.Requests;
using ClassifyErrorMessages.Core.Responses;
using ClassifyErrorMessages.Core.Services;
using Microsoft.Extensions.Localization;

namespace ClassifyErrorMessages.Application.Services;

public sealed class CompanyService(IStringLocalizer<CompanyService> localizer) : ICompanyService
{
    private readonly List<Company> _companies = [];

    public CreateCompanyResponse Create(CreateCompanyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException(localizer["The name is required."]);
        }

        if (string.IsNullOrWhiteSpace(request.Document))
        {
            throw new ArgumentException(localizer["The document is required."]);
        }

        var company = new Company(request.Name, request.Document);
        _companies.Add(company);

        return new(company.Id, company.Name, company.Document);
    }

    public GetCompanyResponse Get(Guid id)
    {
        var company = _companies.FirstOrDefault(x => x.Id == id);

        if (company is null)
        {
            throw new ArgumentException(localizer["Company not found."]);
        }

        return new(company.Id, company.Name, company.Document);
    }
}
