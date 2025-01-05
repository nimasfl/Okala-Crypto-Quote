namespace OkalaCryptoQuote.Domain.Features.Quotes;

public static class QuotesError
{
    private const string FeatureName = "Quotes";

    public static readonly Error BaseCurrencyIsInvalid =
        Error.Problem(FeatureName, "base currency exchange rate is invalid");
}
