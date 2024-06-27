namespace ClassifyErrorMessages.Core.ErrorHandling;

public sealed record ErrorCode(string Namespace, string Category, int Sequence)
{
    public static readonly ErrorCode Empty = new(string.Empty, string.Empty, 0);

    public string Identifier => string.Join(
        ErrorConstants.ErrorCodeSeparator,
        Namespace,
        Category,
        Sequence.ToString().PadLeft(4, '0')
    );

    public override string ToString() => Identifier;
}