namespace OkalaCryptoQuote.Domain.Features.CoinMarketCap;

public static class CoinMarketCapError
{
    private const string FeatureName = "CoinMarketCap";
    public static readonly Error CryptoCodeIsNull = Error.Invalid(FeatureName, "crypto code must have value");
    public static readonly Error InvalidFormatResult = Error.Invalid(FeatureName, "result is not formatted correctly");
    public static readonly Error CryptoCodeNotFound = Error.Invalid(FeatureName, "crypto code is not found");
}
