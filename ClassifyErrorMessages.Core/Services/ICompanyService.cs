using ClassifyErrorMessages.Core.Requests;
using ClassifyErrorMessages.Core.Responses;

namespace ClassifyErrorMessages.Core.Services;

public interface ICompanyService
{
    Result<CreateCompanyResponse> Create(CreateCompanyRequest request);
    Result<GetCompanyResponse> Get(Guid id);
}
