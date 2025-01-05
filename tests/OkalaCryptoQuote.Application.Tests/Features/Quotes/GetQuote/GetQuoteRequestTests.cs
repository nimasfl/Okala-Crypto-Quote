using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

namespace OkalaCryptoQuote.Application.Tests.Features.Quotes.GetQuote;

public class GetQuoteRequestTests
{
    [Fact]
    public void Constructor_SetValues_CryptoCodeIsAsExpected()
    {
        const string cryptoCode = "BTC";

        var instance = new GetQuoteRequest(cryptoCode);

        Assert.Equal(cryptoCode, instance.CryptoCode);
    }
}
