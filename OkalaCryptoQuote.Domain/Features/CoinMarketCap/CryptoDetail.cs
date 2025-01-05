namespace OkalaCryptoQuote.Domain.Features.CoinMarketCap;

public record CryptoDetail(int Id, string Symbol, string Slug, decimal? Price)
{
    public static CryptoDetail FromCoinMarketCapMetadata(CoinMarketCryptoMetadata coinMarketCap)
    {
        decimal? price = null;
        if (coinMarketCap.Quote.TryGetValue("USD", out var usd))
        {
            price = usd.Price;
        }

        return new CryptoDetail(
            coinMarketCap.Id,
            coinMarketCap.Symbol,
            coinMarketCap.Slug,
            price
        );
    }
}
