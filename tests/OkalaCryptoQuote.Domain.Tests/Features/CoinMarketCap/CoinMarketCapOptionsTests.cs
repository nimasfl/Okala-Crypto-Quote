using OkalaCryptoQuote.Domain.Features.CoinMarketCap;

namespace OkalaCryptoQuote.Domain.Tests.Features.CoinMarketCap;

public class CoinMarketCapOptionsTests
{
    [Fact]
    public void SectionName_SetValue_ReturnsCoinMarketCap()
    {
        const string sectionName = CoinMarketCapOptions.SectionName;

        Assert.Equal("CoinMarketCap", sectionName);
    }

    [Fact]
    public void ApiKey_SetValue_ReturnsExpectedValue()
    {
        var options = new CoinMarketCapOptions { ApiKey = "test-api-key" };

        Assert.Equal("test-api-key", options.ApiKey);
    }

    [Fact]
    public void BaseUrl_SetValue_ReturnsExpectedValue()
    {
        var options = new CoinMarketCapOptions { BaseUrl = "http://test.com" };

        Assert.Equal("http://test.com", options.BaseUrl);
    }
}
