namespace OkalaCryptoQuote.Domain.Features.CoinMarketCap;

public record CoinMarketCapOptions
{
    public const string SectionName = "CoinMarketCap";
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
}
