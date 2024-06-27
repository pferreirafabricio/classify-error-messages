using ClassifyErrorMessages.Core;
using ClassifyErrorMessages.Core.Entities;
using ClassifyErrorMessages.Core.ErrorHandling;
using ClassifyErrorMessages.Core.Requests;
using ClassifyErrorMessages.Core.Responses;
using ClassifyErrorMessages.Core.Services;
using Microsoft.Extensions.Localization;

namespace ClassifyErrorMessages.Application.Services;

public sealed class CompanyService(IStringLocalizer<CompanyService> localizer) : ICompanyService
{
    private readonly List<Company> _companies = [];

    public Result<CreateCompanyResponse> Create(CreateCompanyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Failure<CreateCompanyResponse>(
                new(
                    ErrorConstants.CompanyInvalidName,
                    localizer[ErrorConstants.CompanyInvalidName.Identifier]
                )
            );

        if (string.IsNullOrWhiteSpace(request.Document))
            return Result.Failure<CreateCompanyResponse>(
                new(
                    ErrorConstants.CompanyInvalidDocument,
                    localizer[ErrorConstants.CompanyInvalidDocument.Identifier]
                )
            );

        var company = new Company(request.Name, request.Document);
        _companies.Add(company);

        return Result.Success<CreateCompanyResponse>(new(company.Id, company.Name, company.Document));
    }

    public Result<GetCompanyResponse> Get(Guid id)
    {
        var company = _companies.FirstOrDefault(x => x.Id == id);

        if (company is null)
            return Result.Failure<GetCompanyResponse>(
                new(
                    ErrorConstants.CompanyNotFound,
                    localizer[ErrorConstants.CompanyNotFound.Identifier]
                )
            );

        return Result.Success<GetCompanyResponse>(new(company.Id, company.Name, company.Document));
    }
}
