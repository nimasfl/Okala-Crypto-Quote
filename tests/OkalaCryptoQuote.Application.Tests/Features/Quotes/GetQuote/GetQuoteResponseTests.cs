using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

namespace OkalaCryptoQuote.Application.Tests.Features.Quotes.GetQuote;

public class GetQuoteResponseTests
{
    [Fact]
    public void Constructor_SetValues_SlugIsAsExpected()
    {
        const string slug = "bitcoin";
        const string symbol = "BTC";
        var prices = new Dictionary<string, decimal?>
        {
            { "USD", 100 },
            { "EUR", 200 }
        };

        var response = new GetQuoteResponse(slug, symbol, prices);

        Assert.Equal(slug, response.Slug);
    }

    [Fact]
    public void Constructor_SetValues_SymbolIsAsExpected()
    {
        const string slug = "bitcoin";
        const string symbol = "BTC";
        var prices = new Dictionary<string, decimal?>
        {
            { "USD", 100 },
            { "EUR", 200 }
        };

        var response = new GetQuoteResponse(slug, symbol, prices);

        Assert.Equal(symbol, response.Symbol);
    }

    [Fact]
    public void Constructor_SetValues_PricesIsAsExpected()
    {
        const string slug = "bitcoin";
        const string symbol = "BTC";
        var prices = new Dictionary<string, decimal?>
        {
            { "USD", 100 },
            { "EUR", 200 }
        };

        var response = new GetQuoteResponse(slug, symbol, prices);

        Assert.Equal(prices, response.Prices);
    }
}
