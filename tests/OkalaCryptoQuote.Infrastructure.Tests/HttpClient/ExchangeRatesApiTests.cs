using FakeItEasy;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;
using OkalaCryptoQuote.Infrastructure.HttpClients;
using OkalaCryptoQuote.Infrastructure.Tests.Helpers;

namespace OkalaCryptoQuote.Infrastructure.Tests.HttpClient;

public class ExchangeRatesApiTests
{
    [Fact]
    public async Task GetLatestRates_HttpRequestFails_ReturnsFailure()
    {
        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions
            { BaseCurrency = "USD", Currencies = "EUR,GBP", AccessKey = "validAccessKey" });


        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpToThrow<Exception>();
        var api = new ExchangeRatesApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        var ct = CancellationToken.None;
        var result = await api.GetLatestRates(ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(ExchangeRatesError.ServiceUnavailable, result.Error);
    }

    [Fact]
    public async Task GetLatestRates_ResultIsNull_ReturnsFailure()
    {
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseToNull();

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions
            { BaseCurrency = "USD", Currencies = "EUR,GBP", AccessKey = "validAccessKey" });

        var api = new ExchangeRatesApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        var ct = CancellationToken.None;
        var result = await api.GetLatestRates(ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(ExchangeRatesError.InvalidFormatResult, result.Error);
    }

    [Fact]
    public async Task GetLatestRates_ResultSuccess_ReturnsLatestExchangeRateResponse()
    {
        var response = new LatestExchangeRateResponse(true, new Dictionary<string, decimal>
        {
            { "EUR", 1 },
            { "GBP", 2 }
        });
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);

        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions
            { BaseCurrency = "USD", Currencies = "EUR,GBP", AccessKey = "validAccessKey" });

        var api = new ExchangeRatesApi(fakeHttpClient.HttpClient, exchangeRatesOptions);
        var ct = CancellationToken.None;
        var result = await api.GetLatestRates(ct);

        Assert.True(result.IsSuccess);
        Assert.Equal(response.Rates, result.Value.Rates);
        Assert.Equal(1, result.Value.Rates["EUR"]);
        Assert.Equal(2, result.Value.Rates["GBP"]);
    }

    [Fact]
    public async Task GetLatestRates_ApiReturnsFailure_ReturnsFailure()
    {
        var response = new LatestExchangeRateResponse(false, null!);
        var fakeHttpClient = new FakeHttpClient("https://test.com");
        fakeHttpClient.MockHttpResponseTo(response);
        var exchangeRatesOptions = A.Fake<IOptions<ExchangeRatesOptions>>();
        A.CallTo(() => exchangeRatesOptions.Value).Returns(new ExchangeRatesOptions
            { BaseCurrency = "USD", Currencies = "EUR,GBP", AccessKey = "validAccessKey" });


        var api = new ExchangeRatesApi(fakeHttpClient.HttpClient, exchangeRatesOptions);

        var ct = CancellationToken.None;
        var result = await api.GetLatestRates(ct);

        Assert.False(result.IsSuccess);
        Assert.Equal(ExchangeRatesError.InvalidFormatResult, result.Error);
    }
}
