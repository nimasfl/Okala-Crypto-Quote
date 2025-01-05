namespace OkalaCryptoQuote.Infrastructure.HttpClients;

public sealed class CoinMarketCapApi(HttpClient httpClient, IOptions<ExchangeRatesOptions> exchangeRatesOption)
    : ICoinMarketCapApi
{
    public async Task<Result<CryptoDetail>> GetCryptoDetail(string cryptoCode, CancellationToken ct)
    {
        const string url = "/v2/cryptocurrency/quotes/latest?symbol=";
        CoinMarketCapMapResponse? result;
        try
        {
            result = await httpClient.GetFromJsonAsync<CoinMarketCapMapResponse>(url + cryptoCode, ct);
        }
        catch (Exception)
        {
            return CoinMarketCapError.InvalidFormatResult;
        }

        if (result is null || result.Status?.ErrorCode != 0)
        {
            return CoinMarketCapError.InvalidFormatResult;
        }

        var data = result.Data[cryptoCode.ToUpper()].Where(d => d.IsActive).MinBy(d => d.Rank);
        if (data is null)
        {
            return CoinMarketCapError.CryptoCodeNotFound;
        }

        return CryptoDetail.FromCoinMarketCapMetadata(data, exchangeRatesOption.Value.BaseCurrency);
    }
}
