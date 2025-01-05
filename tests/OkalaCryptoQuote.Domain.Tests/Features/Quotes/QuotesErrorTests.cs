using OkalaCryptoQuote.Domain.Base;
using OkalaCryptoQuote.Domain.Features.Quotes;

namespace OkalaCryptoQuote.Domain.Tests.Features.Quotes;

public class QuotesErrorTests
{
    [Fact]
    public void BaseCurrencyIsInvalid_SetValue_IsNotNull()
    {
        var error = QuotesError.BaseCurrencyIsInvalid;
        Assert.NotNull(error);
    }

    [Fact]
    public void BaseCurrencyIsInvalid_SetValue_DescriptionIsNotNull()
    {
        var error = QuotesError.BaseCurrencyIsInvalid;
        Assert.NotNull(error.Description);
    }

    [Fact]
    public void BaseCurrencyIsInvalid_SetValue_CodeIsNotNull()
    {
        var error = QuotesError.BaseCurrencyIsInvalid;
        Assert.NotNull(error.Code);
    }

    [Fact]
    public void BaseCurrencyIsInvalid_SetValue_TypeIsAsExpected()
    {
        var error = QuotesError.BaseCurrencyIsInvalid;
        Assert.Equal(ErrorType.Problem, error.Type);
    }
}
