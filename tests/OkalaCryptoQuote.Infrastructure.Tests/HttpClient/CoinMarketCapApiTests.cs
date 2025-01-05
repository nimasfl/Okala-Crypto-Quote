using FakeItEasy;
using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Domain.Features.CoinMarketCap;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;
using OkalaCryptoQuote.Infrastructure.HttpClients;
using OkalaCryptoQuote.Infrastructure.Tests.Helpers;

namespace OkalaCryptoQuote.Infrastructure.Tests.HttpClient;

public class CoinMarketCapApiTests
{
    [Fact]
    public async Task GetCryptoDetail_HttpRequestFails_ReturnsFailure()
    {
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpToThrow<HttpRequestException>();

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(CoinMarketCapError.InvalidFormatResult, result.Error);
    }

    [Fact]
    public async Task GetCryptoDetail_ApiResultIsNull_ReturnsFailure()
    {
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseToNull();

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(CoinMarketCapError.InvalidFormatResult, result.Error);
    }

    [Fact]
    public async Task GetCryptoDetail_ErrorCodeIsNotZero_ReturnsInvalidFormatError()
    {
        var response = new CoinMarketCapMapResponse
        {
            Status = new CoinMarketCapMapStatusResponse { ErrorCode = 1 }
        };

        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        var cryptoCode = "BTC";
        var ct = CancellationToken.None;


        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(CoinMarketCapError.InvalidFormatResult, result.Error);
    }

    [Fact]
    public async Task GetCryptoDetail_FoundCryptoIsInactive_ReturnsCryptoCodeNotFoundError()
    {
        var response = new CoinMarketCapMapResponse
        {
            Data = new Dictionary<string, CoinMarketCryptoMetadata[]>
            {
                { "BTC", [new CoinMarketCryptoMetadata { RawIsActive = 0 }] }
            },
            Status = new CoinMarketCapMapStatusResponse { ErrorCode = 0 }
        };

        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(CoinMarketCapError.CryptoCodeNotFound, result.Error);
    }


    [Fact]
    public async Task GetCryptoDetail_CryptoNotFound_ReturnsCryptoCodeNotFoundError()
    {
        var response = new CoinMarketCapMapResponse
        {
            Data = new Dictionary<string, CoinMarketCryptoMetadata[]>
            {
                { "BTC", [] }
            },
            Status = new CoinMarketCapMapStatusResponse { ErrorCode = 0 }
        };
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(CoinMarketCapError.CryptoCodeNotFound, result.Error);
    }

    [Fact]
    public async Task GetCryptoDetail_ApiReturnsValidData_ReturnsCryptoDetail()
    {
        var response = new CoinMarketCapMapResponse
        {
            Status = new CoinMarketCapMapStatusResponse { ErrorCode = 0 },
            Data = new Dictionary<string, CoinMarketCryptoMetadata[]>
            {
                { "BTC", [new CoinMarketCryptoMetadata { RawIsActive = 1, Rank = 1, Slug = "bitcoin", Symbol = "BTC"}] }
            }
        };
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);
        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions { BaseCurrency = "USD" });

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("BTC", result.Value.Symbol);
        Assert.Equal("bitcoin", result.Value.Slug);
    }

    [Fact]
    public async Task GetCryptoDetail_WhenApiReturnsMoreThanOneValidData_ReturnsCryptoDetailWithLessRank()
    {
        var cryptoModelA = new CoinMarketCryptoMetadata
        {
            RawIsActive = 1, Rank = 1, Slug = "a",
            Quote = new Dictionary<string, CoinMarketCryptoPriceModel>
                { { "USD", new CoinMarketCryptoPriceModel(10) } }
        };

        var cryptoModelB = new CoinMarketCryptoMetadata
        {
            RawIsActive = 1, Rank = 2, Slug = "b",
            Quote = new Dictionary<string, CoinMarketCryptoPriceModel>
                { { "USD", new CoinMarketCryptoPriceModel(20) } }
        };

        var cryptoModelC = new CoinMarketCryptoMetadata
        {
            RawIsActive = 1, Rank = 3, Slug = "c",
            Quote = new Dictionary<string, CoinMarketCryptoPriceModel>
                { { "USD", new CoinMarketCryptoPriceModel(30) } }
        };


        var response = new CoinMarketCapMapResponse
        {
            Status = new CoinMarketCapMapStatusResponse { ErrorCode = 0 },
            Data = new Dictionary<string, CoinMarketCryptoMetadata[]>
            {
                {
                    "BTC", [cryptoModelA, cryptoModelB, cryptoModelC]
                }
            }
        };

        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);
        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        var api = new CoinMarketCapApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        const string cryptoCode = "BTC";
        var ct = CancellationToken.None;

        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions { BaseCurrency = "USD" });

        var result = await api.GetCryptoDetail(cryptoCode, ct);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("a", result.Value.Slug);
        Assert.Equal(10, result.Value.Price);
    }
}
