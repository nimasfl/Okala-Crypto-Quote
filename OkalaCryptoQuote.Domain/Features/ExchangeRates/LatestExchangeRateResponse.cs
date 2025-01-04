namespace OkalaCryptoQuote.Domain.Features.ExchangeRates;

public record LatestExchangeRateResponse(
    bool Success,
    long Timestamp,
    string Base,
    string Date,
    Dictionary<string, decimal> Rates
);
