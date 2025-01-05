using OkalaCryptoQuote.Domain.Features.ExchangeRates;

namespace OkalaCryptoQuote.Domain.Tests.Features.ExchangeRates;

public class ExchangeRatesOptionsTests
{
    [Fact]
    public void SectionName_ConstantValue_IsAsExpected()
    {
        const string sectionName = ExchangeRatesOptions.SectionName;

        Assert.Equal("ExchangeRates", sectionName);
    }

    [Fact]
    public void Properties_SetValues_BaseUrlIsAsExpected()
    {
        var options = new ExchangeRatesOptions
        {
            BaseUrl = "http://test.com",
            AccessKey = "test-key",
            Currencies = "USD,EUR,GBP",
            BaseCurrency = "USD"
        };

        Assert.Equal("http://test.com", options.BaseUrl);
    }

    [Fact]
    public void Properties_SetValues_AccessKeyIsAsExpected()
    {
        var options = new ExchangeRatesOptions
        {
            BaseUrl = "http://test.com",
            AccessKey = "test-key",
            Currencies = "USD,EUR,GBP",
            BaseCurrency = "USD"
        };

        Assert.Equal("test-key", options.AccessKey);
    }

    [Fact]
    public void Properties_SetValues_CurrenciesIsAsExpected()
    {
        var options = new ExchangeRatesOptions
        {
            BaseUrl = "http://test.com",
            AccessKey = "test-key",
            Currencies = "USD,EUR,GBP",
            BaseCurrency = "USD"
        };

        Assert.Equal("USD,EUR,GBP", options.Currencies);
    }

    [Fact]
    public void Properties_SetValues_BaseCurrencyIsAsExpected()
    {
        var options = new ExchangeRatesOptions
        {
            BaseUrl = "http://test.com",
            AccessKey = "test-key",
            Currencies = "USD,EUR,GBP",
            BaseCurrency = "USD"
        };

        Assert.Equal("USD", options.BaseCurrency);
    }
}
