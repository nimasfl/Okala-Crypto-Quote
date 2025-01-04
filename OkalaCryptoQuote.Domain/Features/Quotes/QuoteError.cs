namespace OkalaCryptoQuote.Domain.Features.Quotes;

public static class QuoteError
{
    private const string FeatureName = "Quote";

    public static readonly Error Test = Error.Problem(FeatureName, "Test");
}
