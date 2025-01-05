using System.Runtime.CompilerServices;

namespace OkalaCryptoQuote.Domain.Base;

public record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.None);

    public static Error NotFound(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.NotFound);

    public static Error Problem(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.Problem);

    public static Error Conflict(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.Conflict);

    public static Error Forbidden(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.Forbidden);

    public static Error Unauthorized(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.Unauthorized);

    public static Error Invalid(string featureName, string description, [CallerMemberName] string code = "") =>
        new($"{featureName}.{code}", description, ErrorType.Invalid);

    public static implicit operator Result(Error error) => Result.Failure(error);
}
