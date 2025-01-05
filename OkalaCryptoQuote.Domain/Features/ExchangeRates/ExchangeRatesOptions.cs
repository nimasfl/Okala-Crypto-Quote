namespace OkalaCryptoQuote.Domain.Features.ExchangeRates;

public record ExchangeRatesOptions
{
    public const string SectionName = "ExchangeRates";
    public string BaseUrl { get; set; }
    public string AccessKey { get; set; }
    public string Currencies { get; set; }
}
