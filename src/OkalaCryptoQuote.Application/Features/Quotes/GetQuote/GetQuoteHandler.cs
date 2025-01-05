using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Application.Abstractions;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public class GetQuoteHandler(
    IExchangeRatesApi exchangeRatesApi,
    ICoinMarketCapApi coinMarketCapApi,
    IOptions<ExchangeRatesOptions> exchangeRatesOption) : IGetQuoteHandler
{
    public async Task<Result<GetQuoteResponse>> GetQuote(GetQuoteRequest request, CancellationToken ct)
    {
        var ratesResult = await exchangeRatesApi.GetLatestRates(ct);
        if (ratesResult.IsSuccess == false)
        {
            return Result.Failure<GetQuoteResponse>(ratesResult.Error);
        }

        var cryptoInfoResult = await coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct);
        if (cryptoInfoResult.IsSuccess == false)
        {
            return cryptoInfoResult.Error;
        }

        var prices = new Dictionary<string, decimal?>();
        foreach (var currency in ratesResult.Value.Rates.Keys)
        {
            var baseRatio = ratesResult.Value.Rates[exchangeRatesOption.Value.BaseCurrency];
            if (ratesResult.Value.Rates.TryGetValue(currency, out var ratio))
            {
                prices.Add(currency, cryptoInfoResult.Value.Price * ratio / baseRatio);
            }
        }

        return new GetQuoteResponse(cryptoInfoResult.Value.Slug, cryptoInfoResult.Value.Symbol, prices);
    }
}
