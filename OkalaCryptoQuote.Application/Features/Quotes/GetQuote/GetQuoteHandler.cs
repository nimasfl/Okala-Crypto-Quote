using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Application.Abstractions;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public class GetQuoteHandler(IExchangeRatesApi exchangeRatesApi, ICoinMarketCapApi coinMarketCapApi): IGetQuoteHandler
{
    public async Task<Result<GetQuoteResponse>> GetQuote(GetQuoteRequest request, CancellationToken ct)
    {
        const string baseCurrency = "USD";
        var rates = await exchangeRatesApi.GetLatestRates(ct);
        var cryptoInfo = await coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct);

        var quote = new Dictionary<string, decimal?>();
        foreach (var currency in rates.Value.Rates.Keys)
        {
            var baseRatio = rates.Value.Rates[baseCurrency];
            if (rates.Value.Rates.TryGetValue(currency, out var ratio))
            {
                quote.Add(currency,cryptoInfo.Value.Price * ratio/baseRatio);
            }
        }

        return new GetQuoteResponse(cryptoInfo.Value.Slug, cryptoInfo.Value.Symbol, quote);
    }
}
