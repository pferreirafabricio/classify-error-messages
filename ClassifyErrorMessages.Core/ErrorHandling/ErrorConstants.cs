namespace ClassifyErrorMessages.Core.ErrorHandling;

public class ErrorConstants
{
    public const string ErrorCodeSeparator = ":";
    public const int ErrorSequenceMaxWidth = 4;

    #region Errors

    public static ErrorCode CompanyInvalidDocument => new("Company", ErrorCategory.Error, 1);
    public static ErrorCode CompanyNotFound => new("Company", ErrorCategory.Error, 2);
    public static ErrorCode CompanyInvalidName => new("Company", ErrorCategory.Error, 3);

    #endregion
}
