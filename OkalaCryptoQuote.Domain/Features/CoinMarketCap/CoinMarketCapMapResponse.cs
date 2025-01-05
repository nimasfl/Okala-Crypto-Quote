namespace OkalaCryptoQuote.Domain.Features.CoinMarketCap;

public record CoinMarketCapMapResponse
{
    public Dictionary<string, CoinMarketCryptoMetadata[]> Data { get; set; }
    public CoinMarketCapMapStatusResponse? Status { get; set; }
}

public record CoinMarketCapMapStatusResponse
{
    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; set; }
}

public record CoinMarketCryptoMetadata
{
    public int Rank { get; set; }
    public string Symbol { get; set; }
    public string Slug { get; set; }

    [JsonPropertyName("is_active")]
    public int RawIsActive { get; set; }
    public bool IsActive => RawIsActive == 1;

    public Dictionary<string, CoinMarketCryptoPriceModel> Quote { get; set; }
}

public record CoinMarketCryptoPriceModel(decimal Price = 0);
