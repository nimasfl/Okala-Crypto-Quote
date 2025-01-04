namespace OkalaCryptoQuote.Domain.Features.ExchangeRates;

public static class ExchangeRatesError
{
    private const string FeatureName = "ExchangeRates";

    public static readonly Error InvalidFormatResult = Error.Invalid(FeatureName, "result is not formatted correctly");
    public static readonly Error ServiceUnavailable = Error.Problem(FeatureName, "service is unavailable");
}
