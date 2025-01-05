namespace OkalaCryptoQuote.Application.Abstractions;

public interface IExchangeRatesApi
{
    public Task<Result<LatestExchangeRateResponse>> GetLatestRates(CancellationToken ct = default);
}
