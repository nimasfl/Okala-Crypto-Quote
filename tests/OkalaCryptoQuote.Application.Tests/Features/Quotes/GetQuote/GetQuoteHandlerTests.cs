using FakeItEasy;
using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Application.Abstractions;
using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;
using OkalaCryptoQuote.Domain.Base;
using OkalaCryptoQuote.Domain.Features.CoinMarketCap;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;
using OkalaCryptoQuote.Domain.Features.Quotes;

namespace OkalaCryptoQuote.Application.Tests.Features.Quotes.GetQuote;

public class GetQuoteHandlerTests
{
    private readonly IExchangeRatesApi _exchangeRatesApi = A.Fake<IExchangeRatesApi>();
    private readonly ICoinMarketCapApi _coinMarketCapApi = A.Fake<ICoinMarketCapApi>();
    private readonly IOptions<ExchangeRatesOptions> _exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();

    [Fact]
    public async Task GetQuote_WhenExchangeRatesApiFails_ReturnsFailure()
    {
        var handler = new GetQuoteHandler(_exchangeRatesApi, _coinMarketCapApi, _exchangeRatesOptions);

        var request = new GetQuoteRequest("BTC");
        var ct = CancellationToken.None;

        var error = new Error("test", "test", ErrorType.Problem);

        A.CallTo(() => _exchangeRatesApi.GetLatestRates(ct)).Returns(error);

        var result = await handler.GetQuote(request, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(error.Description, result.Error.Description);
        Assert.Equal(error.Type, result.Error.Type);
    }

    [Fact]
    public async Task GetQuote_WhenCoinMarketCapApiFails_ReturnsFailure()
    {
        var handler = new GetQuoteHandler(_exchangeRatesApi, _coinMarketCapApi, _exchangeRatesOptions);

        var request = new GetQuoteRequest("BTC");
        var ct = CancellationToken.None;

        var error = new Error("test", "test", ErrorType.Problem);

        A.CallTo(() => _exchangeRatesApi.GetLatestRates(ct))
            .Returns(new LatestExchangeRateResponse(true, new Dictionary<string, decimal> { { "USD", 100 } }));

        A.CallTo(() => _coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct))
            .Returns(error);

        var result = await handler.GetQuote(request, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(error.Description, result.Error.Description);
        Assert.Equal(error.Type, result.Error.Type);
    }

    [Fact]
    public async Task GetQuote_BaseCurrencyIsInvalid_ReturnsFailure()
    {
        var handler = new GetQuoteHandler(_exchangeRatesApi, _coinMarketCapApi, _exchangeRatesOptions);

        var request = new GetQuoteRequest("BTC");
        var ct = CancellationToken.None;

        var exchangeRatesResponse = new LatestExchangeRateResponse(true, new Dictionary<string, decimal>
        {
            { "A", 100 },
            { "B", 50 },
            { "USD", 1 }
        });
        var cryptoDetail = new CryptoDetail("BTC", "bitcoin", 1000);
        var exchangeRatesOptions = new ExchangeRatesOptions { BaseCurrency = "C" };
        A.CallTo(() => _exchangeRatesApi.GetLatestRates(ct)).Returns(exchangeRatesResponse);
        A.CallTo(() => _coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct)).Returns(cryptoDetail);
        A.CallTo(() => _exchangeRatesOptions.Value).Returns(exchangeRatesOptions);

        var result = await handler.GetQuote(request, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(QuotesError.BaseCurrencyIsInvalid.Description, result.Error.Description);
        Assert.Equal(QuotesError.BaseCurrencyIsInvalid.Type, result.Error.Type);
    }

    [Fact]
    public async Task GetQuote_DependenciesAreSuccess_ReturnsQuote()
    {
        var handler = new GetQuoteHandler(_exchangeRatesApi, _coinMarketCapApi, _exchangeRatesOptions);

        var request = new GetQuoteRequest("BTC");
        var ct = CancellationToken.None;

        var exchangeRatesResponse = new LatestExchangeRateResponse(true, new Dictionary<string, decimal>
        {
            { "A", 100 },
            { "B", 50 },
            { "USD", 1 }
        });
        var cryptoDetail = new CryptoDetail("BTC", "bitcoin", 1000);
        var exchangeRatesOptions = new ExchangeRatesOptions { BaseCurrency = "USD" };
        A.CallTo(() => _exchangeRatesApi.GetLatestRates(ct)).Returns(exchangeRatesResponse);
        A.CallTo(() => _coinMarketCapApi.GetCryptoDetail(request.CryptoCode, ct)).Returns(cryptoDetail);
        A.CallTo(() => _exchangeRatesOptions.Value).Returns(exchangeRatesOptions);

        var result = await handler.GetQuote(request, ct);

        Assert.True(result.IsSuccess);
        var quoteResponse = result.Value;
        Assert.Equal("bitcoin", quoteResponse.Slug);
        Assert.Equal("BTC", quoteResponse.Symbol);
        Assert.Contains("A", quoteResponse.Prices);
        Assert.Contains("B", quoteResponse.Prices);
        Assert.Contains("USD", quoteResponse.Prices);
        Assert.Equal(100_000, quoteResponse.Prices["A"]);
        Assert.Equal(50_000, quoteResponse.Prices["B"]);
        Assert.Equal(1000, quoteResponse.Prices["USD"]);
    }
}
