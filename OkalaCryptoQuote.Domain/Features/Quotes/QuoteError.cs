namespace OkalaCryptoQuote.Domain.Features.Quotes;

public static class QuoteError
{
    private const string FeatureName = "Quote";

    public static readonly Error CryptoCodeIsEmpty = Error.Invalid(FeatureName, "crypto code must have value");
}
