using OkalaCryptoQuote.Domain.Features.CoinMarketCap;

namespace OkalaCryptoQuote.Domain.Tests.Features.CoinMarketCap;

public class CryptoDetailTests
{
    [Fact]
    public void Constructor_WithValidParameters_SymbolIsAsExpected()
    {
        const string symbol = "BTC";
        const string slug = "bitcoin";
        const decimal price = 100m;

        var cryptoDetail = new CryptoDetail(symbol, slug, price);

        Assert.Equal(symbol, cryptoDetail.Symbol);
    }

    [Fact]
    public void Constructor_WithValidParameters_SlugIsAsExpected()
    {
        const string symbol = "BTC";
        const string slug = "bitcoin";
        const decimal price = 100m;

        var cryptoDetail = new CryptoDetail(symbol, slug, price);

        Assert.Equal(slug, cryptoDetail.Slug);
    }

    [Fact]
    public void Constructor_WithValidParameters_PriceIsAsExpected()
    {
        const string symbol = "BTC";
        const string slug = "bitcoin";
        const decimal price = 100m;

        var cryptoDetail = new CryptoDetail(symbol, slug, price);

        Assert.Equal(price, cryptoDetail.Price);
    }

    [Fact]
    public void FromCoinMarketCapMetadata_WithValidArgument_ReturnsCryptoDetailWithPrice()
    {
        var metadata = new CoinMarketCryptoMetadata
        {
            Symbol = "ETH",
            Slug = "ethereum",
            Quote = new Dictionary<string, CoinMarketCryptoPriceModel>
            {
                { "USD", new CoinMarketCryptoPriceModel { Price = 100 } }
            }
        };
        const string baseCurrency = "USD";

        var cryptoDetail = CryptoDetail.FromCoinMarketCapMetadata(metadata, baseCurrency);

        Assert.Equal("ETH", cryptoDetail.Symbol);
        Assert.Equal("ethereum", cryptoDetail.Slug);
        Assert.Equal(100, cryptoDetail.Price);
    }

    [Fact]
    public void FromCoinMarketCapMetadata_WithMissingQuote_ReturnsCryptoDetailWithNullPrice()
    {
        var metadata = new CoinMarketCryptoMetadata
        {
            Symbol = "BTC",
            Slug = "bitcoin",
            Quote = new Dictionary<string, CoinMarketCryptoPriceModel>()
        };
        const string baseCurrency = "USD";

        var cryptoDetail = CryptoDetail.FromCoinMarketCapMetadata(metadata, baseCurrency);

        Assert.Equal("BTC", cryptoDetail.Symbol);
        Assert.Equal("bitcoin", cryptoDetail.Slug);
        Assert.Null(cryptoDetail.Price);
    }

    [Fact]
    public void FromCoinMarketCapMetadata_WithNullQuote_ReturnsCryptoDetailWithNullPrice()
    {
        var metadata = new CoinMarketCryptoMetadata
        {
            Symbol = "XRP",
            Slug = "ripple",
            Quote = null!
        };
        const string baseCurrency = "USD";

        var cryptoDetail = CryptoDetail.FromCoinMarketCapMetadata(metadata, baseCurrency);

        Assert.Equal("XRP", cryptoDetail.Symbol);
        Assert.Equal("ripple", cryptoDetail.Slug);
        Assert.Null(cryptoDetail.Price);
    }
}
