using ClassifyErrorMessages.Core.Requests;
using ClassifyErrorMessages.Core.Responses;

namespace ClassifyErrorMessages.Core.Services;

public interface ICompanyService
{
    CreateCompanyResponse Create(CreateCompanyRequest request);
    GetCompanyResponse Get(Guid id);
}
