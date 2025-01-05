using OkalaCryptoQuote.Application.Abstractions;
using OkalaCryptoQuote.Domain.Features.Quotes;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public class GetQuoteHandler(IExchangeRatesApi exchangeRatesApi, ICoinMarketCapApi coinMarketCapApi) : IGetQuoteHandler
{
    public async Task<Result<GetQuoteResponse>> GetQuote(GetQuoteRequest request, CancellationToken ct)
    {
        const string baseCurrency = "USD";
        var validationResult = ValidateRequest(request);
        if (validationResult.IsSuccess == false)
        {
            return Result.Failure<GetQuoteResponse>(validationResult.Error);
        }

        var ratesResult = await exchangeRatesApi.GetLatestRates(ct);
        if (ratesResult.IsSuccess == false)
        {
            return Result.Failure<GetQuoteResponse>(ratesResult.Error);
        }

        var cryptoInfoResult = await coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct);
        if (cryptoInfoResult.IsSuccess == false)
        {
            return Result.Failure<GetQuoteResponse>(cryptoInfoResult.Error);
        }

        var prices = new Dictionary<string, decimal?>();
        foreach (var currency in ratesResult.Value.Rates.Keys)
        {
            var baseRatio = ratesResult.Value.Rates[baseCurrency];
            if (ratesResult.Value.Rates.TryGetValue(currency, out var ratio))
            {
                prices.Add(currency, cryptoInfoResult.Value.Price * ratio / baseRatio);
            }
        }

        return new GetQuoteResponse(cryptoInfoResult.Value.Slug, cryptoInfoResult.Value.Symbol, prices);
    }

    private static Result ValidateRequest(GetQuoteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CryptoCode))
        {
            return QuoteError.CryptoCodeIsEmpty;
        }
    }
}
