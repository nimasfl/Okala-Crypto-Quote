namespace OkalaCryptoQuote.Domain.Features.ExchangeRates;

public record LatestExchangeRateResponse(
    bool Success,
    Dictionary<string, decimal> Rates
);
