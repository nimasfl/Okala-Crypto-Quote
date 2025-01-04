using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Application.Abstractions;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public class GetQuoteHandler(IExchangeRatesApi exchangeRatesApi): IGetQuoteHandler
{
    public Result<GetQuoteResponse> GetQuote(GetQuoteRequest request, CancellationToken cancellationToken)
    {
        var rates = exchangeRatesApi.GetLatestRates();

        return Result.Success(new GetQuoteResponse("1", "2", "3"));
    }
}
