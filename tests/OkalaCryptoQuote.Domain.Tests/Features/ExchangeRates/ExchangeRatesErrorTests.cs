using OkalaCryptoQuote.Domain.Base;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;

namespace OkalaCryptoQuote.Domain.Tests.Features.ExchangeRates;

public class ExchangeRatesErrorTests
{
    [Fact]
    public void InvalidFormatResult_SetValue_IsNotNull()
    {
        var error = ExchangeRatesError.InvalidFormatResult;

        Assert.NotNull(error);
    }

    [Fact]
    public void InvalidFormatResult_SetValue_DescriptionIsAsExpected()
    {
        var error = ExchangeRatesError.InvalidFormatResult;

        Assert.Equal("result is not formatted correctly", error.Description);
    }

    [Fact]
    public void InvalidFormatResult_SetValue_TypeIsAsExpected()
    {
        var error = ExchangeRatesError.InvalidFormatResult;

        Assert.Equal(ErrorType.Invalid, error.Type);
    }

    [Fact]
    public void ServiceUnavailable_SetValue_IsNotNull()
    {
        var error = ExchangeRatesError.ServiceUnavailable;

        Assert.NotNull(error);
    }

    [Fact]
    public void ServiceUnavailable_SetValue_DescriptionIsAsExpected()
    {
        var error = ExchangeRatesError.ServiceUnavailable;

        Assert.Equal("service is unavailable", error.Description);
    }

    [Fact]
    public void ServiceUnavailable_SetValue_TypeIsAsExpected()
    {
        var error = ExchangeRatesError.ServiceUnavailable;

        Assert.Equal(ErrorType.Problem, error.Type);
    }
}
