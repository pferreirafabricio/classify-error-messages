using ClassifyErrorMessages.Core;

namespace ClassifyErrorMessages.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblem(this Result result, int statusCode = StatusCodes.Status500InternalServerError)
        => Results.Problem(
            title: result.Error.Code.ToString(),
            detail: result.Error.Description,
            statusCode: statusCode,
            type: result.Error.Code.Category
        );
}
