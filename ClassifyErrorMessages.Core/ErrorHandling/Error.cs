namespace ClassifyErrorMessages.Core.ErrorHandling;

public sealed record Error(ErrorCode Code, string Description)
{
    public static readonly Error None = new(ErrorCode.Empty, string.Empty);
}
