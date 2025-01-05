using OkalaCryptoQuote.Domain.Base;
using OkalaCryptoQuote.Domain.Features.CoinMarketCap;

namespace OkalaCryptoQuote.Domain.Tests.Features.CoinMarketCap;

public class CoinMarketCapErrorTests
{
    [Fact]
    public void InvalidFormatResult_SetValue_IsNotNull()
    {
        var error = CoinMarketCapError.InvalidFormatResult;

        Assert.NotNull(error);
    }

    [Fact]
    public void InvalidFormatResult_SetValue_DescriptionIsCorrect()
    {
        var error = CoinMarketCapError.InvalidFormatResult;

        Assert.Equal("result is not formatted correctly", error.Description);
    }

    [Fact]
    public void InvalidFormatResult_SetValue_ErrorTypeIsInvalid()
    {
        var error = CoinMarketCapError.InvalidFormatResult;

        Assert.Equal(ErrorType.Invalid, error.Type);
    }

    [Fact]
    public void CryptoCodeNotFound_SetValue_IsNotNull()
    {
        var error = CoinMarketCapError.CryptoCodeNotFound;

        Assert.NotNull(error);
    }

    [Fact]
    public void CryptoCodeNotFound_SetValue_DescriptionIsCorrect()
    {
        var error = CoinMarketCapError.CryptoCodeNotFound;

        Assert.Equal("crypto code is not found", error.Description);
    }

    [Fact]
    public void CryptoCodeNotFound_SetValue_ErrorTypeIsInvalid()
    {
        var error = CoinMarketCapError.CryptoCodeNotFound;

        Assert.Equal(ErrorType.Invalid, error.Type);
    }
}
